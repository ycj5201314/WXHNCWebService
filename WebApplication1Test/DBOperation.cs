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
    public class DBOperation : IDisposable
    {
        public static SqlConnection sqlCon;  //用于连接数据库

        //将下面的引号之间的内容换成上面记录下的属性中的连接字符串
        private String ConServerStr = @"Data Source=localhost;Initial Catalog=WXHNC;Integrated Security=True;user=sa;pwd=wdsjnsjydjy";

        //默认构造函数
        public DBOperation()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection();
                sqlCon.ConnectionString = ConServerStr;
                sqlCon.Open();
            }
        }

        //关闭/销毁函数，相当于Close()
        public void Dispose()
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }



        public List<string> selectallinformation()
        {
            List<string> list = new List<string>();

            try
            {

                string sql = "select * from yonghudenglu";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    //list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return list;
        }


        public Boolean DengLu(string aa,string bb)
        {
            Boolean result = false;
            List<string> list = new List<string>();

            try
            {
                string sql =string.Format( "select * from yonghudenglu where userid='{0}' and userpass='{1}' ",aa.Trim(),bb.Trim());
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                int flag = Convert.ToInt32(cmd.ExecuteScalar());
                SqlDataReader reader = cmd.ExecuteReader();
                

                if (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                     //list.Add(reader[0].ToString());
                     //list.Add(reader[1].ToString());
                    // list.Add(reader[2].ToString());
                    result = true;

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }


        public Boolean dengluxinxi(string aa, string bb)
        {
            Boolean result = false;
            List<string> list = new List<string>();

            try
            {
                string sql = "select * from CusomerInfomation where YonghuID='" + aa.Trim()+ "' and Password='" + bb.Trim()+"'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = true;
                    //将结果集信息添加到返回向量中
                    //list.Add(reader[0].ToString());
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString());


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return result;
        }




        public Boolean zhucexinxi(string aa, string bb,string cc)
        {
            Boolean result = false;
            List<string> list = new List<string>();

            try
            {
                string sql = "insert into CusomerInfomation (YonghuID,Nickname,Password) values ('" + aa.Trim()+ "','"+bb.Trim()+"','"+cc.Trim()+"')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //SqlDataReader reader = cmd.ExecuteReader();

                if (cmd.ExecuteNonQuery()==1)
                {
                    result = true;
                    //将结果集信息添加到返回向量中

                }

                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return result;
        }





        public String chaxunfrieds(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select * from yonghudenglu where userid in (select frienduserid from friends where myuserid="+aa.Trim()+")";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                   // list.Add(reader[0].ToString()+":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString()+":");
                    data += reader[0].ToString() + ":" + reader[2].ToString() + ":";

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }






        public String zhaohuimima(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select * from CusomerInfomation where YonghuID='" + aa.Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   
                    data = reader[2].ToString();

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data.Trim();
        }




        public String fujinderen(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select * from CusomerInfomation where YonghuID not in (select OtherID from Caution where MyID='" + aa.Trim() + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    //list.Add(reader[0].ToString() + ":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    data+= reader[0].ToString() + ":" + reader[1].ToString() + ":";

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }




        //查询所有用户信息
        public String allinformation(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select * from CusomerInfomation where YonghuID not in ('" + aa.Trim() + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    //list.Add(reader[0].ToString() + ":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    data += reader[0].ToString() + ":" + reader[1].ToString() + ":";

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }



        //查找关注的歌友
        public String gaobaiduixiang(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select * from CusomerInfomation where YonghuID in (select OtherID from Caution where MyID='" + aa.Trim() + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    //list.Add(reader[0].ToString() + ":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    data += reader[0].ToString() + ":" + reader[1].ToString() + ":";

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }




        //查找关注我的人
        public String wodefensi(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select * from CusomerInfomation where YonghuID in (select MyID from Caution where OtherID='" + aa.Trim() + "')"; 
                 SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    //list.Add(reader[0].ToString() + ":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    data += reader[0].ToString() + ":" + reader[1].ToString() + ":";

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }



        public String chazhaofriend(string aa)
        {
            List<string> list = new List<string>();
            String data=null;

            try
            {
                string sql = "select * from CusomerInfomation where YonghuID ='" + aa.Trim()+"'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    // list.Add(reader[0].ToString()+":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    data= reader[0].ToString() + ":" + reader[1].ToString() + ":";


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }




        //查询消息
        public String chaxunxiaoxi(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select top(1) * from Chat where YonghuID1 ='" + aa.Trim()+ "'  and State='1'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    // list.Add(reader[0].ToString()+":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    data ="true:"+reader[3].ToString().Trim();


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }





        //查询消息
        public String chaxuninvite(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select top(1) * from InviteInfomation where MyID ='" + aa.Trim() + "'  and State='1'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    // list.Add(reader[0].ToString()+":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    data = "true:" + reader[2].ToString().Trim();


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }






        //查询评论内容
        public String chaxundiscuss()
        {
          
            String data = null;

            try
            {
                string sql = "select * from CompseDiscuss order by CompositionID";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    // list.Add(reader[0].ToString()+":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");


                    data += reader[0].ToString().Trim() +"t"+ reader[1].ToString().Trim() + "t" + reader[2].ToString().Trim() + "t";


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }






        //查询消息中的朋友ID
        public String chaxunfriendid(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select top(1) * from Chat where YonghuID1 ='" + aa.Trim() + "'  and state='1'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    // list.Add(reader[0].ToString()+":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    data = "true:" + reader[1].ToString().Trim() ;


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }



        //查询邀请中的朋友ID
        public String invitefriendid(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select top(1) * from InviteInfomation where MyID ='" + aa.Trim() + "'  and state='1'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    // list.Add(reader[0].ToString()+":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    data = "true:" + reader[1].ToString().Trim();


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }






        //查询消息中的朋友昵称
        public String chaxunfriendname(string aa)
        {
           
            String data = null,tt=null;

            try
            {
                string sql = "select top(1) * from Chat where YonghuID1 ='" + aa.Trim() + "'  and state='1'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    // list.Add(reader[0].ToString()+":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    tt = reader[1].ToString().Trim();

                    
                    
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            if (tt != null)
            {
                try
                {
                    string ssql = "select * from CusomerInfomation where YonghuID='" + tt + "'";
                    SqlCommand cmds = new SqlCommand(ssql, sqlCon);
                    SqlDataReader readers = cmds.ExecuteReader();

                    if (readers.Read())
                    {
                        data = "true:" + readers[1].ToString().Trim();
                    }
                    readers.Close();
                    cmds.Dispose();
                }
                catch (Exception e)
                {

                }
            }
            return data;
        }




        //查询邀请中的朋友昵称
        public String invitefriendname(string aa)
        {

            String data = null, tt = null;

            try
            {
                //string sql = "select top(1) * from Chat where YonghuID1 ='" + aa.Trim() + "'  and state='1'";
                string sql = "select top(1) * from InviteInfomation where MyID ='" + aa.Trim() + "'  and state='1'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    // list.Add(reader[0].ToString()+":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    tt = reader[1].ToString().Trim();



                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            if (tt != null)
            {
                try
                {
                    string ssql = "select * from CusomerInfomation where YonghuID='" + tt + "'";
                    SqlCommand cmds = new SqlCommand(ssql, sqlCon);
                    SqlDataReader readers = cmds.ExecuteReader();

                    if (readers.Read())
                    {
                        data = "true:" + readers[1].ToString().Trim();
                    }
                    readers.Close();
                    cmds.Dispose();
                }
                catch (Exception e)
                {

                }
            }
            return data;
        }







        //添加朋友
        public Boolean addfriend(string aa, string bb)
        {
            Boolean result = false;
            List<string> list = new List<string>();

            try
            {
                string sql = "insert into Caution (MyID,OtherID) values ('" + aa.Trim() + "','" + bb.Trim() + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //SqlDataReader reader = cmd.ExecuteReader();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    result = true;
                    //将结果集信息添加到返回向量中

                }

                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return result;
        }


        //为他点歌

        public Boolean diansong(string aa, string bb,string cc,string dd)
        {
            Boolean result = false;
            //List<string> list = new List<string>();

            try
            {
                string sql = "insert into singsong (myuserid,frienduserid,song,author,flag) values ('" + aa.Trim() + "','" + bb.Trim() + "','" + cc.Trim() + "','" + dd.Trim() + "',1)";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //SqlDataReader reader = cmd.ExecuteReader();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    result = true;

                }

                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return result;
        }


        //查询歌单
        public String chaxungedan(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select top(1) * from singsong where frienduserid='" + aa.Trim() + "' and flag=1";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    // list.Add(reader[0].ToString()+":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    data = "true:" + reader[0].ToString() +":"+ reader[2].ToString() + ":" + reader[3].ToString();


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }



        //演唱歌单
        public Boolean hadsing(string aa,string bb)
        {
            Boolean result = false;
            List<string> list = new List<string>();

            try
            {
                string sql = "update top(1) singsong set flag=0 where frienduserid ='" + aa.Trim() + "' and flag=1";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //SqlDataReader reader = cmd.ExecuteReader();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    result = true;
                    //将结果集信息添加到返回向量中

                }

                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return result;
        }



        //历史歌单
        public String historysong(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select * from singsong where flag=0 and frienduserid=" + aa.Trim() + ")";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    //list.Add(reader[0].ToString() + ":");
                    //list.Add(reader[1].ToString());
                    //list.Add(reader[2].ToString() + ":");
                    data += reader[0].ToString() + ":" + reader[2].ToString() + ":";

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }


        //阅读消息
        public Boolean hadread(string aa)
        {
            Boolean result = false;
            List<string> list = new List<string>();

            try
            {
                string sql = "update top(1) Chat set State='0' where YonghuID1='" + aa.Trim()+ "' and State='1'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //SqlDataReader reader = cmd.ExecuteReader();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    result = true;
                    //将结果集信息添加到返回向量中

                }

                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return result;
        }




        //拒绝邀请
        public Boolean refuseinvite(string aa)
        {
            Boolean result = false;
            List<string> list = new List<string>();

            try
            {
                string sql = "update top(1) InviteInfomation set State='0' where MyID='" + aa.Trim() + "' and State='1'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //SqlDataReader reader = cmd.ExecuteReader();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    result = true;
                    //将结果集信息添加到返回向量中

                }

                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return result;
        }





        //发表评论
        public Boolean fabiaopinglun(string aa, string bb,string cc)
        {
            Boolean result = false;
            List<string> list = new List<string>();

            try
            {
                string sql = "insert into CompseDiscuss (CompositionID,YonghuID,Disscuss) values ('" + aa.Trim() + "','" + bb.Trim() + "','" + cc.Trim() + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //SqlDataReader reader = cmd.ExecuteReader();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    result = true;
                    //将结果集信息添加到返回向量中

                }

                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return result;
        }





        //邀请歌友
        public Boolean invitefriend(string aa, string bb, string cc, string dd)
        {
            Boolean result = false;
            List<string> list = new List<string>();

            try
            {
                string sql = "insert into InviteInfomation (MyID,InviteID,SongName,RoomName,State) values ('" + aa.Trim() + "','" + bb.Trim() + "','" + cc.Trim() + "','" + dd.Trim() + "','1')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //SqlDataReader reader = cmd.ExecuteReader();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    result = true;
                    //将结果集信息添加到返回向量中

                }

                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return result;
        }




        //发送消息
        public Boolean addinformation(string aa, string bb, string cc)
        {
            Boolean result = false;
            List<string> list = new List<string>();

            try
            {
                string sql = "insert into Chat (YonghuID1,YonghuID2,State,Information) values ('" + aa.Trim() + "','" + bb.Trim() + "','1','" + cc.Trim() + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //SqlDataReader reader = cmd.ExecuteReader();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    result = true;
                    //将结果集信息添加到返回向量中

                }

                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return result;
        }






        //对手1出拳统计
        public Boolean addchuquan(string aa, string bb,string cc)
        {
            Boolean result = false;
            List<string> list = new List<string>();

            try
            {
                string sql = "insert into result (yonghu1,yonghu2,type,result1) values ('" + aa.Trim() + "','" + bb.Trim() + "',1," +Convert.ToInt32( cc.Trim()) + ")";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //SqlDataReader reader = cmd.ExecuteReader();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    result = true;
                    //将结果集信息添加到返回向量中

                }

                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return result;
        }



        //对手2出拳统计
        public Boolean addchuquan2(string bb,string cc)
        {
            string[] tem = new string[5];
            Boolean result = false;
            int tag = 0,a=100,b=100,r1=100,r2=100;
            List<string> list = new List<string>();

            try
            {
                string sql = "update top(1) result set result2="+Convert.ToInt32(cc)+ " where type =1 and yonghu2 = '" + bb.Trim() + "' and yonghu1 is not null";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //SqlDataReader reader = cmd.ExecuteReader();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    result = true;tag = 1;
                    //将结果集信息添加到返回向量中

                }

                cmd.Dispose();



                if (tag == 1)
                {
                    string chaxun = "select top(1) * from result where type=1 and yonghu2 = '" + bb.Trim() + "' and yonghu1 is not null";
                    SqlCommand execut = new SqlCommand(chaxun, sqlCon);
                    SqlDataReader reader = execut.ExecuteReader();

                    if (reader.Read())
                    {

                        tem[0] = reader[0].ToString().Trim();
                        tem[1] = reader[1].ToString().Trim();
                        tem[2] = reader[2].ToString().Trim();
                        tem[3] = reader[3].ToString().Trim();
                        tem[4] = reader[4].ToString().Trim();


                        a = Convert.ToInt32(tem[3]);
                        b = Convert.ToInt32(tem[4]);

                    }
                    reader.Close();
                    cmd.Dispose();


                    if (a == 1 && b == 1 || a == 2 && b == 2 || a == 3 && b == 3)
                    {
                        r1 = 3; r2 = 3;
                    }
                    else if (a == 1 && b == 2 || a == 2 && b == 3 || a == 3 && b == 1)
                    {
                        r1 = 1; r2 = 2;
                    }
                    else
                    {
                        r1 = 2; r2 = 1;
                    }

                    if (r1 != 100 &&r2!=100 && tem[0].Trim() != null && tem[1].Trim() != null)
                    {
                        string sql1 = "update top(1) result set type=0 where type=1 and yonghu1 ='" + tem[0].Trim() + "' and yonghu2='" + tem[1].Trim() + "'";
                        SqlCommand cmd1 = new SqlCommand(sql1, sqlCon);
                  
                        if (cmd1.ExecuteNonQuery() == 1)
                        {
                            result = true; cmd1.Dispose();

                        }
                        else
                        {
                            result = false;
                        }



                        sql = "insert into resultlook (yonghu1,yonghu2,type,result) values ('" + tem[0].Trim() + "','" + tem[1].Trim() + "',1," +r1 + ")";
                         cmd = new SqlCommand(sql, sqlCon);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            result = true; cmd.Dispose();

                        }
                        else
                        {
                            result = false;
                        }

                        sql = "insert into resultlook (yonghu1,yonghu2,type,result) values ('" + tem[1].Trim() + "','" + tem[0].Trim() + "',1," + r2 + ")";
                        cmd = new SqlCommand(sql, sqlCon);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            result = true; cmd.Dispose();

                        }
                        else
                        {
                            result = false;
                        };


                    }

                }










            }
            catch (Exception ex)
            {
                //result = false;
            }
            return result;
        }





        //查询战斗
        public String chaxunzhandou(string aa)
        {
            List<string> list = new List<string>();
            String data = null;

            try
            {
                string sql = "select top(1) * from result where type=1 and yonghu2 = '" + aa.Trim() + "' and yonghu1 is not null";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //将结果集信息添加到返回向量中  
                    data = "true:" + reader[0].ToString();


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return data;
        }












        //最终结果
        public String lastresult(string aa)
        {
            Boolean result = false;
            List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            string[] tem = new string[8];
            int a = 100, b = 100,r1=100,r2=100;
            string data = null;

            try
            {
                string sql = "select top(1) * from resultlook where type=1 and yonghu1='"+aa.Trim()+"'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                 
                    tem[0]= reader[0].ToString().Trim();
                    tem[1]= reader[1].ToString().Trim();
                    tem[2]= reader[2].ToString().Trim();
                    tem[3]= reader[3].ToString().Trim();
                   
                    a = Convert.ToInt32(tem[3]);
                    if (a == 1)
                    {
                        data = "true:与" + tem[1].Trim()+"的比赛，棋差一招！";
                    }
                    else if (a == 2)
                    {
                        data = "true:与" + tem[1].Trim() + "的比赛，大获全胜！";
                    }
                    else if (a == 3)
                    {
                        data = "true:与" + tem[1].Trim() + "的比赛，旗鼓相当！";
                    }
                    else
                    {
                        data = "false:";
                    }

                }
                else
                {
                    data = "false:";
                }
                reader.Close();
                cmd.Dispose();

                if (a != 100 && tem[0].Trim() != null && tem[1].Trim() != null)
                {
                    string sql1 = "update top(1) resultlook set type=0 where type=1 and  yonghu1 ='" + tem[0].Trim() + "' and yonghu2='" + tem[1].Trim()+"'";
                    SqlCommand cmd1 = new SqlCommand(sql1, sqlCon);
                    if (cmd1.ExecuteNonQuery() != 1)
                    {
                        data = "false";
                    }
                    cmd1.Dispose();
                }
                

            }
            catch (Exception)
            {

            }
            finally
            {

            }
            return data;
        }









    }
}