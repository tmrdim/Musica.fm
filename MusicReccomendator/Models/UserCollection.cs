using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicReccomendator.Models
{
    public class UserCollection
    {
        [Key]
        public int UserCollectionId { get; set; }
        public string CollectionName { get; set; }
        public DateTime Timestamp { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public ICollection<Song> Songs { get; set; }

    }
}