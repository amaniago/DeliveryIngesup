using SQLite;

namespace DeliveryIngesup.Models
{
    public class Livreur
    {
        [PrimaryKey, Column("idlivreur")]
        public int IdLivreur { get; set; }
        [Column("email"), NotNull]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
    }
}
