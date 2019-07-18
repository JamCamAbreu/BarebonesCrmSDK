
Notes on Writing Plugins (aka, event handlers) for Microsoft CRM using the SDK

The 'FollowupPlugin' class in this project follows the tutorial online from microsoft docs:
https://docs.microsoft.com/en-us/powerapps/developer/common-data-service/tutorial-write-plug-in

The tutorial starts by showing you how to build a dll project like this with the FollowupPlugin class.

If you are like me, you would have started to wonder while writing/copying the code how the plugin
knew that we were trying to write this plugin for a "Create" event for an "account" entity,
because there is nothing in the code that indicates that.

If you scroll down the tutorial, you'll see that all of that is handled in the Plugin Registration Tool. 
All we care about in code is making sure we have the correct arguments, performing any necessary CRUD 
operations through our 'business logic', and handling any exceptions needed. 

After building this dll, we will use the Microsoft Plugin Registration tool to include it in our CRM. 
See the tutorial from the link above about how to register this code to the CRM.
