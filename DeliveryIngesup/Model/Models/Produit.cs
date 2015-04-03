using SQLite.Net.Attributes;

namespace Model.Models
{
    public class Produit
    {
        [Column("idproduit"), AutoIncrement, PrimaryKey, NotNull]
        public int IdProduit { get; set; }
        [Column("nom"),  NotNull]
        public string Nom { get; set; }
        [Column("prix"), NotNull]
        public int Prix { get; set; }

        public int Quantite { get; set; }
    }
}
