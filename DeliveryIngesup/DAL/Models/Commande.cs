using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace DAL.Models
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
