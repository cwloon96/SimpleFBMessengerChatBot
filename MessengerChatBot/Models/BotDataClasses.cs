using System.Collections.Generic;

namespace MessengerChatBot.Models
{
    public class BotRequest
    {
        public string Object { get; set; }
        public List<BotEntry> Entry { get; set; }
    }

    public class BotEntry
    {
        public string Id { get; set; }
        public long Time { get; set; }
        public List<BotMessageReceivedRequest> Messaging { get; set; }
    }

    public class BotMessageReceivedRequest
    {
        public BotUser Sender { get; set; }
        public BotUser Recipient { get; set; }
        public string Timestamp { get; set; }
        public BotMessage Message { get; set; }
    }

    public class BotPostback
    {
        public string Payload { get; set; }
    }

    public class BotMessageResponse
    {
        public BotUser Recipient { get; set; }
        public MessageResponse Message { get; set; }
    }

    public class BotMessage
    {
        public string Mid { get; set; }
        public List<MessageAttachment> Attachments { get; set; }
        public long Seq { get; set; }
        public string Text { get; set; }
        public QuickReply Quick_reply { get; set; }
        public NLP Nlp { get; set; }
    }

    public class NLP
    {
        public Entity Entities { get; set; }
    }

    public class Entity
    {
        public List<Sentiment> Sentiment { get; set; }
        public List<Greeting> Greetings { get; set; }
        public List<Email> Email { get; set; }
    }

    public class BotUser
    {
        public string Id { get; set; }
        public string Profile_Pic { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
    }

    public class MessageResponse
    {
        public MessageAttachment Attachment { get; set; }
        public List<QuickReply> Quick_replies { get; set; }
        public string Text { get; set; }
    }

    public class QuickReply
    {
        public string Content_type { get; set; }
        public string Title { get; set; }
        public string Payload { get; set; }
    }

    public class ResponseButtons
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Payload { get; set; }

        public string Url { get; set; }
        public string Webview_height_ratio { get; set; }
    }

    public class MessageAttachment
    {
        public string Type { get; set; }
        public MessageAttachmentPayLoad Payload { get; set; }
    }

    public class MessageAttachmentPayLoad
    {
        public string Url { get; set; }
        public string Template_type { get; set; }
        public string Top_element_style { get; set; }
        public List<PayloadElements> Elements { get; set; }
        public List<ResponseButtons> Buttons { get; set; }
        public string Recipient_name { get; set; }
        public string Order_number { get; set; }
        public string Currency { get; set; }
        public string Payment_method { get; set; }
        public string Order_url { get; set; }
        public string Timestamp { get; set; }
        public Address Address { get; set; }
        public Summary Summary { get; set; }
    }

    public class PayloadElements
    {
        public string Title { get; set; }
        public string Image_url { get; set; }
        public string Subtitle { get; set; }
        public List<ResponseButtons> Buttons { get; set; }
        public string Item_url { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string Currency { get; set; }
    }

    public class Address
    {
       internal string Street_2;
        public string Street_1 { get; set; }
        public string City { get; set; }
        public string Postal_code { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }

    public class Summary
    {
        public decimal? Subtotal { get; set; }
        public decimal? Shipping_cost { get; set; }
        public decimal? Total_tax { get; set; }
        public decimal Total_cost { get; set; }
    }
}
