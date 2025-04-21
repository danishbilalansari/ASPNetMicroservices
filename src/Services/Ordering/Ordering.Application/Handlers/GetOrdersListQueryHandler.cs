using AutoMapper;
using MediatR;
using Ordering.Application.Responses;
using Ordering.Application.Queries;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrderResponse>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<OrderResponse>>(orderList);
        }
    }
}
