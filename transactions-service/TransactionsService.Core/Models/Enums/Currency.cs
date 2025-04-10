using System.Runtime.Serialization;

namespace TransactionsService.Core.Models.Enums
{
    public enum Currency
    {
        [EnumMember]
        USD,

        [EnumMember]
        EUR,

        [EnumMember]
        GBP,

        [EnumMember]
        NGN,
    }
}
