using Microsoft.AspNetCore.Mvc;
using CoreFaces.Identity.Services;
using CoreFaces.Identity.Repositories;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Users;
using CoreFaces.Identity.Helper;
using CoreFaces.Helper;

namespace CoreFaces.Identity.PublicControllers.ControllersV1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {

        private string error = "";
        private bool status = false;

        private readonly IdentityDatabaseContext _identityDatabaseContext;
        private readonly IUserService _userService;

        public LoginController(IdentityDatabaseContext identityDatabaseContext, IUserService userService)
        {
            _identityDatabaseContext = identityDatabaseContext;
            _userService = userService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // POST api/values
        [HttpPost]
        public CommonApiResponse<UserView> Post([FromBody]UserLoginView userLoginView)
        {
            UserView _user = _userService.LoginByEmail(userLoginView.Email, userLoginView.Password);
            if (_user == null)
            {
                error = "User information is invalid.";
                status = false;
            }
            else
            {
                status = true;
            }
            CommonApiResponse<UserView> result = CommonApiResponse<UserView>.Create(Response, System.Net.HttpStatusCode.OK, status, _user, error);
            return result;
        }
    }
}
