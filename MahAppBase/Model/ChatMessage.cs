using System;
using System.Collections.Generic;

namespace MahAppBase
{
    public class ChatMessageList
    {
        public int SenderID { get; set; }
        public int ReceipientID { get; set; }
        public int MessageSN { get; set; }
        public string ChatID { get; set; }
        public int ChannelType { get; set; }
        public int MsgType { get; set; }
        public string MsgContent { get; set; }
        public string Content2 { get; set; }
        public DateTime CreateTime { get; set; }
        public object CreateTimeUTCValue { get; set; }
        public object Location { get; set; }
        public bool InOut { get; set; }
        public bool HasRead { get; set; }
        public string AtUsers { get; set; }
        public bool IsAbleUnsend { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Status { get; set; }
        public int SourceType { get; set; }
        public int SourceID { get; set; }
        public int ContactType { get; set; }
        public object ReplyBatchID { get; set; }
        public bool IsNearlineData { get; set; }
        public int MsgFrom { get; set; }
        public string FlexFooter { get; set; }
        public List<object> UrlPreviewList { get; set; }
        public string BatchID { get; set; }
        public bool IsUnsend { get; set; }
        public int ReadCount { get; set; }
        public int ReceiverTotalCount { get; set; }
    }

    public class ChatMessageRoot
    {
        public DateTime LastServerDate { get; set; }
        public List<ChatMessageList> ChatMessageList { get; set; }
        public bool IsSuccess { get; set; }
        public string Description { get; set; }
    }
}
