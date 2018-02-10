using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Web;
using Microsoft.SharePoint.Client;

namespace SharePointDocumentOdata
{
    public class SpDocumentDataSource
    {
        private static SpDocumentDataSource instance = null;
        public static SpDocumentDataSource Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpDocumentDataSource();
                }
                return instance;
            }
        }
        public List<SpDocument> SpDocuments { get; set; }

        private SpDocumentDataSource()
        {
            this.Reset();
            this.Initialize();
        }
        public void Reset()
        {
            this.SpDocuments = new List<SpDocument>();
        }
        public void Initialize()
        {
            var username = ConfigurationManager.AppSettings["SpUsername"];
            var password = ConfigurationManager.AppSettings["SpPassword"];
            var SpUrl = ConfigurationManager.AppSettings["SpUrl"];

            using (var siteContext = new ClientContext(SpUrl))
            {
                siteContext.Credentials = new SharePointOnlineCredentials(username, GetPwd(password));
                var rootWeb = siteContext.Site.RootWeb;
                siteContext.Load(rootWeb);
                siteContext.ExecuteQuery();
                var orderList = siteContext.Web.Lists.GetByTitle("ContractType");
                var listItems = orderList.GetItems(new CamlQuery());
                siteContext.Load(listItems);
                siteContext.ExecuteQuery();

                foreach (var doc in listItems)
                {
                    SpDocuments.Add(new SpDocument()
                    {
                        Id = new Guid(doc.FieldValues["GUID"].ToString()),
                        Name = doc.FieldValues["Title"].ToString(),
                        Url = doc.FieldValues["ServerRedirectedEmbedUri"].ToString()

                    });
                }
            }
        }

        private static SecureString GetPwd(string password)
        {
            var secure = new SecureString();
            foreach (char c in password)
            {
                secure.AppendChar(c);
            }
            return secure;
        }
    }
}