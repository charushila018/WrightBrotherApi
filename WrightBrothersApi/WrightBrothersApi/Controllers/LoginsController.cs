using WrightBrothersApi.Login;
using Microsoft.AspNetCore.Mvc;

namespace WrightBrothersApi.Controllers;

[ApiController]
[Route("[controller]")]
/// <summary>
/// Exposes authentication endpoints for validating user credentials.
/// </summary>
public class LoginsController : ControllerBase
{
    /// <summary>
    /// Service responsible for verifying credentials against the data store.
    /// </summary>
    private readonly SecureLogin _secureLogin;

    /// <summary>
    /// Creates a controller instance with a login service.
    /// </summary>
    /// <remarks>
    /// The connection string is currently hardcoded for training/demo purposes.
    /// Replace it with configuration-based injection before production use.
    /// </remarks>
    public LoginsController()
    {
        // TODO: Replace "YourConnectionString" with your actual connection string.
        _secureLogin = new SecureLogin("YourConnectionString");
    }

    [HttpPost]
    /// <summary>
    /// Authenticates a user by username and password.
    /// </summary>
    /// <param name="username">The username supplied by the client.</param>
    /// <param name="password">The password supplied by the client.</param>
    /// <returns>
    /// <see cref="OkResult"/> when credentials are valid; otherwise <see cref="UnauthorizedResult"/>.
    /// </returns>
    public IActionResult Authenticate(string username, string password)
    {
        bool isAuthenticated = _secureLogin.AuthenticateUser(username, password);

        if (isAuthenticated)
        {
            return Ok();
        }
        else
        {
            return Unauthorized();
        }
    }
}
