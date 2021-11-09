using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationUtility
{
    public partial class ClientConfiguration
    {
        public static ClientConfiguration Default { get { return ClientConfiguration.OneBox; } }

        public static ClientConfiguration OneBox = new ClientConfiguration()
        {
            // You only need to populate this section if you are logging on via a native app. For Service to Service scenarios in which you e.g. use a service principal you don't need that.
            //UriString = "https://usnconeboxax1aos.cloud.onebox.dynamics.com",
            //UserName = "rzhang@hsochina.onmicrosoft.com",
            // Insert the correct password here for the actual test.
            //Password = "dfaer#$^F3242",
            //UserName = "jfu@hsochina.onmicrosoft.com",
            // Insert the correct password here for the actual test.
            //Password = "FzyZd@(^(*",
            //UserName = "cwepoc@cwed365.onmicrosoft.com",
            // Insert the correct password here for the actual test.
            //Password = "Password01!qwe",
            //UserName = "axserviceuser@d365poc.com",
            //Password = "Password01!",
            // You need this only if you logon via service principal using a client secret. See: https://docs.microsoft.com/en-us/dynamics365/unified-operations/dev-itpro/data-entities/services-home-page to get more data on how to populate those fields.
            // You can find that under AAD in the azure portal
            //ActiveDirectoryResource = "https://usnconeboxax1aos.cloud.onebox.dynamics.com", // Don't have a trailing "/". Note: Some of the sample code handles that issue.

            //ActiveDirectoryTenant = "https://login.windows.net/cwed365.onmicrosoft.com", // Some samples: https://login.windows.net/yourtenant.onmicrosoft.com, https://login.windows.net/microsoft.com
            //ActiveDirectoryTenant = "https://login.microsoftonline.com/76d68281-d018-4a23-8bfd-3acdc892957/saml2",
            //ActiveDirectoryClientAppId = "7e6345c7-da0c-40d8-8600-1ab3f2e51408",
            //ActiveDirectoryClientAppId = "e1fc2790-bada-4b4a-9dff-d10c5988b450",
            // Insert here the application secret when authenticate with AAD by the application
            //ActiveDirectoryClientAppSecret = "p/1T4ubH9yz2KsGE:YfZjqiqvqisIv:.",
            //ActiveDirectoryClientAppSecret = "OIKM1FwzE_Dn1qYDJV7dqrqsn?=Bge-5",

            //Sanbox
            UriString = "https://bjerp.d365poc.com",
            ActiveDirectoryResource = "https://bjerp.d365poc.com",
            ActiveDirectoryTenant = "https://adfs365.d365poc.com/adfs/oauth2",
            ActiveDirectoryClientAppId = "2d248b39-31c3-4036-a485-f8fe7b352a06",
            ActiveDirectoryClientAppSecret = "i4HMNbViiMLtEu_bFTm6g_YxIAlkr4su6lmcmztG",

            // Change TLS version of HTTP request from the client here
            // Ex: TLSVersion = "1.2"
            // Leave it empty if want to use the default version
            TLSVersion = "",
        };

        public string TLSVersion { get; set; }
        public string UriString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
        public string ActiveDirectoryClientAppSecret { get; set; }
    }
}
