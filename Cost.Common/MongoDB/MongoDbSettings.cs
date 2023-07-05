using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cost.Common.MongoDB
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }  
        public string ConnectionString { get; set; }
    }
}
