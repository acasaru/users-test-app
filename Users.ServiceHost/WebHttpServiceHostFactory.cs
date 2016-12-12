using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.Web;

namespace Users.ServiceHost
{
    public class WebHttpServiceHostFactory : ServiceHostFactory
    {
        protected override System.ServiceModel.ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new System.ServiceModel.ServiceHost(serviceType, baseAddresses);

            var debugBehavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (debugBehavior != null)
            {
                debugBehavior.IncludeExceptionDetailInFaults = false;
            }

            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpsGetEnabled = true, HttpGetEnabled = true });

            var binding = new WebHttpBinding();
            binding.Security.Mode = WebHttpSecurityMode.None;

            var endpoint = host.AddServiceEndpoint(serviceType, binding, string.Empty);

            endpoint.Behaviors.Add(new WebHttpBehavior());

            return host;
        }
    }
}