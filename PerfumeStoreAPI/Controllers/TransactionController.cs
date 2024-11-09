using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Service.Service;
using PerfumeStore.API.RequestModel;
using PerfumeStore.API.ResponseModel;

namespace PerfumeStore.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController :ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTransactinByOrder(Guid orderId)
        {
            var transactions = await _transactionService.GetTransactionByOrderId(orderId);

            if (transactions == null || !transactions.Any())
            {
                return NotFound("Don't have any activity or user not found.");
            }

            return Ok(transactions);
        }


    }
}
