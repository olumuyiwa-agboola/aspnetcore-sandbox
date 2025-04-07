using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TransactionsService.Core.Models.DTOs
{
    public class TransactionDetailsRequest
    {
        [Required]
        [FromRoute]
        public string? Reference{ get; set; }
    };
}
