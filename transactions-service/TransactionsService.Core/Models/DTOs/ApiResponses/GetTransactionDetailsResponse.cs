using TransactionsService.Core.Models.Enums;
using TransactionsService.Core.Models.Entities;

namespace TransactionsService.Core.Models.DTOs.ApiResponses
{
    public record GetTransactionDetailsResponse
    {
        public decimal Amount { get; init; }
        public string Remarks { get; init; }
        public Currency Currency { get; init; }
        public DateTime DateTime { get; init; }
        public string SenderName { get; init; }
        public string Reference { get; init; }
        public string SenderBankName { get; init; }
        public string BeneficiaryName { get; init; }
        public TransactionStatus Status { get; init; }
        public string SenderAccountNumber { get; init; }
        public string BeneficiaryBankName { get; init; }
        public string BeneficiaryAccountNumber { get; init; }

        public GetTransactionDetailsResponse(Transaction transaction)
        {
            Status = transaction.Status;
            Amount = transaction.Amount;
            Remarks = transaction.Remarks;
            Currency = transaction.Currency;
            DateTime = transaction.DateTime;
            Reference = transaction.Reference;
            SenderName = transaction.SenderName;
            SenderBankName = transaction.SenderBankName;
            BeneficiaryName = transaction.BeneficiaryName;
            SenderAccountNumber = transaction.SenderAccountNumber;
            BeneficiaryBankName = transaction.BeneficiaryBankName;
            BeneficiaryAccountNumber = transaction.BeneficiaryAccountNumber;
        }
    }
}
