using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DataSet
    {
        public int DataSetId { get; set; }
        public List<Property> Properties { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
