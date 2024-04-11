using Microsoft.AspNetCore.Mvc;

namespace PersonnelDepartment.Infrastructure;

public class BaseController : Controller
{
    public RedirectResult Error403 => Redirect("/Error/404");
    public RedirectResult Error404 => Redirect("/Error/404");

    protected ViewResult ReactApp()
    {
        return View(nameof(ReactApp), new ReactApp("ReactApp"));
    }

    protected void WriteCookie(String key, String value, DateTime expires, Boolean httpOnly, Boolean secure)
    {
        Response.Cookies.Append(
            key: key,
            value: value,
            options: new CookieOptions
            {
                Expires = expires,
                HttpOnly = httpOnly,
                Secure = secure
            }
        );
    }
}
