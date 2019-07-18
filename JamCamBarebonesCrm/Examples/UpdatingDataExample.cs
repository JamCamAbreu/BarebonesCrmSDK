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
    public class UpdatingDataExample
    {


        public void Example(IOrganizationService service)
        {

            /* Notes:
                - retrieve is NOT required before updating
                - specify only the columns you need to update
                - All columns passed will be tracked as changed by CRM events and auditing even if they didn't change
                - you can update statuscode but not statecode directly
                - Modified By and Modified On are updated by CRM
            */

            // When working with items that you have already retrieved, be careful about the columns:
            Entity account = new Entity("account"); // pretend that this is not a new entity, but rather one that we pulled from CRM
            Entity accountInfo = service.Retrieve(
                account.LogicalName,
                Guid.NewGuid(),
                new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
            if (!accountInfo.Contains("creditlimit"))
            {
                accountInfo["creditlimit"] = new Money(500.00M);
                service.Update(accountInfo); // HERE:
                                             // Unfortunately, ALL columns on the account will be updated (and audited),
                                             // INCLUDING any plugins that run on those columns being modified!!
            }

            // instead, use:
            Entity accountInfo2 = service.Retrieve(
                account.LogicalName,
                Guid.NewGuid(),
                new Microsoft.Xrm.Sdk.Query.ColumnSet("accountid", "creditlimit")); // specify exact columns


            // You CANNOT Update the following:
            // Primary key
            // createdon, createdby, modifiedon, modifiedby 
            // (see exception in GettingDataExample)
            // ownerid, owningteam, owninguser
            // statecode


            // ***** SIMPLE EARLY BOUND EXAMPLE ******
            entities.Account acc = new entities.Account();
            acc.CreditLimit = new Money((decimal)50000);
            Guid accountID = service.Create(acc);
            entities.Account retAccount = service.Retrieve("account", accountID, new ColumnSet("creditlimit")) as entities.Account;

            // Updating the credit limit:
            retAccount.CreditLimit = new Money((decimal)500);

            // Update the account (only updates the fields we specified in the ColumnSet when we pulled the account from CRM)
            service.Update(retAccount);



            // ***** UPDATE USING A NEW ENTITY *****
            entities.Account retAccount2 = service.Retrieve("account", accountID, new ColumnSet("creditlimit")) as entities.Account;

            // Use the Id of the queried account in the constructor of a new account object:
            entities.Account updater = new entities.Account() { Id = retAccount2.Id };

            // Updating the credit limit:
            updater.CreditLimit = new Money((decimal)400);

            // Update the retAccount2 account with our new account:
            service.Update(updater);




            // ***** SETTING STATE *****
            // Entities have Status (aka statecode) and status reason (aka statuscode)
            // Generated types include enums for system entity state values
            // Status and Status Reason can be set using SetStateRequest

            Entity lateBoundAccount = new Entity("account");
            account["name"] = "test"; 
            Guid myAccountId = service.Create(lateBoundAccount);
            lateBoundAccount = service.Retrieve("account", myAccountId, new ColumnSet("name"));

            SetStateRequest reqState = new SetStateRequest();

            // moniker is now an EntityReference object (moniker means 'name')
            reqState.EntityMoniker = lateBoundAccount.ToEntityReference();
            reqState.State = new OptionSetValue((int)entities.AccountState.Inactive);

            // status is also inactive
            reqState.Status = new OptionSetValue(2);

            var respState = service.Execute(reqState) as SetStateResponse;



            // ***** ASSIGNING RECORDS *****
            // User/Team owned records can have owner assigned/changed
            // At time of create ownerid defaults to calling user unless set
            // Use AssignRequest/Response to change owner

            entities.SystemUser user = new entities.SystemUser();
            entities.Account myAccount = new entities.Account();

            AssignRequest assignreq = new AssignRequest();
            assignreq.Assignee = user.ToEntityReference();
            assignreq.Target = myAccount.ToEntityReference();
            var assignResp = service.Execute(assignreq) as AssignResponse;
        }

    }
}
