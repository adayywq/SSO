using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mdl.Entity
{
    public class SsoUser
    {
        public SsoUser() { }

        public string UserId { get; set; }
        public string Sno { get; set; }
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Department { get; set; }
        public string Job { get; set; }
        public string Identity { get; set; }
    }
}
