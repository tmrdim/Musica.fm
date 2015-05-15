using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicReccomendator.Models
{
    public class Collecton
    {
        [Key]
        public int CollectionId { get; set; }
        public string CollectionName { get; set; }
        public int NumberOfSongs { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime Timestamp { get; set; }

        public Genre Genre { get; set; }
    
    }
}