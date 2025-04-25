using TransactionsService.Core.Models.Enums;

namespace TransactionsService.Core.Models.DTOs.ApiResponses
{
    public record PostTransactionResponse
    (
        string Reference,

        TransactionStatus Status
    );
}
