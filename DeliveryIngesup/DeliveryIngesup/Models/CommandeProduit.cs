using SQLiteNetExtensions.Attributes;

namespace DeliveryIngesup.Models
{
    public class CommandeProduit
    {
        [ForeignKey(typeof(Commande))]
        public int Commande { get; set; }
        [ForeignKey(typeof(Produit))]
        public int Produit { get; set; }

    }
}
