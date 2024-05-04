using Microsoft.AspNetCore.Mvc;
using PersonnelDepartment.Infrastructure;

namespace PersonnelDepartment.Controllers;
public class HomeController : BaseController
{
    [HttpGet("/")]
    public IActionResult Index() => ReactApp();
}
