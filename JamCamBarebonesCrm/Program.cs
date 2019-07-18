using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamCamBarebonesCrm
{
    class Program
    {
        static void Main(string[] args)
        {

            // This CRMConnection constructor will take in a string, and then
            // search the App.Config's "ConnectionStrings" element for a
            // connection with this name. In this case a connection called 'CRM':
            CrmConnection con = new CrmConnection("CRM");

            // For more information and an example App.config, look for the
            // ExampleApp.Config.txt file in the Notes folder

            IOrganizationService service = new OrganizationService(con);

            // Example service execution:
            var response = service.Execute(new WhoAmIRequest());
            // note: Field level security is applied based on the CALLER to the service
            // (same for CRUD operations)

            // the IOrganizationService class has CRUD methods available,
            // see individual examples in the Examples folder

            // For Early-Bound (aka, 'typed') classes for the entities, 
            // see the ExampleCrmSvcUtil.txt file in the Notes folder


            // **** MAKING PLUGINS (aka event handlers) ****
            // Plugins can be made with a Class Library project (.NET framework)
            // https://docs.microsoft.com/en-us/powerapps/developer/common-data-service/tutorial-write-plug-in
            // Also don't forget the CS examples in the SDK


            // NEXT GOAL:
                // Dynamics CRM Developer Part 3
                // Chapter 4: Common Data and Service Operations
                // Chapter 8: Working with Metadata
        }
    }
}
