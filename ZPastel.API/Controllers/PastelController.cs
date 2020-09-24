using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Controllers
{
    [ApiController]
    [Route("api/pasteis/")]
    public class PastelController : ControllerBase
    {
        private readonly ILogger<PastelController> _logger;

        public PastelController(ILogger<PastelController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = nameof(GetPasteis))]
        [Produces("application/json", Type = typeof(Pastel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IReadOnlyCollection<PastelResource>> GetPasteis()
        {
            return new[] 
            { 
                new PastelResource
                { 
                    Id = 1,
                    Ingdredients = "4 Queijos",
                    Price = 5
                },
                new PastelResource
                {
                    Id = 2,
                    Ingdredients = "Carne",
                    Price = 4.50m
                }
            };
        }
    }
}
