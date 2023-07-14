using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MahAppBase.Command;
using MahAppBase.CustomerUserControl;
using Newtonsoft.Json;
using Notifications.Wpf;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;

namespace MahAppBase.ViewModel
{
    public class MainComponent:ViewModelBase
    {
        #region Declarations
        private ObservableCollection<ChatRoomItem> _ChatList = new ObservableCollection<ChatRoomItem>();
        ucDonate donate = new ucDonate();
        private ChatRoomItem _CurrentItem = new ChatRoomItem();
        private string _Account;
        private string _Password;
        private string _MessageListAPI = "https://emp.cathayholdings.com/EIM/Chat/ChatMainHandler.ashx";
        private string _MessageAPI = "https://emp.cathayholdings.com/EIM/Chat/ChatMainHandler.ashx";
        private string _UserAPI = "https://emp.cathayholdings.com/EIM/Contact/SelectContactApi.ashx";
        private int _LoadCount = 10000;
        private bool _IsLogin = true;
        #endregion

        #region Property

        public bool IsLogin
		{
			get
			{
				return _IsLogin;
			}
			set
			{
				_IsLogin = value;
                OnPropertyChanged();
			}
		}
        public int LoadCount
		{
			get
			{
				return _LoadCount;
			}
			set
			{
				_LoadCount = value;
                OnPropertyChanged();
			}
		}
        public Dictionary<string, string> Cookies { get; set; } = new Dictionary<string, string>();
        public string MessageListAPI
		{
			get
			{
				return _MessageListAPI;
			}
			set
			{
				_MessageListAPI = value;
                OnPropertyChanged();
			}
		}

        public string MessageAPI
		{
			get
			{
				return _MessageAPI;
			}
			set
			{
				_MessageAPI = value;
                OnPropertyChanged();
            }
        }
		public string UserAPI
		{
			get
			{
				return _UserAPI;
			}
			set
			{
				_UserAPI = value;
                OnPropertyChanged();

            }
        }
        public NoParameterCommand ButtonDonateClick { get; set; }
        public NoParameterCommand LoginCommand { get; set; }

        public NoParameterCommand ExportCommand { get; set; }
        public bool DonateIsOpen { get; set; }

        public ObservableCollection<ChatRoomItem> ChatList
        {
            get
            {
                return _ChatList;
            }
            set
            {
                _ChatList = value;
                OnPropertyChanged();
            }
        }

        public string Account
		{
			get
			{
				return _Account;
			}
			set
			{
				_Account = value;
                OnPropertyChanged();
			}
		}

		public string Password
		{
			get
			{
				return _Password;
			}
			set
			{
				_Password = value;
                OnPropertyChanged();
            }
        }
        public ChatRoomItem CurrentItem 
        {
			get 
            {
                return _CurrentItem;
            }
			set 
            {
                _CurrentItem = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region MemberFunction
        public MainComponent()
        {
            ButtonDonateClick = new NoParameterCommand(ButtonDonateClickAction);
            LoginCommand = new NoParameterCommand(LoginCommandAction);
            ExportCommand = new NoParameterCommand(ExportCommandAction);
        }
		private void ExportCommandAction()
		{
            ((App)System.Windows.Application.Current).ShowMessage("訊息", $"開始儲存對話紀錄...", NotificationType.Information);
            var client = new RestClient(MessageAPI);
            var request = new RestRequest();
            foreach (var item in Cookies)
            {
                request.AddCookie(item.Key, item.Value);
            }

            if(CurrentItem.ChatID.Length>12)
                //群聊
                request.AddParameter("ChannelType", "1", ParameterType.QueryString);
            else
                //個人
                request.AddParameter("ChannelType", "0", ParameterType.QueryString);
            request.AddParameter("action", "getInitialMessageList", ParameterType.QueryString);
			
			request.AddParameter("Mobile", CurrentItem.ID, ParameterType.QueryString);
            request.AddParameter("ChatID", CurrentItem.ChatID, ParameterType.QueryString);
            request.AddParameter("LoadCount", LoadCount, ParameterType.QueryString);

			var _response = client.Post(request);
			if (_response.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                ChatMessageRoot myDeserializedClass = JsonConvert.DeserializeObject<ChatMessageRoot>(_response.Content);
                List<string> chatContent = new List<string>();
                List<int> senderList = new List<int>();
                foreach(var item in myDeserializedClass.ChatMessageList) 
                {
                    if(!senderList.Contains(item.SenderID))
                        senderList.Add(item.SenderID);
                }
                var userName = getUserName(senderList);
                foreach (var item in myDeserializedClass.ChatMessageList)
                {
                    chatContent.Add($"[{item.CreateTime}]{userName[item.SenderID]} : {item.MsgContent}");
                    
                }
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (!File.Exists($"{path}\\{CurrentItem.UserName}.txt"))
                {
                    using (StreamWriter sw = File.CreateText($"{path}\\{CurrentItem.UserName}.txt"))
                    {
                        foreach(var item in chatContent) 
                        {
                            sw.Write(item + "\n");
                        }
                    }
                    ((App)System.Windows.Application.Current).ShowMessage("訊息", $"對話紀錄儲存成功 {path}\\{CurrentItem.UserName}.txt", NotificationType.Success);
                }
                else
                {
                    ((App)System.Windows.Application.Current).ShowMessage("訊息", $"{path}\\{CurrentItem.UserName}.txt 檔案已存在", NotificationType.Error);
                }
            }
        }

		private Dictionary<int, string> getUserName(List<int> senderList)
		{
            Dictionary<int, string> result = new Dictionary<int, string>();
            var client = new RestClient(UserAPI);
            var request = new RestRequest();
            string senderString = "[";
            foreach(var item in senderList) 
            {
                senderString += $"{item},";
            }
            senderString += "]";
            foreach (var item in Cookies)
            {
                request.AddCookie(item.Key, item.Value);
            }
            request.AddParameter("action", "getContactByUserNoList", ParameterType.QueryString);
            request.AddParameter("userNoArrayJson", senderString, ParameterType.QueryString);
            var _response = client.Post(request);
            if(_response.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                UserList.Root myDeserializedClass = JsonConvert.DeserializeObject<UserList.Root>(_response.Content);
                foreach(var item in myDeserializedClass.MemberList) 
                {
                    if(!result.ContainsKey(item.UserNo))
                        result.Add(item.UserNo, item.UserName);
                }
            }
			else 
            {
                ((App)System.Windows.Application.Current).ShowMessage("訊息", $"取得使用者資訊失敗", NotificationType.Error);
            }
            return result;

        }

		private void LoginCommandAction()
		{
            IsLogin = false;
            Task.Run(() =>
            {
				try 
                {
                    var chromeOptions = new ChromeOptions();
                    var Cookie = new List<string>();
                    var Driver = new ChromeDriver(chromeOptions);
                    Driver.Navigate().GoToUrl("https://w3.cathaylife.com.tw/eai/ZPWeb/login.jsp");
                    var idInput = Driver.FindElement(By.Id("UID"));
                    idInput.SendKeys(Account);
                    var pwInput = Driver.FindElement(By.Id("KEY"));
                    pwInput.SendKeys(Password);
                    Driver.FindElement(By.LinkText("登入")).Click();
                    Thread.Sleep(5000);
                    Driver.Navigate().GoToUrl("https://emp.cathayholdings.com/EIM/User/WallMain.aspx");
                    Driver.Navigate().GoToUrl("https://emp.cathayholdings.com/EIM/Messenger/MessengerMain.aspx");

                    Cookies.Add(Driver.Manage().Cookies.GetCookieNamed("TS0112e979").Name, Driver.Manage().Cookies.GetCookieNamed("TS0112e979").Value);
                    Cookies.Add(Driver.Manage().Cookies.GetCookieNamed("TS82ddb215027").Name, Driver.Manage().Cookies.GetCookieNamed("TS82ddb215027").Value);
                    Cookies.Add(Driver.Manage().Cookies.GetCookieNamed("BIGipServerpool_P_CFH_EMP_443").Name, Driver.Manage().Cookies.GetCookieNamed("BIGipServerpool_P_CFH_EMP_443").Value);
                    //Cookies.Add(Driver.Manage().Cookies.GetCookieNamed("MRSSID").Name, Driver.Manage().Cookies.GetCookieNamed("MRSSID").Value);
                    Cookies.Add(Driver.Manage().Cookies.GetCookieNamed("TSSID").Name, Driver.Manage().Cookies.GetCookieNamed("TSSID").Value);
                    Cookies.Add(Driver.Manage().Cookies.GetCookieNamed("LR").Name, Driver.Manage().Cookies.GetCookieNamed("LR").Value);

                    var client = new RestClient(MessageListAPI);
                    var request = new RestRequest();
                    foreach (var item in Cookies)
                    {
                        request.AddCookie(item.Key, item.Value);
                    }
                    request.AddParameter("action", "loadPersonalLogListForMessenger", ParameterType.QueryString);
                    request.AddParameter("loadCount", "100", ParameterType.QueryString);
                    request.AddParameter("compareSN", "0", ParameterType.QueryString);
                    request.AddParameter("searchKey", "", ParameterType.QueryString);

                    var _response = client.Post(request);
                    if (_response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            ChatList.Clear();
                        });
                        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(_response.Content);
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            foreach (var item in myDeserializedClass.AppMessageLogList)
                            {
                                ChatList.Add(new ChatRoomItem()
                                {
                                    UserName = item.UserName,
                                    ID = item.Mobile,
                                    ChatID = item.ChatID
                                });
                            }
                        });
                        
                        IsLogin = false;
                    }
                    else
                    {
                        IsLogin = true;
                    }
                }
                catch(Exception ex) 
                {
                    ((App)System.Windows.Application.Current).ShowMessage("訊息", $"登入時發生例外 {ex.Message}\r\n{ex.StackTrace}", NotificationType.Error);

                }
            });
            
            

        }
        public void ButtonDonateClickAction()
        {
            if (!DonateIsOpen)
            {
                donate = new ucDonate
                {
                    Topmost = true
                };
                donate.Closed += Donate_Closed;
                DonateIsOpen = true;
                donate.Show();
            }
            else
            {

            }
        }
        private void Donate_Closed(object sender, EventArgs e)
        {
            DonateIsOpen = false;
        }
        #endregion
    }
}