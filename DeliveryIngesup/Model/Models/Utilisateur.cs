using SQLite.Net.Attributes;

namespace Model.Models
{
    public class Utilisateur
    {
        [PrimaryKey, Column("email"), NotNull]
        public string Email { get; set; }
        [Column("nom"), NotNull]
        public string Nom { get; set; }
        [Column("prenom"), NotNull]
        public string Prenom { get; set; }
        [Column("password"), NotNull]
        public string Password { get; set; }
    }
}
