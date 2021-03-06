﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace Catalogue_re.DAL.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }
        public ApplicationRole(string name) { Name = name; }
        public string Description { get; set; }
    }
}
