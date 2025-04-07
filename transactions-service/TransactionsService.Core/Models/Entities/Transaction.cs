using System.ComponentModel.DataAnnotations;
using TransactionsService.Core.Models.Enums;

namespace TransactionsService.Core.Models.Entities
{
    public record Transaction
    {
        [Key]
        [Required]
        [MinLength(30)]
        [MaxLength(30)]
        public required string Reference { get; init; }

        [Required]
        [MaxLength(20)]
        [Range(0, 9999999999999999.99)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public required decimal Amount { get; init; }

        [Required]
        [MaxLength(100)]
        public required string Remarks { get; init; }

        [Required]
        [DataType(DataType.Currency)]
        public required Currency Currency { get; init; }

        [Required]
        [DataType(DataType.DateTime)]
        public required DateTime DateTime { get; init; }

        [Required]
        [DataType(nameof(TransactionStatus))]
        public required TransactionStatus Status { get; init; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public required string SenderName { get; init; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public required string SenderBankName { get; init; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        [DataType(DataType.Text)]
        [RegularExpression(@"\d{10}")]
        public required string SenderAccountNumber { get; init; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public required string BeneficiaryName { get; init; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public required string BeneficiaryBankName { get; init; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        [DataType(DataType.Text)]
        [RegularExpression(@"\d{10}")]
        public required string BeneficiaryAccountNumber { get; init; }
    }
}
