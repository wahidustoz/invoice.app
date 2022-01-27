using System.Text.Json;
using invoice.app.Data;
using invoice.app.Entity;
using invoice.app.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace invoice.app.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AppDbContext _ctx;

    public AccountController(
        ILogger<AccountController> logger,
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        SignInManager<AppUser> signInManager,
        AppDbContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _ctx = context;
    }

    [HttpGet]
    public IActionResult Signup(Guid joinCode, string returnUrl)
    {
        return View(new SignupViewModel() { JoinCode = joinCode, ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Signup(SignupViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View(model);
        }

        if(await _userManager.Users.AnyAsync(u => u.NormalizedEmail == model.Email.ToUpper()))
        {
            ModelState.AddModelError("Email", "Email already exists");
            return View(model);
        }

        if(await _userManager.Users.AnyAsync(u => u.PhoneNumber == model.Phone))
        {
            ModelState.AddModelError("Pone", "Phone already exists");
            return View(model);
        }

        if(model.JoinCode != null)
        {
            var code = await _ctx.JoinCodes.FirstOrDefaultAsync(c => c.Code == model.JoinCode);
            if(code == null || code == default)
            {
                ModelState.AddModelError("JoinCode", "Joining code doesn't exist or is expired.");
                return View();
            }

            if(code.ForEmail.ToLower() != model.Email.ToLower())
            {
                ModelState.AddModelError("JoinCode", "Wrong email for joining code.");
                return View();
            }
        }

        var user = new AppUser()
        {
            Fullname = model.Fullname,
            Email = model.Email,
            UserName = model.Email.Substring(0, model.Email.IndexOf('@')),
            PhoneNumber = model.Phone
        };
        
        var userResult = await _userManager.CreateAsync(user, model.Password);
        if(!userResult.Succeeded)
        {
            return StatusCode(500, JsonSerializer.Serialize(userResult.Errors));
        }

        if(model.JoinCode != default)
        {
            var code = await _ctx.JoinCodes.FirstOrDefaultAsync(c => c.Code == model.JoinCode);

            var employeeOrganization = new EmployeeOrganization()
            {
                EmployeeId = user.Id,
                OrganizationId = code.OrganizationId
            };
            await _ctx.EmployeeOrganizations.AddAsync(employeeOrganization);
            await _ctx.SaveChangesAsync();

            await _userManager.AddToRoleAsync(user, "employee");
        }
        else
        {
            await _userManager.AddToRoleAsync(user, "owner");
        }

        return LocalRedirect(model.ReturnUrl ?? "/");
    }

    public IActionResult Signin(string returnUrl)
    {
        return View(new SigninViewModel() { ReturnUrl = returnUrl });
    }

     [HttpPost]
    public async Task<IActionResult> Signin(SigninViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View(model);
        }

        var user = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
        if(user == default)
        {
            ModelState.AddModelError("Password", "Email yoki parol noto'g'ri kiritilgan.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
        if(result.Succeeded)
        {
            return LocalRedirect(model.ReturnUrl ?? "/");
        }

        return BadRequest(result.IsNotAllowed);
    }

    [HttpPost]
    public async Task<IActionResult> Signout()
    {
        if(_signInManager.IsSignedIn(User))
        {
            await _signInManager.SignOutAsync();

        }
        return LocalRedirect("/");
    }

    [HttpGet]
    public IActionResult AccessDenied(string returnUrl)
    {
        return View(new { ReturnUrl = returnUrl });
    }
}