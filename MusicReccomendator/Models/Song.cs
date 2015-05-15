using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicReccomendator.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }
        public string SongTitle { get; set; }
        public double SongDuration { get; set; }
        public DateTime Timestamp { get; set; }

        public Genre Genre { get; set; }

        public ICollection<UserCollection> UserCollections { get; set; }
        public ICollection<Collecton> Collections { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Artist> Artists { get; set; }
    }
}