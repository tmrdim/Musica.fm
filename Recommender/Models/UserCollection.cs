using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recommender.Models
{
    public class UserCollection
    {
        public string UserId { get; set; }

        public int UserCollectionId { get; set; }

        public string UserCollectionName { get; set; }

        public DateTime? Timestamp { get; set; }

    }
}