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
        private string[] terms = {
                                     "2013L",
                                     "2013Z",
                                     "2012L",
                                     "2012Z",
                                     "2011L",
                                     "2011Z",
                                     //"2010L",
                                     //"2010Z",
                                 };

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
            var requestDataTemplate = new Dictionary<string,string>{{"fields", "value_symbol"}};
            string accessToken = webConsumer.ProcessUserAuthorization().AccessToken;
            var res = new Dictionary<string, Mark>();
            foreach (KeyValuePair<string, string> x in Mark.nameMap) {
                foreach(string y in terms) {
                    if (res.ContainsKey(x.Key)) break;
                    var requestData = new Dictionary<string, string> (requestDataTemplate);
                    requestData.Add("term_id", y);
                    requestData.Add("course_id", x.Value);
                    var request = webConsumer.PrepareAuthorizedRequest(endp, accessToken,requestData);
                    System.Net.WebResponse response;
                    //try {
                        response = request.GetResponse();
                        
                    //} catch (Exception e){
                    //    continue;
                    //}

                    string json = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
                    var parsed = ResponseParser.ParseCourseEdition(json);
                    if (parsed == null)
                        continue;
                    res.Add(x.Key, new Mark(x.Key, Mark.Convert(parsed)));
                    
                }
                
            }
            var bld = new System.Text.StringBuilder("");
            foreach (KeyValuePair<string, Mark> x in res)
            {
                bld.Append(Mark.nameMap[x.Key] +": " +(float)x.Value.value/2.0+"\n");
            }
            ViewData["res"] = bld.ToString();

            return View();
        }
    }
}