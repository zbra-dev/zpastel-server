using System.Collections.Generic;
using System.Linq;
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
    [Route("api/orders/")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService orderService;
        private readonly OrderConverter orderConverter;

        public OrderController(
            ILogger<OrderController> logger, 
            IOrderService orderService,
            OrderConverter orderConverter)
        {
            _logger = logger;
            this.orderService = orderService;
            this.orderConverter = orderConverter;
        }

        [HttpPost("create", Name = nameof(CreateOrder))]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderResource>> CreateOrder(OrderResource orderResource)
        {
            var order = orderConverter.ConvertToModel(orderResource);

            var savedOrder = await orderService.CreateOrder(order);

            var savedOrderResource = orderConverter.ConvertToResource(savedOrder);

            return Ok(savedOrderResource);
        }

        [HttpGet(Name = nameof(FindAll))]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<OrderResource>>> FindAll()
        {
            var allOrders = await orderService.FindAll();

            var allOrdersResource = allOrders.Select(o => orderConverter.ConvertToResource(o)).ToList();

            return Ok(allOrdersResource);
        }

        [HttpGet("{id}", Name = nameof(FindById))]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderResource>> FindById(long id)
        {
            var foundOrder = await orderService.FindById(id);

            var foundOrderResource = orderConverter.ConvertToResource(foundOrder);

            return Ok(foundOrderResource);
        }

        [HttpDelete("delete/{id}", Name = nameof(DeleteOrder))]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteOrder(long id)
        {
            await orderService.DeleteOrder(id);

            return Ok();
        }

        [HttpGet("user/{userId}", Name = nameof(FindByUserId))]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<OrderResource>>> FindByUserId(long userId)
        {
            var allUserOrders = await orderService.FindByUserId(userId);

            var allUserOrdersResource = allUserOrders.Select(o => orderConverter.ConvertToResource(o)).ToList();

            return Ok(allUserOrdersResource);
        }
    }
}
