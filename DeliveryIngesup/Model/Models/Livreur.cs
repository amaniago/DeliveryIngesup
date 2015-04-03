using SQLite.Net.Attributes;

namespace Model.Models
{
    public class Livreur
    {
        [PrimaryKey, Column("idlivreur")]
        public int IdLivreur { get; set; }
        [PrimaryKey, Column("nom")]
        public string Nom { get; set; }
    }
}
