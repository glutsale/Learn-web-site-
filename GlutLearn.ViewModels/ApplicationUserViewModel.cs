using GlutLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutLearn.ViewModels
{
    public class ApplicationUserViewModel
    {
        public List<ApplicationUser> Teachers { get; set; }= new List<ApplicationUser>();
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public ApplicationUserViewModel() { }

        public ApplicationUserViewModel(ApplicationUser user)
        { 
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
        }

        public ApplicationUser ConvertViewModelToModel(ApplicationUserViewModel user)
        {
            return new ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

        }

        public List<ApplicationUser> Teacher { get; set; } = new List<ApplicationUser>();
    }   
}
