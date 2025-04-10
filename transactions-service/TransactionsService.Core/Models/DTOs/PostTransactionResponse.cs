using TransactionsService.Core.Models.Enums;

namespace TransactionsService.Core.Models.DTOs
{
    public record PostTransactionResponse
    (
        string Reference,

        TransactionStatus Status
    );
}
