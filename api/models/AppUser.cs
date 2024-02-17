using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.models
{ 
    public class AppUser : IdentityUser // añade el pass y pass confirmation
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}