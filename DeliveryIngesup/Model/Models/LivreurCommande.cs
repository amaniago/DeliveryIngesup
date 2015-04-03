﻿using SQLiteNetExtensions.Attributes;

namespace Model.Models
{
    class LivreurCommande
    {
        [ForeignKey(typeof(Commande))]
        public int Commande { get; set; }
        [ForeignKey(typeof(Livreur))]
        public int Livreur { get; set; }
    }
}
