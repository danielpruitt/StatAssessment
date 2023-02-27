using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAssesment.Models
{
    public class CsvMap
    {
        [Index(0)]
        public int Quantity { get; set; }
        [Index(1)]
        public string Name { get; set; }
        [Index(2)]
        public string PONumber { get; set; }

    }
}
