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

namespace JamCamBarebonesCrm.Examples
{
    public class CreatingRelationshipsExample
    {

        public void Example(IOrganizationService service)
        {

            entities.Account earlyBoundAccount = new entities.Account();
            earlyBoundAccount.Name = "Cool Business";
            Guid accountID = service.Create(earlyBoundAccount);

            entities.Contact earlyBoundContact = new entities.Contact();
            earlyBoundContact.FirstName = "Jimmy";
            earlyBoundContact.LastName = "Beans";
            Guid contactID = service.Create(earlyBoundContact);

            var relInfo = new Relationship("account_primary_contact");
            var relatedIDs = new EntityReferenceCollection();
            relatedIDs.Add(earlyBoundAccount.ToEntityReference());

            // How to use Associate:
            service.Associate(entities.Contact.EntityLogicalName, contactID, relInfo, relatedIDs);

            // How to use Disassociate:
            service.Disassociate(entities.Contact.EntityLogicalName, contactID, relInfo, relatedIDs);



        }
    }
}
