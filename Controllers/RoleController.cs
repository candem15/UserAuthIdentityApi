using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserAuthIdentityApi.Models;

namespace UserAuthIdentityApi.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
         private readonly ILogger<RoleController> _logger;
        public RoleController(RoleManager<IdentityRole> roleManager, ILogger<RoleController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        //ADD ROLE
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }

        //DELETE ROLE
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var record = await _roleManager.FindByIdAsync(roleId);
            if (roleId != null)
            {
                await _roleManager.DeleteAsync(record);
            }
            return RedirectToAction("Index");
        }

        //UPDATE ROLE
        [HttpPost]
        public async Task<IActionResult> UpdateRole(string roleId, string roleName)
        {
            var record = await _roleManager.FindByIdAsync(roleId);
            if (roleId != null)
            {
                record.Name = roleName;
                await _roleManager.UpdateAsync(record);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}