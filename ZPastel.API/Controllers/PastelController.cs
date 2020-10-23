using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPastel.API.Converters;
using ZPastel.API.Resources;
using ZPastel.Model;
using ZPastel.Service.API.Contract;

namespace ZPastel.API.Controllers
{
    [ApiController]
    [Route("api/pasteis/")]
    public class PastelController : ControllerBase
    {
        private readonly ILogger<PastelController> logger;
        private readonly IPastelService pastelService;
        private readonly PastelConverter pastelConverter;

        public PastelController(
            ILogger<PastelController> logger, 
            IPastelService pastelService,
            PastelConverter pastelConverter)
        {
            this.logger = logger;
            this.pastelService = pastelService;
            this.pastelConverter = pastelConverter;
        }

        [HttpGet(Name = nameof(GetPasteis))]
        [Produces("application/json", Type = typeof(Pastel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IReadOnlyCollection<PastelResource>> GetPasteis()
        {
            var pasteis =  await pastelService.FindAll();

            return pasteis
                .Select(p => pastelConverter.ConvertToResource(p))
                .ToList();
        }
    }
}
