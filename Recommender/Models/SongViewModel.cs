using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recommender.Models
{
    public class SongsViewModel
    {
        public int SongId { get; set; }

        public string SongTitle { get; set; }

        public int ArtistId { get; set; }

        public int? AuthorId { get; set; }

        public int GenreId { get; set; }

        public DateTime Timestamp { get; set; }

        public double? Duration { get; set; }

        public int CollectionId { get; set; }

        public string ArtistName { get; set; }

        public string AuthorName { get; set; }

        public string GenreName { get; set; }
    }
}