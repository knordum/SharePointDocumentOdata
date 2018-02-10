using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;

namespace SharePointDocumentOdata.Controllers
{
    [EnableQuery]
    public class SpDocumentsController : ODataController
    {
        public IHttpActionResult Get()
        {
            return Ok(SpDocumentDataSource.Instance.SpDocuments.AsQueryable());
        }
    }
}
