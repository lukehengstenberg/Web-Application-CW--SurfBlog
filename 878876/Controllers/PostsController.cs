using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _878876.Data;
using _878876.Models;
using _878876.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Security.Application;

namespace _878876.Controllers
{
    [Authorize]
    [RequireHttps]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Post.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            PostCommentsViewModel viewModel = await GetPostCommentsViewModelFromPost(post);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy ="canComment")]
        public async Task<IActionResult> Details([Bind("PostID,Author,CommentDate,Content")] PostCommentsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment();

                comment.Author = viewModel.Author;
                comment.CommentDate = DateTime.Now;
                comment.Content = viewModel.Content;

                Post post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == viewModel.PostID);
                if (post == null)
                {
                    return NotFound();
                }

                comment.MyPost = post;
                _context.Comment.Add(comment);
                await _context.SaveChangesAsync();

                viewModel = await GetPostCommentsViewModelFromPost(post);
            }

            return View(viewModel);
        }

        private async Task<PostCommentsViewModel> GetPostCommentsViewModelFromPost(Post post)
        {
            PostCommentsViewModel viewModel = new PostCommentsViewModel();

            viewModel.Post = post;

            List<Comment> comments = await _context.Comment
                .Where(m => m.MyPost == post).ToListAsync();

            viewModel.Comments = comments;
            return viewModel;
        }

        // GET: Posts/Create
        [Authorize(Policy = "canEdit")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy= "canEdit")]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,PostDate,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Content = Sanitizer.GetSafeHtmlFragment(post.Content);
                post.Author = User.Identity.Name.ToString();
                post.PostDate = DateTime.Now;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Policy = "canEdit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "canEdit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,PostDate,Content")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    post.Content = Sanitizer.GetSafeHtmlFragment(post.Content);
                    post.Author = User.Identity.Name.ToString();
                    post.PostDate = DateTime.Now;
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Policy = "canEdit")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "canEdit")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }

        [Authorize(Policy = "canComment")]
        public async Task<IActionResult> DeleteComment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "canComment")]
        public async Task<IActionResult> DeleteCommentConfirmed(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
