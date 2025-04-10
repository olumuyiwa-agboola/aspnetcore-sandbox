using System.ComponentModel;
using TransactionsService.Core.Models.Enums;

namespace TransactionsService.Core.Models.DTOs
{
    public record PostTransactionRequest
    (
        string SenderName,

        string SenderBankName,

        string SenderAccountNumber,

        string BeneficiaryName,

        string BeneficiaryBankName,

        string BeneficiaryAccountNumber,

        Currency Currency,

        string Remarks,

        decimal Amount
    );
}
