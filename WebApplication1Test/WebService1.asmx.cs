using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.Services;

namespace WebApplication1Test
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        DBOperation dbOperation = new DBOperation();
        

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(Description = "获取所有的信息")]
        public string[] selectAllCargoInfor()
        {
            return dbOperation.selectallinformation().ToArray();
        }

        [WebMethod(Description = "登录验证")]
        public Boolean DengLu(string userid,string userpass)
        {
            return dbOperation.DengLu(userid,userpass);
        }


        [WebMethod(Description = "登录信息")]
        public Boolean dengluxinxi(string userid, string userpass)
        {
            if(dbOperation.dengluxinxi(userid.Trim(), userpass.Trim()))
            {
                return true;
            }
            return false;
        }



        [WebMethod(Description = "注册信息")]
        public Boolean zhucexinxi(string userid, string username, string userpass)
        {
            if (dbOperation.zhucexinxi(userid, username, userpass))
            {
                return true;
            }
            return false;
        }


        [WebMethod(Description = "找回密码")]
        public string zhaohuimima(string userid)
        {
            return dbOperation.zhaohuimima(userid).ToString().Trim();
           
        }

        [WebMethod(Description = "获取朋友的信息")]
        public string chaxunfriends(string myuserid)
        {
            return dbOperation.chaxunfrieds(myuserid).ToString();
        }

        [WebMethod(Description = "查找附近的人")]
        public string fujinderen(string mynumber)
        {
            return dbOperation.fujinderen(mynumber).ToString();
        }


        [WebMethod(Description = "查询所有用户信息")]
        public string allinformation(string mynumber)
        {
            return dbOperation.allinformation(mynumber).ToString();
        }



        [WebMethod(Description = "查询我的关注")]
        public string gaobaiduixiang(string mynumber)
        {
            return dbOperation.gaobaiduixiang(mynumber).ToString();
        }


        [WebMethod(Description = "查询我的粉丝")]
        public string wodefensi(string mynumber)
        {
            return dbOperation.wodefensi(mynumber).ToString();
        }


        [WebMethod(Description = "查找朋友")]
        public string chazhaofriend(string friendid)
        {
            return dbOperation.chazhaofriend(friendid).ToString();
        }

        [WebMethod(Description = "查询消息")]
        public string chaxunxiaoxi(string userid)
        {
            return dbOperation.chaxunxiaoxi(userid).ToString();
        }


        [WebMethod(Description = "查询邀请")]
        public string chaxuninvite(string userid)
        {
            return dbOperation.chaxuninvite(userid).ToString();
        } 


        [WebMethod(Description = "查询评论")]
        public string chaxundiscuss(string userid)
        {
            return dbOperation.chaxundiscuss().ToString();
        }


        [WebMethod(Description = "查询消息朋友ID")]
        public string chaxunfriendid(string userid)
        {
            return dbOperation.chaxunfriendid(userid).ToString();
        }

        [WebMethod(Description = "查询消息朋友昵称")]
        public string chaxunfriendname(string userid)
        {
            return dbOperation.chaxunfriendname(userid).ToString();
        }


        [WebMethod(Description = "查询邀请朋友ID")]
        public string invitefriendid(string userid)
        {
            return dbOperation.invitefriendid(userid).ToString();
        }

        [WebMethod(Description = "查询邀请朋友昵称")]
        public string invitefriendname(string userid)
        {
            return dbOperation.invitefriendname(userid).ToString();
        }


        [WebMethod(Description = "添加朋友")]
        public Boolean addfriend(string myuserid, string frienduserid)
        {
            return dbOperation.addfriend(myuserid, frienduserid);
        }


        [WebMethod(Description = "邀请歌友")]
        public Boolean invitefriend(string myuserid, string frienduserid, string songname,string roomname)
        {
            return dbOperation.invitefriend(frienduserid, myuserid,songname,roomname);
        }


        [WebMethod(Description = "为他点歌")]
        public Boolean diansong(string myuserid, string frienduserid,string song,string author)
        {
            return dbOperation.diansong(myuserid, frienduserid,song,author);
        }

        [WebMethod(Description = "查询歌单")]
        public string chaxungedan(string userid)
        {
            return dbOperation.chaxungedan(userid).ToString();
        }

        [WebMethod(Description = "已唱歌曲")]
        public Boolean hadsing(string myuserid,string frienduserid)
        {
            return dbOperation.hadsing(myuserid,frienduserid);
        }

        [WebMethod(Description = "历史歌单")]
        public string historysong(string mynumber)
        {
            return dbOperation.historysong(mynumber).ToString();
        }


        [WebMethod(Description = "阅读消息")]
        public Boolean hadread(string myuserid)
        {
            return dbOperation.hadread(myuserid);
        }


        [WebMethod(Description = "拒绝邀请")]
        public Boolean refuseinvite(string myuserid)
        {
            return dbOperation.refuseinvite(myuserid);
        }



        [WebMethod(Description = "发送信息")]
        public Boolean addinformation(string myuserid, string frienduserid,string information)
        {
            return dbOperation.addinformation(frienduserid, myuserid, information);
        }



        [WebMethod(Description = "发表评论")]
        public Boolean fabiaopinglun(string myuserid, string frienduserid, string information)
        {
            return dbOperation.fabiaopinglun(myuserid, frienduserid, information);
        }





        [WebMethod(Description = "添加出拳")]
        public Boolean addchuquan(string myid, string friendid,string result1)
        {
            return dbOperation.addchuquan(myid, friendid, result1);
        }


        [WebMethod(Description = "查询战斗")]
        public string chaxunzhandou(string userid)
        {
            return dbOperation.chaxunzhandou(userid).ToString();
        }


        [WebMethod(Description = "对手出拳")]
        public Boolean addchuquan2(string myid,string result2)
        {
            return dbOperation.addchuquan2(myid,result2);
        }

        [WebMethod(Description = "比赛结果")]
        public string lastresult(string myid)
        {
            return dbOperation.lastresult(myid);
        }


    }
}
