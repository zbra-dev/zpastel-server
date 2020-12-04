using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZPastel.API.Converters;
using ZPastel.API.Resources;
using ZPastel.Service.Contract;

namespace ZPastel.API.Controllers
{
    [ApiController]
    [Route("api/users/")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserService userService;
        private readonly UserConverter userConverter;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService,
            UserConverter userConverter)
        {
            this.logger = logger;
            this.userService = userService;
            this.userConverter = userConverter;
        }

        [HttpPost("save", Name = nameof(Save))]
        [Produces("application/json", Type = typeof(UserResource))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserResource>> Save(UserResource userResource)
        {
            var user = userConverter.ConvertToModel(userResource);

            var savedUser = await userService.Save(user);

            return Ok(userConverter.ConvertToResource(savedUser));
        }

        [HttpGet("{firebaseId}", Name = nameof(FindUserByFirebaseId))]
        [Produces("application/json", Type = typeof(UserResource))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserResource>> FindUserByFirebaseId(string firebaseId)
        {
            var user = await userService.FindByFirebaseId(firebaseId);

            if (user == null)
            {
                return NoContent();
            }

            return Ok(userConverter.ConvertToResource(user));
        }
    }
}
