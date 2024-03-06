using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BO.Models;
using Repo;

namespace Assignment_2.Pages.Manage
{
    public class EditModel : PageModel
    {
        IHospitalRepo repo = new HospitalRepo();

        [BindProperty]
        public DoctorInformation DoctorInformation { get; set; } = default!;

        public IActionResult OnGetAsync(string id)
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


            if (id == null)
            {
                return RedirectToPage("./Index");
            }

            var correspondingauthor = repo.GetDoctorById(id);
            if (correspondingauthor == null)
            {
                return RedirectToPage("./Index");
            }

            DoctorInformation = correspondingauthor;
           ViewData["DepartmentId"] = new SelectList(repo.GetDepartments(), "DepartmentId", "DepartmentName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPostAsync()
        {
            if (DoctorInformation == null)
            {
                return Page();
            }

            if (DoctorInformation.Birthday <= DateTime.MinValue)
            {
                ModelState.AddModelError("DoctorInformation.Birthday",
                    "Birthday must be a valid date.");
                return OnGetAsync(DoctorInformation.DoctorId.ToString());
            }

            if (!(DoctorInformation.GraduationYear <= 2024 && DoctorInformation.GraduationYear >= 2008))
            {
                ModelState.AddModelError("DoctorInformation.GraduationYear",
                    "2008 <= Graduation Year <=2024");
                return OnGetAsync(DoctorInformation.DoctorId.ToString());
            }

            if (!CheckName(DoctorInformation.DoctorName))
            {
                ModelState.AddModelError("DoctorInformation.DoctorName",
                    "DoctorName from 15 to 120 characters. Each word must begin with capital letter");
                return OnGetAsync(DoctorInformation.DoctorId.ToString());
            }

            var result = repo.UpdateDoctor(DoctorInformation);

            return RedirectToPage("./Index", new { id = result?.DoctorId });
        }

        private bool CheckName(string name)
        {
            bool check = true;
            if (name.Length <= 14 || name.Length >= 121)
            {
                check = false;
            }

            // Check if each word starts with a capital letter
            string[] nameParts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in nameParts)
            {
                if (!char.IsUpper(part[0]))
                {
                    check = false;
                }
            }

            return check;
        }
    }
}
