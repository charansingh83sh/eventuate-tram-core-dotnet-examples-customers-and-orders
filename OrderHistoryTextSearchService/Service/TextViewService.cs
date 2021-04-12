using Microsoft.Extensions.Logging;
using Nest;
using OrderHistoryTextSearchService.Controllers;
using ServiceCommon.OrderHistoryTextSearchCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace OrderHistoryTextSearchService.Service
{
    public class TextViewService<T> : TextView where T : class
    {
        private readonly IElasticClient _elasticClient;
        private String _type;
        private String _index;
        private readonly ILogger _logger;
        public TextViewService(IElasticClient elasticClient, String index, String type, ILogger logger)
        {
            _logger = logger;
            _elasticClient = elasticClient;
            _type = type;
            _index = index;
        }
        public void Index(T textView)
        {
            try
            {
                _logger.LogInformation("CREATE INDEX");
                var response = _elasticClient.Index(textView, i => i.Index(_index));
                _logger.LogInformation(response.DebugInformation);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<T> Search(string value)
        {
            _logger.LogInformation("GET SEARCH");
            var response = _elasticClient.Search<T>(s => s
            .Index(_index)
            .Query(q => q.QueryString(d => d.Query(value))));
            var docs = response.Documents.ToList();
            _logger.LogInformation(docs.Count().ToString());
            return docs;
        }
    }
}
