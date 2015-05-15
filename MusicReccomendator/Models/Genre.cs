using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MusicReccomendator.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public DateTime Timestamp { get; set; }

        public ICollection<Song> Songs { get; set; }
        public ICollection<Collecton> Collections { get; set; }
    }
}
