using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowCashWebAPI.Model
{
    [Table("Accounts")]
    public class AccountModel
    {
        [Key]
        [Column("Id")]
        public int Id {get; set; } 

        [Column("Id_User")]
        public required int Id_User {get; set; }

        [Column("Name_Account")]
        public required string Name_Account {get; set; }

        [Column("Active")]
        public required bool Active {get; set; }
        public List<RevenueModel>? Revenues {get; set;}
        public List<ExpenseModel>? Expenses {get; set;}
        public UserModel? User {get; set;}
    }
}