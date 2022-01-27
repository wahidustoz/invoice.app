using invoice.app.Data;
using invoice.app.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace invoice.app.Controllers;

public class OrganizationController : Controller
{
    private readonly AppDbContext _ctx;
    private readonly UserManager<AppUser> _userm;

    public OrganizationController(
        AppDbContext context,
        UserManager<AppUser> usermanager)
    {
        _ctx = context;
        _userm = usermanager;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Created()
    {
        var user = await _userm.GetUserAsync(User);
        var orgs = await _ctx.Organizations.Where(o => o.OwnerId == user.Id).ToListAsync();

        return View(orgs);
    }
}