using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DeliveryIngesup.Models
{
    public class Commande
    {
        [ForeignKey(typeof(Utilisateur))]
        public string Utilisateur { get; set; }
        [ForeignKey(typeof(Produit))]
        public string Produit { get; set; }
        [Column("horaire"), NotNull]
        public string Horaire { get; set; }
    }
}
