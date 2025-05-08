using CraftIQ.Inventory.Core.Entities.Orders;
using CraftIQ.Inventory.Core.Entities.Orders.Specifications;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Shared.Contracts.Orders;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System.Net;

namespace CraftIQ.Inventory.Services.OrdersImplementations
{
    public class OrdersServices<TRequest, TResponse>(IRepository<Order> orderRepository) : IGenericServices<TRequest, TResponse>
    {
        private readonly IRepository<Order> _orderRepository = orderRepository;
        public async ValueTask<TRequest> Create(TRequest contract)
        {
            var oContarct = contract as OrdersOperationsContract;
            var oOrder = new Order(oContarct!.SupplierId,
                                   oContarct.TotalAmount,
                                   oContarct.Status,
                                   oContarct.ExpectedDeliveryDate,
                                   oContarct.OrderType);
            var oOrderResult = await _orderRepository.AddAsync(oOrder);
            if (oOrderResult == null)
                return default;
            oContarct.OrderId = oOrderResult.OrderId;
            return oContarct as dynamic;
        }

        public async ValueTask Delete(Guid ContractId)
        {
            var oReadByIdSpec = new ReadOrdersByIdSpecification(ContractId);
            var oResult = await _orderRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
                await _orderRepository.DeleteAsync(oResult);
            else throw new ResultException("You can't delete an object that does not exist.", (int)HttpStatusCode.Forbidden);
        }

        public async ValueTask<List<TResponse>> Read()
        {
            var oReadSpec = new ReadOrdersSpecification();
            var oData = await _orderRepository.ListAsync(oReadSpec);
            if (oData != null && oData.Count > 0)
            {
                var oResult = oData.Select(o => new OrdersContract(o.OrderId,
                                                                   o.SupplierId,
                                                                   o.OrderDate,
                                                                   o.TotalAmount,
                                                                   o.Status,
                                                                   o.ExpectedDeliveryDate,
                                                                   o.OrderType,
                                                                   o.ReceivedDate)).ToList();
                return oResult as dynamic;
            }
            else return new List<OrdersContract>() as dynamic;
        }

        public async ValueTask<TResponse> ReadById(Guid ContractId)
        {
            var oReadByIdSpec = new ReadOrdersByIdSpecification(ContractId);
            var oResult = await _orderRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
                return new OrdersContract(oResult.OrderId,
                                          oResult.SupplierId,
                                          oResult.OrderDate,
                                          oResult.TotalAmount,
                                          oResult.Status,
                                          oResult.ExpectedDeliveryDate,
                                          oResult.OrderType,
                                          oResult.ReceivedDate,
                                          oResult.CreatedBy,
                                          oResult.ModifiedBy,
                                          oResult.CreatedOn,
                                          oResult.ModifiedOn) as dynamic;
            else throw new ResultException("This object does not exist", (int)HttpStatusCode.NotFound);
        }

        public async ValueTask Update(Guid ContractId, TRequest contract)
        {
            var oContract = contract as OrdersOperationsContract;
            var oReadByIdSpec = new ReadOrdersByIdSpecification(ContractId);
            var oResult = await _orderRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
            {
                oResult.UpdateOrder(oContract);
                await _orderRepository.UpdateAsync(oResult);
            }
            else throw new ResultException("This object does not exist", (int)HttpStatusCode.NotFound);
        }

        public async ValueTask<List<TResponse>> ReadByParentId(Guid parentContractId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<TResponse> ReadSingleByParentId(Guid contractId, Guid parentContractId)
        {
            throw new NotImplementedException();
        }



        public async ValueTask UpdateParentId(Guid ContractId, Guid parentContractId)
        {
            throw new NotImplementedException();
        }
    }
}
