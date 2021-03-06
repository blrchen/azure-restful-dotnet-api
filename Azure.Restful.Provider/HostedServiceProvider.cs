﻿using System;
using Azure.Restful.Model;
using Azure.Restful.Common;

namespace Azure.Restful.Provider
{
    public class HostedServiceProvider : BaseProvider<HostedService>
    {

        public HostedServiceProvider(SubscriptionAccount subscriptionAccount)
            : base(subscriptionAccount)
        {
        }

        public HostedServiceProvider()
            : this(null)
        {

        }

        public HostedService GetSingle(string name, bool embedDetail)
        {
            if (!embedDetail)
            {
                return GetSingle(name);
            }
            string opName = "GetHostedService";
            RequestInfo request = XmlProvider.CreateRequestInfo<HostedService>(opName, null);
            request.Url = request.Url.Replace("[name]", name) + "?embed-detail=true";
            return provider.GetResponseEntity<HostedService>(subscriptionAccount, request);
        }

        public bool CheckNameAvaliable(string name)
        {
            try
            {
                RequestInfo request = new RequestInfo
                    {
                        Url = "[service-endpoint]/[subscription-id]/services/hostedservices/operations/isavailable/" + name,
                        Method = "GET"
                    };
                provider.GetResponse(subscriptionAccount, request);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
