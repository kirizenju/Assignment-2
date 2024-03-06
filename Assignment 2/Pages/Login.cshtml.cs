
using BO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repo;


namespace Assignment_2.Pages
{
    public class LoginModel : PageModel
    {
        IHospitalRepo repo = new HospitalRepo();

        [BindProperty]
        public StaffAccount StaffAccount { get; set; } = default!;

        public IActionResult OnPostLogin()
        {
            StaffAccount? loginAccount = repo.Login(StaffAccount.Hremail, StaffAccount.Hrpassword);
            if (loginAccount == null)
            {
                ViewData["notification"] = "Email or Password is wrong!";
                return Page();
            }
            else
            if (loginAccount.StaffRole == 1) //manager
            {
                HttpContext.Session.SetString("User", loginAccount.HraccountId);
                return RedirectToPage("./Manage/Index");
            }
            else
            {
                ViewData["notification"] = "You do not have permission to do this function!";
                return Page();
            }

        }
        public IActionResult OnPostLogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }

    }
}
