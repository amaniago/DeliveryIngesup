using SQLite.Net.Attributes;

namespace DataAccess.Models
{
    public class Livreur
    {
        [PrimaryKey, Column("idlivreur")]
        public int IdLivreur { get; set; }
        [PrimaryKey, Column("nom")]
        public string Nom { get; set; }
    }
}
