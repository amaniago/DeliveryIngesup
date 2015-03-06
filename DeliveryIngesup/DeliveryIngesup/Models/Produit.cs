using SQLite;

namespace DeliveryIngesup.Models
{
    public class Produit
    {
        [PrimaryKey, NotNull]
        public int IdProduit { get; set; }
        [NotNull]
        public string Nom { get; set; }
        [NotNull]
        public string Prix { get; set; }
    }
}
