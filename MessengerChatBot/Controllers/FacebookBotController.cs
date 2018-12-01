using MessengerChatBot.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace MessengerChatBot.Controllers
{
    public class FacebookBotController : Controller
    {
        public ActionResult Receive()
        {
            var query = Request.QueryString;

            if (query["hub.mode"] == "subscribe" && query["hub.verify_token"] == "ChatBotTesting")
            {
                var returnValue = query["hub.challenge"];

                return Json(int.Parse(returnValue), JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Receive(BotRequest data)
        {
            foreach (var entry in data.Entry)
            {
                foreach (var message in entry.Messaging)
                {
                    if (string.IsNullOrEmpty(message?.Message?.Text))
                        continue;

                    string msg = "";
                    string userText = message?.Message?.Text;

                    SendSenderAction(message.Sender.Id);

                    if (message.Message.Nlp != null)
                    {
                        if (isGreeting(message.Message.Nlp.Entities.Greetings))
                        {
                            var user = GetUserProfile(message.Sender.Id);
                            msg = GetGreetingMessage(user);
                        }
                    }

                    if(userText.ToLower() == "generic")
                    {
                        msg = GetGenericMessage();
                    }

                    if (userText.ToLower() == "quick")
                    {
                        msg = GetQuickReply();
                    }

                    if (string.IsNullOrEmpty(msg))
                    {
                        msg = $@"text: ""You said: {userText}""";
                    }

                    string requestBody = $@"{{recipient: {{  id: {message.Sender.Id}}},message: {{ {msg} }}}}";
                    SendMessage(requestBody);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private void SendMessage(string data)
        {
            string url = new RequestUriBuilder().GetReplyMessageUri();

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            var rs = request.GetRequestStream();
            using (var requestWriter = new StreamWriter(request.GetRequestStream()))
            {
                requestWriter.Write(data);
            }

            var response = (HttpWebResponse)request.GetResponse();
        }

        private BotUser GetUserProfile(string userId)
        {
            string url = new RequestUriBuilder().GetUserProfileUri(userId);
            var request = (HttpWebRequest)WebRequest.Create(url);

            var response = (HttpWebResponse)request.GetResponse();
            if (response == null)
                return new BotUser();

            BotUser user;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var reader = sr.ReadToEnd();
                user = JObject.Parse(reader).ToObject<BotUser>();
            }
            return user;
        }

        private bool isGreeting(List<Greeting> greetings)
        {
            if (greetings == null || greetings.Count == 0)
                return false;

            var greeting = greetings.FirstOrDefault();

            if(greeting.Value == "true" && greeting.Confidence > 0.8)
            {
                return true;
            }

            return false;
        }

        private string GetGreetingMessage(BotUser user)
        {
            string normalMessage = $@"text: ""Hi {user.First_Name} {user.Last_Name} ! Nice to meet you !""";

            return normalMessage;
        }

        private string GetGenericMessage()
        {
            string genericMsg = $@"
             attachment: {{
                type: ""template"",
                payload: {{
                    template_type: ""generic"",
                    elements: [{{
                        title: ""There Are Many Other Templates !"",
                        subtitle: ""Try it out!"",
                        image_url: ""https://cdn-images-1.medium.com/max/1200/0*oz2e-hQtsHOWzoB4."",
                        buttons: [{{
                            type: ""web_url"",
                            url: ""https://developers.facebook.com/docs/messenger-platform/send-messages/templates"",
                            title: ""Other Templates""
                        }}],
                    }},{{
                        title: ""Chatbots FAQ"",
                        subtitle: ""Asking the Deep Questions"",
                        image_url: ""https://tctechcrunch2011.files.wordpress.com/2016/04/facebook-chatbots.png?w=738"",
                        buttons: [{{
                            type: ""postback"",
                            title: ""What's the benefit?"",
                            payload: ""Chatbots make content interactive instead of static""
                        }},{{
                            type: ""postback"",
                            title: ""The Future"",
                            payload: ""Chatbots are fun! One day your BFF might be a Chatbot""
                        }}],
                    }}
                   ]
                }}
            }}";


            return genericMsg;
        }

        private string GetQuickReply()
        {
            string quickReply = $@"
                text: ""Pick a color !"",
                quick_replies: [{{
                    content_type: ""text"",
                    title: ""RED"",
                    image_url: ""https://images-na.ssl-images-amazon.com/images/I/41d-kZxsuIL._SY550_.jpg"",
                    payload: ""RED_COLOR""
                }},{{
                    content_type: ""text"",
                    title: ""GREEN"",
                    image_url: ""https://images-na.ssl-images-amazon.com/images/I/21GI6DWI2kL._SL500_AC_SS350_.jpg"",
                    payload: ""GREEN_COLOR""
                }}]";

            return quickReply;
        }

        private double GetNlpConfidence(NLP nlp, string name)
        {
            return 0;
        }

        private void SendSenderAction(string id)
        {
            var senderAction = $@" {{recipient: {{  id: {id}}}, sender_action: ""typing_on"" }}";

            SendMessage(senderAction);
        }

    }
}