using SQLite;

namespace DeliveryIngesup.Models
{
    public class Produit
    {
        [PrimaryKey, Column("nom"), NotNull]
        public string Nom { get; set; }
        [Column("prix"), NotNull]
        public string Prix { get; set; }
    }
}
