using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Users.UserServices.Infrastructure
{
    public class AutomapServiceBehaviour : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            AutomapBoostrap.InitializeMap();
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {  
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {  
        }
    }
}
