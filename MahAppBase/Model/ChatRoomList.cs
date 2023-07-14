using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahAppBase
{
    
    public class AppMessageLogList
    {
        public int MessageSN { get; set; }
        public int SN { get; set; }
        public int UserNo { get; set; }
        public string UserName { get; set; }
        public string AllUserName { get; set; }
        public string ShortAllUserName { get; set; }
        public string Mobile { get; set; }
        public string MsgContent { get; set; }
        public string Content2 { get; set; }
        public int InOut { get; set; }
        public int MsgType { get; set; }
        public int UnreadCount { get; set; }
        public string CreateTime { get; set; }
        public string CreateTimeZone { get; set; }
        public object CreateTimeUTC { get; set; }
        public string ChatID { get; set; }
        public int ChatType { get; set; }
        public int ChatStatus { get; set; }
        public string Icon { get; set; }
        public int ChannelType { get; set; }
        public bool IsManager { get; set; }
        public bool IsNotification { get; set; }
        public string TimeDesc { get; set; }
        public object MemberData { get; set; }
        public int EnableManagerFeature { get; set; }
        public bool ChatGroupIsLock { get; set; }
        public List<Top4UserIconList> Top4UserIconList { get; set; }
        public int ContactType { get; set; }
    }
    public class Bulletin
    {
        public int MessageSN { get; set; }
        public int SN { get; set; }
        public int UserNo { get; set; }
        public string UserName { get; set; }
        public string AllUserName { get; set; }
        public string ShortAllUserName { get; set; }
        public string Mobile { get; set; }
        public string MsgContent { get; set; }
        public object Content2 { get; set; }
        public int InOut { get; set; }
        public int MsgType { get; set; }
        public int UnreadCount { get; set; }
        public string CreateTime { get; set; }
        public string CreateTimeZone { get; set; }
        public long CreateTimeUTC { get; set; }
        public string ChatID { get; set; }
        public int ChatType { get; set; }
        public int ChatStatus { get; set; }
        public string Icon { get; set; }
        public int ChannelType { get; set; }
        public bool IsManager { get; set; }
        public bool IsNotification { get; set; }
        public string TimeDesc { get; set; }
        public string MemberData { get; set; }
        public int EnableManagerFeature { get; set; }
        public bool ChatGroupIsLock { get; set; }
        public List<object> Top4UserIconList { get; set; }
        public int ContactType { get; set; }
    }
    public class Root
    {
        public List<AppMessageLogList> AppMessageLogList { get; set; }
        public List<object> ChatLogPinTopRecordDataList { get; set; }
        public DateTime SearchTime { get; set; }
        public long SearchTimeTicks { get; set; }
        public int LatestSN { get; set; }
        public int LastSN { get; set; }
        public SuperHub superHub { get; set; }
        public Bulletin bulletin { get; set; }
        public bool HasMore { get; set; }
        public bool IsSessionTimeOut { get; set; }
        public bool IsSuccess { get; set; }
        public object Description { get; set; }
    }
    public class SuperHub
    {
        public int MessageSN { get; set; }
        public int SN { get; set; }
        public int UserNo { get; set; }
        public string UserName { get; set; }
        public string AllUserName { get; set; }
        public string ShortAllUserName { get; set; }
        public string Mobile { get; set; }
        public string MsgContent { get; set; }
        public object Content2 { get; set; }
        public int InOut { get; set; }
        public int MsgType { get; set; }
        public int UnreadCount { get; set; }
        public string CreateTime { get; set; }
        public string CreateTimeZone { get; set; }
        public long CreateTimeUTC { get; set; }
        public string ChatID { get; set; }
        public int ChatType { get; set; }
        public int ChatStatus { get; set; }
        public string Icon { get; set; }
        public int ChannelType { get; set; }
        public bool IsManager { get; set; }
        public bool IsNotification { get; set; }
        public string TimeDesc { get; set; }
        public string MemberData { get; set; }
        public int EnableManagerFeature { get; set; }
        public bool ChatGroupIsLock { get; set; }
        public List<object> Top4UserIconList { get; set; }
        public int ContactType { get; set; }
    }
    public class Top4UserIconList
    {
        public string Icon { get; set; }
        public int UserNo { get; set; }
    }
}
