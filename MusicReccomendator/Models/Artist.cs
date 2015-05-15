using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicReccomendator.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public DateTime Timestamp { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}