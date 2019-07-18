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
    public class CreatingDataExample
    {

        public void Example(IOrganizationService service)
        {

            // NOTES:
            // - You can use importsequencenumber on create for things like import batch number, etc...


            /* ***********************************************************
             *              EXAMPLE: 'Late Bound' Entity 
             *         (late-bound means that it is not typed)
             * ***********************************************************/

            /* **** SETTING different kinds of fields on a late-bound **** */
            Entity account = new Entity("account");
            account["name"] = "test"; // make sure that 'name' is an attribute that exists, 
                                      // and that its type is string, otherwise error

            // INTEGER FIELDS
            account["numberofemployees"] = 12345;
            account["numberofemployees"] = "12345"; // this will throw an error

            // OPTION SET FIELDS
            account["accountcategorycode"] = new OptionSetValue(1);

            // MONEY FIELDS
            account["creditlimit"] = new Money((decimal)50000);

            // LOOKUP FIELDS
            var contact = new Entity("contact") { Id = Guid.NewGuid() }; // dummy contact
            account["primarycontactid"] = new EntityReference(contact.LogicalName, contact.Id);

            // LOOKUP FIELDS (method 2):
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; // dummy contact
            account["primarycontactid"] = contact.ToEntityReference();

            // Create using the IOrganizationService
            Guid accountID = service.Create(account);

            // Retrieve the account we just created:
            entities.Account retAccount = service.Retrieve("account", accountID, new ColumnSet(true)) as entities.Account;


            // Some system fields, such as createdon, can be overridden with special fields:
            account["overriddencreatedon"] = new DateTime(2011, 11, 1, 1, 00, 00);
            //      - During the objects creation, the 'overriddencreatedon' that we set, and the 
            //       'createdon' system field will swap


            /* ***********************************************************
             *              EXAMPLE: 'EARLY Bound' Entity 
             *   (early-bound means that we have defined a class for each
             *                  entity type in our CRM)
             * ***********************************************************/

            entities.Account account2 = new entities.Account();
            account2.Name = "Snapographcs, LLC";
            account2.NumberOfEmployees = 124;
            account2.AccountCategoryCode = new OptionSetValue(1);
            account2.CreditLimit = new Money((decimal)50000);

            // Create using the IOrganizationService
            Guid accountID2 = service.Create(account2);

            // Retrieve the account we just created:
            entities.Account retAccount2 = service.Retrieve("account", accountID2, new ColumnSet(true)) as entities.Account;

        }
    }
}
