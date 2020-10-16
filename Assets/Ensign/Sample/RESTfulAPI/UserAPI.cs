using Ensign.Model;
using Ensign.Unity.API;
using Ensign.Unity.Network.Http;
using System;
using System.Collections.Generic;

namespace Ensign.Unity.Sample
{
    public class UserAPI : BaseAPI
    {
        public void GetAppConfig(string deviceId, DelegateHttpCallback<ResponseAppConfig> callback)
        {
            Dictionary<string, object> postData = new Dictionary<string, object>
            {
                { "c", 6 },
                { "v", "1.0" },
                { "pf", "android" },
                { "did", deviceId }
            };
            Post<ResponseAppConfig>("api/login", postData, callback);
        }

        public void GetRegister(string userName, string password, DelegateHttpCallback<ResponseRegisterUser> callback)
        {
            Dictionary<string, object> postData = new Dictionary<string, object>
            {
                { "c", 1 },
                { "un", userName },
                { "pw", password }
            };
            Post<ResponseRegisterUser>("api/register", postData, callback);
        }

        internal void TestMethodPost(string url, Action<HttpResult> callback)
        {
            Dictionary<string, object> postData = new Dictionary<string, object>();
            Post<BaseEntity>(url, postData,
                new DelegateHttpCallback<BaseEntity>((result, enity) => callback?.Invoke(result)
            ));
        }
        internal void TestMethodGet(string url, Action<HttpResult> callback)
        {
            Dictionary<string, object> postData = new Dictionary<string, object>();
            Get<BaseEntity>(url, postData,
                new DelegateHttpCallback<BaseEntity>((result, enity) => callback?.Invoke(result)
            ));
        }
        internal void TestAllowAutoRedirect(string url, Action<HttpResult> callback)
        {
            Dictionary<string, object> postData = new Dictionary<string, object>();
            IHttpRequest request = Get<BaseEntity>(url, postData,
                new DelegateHttpCallback<BaseEntity>((result, enity) => callback?.Invoke(result)
            ));
            request.RedirectLimit = 0;
        }
    }
}
