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
        private readonly PastelFilterConverter pastelFilterConverter;

        public PastelController(
            ILogger<PastelController> logger, 
            IPastelService pastelService,
            PastelConverter pastelConverter,
            PastelFilterConverter pastelFilterConverter)
        {
            this.logger = logger;
            this.pastelService = pastelService;
            this.pastelConverter = pastelConverter;
            this.pastelFilterConverter = pastelFilterConverter;
        }

        [HttpGet(Name = nameof(GetPasteis))]
        [Produces("application/json", Type = typeof(IReadOnlyList<PastelResource>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<PastelResource>>> GetPasteis()
        {
            var pasteis =  await pastelService.FindAll();

            var pasteisResource = pasteis
                .Select(p => pastelConverter.ConvertToResource(p))
                .ToList();

            return Ok(pasteisResource);
        }

        [HttpPost("filter", Name = nameof(FilterPasteis))]
        [Produces("application/json", Type = typeof(IReadOnlyList<PastelResource>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<PastelResource>>> FilterPasteis(PastelFilterResource pastelFilterResource)
        {
            var pastelFilter = pastelFilterConverter.ConvertToModel(pastelFilterResource);

            var pasteis = await pastelService.Filter(pastelFilter);

            var pasteisResource = pasteis
                .Select(p => pastelConverter.ConvertToResource(p))
                .ToList();

            return Ok(pasteisResource);
        }
    }
}
