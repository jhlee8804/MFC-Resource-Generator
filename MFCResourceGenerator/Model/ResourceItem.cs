using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFCResourceGenerator.Model
{
    public class ResourceItem
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string Type { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
