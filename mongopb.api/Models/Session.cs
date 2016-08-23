using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mongopb.Models
{
    public class Session
    {
        public ObjectId Id { get; set; }
        public String userid { get; set; }
        public DateTime expires { get; set; }
    }
}
