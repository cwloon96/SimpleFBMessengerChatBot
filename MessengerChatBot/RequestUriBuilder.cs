using System;

namespace MessengerChatBot
{
    public class RequestUriBuilder
    {
        public const string BaseUrl = "https://graph.facebook.com/";
        public const string ReplyMessageUrl = "v2.6/me/messages?access_token={0}";
        public const string GetUserProfileUrl = "{0}?access_token={1}";
        public const string Token = "EAAfFIcAcJt8BAODsOHmnhOUGEEoGr3q7vZA6xbunIJNKNEOAXjqtxUtjWZBPJwDkSlorhiinD5mgDzVbU3TxYWYLErDd3DSIlZBcjlnaafnGIkGfVj0jVq4UIhBh2eQZBBbxYMesk9ZCbXbgxI5ZABVKCY5R1Cqu1tWv8BWggaODUHcGGKwjUN";

        public string GetReplyMessageUri()
        {
            var uriBuilder = new UriBuilder(BaseUrl + string.Format(ReplyMessageUrl, Token));
            uriBuilder.Port = -1;

            return uriBuilder.ToString();
        }

        public string GetUserProfileUri(string userId)
        {
            var uriBuilder = new UriBuilder(BaseUrl + string.Format(GetUserProfileUrl, userId, Token));
            uriBuilder.Port = -1;

            return uriBuilder.ToString();
        }
    }
}