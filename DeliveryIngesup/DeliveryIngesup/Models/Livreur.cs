using SQLite;

namespace DeliveryIngesup.Models
{
    public class Livreur
    {
        [PrimaryKey, Column("idlivreur")]
        public int IdLivreur { get; set; }
        [Column("nom"), NotNull]
        public string Nom { get; set; }
        [Column("password")]
        public string Password { get; set; }
    }
}
