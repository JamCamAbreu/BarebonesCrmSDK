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
    public class QueryExample
    {

        public void Example(IOrganizationService service)
        {

            // Using QueryByAttribute Example:
            QueryByAttribute qa = new QueryByAttribute();
            qa.ColumnSet = new ColumnSet("accountid", "name");
            qa.Attributes.AddRange("address1_city");
            qa.Values.AddRange("Colorado Springs");
            qa.AddOrder("name", OrderType.Ascending);
            var qaResults = service.RetrieveMultiple(qa);


            // Using QueryExpression Example (this one is more flexible, and probably better)
            QueryExpression qe = new QueryExpression();
            qe.ColumnSet = new ColumnSet("accountid", "name");
            qe.Criteria = new FilterExpression();
            qe.Criteria.AddCondition("address1_city", ConditionOperator.Equal, "Colorado Springs");
            qe.AddOrder("name", OrderType.Ascending);
            var qeResults = service.RetrieveMultiple(qe);


            // Using FetchXml Example:
            string fetchQuery = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                    <entity name='account'>
                                        <attribute name='name' />
                                        <order attribute='name' descending='false' />
                                        <filter type='and'>
                                            <condition attribute='address1_city' operator='eq' value='Colorado Springs' />
                                        </filter>
                                    </entity>
                                </fetch>";

            var fetchResults = service.RetrieveMultiple(new FetchExpression(fetchQuery));

        }

    }
}
