using SQLite.Net.Attributes;

namespace DAL.Models
{
    public class Livreur
    {
        [PrimaryKey, Column("idlivreur")]
        public int IdLivreur { get; set; }
        [PrimaryKey, Column("nom")]
        public string Nom { get; set; }
    }
}
