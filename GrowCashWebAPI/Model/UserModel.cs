using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowCashWebAPI.Model
{
    [Table("Users")]
    public class UserModel
    {
        [Key]
        [Column("Id")]
        public int Id {get; set; }

        [Column("Name")]
        public required string Name {get; set; }

        [Column("Email")]
        public required string Email {get; set; }

        [Column("Password")]
        public required string Password {get; set; }

        [DataType(DataType.DateTime)]
        [Column("Created_At")]
        public DateTime Created_At {get; set; }

        [Column("Login_Attempts")]
        public int Login_Attempts {get; set; }

        [Column("Active")]
        public bool Active {get; set; }

        public List<AccountModel>? Account {get; set;}
    }
}