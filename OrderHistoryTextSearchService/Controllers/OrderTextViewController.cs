using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using OrderHistoryTextSearchService.Service;
using ServiceCommon.OrderHistoryTextSearchCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderHistoryTextSearchService.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderTextViewController : ControllerBase
    {
        public TextViewService<OrderTextView> orderTextViewService;
        private readonly IElasticClient _elasticClient;
        private readonly ILogger _logger;
        public OrderTextViewController(IElasticClient elasticClient, ILogger logger)
        {
            _logger = logger;
            _elasticClient = elasticClient;
            orderTextViewService = new TextViewService<OrderTextView>(_elasticClient, OrderTextView.INDEX, OrderTextView.TYPE, logger);
        }
        [HttpPost]
        public IActionResult CreateOrderTextView([FromBody] OrderTextView orderTextView)
        {
            try
            {
                _logger.LogInformation("CreateOrderTextView Request");
                orderTextViewService.Index(orderTextView);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult Search([FromQuery] string search)
        {
            _logger.LogInformation("ORDER Search Request");
            var result = orderTextViewService.Search(search);
            return Ok(result);
        }
    }
}
