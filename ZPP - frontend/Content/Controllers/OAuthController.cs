using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth.Messages;
using DotNetOpenAuth.OAuth.ChannelElements;
using System.Net.Http;
using System.Net.Http.Headers;
using DotNetOpenAuth.AspNet.Clients;
using System.Collections.Generic;
using ZPP___frontend.Content;

namespace ZPP___frontend.Content.Controllers
{
    public class OAuthController : Controller
    {
        private WebConsumer webConsumer;// = new WebConsumer(GetSPD(), new SimpleConsumerTokenManager("uvTtX63RWFaCf9pAxdtT", "5Jn3t9KNVMvSCeBtREX3nCvcKAnL55UrJKbcTvxD", new CookieOAuthTokenManager()));
        private void prepareConsumer() {
            if (webConsumer == null)
            {
                CookieOAuthTokenManager mgr = new CookieOAuthTokenManager();
                IConsumerTokenManager mgr2 = new SimpleConsumerTokenManager("uvTtX63RWFaCf9pAxdtT", "5Jn3t9KNVMvSCeBtREX3nCvcKAnL55UrJKbcTvxD", mgr);
                webConsumer = new WebConsumer(GetSPD(), mgr2);
            }
        }
        private static ServiceProviderDescription GetSPD()
        {
            ServiceProviderDescription mimuw = new ServiceProviderDescription();
            mimuw.RequestTokenEndpoint = new MessageReceivingEndpoint("https://usosapps.uw.edu.pl/services/oauth/request_token", HttpDeliveryMethods.PostRequest);
            mimuw.AccessTokenEndpoint = new MessageReceivingEndpoint("https://usosapps.uw.edu.pl/services/oauth/access_token", HttpDeliveryMethods.PostRequest);
            mimuw.UserAuthorizationEndpoint = new MessageReceivingEndpoint("https://usosapps.uw.edu.pl/services/oauth/authorize", HttpDeliveryMethods.PostRequest);
            mimuw.ProtocolVersion = ProtocolVersion.V10a;
            mimuw.TamperProtectionElements = new DotNetOpenAuth.Messaging.ITamperProtectionChannelBindingElement[] {
                                        new PlaintextSigningBindingElement(),
                                };
            return mimuw;
        }
        private int Process()
        {
            return 0;
        }

        // GET: OAuth
        public ActionResult Index()
        {
            prepareConsumer();
            IDictionary<string, string> data = new Dictionary<string, string>();
            data.Add("scopes", "grades");
            UserAuthorizationRequest rq = webConsumer.PrepareRequestUserAuthorization(new Uri("http://zpptestvm.cloudapp.net/OAuth/T"),data,null);
            webConsumer.Channel.Send(rq);
            return View();
        }
        public ActionResult T()
        {
            prepareConsumer();
            MessageReceivingEndpoint endp = new MessageReceivingEndpoint("https://usosapps.uw.edu.pl/services/grades/course_edition", HttpDeliveryMethods.GetRequest);
            MessageReceivingEndpoint endp2 = new MessageReceivingEndpoint("https://usosapps.uw.edu.pl/services/grades/course_edition", HttpDeliveryMethods.GetRequest);
            IDictionary<string,string> requestData = new Dictionary<string,string>();
            requestData.Add("term_id", "2012Z");
            requestData.Add("fields", "value_symbol");
            requestData.Add("course_id", "1000-211bPM");
            var requestData2 = new Dictionary<string,string>(requestData);
            requestData2["course_id"] = "1000-212bMD";
            requestData2["term_id"]="2012L";
            string accessToken = webConsumer.ProcessUserAuthorization().AccessToken;
            ViewData["token"] = accessToken;
            ///webConsumer.PrepareAuthorizedRequest(,)
            var request = webConsumer.PrepareAuthorizedRequest(endp, accessToken,requestData);
            var request2 = webConsumer.PrepareAuthorizedRequest(endp2, accessToken, requestData2);
            var response = request.GetResponse();
            var response2 = request2.GetResponse();
            ViewData["res"] = (new System.IO.StreamReader(response.GetResponseStream())).ReadToEnd();
            ViewData["res2"] = (new System.IO.StreamReader(response2.GetResponseStream())).ReadToEnd();
 
            return View();
        }
    }
}