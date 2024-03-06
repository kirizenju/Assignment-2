using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BO.Models;
using Repo;

namespace Assignment_2.Pages.Manage
{
    public class DeleteModel : PageModel
    {
        IHospitalRepo repo = new HospitalRepo();

        [BindProperty]
      public DoctorInformation DoctorInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var loginId = HttpContext.Session.GetString("User");
            if (string.IsNullOrEmpty(loginId))
            {
                return RedirectToPage("../Login");
            }
            if (repo.CheckLogin(loginId) == null)
            {
                return RedirectToPage("../Login");
            }


            var correspondingauthor = repo.GetDoctorById(id);

            if (correspondingauthor == null)
            {
                return NotFound();
            }
            else
            {
                DoctorInformation = correspondingauthor;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            repo.DeleteDoctor(id);

            return RedirectToPage("./Index");
        }
    }
}
