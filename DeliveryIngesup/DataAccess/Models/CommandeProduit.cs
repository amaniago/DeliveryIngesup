using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace DataAccess.Models
{
    public class CommandeProduit
    {
        [ForeignKey(typeof(Commande))]
        public int Commande { get; set; }
        [ForeignKey(typeof(Produit))]
        public int Produit { get; set; }
        [Column("quantite")]
        public int Quantite { get; set; }
    }
}
