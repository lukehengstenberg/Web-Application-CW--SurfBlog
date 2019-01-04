using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _878876.Data
{
    public static class ClaimData
    {
        public static List<string> UserClaims { get; set; } = new List<string>
        {
            "canEdit",
            "canComment",
            "canAddUser",
            "canEditUser",
            "canDeleteUser"
        };
    }
}
