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
        [Column("horaire")]
        public DateTime Horaire { get; set; }
        [Column("adresse")]
        public string Adresse { get; set; }
        [Column("codepostal")]
        public string CodePostal { get; set; }
        [Column("ville")]
        public string Ville { get; set; }
        [Column("etat")]
        public string Etat { get; set; }
    }
}
