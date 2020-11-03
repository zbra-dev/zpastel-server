using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZPastel.API.Converters;
using ZPastel.API.Resources;
using ZPastel.Model;
using ZPastel.Service.Contract;

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
        private readonly PastelPageConverter pastelPageConverter;

        public PastelController(
            ILogger<PastelController> logger, 
            IPastelService pastelService,
            PastelConverter pastelConverter,
            PastelFilterConverter pastelFilterConverter,
            PastelPageConverter pastelPageConverter)
        {
            this.logger = logger;
            this.pastelService = pastelService;
            this.pastelConverter = pastelConverter;
            this.pastelFilterConverter = pastelFilterConverter;
            this.pastelPageConverter = pastelPageConverter;
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

        [HttpPost("filter", Name = nameof(FilterPasteis))]
        [Produces("application/json", Type = typeof(PageResource<PastelResource>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PageResource<PastelResource>>> FilterPasteis(PastelFilterResource pastelFilterResource)
        {
            var pastelFilter = pastelFilterConverter.ConvertToModel(pastelFilterResource);

            var pasteisPage = await pastelService.Filter(pastelFilter);

            return pastelPageConverter.ConvertToResource(pasteisPage);
        }
    }
}
