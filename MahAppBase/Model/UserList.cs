using System.Collections.Generic;

namespace MahAppBase
{
	public class UserList
	{
        public class MemberList
        {
            public int MemberType { get; set; }
            public string DefaultImage { get; set; }
            public string DeptName { get; set; }
            public string DeptCode { get; set; }
            public int UserGroupID { get; set; }
            public int ParentGroupID { get; set; }
            public int ParentSuperGroupID { get; set; }
            public int SuperGroupID { get; set; }
            public string Mobile { get; set; }
            public string JobContent { get; set; }
            public bool IsRootDept { get; set; }
            public bool IsExternal { get; set; }
            public int LastActiveTime { get; set; }
            public OnlineStatus OnlineStatus { get; set; }
            public bool CanWebRTC { get; set; }
            public object SIPAccount { get; set; }
            public object MVPN { get; set; }
            public object OfficePhoneExt { get; set; }
            public string CellPhone { get; set; }
            public int UserNo { get; set; }
            public string UserName { get; set; }
            public string LoginName { get; set; }
        }

        public class OnlineStatus
        {
            public int ID { get; set; }
            public string Content { get; set; }
            public int ColorType { get; set; }
            public string StatusMessage { get; set; }
        }

        public class Root
        {
            public List<MemberList> MemberList { get; set; }
            public bool IsSuccess { get; set; }
            public string Description { get; set; }
        }
    }
}
