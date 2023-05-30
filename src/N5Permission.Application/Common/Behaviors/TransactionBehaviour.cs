using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using N5Permission.Application.Common.Services;
using N5Permission.Domain.Common;  

namespace N5Permission.Application.Common.Behaviors
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly IKafkaService _KafkaService;
        private readonly AppSetting _appSetting;
        public TransactionBehaviour(IKafkaService KafkaService, IOptions<AppSetting> appSetting,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _KafkaService = KafkaService ?? throw new ArgumentException(nameof(IKafkaService)); ;
            _appSetting = appSetting.Value;
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = default(TResponse);
            var requestName = typeof(TRequest).Name;

            try
            {
                string operationName = Constants.OperationNames.FirstOrDefault(x => x.Key.Equals(requestName, StringComparison.InvariantCultureIgnoreCase)).Value ?? requestName;

                var baseEvent = new BaseEvent
                {
                    Id = Guid.NewGuid().ToString(),
                    OperationName = operationName,
                    OperationFullName = typeof(TRequest).FullName ?? operationName,
                };
                _logger.LogInformation("INFO:: Initial Handling transaction for {OperationName}", requestName, request);

                response = await next();

                _logger.LogInformation("INFO:: End Handling transaction for {OperationName}", requestName, request);

                await _KafkaService.PublishEventAsync(_appSetting.ProducerSettings.TopicName, baseEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "::ERROR:: Handling transaction for {OperationName}", requestName, request);
                throw;
            }

            return response;
        }
    }
}
