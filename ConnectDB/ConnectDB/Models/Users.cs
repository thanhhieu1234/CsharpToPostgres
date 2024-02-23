using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectDB.Models
{
    [Table("users")]
    public class Users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public long age { get; set; }
    }
}
