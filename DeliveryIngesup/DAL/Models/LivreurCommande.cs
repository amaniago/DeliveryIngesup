using SQLiteNetExtensions.Attributes;

namespace DAL.Models
{
    class LivreurCommande
    {
        [ForeignKey(typeof(Commande))]
        public int Commande { get; set; }
        [ForeignKey(typeof(Livreur))]
        public int Livreur { get; set; }
    }
}
