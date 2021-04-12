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
    [Route("customers")]
    public class CustomerTextViewController : ControllerBase
    {
        public TextViewService<CustomerTextView> customerTextViewService;
        private readonly IElasticClient _elasticClient;
        private readonly ILogger _logger;

        public CustomerTextViewController(IElasticClient elasticClient, ILogger<CustomerTextViewController> logger)
        {
            _logger = logger;
            _elasticClient = elasticClient;
            customerTextViewService = new TextViewService<CustomerTextView>(_elasticClient, CustomerTextView.INDEX, CustomerTextView.TYPE, _logger);
        }
        [HttpPost]
        public IActionResult CreateCustomerTextView([FromBody] CustomerTextView customerTextView)
        {
            try
            {
                _logger.LogInformation("CreateCustomerTextView Request");
                customerTextViewService.Index(customerTextView);
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
            _logger.LogInformation("Search Request");
            var result = customerTextViewService.Search(search);
            return Ok(result);
        }
    }
}
