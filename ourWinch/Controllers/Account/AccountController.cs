using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ourWinch.Models.Account;




/// <summary>
/// Controller responsible for managing user accounts and authentication in the application.
/// </summary>
public class AccountController : Controller
{


    /// <summary>
    /// The user manager
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;
    /// <summary>
    /// The role manager
    /// </summary>
    private readonly RoleManager<IdentityRole> _roleManager;
    /// <summary>
    /// The sign in manager
    /// </summary>
    private readonly SignInManager<ApplicationUser> _signInManager;
    /// <summary>
    /// The iris service
    /// </summary>
    private readonly INotyfService _irisService;
    //private readonly IEmailSender _emailSender;




    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController" /> class.
    /// This constructor injects multiple manager services for handling user accounts,
    /// authentication, roles, and notifications.
    /// </summary>
    /// <param name="userManager">The user manager for handling user-related operations.</param>
    /// <param name="signInManager">The sign-in manager for handling user authentication.</param>
    /// <param name="roleManager">The role manager for handling user roles.</param>
    /// <param name="irisService">The notification service for sending notifications.</param>
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,  RoleManager<IdentityRole> roleManager, INotyfService irisService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
       // _emailSender = emailSender;
        _roleManager = roleManager;
        //notfity
        _irisService = irisService;
    }



    /// <summary>
    /// Initiates the user registration process.
    /// This method checks if the 'Admin' and 'Ansatt' roles exist and creates them if they do not.
    /// It then presents a registration view model to the user.
    /// </summary>
    /// <returns>
    /// A view for the registration process with the registration view model.
    /// </returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Register()
    {

        // Check if the 'Admin' role exists; create it if it doesn't.
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            // create the role
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Ansatt"));
        }

        // Initialize the registration view model.
        RegisterViewModel registerViewModel = new RegisterViewModel()
       ;

        // Return the registration view with the view model.
        return View(registerViewModel);
    }



    /// <summary>
    /// Processes the user registration submission.
    /// If the model state is valid, it creates a new user and assigns a role based on the submitted data.
    /// On successful registration, the user is redirected to the dashboard.
    /// If the registration fails, it returns the registration view with error messages.
    /// </summary>
    /// <param name="model">The registration view model containing user data.</param>
    /// <returns>
    /// A redirection to the dashboard on successful registration or the registration view with errors on failure.
    /// </returns>

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {

        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.MobilNo, Fornavn = model.Fornavn, Etternavn = model.Etternavn,
                MellomNavn = model.MellomNavn, Email = model.Email, PhoneNumber = model.MobilNo
            };


            var result = await _userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                // Assign a role to the user based on the selected role in the form.
                if (model.Role!=null && model.Role.Length > 0 && model.Role=="Admin")
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Ansatt");
                }

                _irisService.Success("Brukeren ble registrert!",3);
                return RedirectToAction("Index", "Dashboard");

            }
            AddErrors(result);
           

        }
        // Return the view with the model to display any validation errors.
        return View(model);
    }
    /// <summary>
    /// Logins this instance.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
  
    public  IActionResult Login()
    {
        
        return View();
    }


    /// <summary>
    /// Processes the login submission.
    /// Authenticates the user based on the provided credentials. On successful login, the user is redirected to the dashboard.
    /// If the user is locked out, a lockout view is presented.
    /// In case of an invalid login attempt, the login view is returned with an error message.
    /// </summary>
    /// <param name="model">The login view model containing user login data.</param>
    /// <returns>
    /// A redirection to the dashboard on successful login, a lockout view if the user is locked out,
    /// or the login view with errors on an invalid login attempt.
    /// </returns>

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Mobilno, model.Password, model.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
               // TempData[SD.Success] = "Login Successfull!";
               // TempData[SD.Warning] = "Logged out!";
                //TempData["LoginSuccessMessage"] = "Vellykket innlogging!";
                _irisService.Success("Successfully Logged in", 1);
                return RedirectToAction("Index", "Dashboard");
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty,"Invalid login attempt. ");
                return View(model);
            }

            


        }
        // Return the view with the model to display any validation errors.
        return View(model);
    }


    /// <summary>
    /// Handles the user logout process.
    /// Signs the user out of the application and redirects them to the login page.
    /// </summary>
    /// <returns>
    /// A redirection to the login page of the account controller.
    /// </returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Logout()
    {

       await _signInManager.SignOutAsync();
       
        return RedirectToAction("Login", "Account");
        
    }


    /// <summary>
    /// Processes the request to send a code for password reset.
    /// If the model state is valid, it sets a flag indicating that the code has been sent.
    /// It then returns to the 'ForgotPassword' view with the provided model.
    /// </summary>
    /// <param name="model">The view model containing the user's data for password reset.</param>
    /// <returns>
    /// The 'ForgotPassword' view along with the model and an indication of whether the code was sent.
    /// </returns>
    [HttpPost]
   
    public IActionResult SendCode(ForgotPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            ViewBag.CodeSent = true;  // Indicate that the code has been sent.
        }
        return View("ForgotPassword", model); // Return to the 'ForgotPassword' view with the model.
    }


    /// <summary>
    /// Displays the view for the password reset process.
    /// This method is called when a user requests the page to reset their password.
    /// </summary>
    /// <returns>
    /// The 'ForgotPassword' view.
    /// </returns>
    [HttpGet]
    public IActionResult ForgotPassword()
    {

        return View(); // Return the 'ForgotPassword' view.
    }


    /// <summary>
    /// Processes the password reset request.
    /// If the model state is valid and the user exists, a password reset token is generated and a link is sent to the user's email.
    /// Redirects to 'ForgotPasswordConfirmation' view upon completion, regardless of whether the user was found.
    /// </summary>
    /// <param name="model">The view model containing the user's email for password reset.</param>
    /// <returns>
    /// A redirection to the 'ForgotPasswordConfirmation' view after processing the request.
    /// If the model state is not valid, it returns the 'ForgotPassword' view with the provided model for corrections.
    /// </returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {

                // Redirect to confirmation view even if the user does not exist
                // as a security measure to prevent enumeration of registered emails.
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

          //  await _emailSender.SendEmailAsync(model.Email, "Reset Password - Identity Manager",
            //    "Please reset your password by clicking here: <a href=\"" + callbackurl + "\">link</a>");

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        // Return the view with the model to display any validation errors.
        return View(model);
    }

    /// <summary>
    /// Forgots the password confirmation.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }



    /// <summary>
    /// Displays the confirmation view after a user has requested a password reset.
    /// This method is called to inform the user that the password reset process has been initiated,
    /// and instructions have been sent to their email, if applicable.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>
    /// The 'ForgotPasswordConfirmation' view.
    /// </returns>
    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

}
