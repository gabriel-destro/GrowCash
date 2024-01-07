using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowCashWebAPI.Model
{
    [Table("Revenues")]
    public class RevenueModel
    {
        [Key]
        [Column("Id")]
        public int Id {get; set;}

        [Column("Id_Account")]
        public required int Id_Account {get; set;}

        [Column("Value")]
        public required decimal Value {get; set;}

        [Column("Description")]
        public required string Description {get; set;}

        public AccountModel? Account {get; set;}
    }
}