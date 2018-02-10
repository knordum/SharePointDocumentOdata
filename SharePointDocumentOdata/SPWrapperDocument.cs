using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharePointDocumentOdata
{
    public class SpDocument
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Url { get; set; }
    }
}