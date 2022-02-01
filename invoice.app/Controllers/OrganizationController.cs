using invoice.app.Data;
using invoice.app.Entity;
using invoice.app.Mappers;
using invoice.app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace invoice.app.Controllers;

[Authorize]
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
    public async Task<IActionResult> Created()
    {
        var user = await _userm.GetUserAsync(User);
        var orgs = await _ctx.Organizations.Where(o => o.OwnerId == user.Id).ToListAsync();
        
        var model = orgs.ToModel();
        return View(model);
    }

    [HttpGet]
    public IActionResult Joined()
    {
        return View();
    }
    

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrganizationViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View();
        }

        var user = await _userm.GetUserAsync(User);

        var org = new Organization()
        {
            Name = model.Name,
            Address = model.Address,
            Phone = model.Phone,
            Email = model.Email,
            OwnerId = user.Id
        };

        await _ctx.Organizations.AddAsync(org);

        var empOrg = new EmployeeOrganization()
        {
            EmployeeId = user.Id,
            OrganizationId = org.Id
        };
        
        await _ctx.EmployeeOrganizations.AddAsync(empOrg);

        await _ctx.SaveChangesAsync();

        return RedirectToAction(nameof(Created));
    }

    [HttpGet]
    public IActionResult AddMember(Guid id) => View(new AddMemberViewModel() { OrganizationId = id });

    [HttpPost]
    public async Task<IActionResult> AddMember(AddMemberViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View();
        }
        if(await _ctx.JoinCodes.AnyAsync(c => c.ForEmail == model.ForEmail))
        {
            ModelState.AddModelError("ForEmail", "Bu emailga taklif jo'natilgan.");
            return View();
        }
        if(await _ctx.Users.AnyAsync(u => u.Email == model.ForEmail))
        {
            ModelState.AddModelError("ForEmail", "Bu email allaqachon olingan.");
            return View();
        }

        var user = await _userm.GetUserAsync(User);


        var joinCode = new JoinCode()
        {
            ForEmail = model.ForEmail,
            OrganizationId = model.OrganizationId,
            CreatorId = user.Id
        };

        await _ctx.JoinCodes.AddAsync(joinCode);
        await _ctx.SaveChangesAsync();

        // send some email here

        return RedirectToAction(nameof(Created));
    }
}