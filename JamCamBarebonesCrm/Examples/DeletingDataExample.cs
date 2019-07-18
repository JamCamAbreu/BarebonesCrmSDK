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
    public class DeletingDataExample
    {
        public void Example(IOrganizationService service)
        {

            /* Notes:
                - Use Delete when removing a small number of records
                - Use BulkDeleteRequest/Response when you need to remove larger number of records
                - Remember delete can be blocked by relationship rules (cascade, etc..) for related data
                - Same rules can cause delete of one record to delete many records
                - Choose beetween Delete and Inactivate of records
            */

            entities.Account acc = new entities.Account();
            acc.CreditLimit = new Money((decimal)50000);
            Guid accountID = service.Create(acc);
            entities.Account retAccount = service.Retrieve("account", accountID, new ColumnSet("creditlimit")) as entities.Account;

            service.Delete(retAccount.LogicalName, retAccount.Id);
        }
    }
}
