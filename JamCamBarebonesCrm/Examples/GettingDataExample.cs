using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamCamBarebonesCrm
{
    public class GettingDataExample
    {

        public void Example(IOrganizationService service)
        {
            /* ***********************************************************
             *              EXAMPLE: 'Late Bound' Entity 
             *         (late-bound means that it is not typed)
             * ***********************************************************/

            Guid someId = Guid.Parse("A6AEE2FF-D991-E911-9C2F-00155D03042A");
            var account = service.Retrieve("account", someId, new ColumnSet("name"));

            string name = account["name"].ToString(); // account["someField"] returns an
                                                      // object that must be cast accordingly
            // OR
            if (account.Contains("name")) { } // use to gracefully check first

            // OR
            name = account.GetAttributeValue<string>("name"); // returns default if doesn't exist
                                                              // -- Default Values by type --
                                                              // string:          null
                                                              // OptionSetValue:  null
                                                              // Money:           null
                                                              // EntityReference: null
                                                              // int:             0
                                                              // bool:            false
                                                              // DateTime:        DateTime.MinValue


            /* ***********************************************************
             *              EXAMPLE: 'Early Bound' Entity 
             *   (early-bound means that we have defined a class for each
             *                  entity type in our CRM)
             * ***********************************************************/

            // You can generate typed classes for each entity by using the CrmSvcUtil.exe
            // The CrmSvcUtil.exe is located in your SDK under SDK/Bin
            // See the "ExampleCrmSvcUtil.txt" under this projects Notes folder on how 
            // to use this tool to generate the typed classes for your entities

            // Notice the cast at the end is necessary:
            entities.Account acc = service.Retrieve("account", someId, new ColumnSet("name")) as entities.Account;
            string name2 = acc.Name; // compile time type-checking ensures cleaner code
        }


    }
}
