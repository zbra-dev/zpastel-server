using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public async Task<IReadOnlyCollection<Pastel>> GetPasteis()
        {
            return new[] 
            { 
                new Pastel("4 Queijos", 5)
                { 
                    Id = 1
                },
                new Pastel("Carne", 4.50m)
                {
                    Id = 2
                }
            };
        }
    }
}
