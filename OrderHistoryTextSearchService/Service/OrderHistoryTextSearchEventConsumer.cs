using IO.Eventuate.Tram.Events.Subscriber;
using IO.Eventuate.Tram.Messaging.Common;
using Microsoft.Extensions.Logging;
using Nest;
using OrderHistoryTextSearchService.Controllers;
using ServiceCommon.Classes;
using ServiceCommon.OrderHistoryTextSearchCommon;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace OrderHistoryTextSearchService.Service
{
    public class OrderHistoryTextSearchEventConsumer : IDomainEventHandler<CustomerCreatedEvent>, IDomainEventHandler<OrderCreatedEvent>
    {
        public TextViewService<CustomerTextView> customerTextViewService;
        public TextViewService<OrderTextView> orderTextViewService;
        private readonly IElasticClient _elasticClient;
        private readonly ILogger _logger;
        public OrderHistoryTextSearchEventConsumer(IElasticClient elasticClient, ILogger<OrderHistoryTextSearchEventConsumer> logger)
        {
            _logger = logger;
            _elasticClient = elasticClient;
            customerTextViewService = new TextViewService<CustomerTextView>(_elasticClient, CustomerTextView.INDEX, CustomerTextView.TYPE, _logger);
            orderTextViewService = new TextViewService<OrderTextView>(_elasticClient, OrderTextView.INDEX, OrderTextView.TYPE, _logger);
        }
        public void Handle(IDomainEventEnvelope<CustomerCreatedEvent> customerCreatedEvent)
        {
            _logger.LogInformation("Handle CustomerCreatedEvent");
            var customerTextView = new CustomerTextView
            {
                id = customerCreatedEvent.AggregateId,
                Name = customerCreatedEvent.Event.Name,
                CreditLimit = customerCreatedEvent.Event.CreditLimit.Amount.ToString()
            };
            customerTextViewService.Index(customerTextView);
        }
        public void Handle(IDomainEventEnvelope<OrderCreatedEvent> orderCreatedEvent)
        {
            _logger.LogInformation("Handle OrderCreatedEvent");
            var orderTextView = new OrderTextView
            {
                id = orderCreatedEvent.AggregateId,
                CustomerId = orderCreatedEvent.Event.OrderDetails.CustomerId.ToString(),
                OrderTotal = orderCreatedEvent.Event.OrderDetails.OrderTotal.Amount.ToString(),
                State = "PENDING",
            };
            orderTextViewService.Index(orderTextView);
        }
    }
}
