using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DeliveryIngesup.Models
{
    public class Commande
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int IdCommande { get; set; }
        [ForeignKey(typeof(Utilisateur))]
        public string Utilisateur { get; set; }
        [Column("horaire"), NotNull]
        public DateTime Horaire { get; set; }
    }
}
