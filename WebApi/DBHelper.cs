using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace WebApi
{
    public static class DBHelper<T> where T: class, new()
    {
        private static string conStr = "Data Source=.;Initial Catalog=Blog_DB;Integrated Security=True";
        /// <summary>
        /// 执行增删改
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ExecterQuery(string str)
        {
            using (SqlConnection conn=new SqlConnection(conStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(str,conn);
                return cmd.ExecuteNonQuery();
            }
        }
        public static DataTable GetTable(string str)
        {
            using (SqlConnection conn=new SqlConnection(conStr))
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter dpt = new SqlDataAdapter(str,conn);
                dpt.Fill(dt);
                return dt;
            }
        }
        public static int Add(T t)
        {
            var type = t.GetType();
            var tableName = type.Name;
            var colName =string.Join(",",type.GetProperties().Where(p=>p.Name.ToLower()!="id").Select(p=>p.Name).ToList());
            var colValues = string.Join(",",type.GetProperties().Where(p=>p.Name.ToLower()!="id").Select(p=>"'"+p.GetValue(t)+"'"));
            string sql = $"insert into {tableName} ({colName}) values({colValues})";
            return ExecterQuery(sql);
        }
        public static int Update(T t,int Id)
        {
            var type = t.GetType();
            var tableName = type.Name;
            var colValues = string.Join(",",type.GetProperties().Where(p=>p.Name.ToLower()!="id").Select(p=>p.Name+"'"+p.GetValue(t)+"'").ToList());
            string sql = $"update {tableName} set {colValues} where Id={Id}";
            return ExecterQuery(sql);
        }
        public static int Delete(int Id)
        {
            var type = typeof(T);
            var tableName = type.Name;
            string sql = $"delete from {tableName} where Id={Id}";
            return ExecterQuery(sql);
        }
        public static List<T> GetAll()
        {
            var type = typeof(T);
            var tableName = type.Name;
            string sql = $"select * from {tableName}";
            var dt = GetTable(sql);
            return JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(dt));
        }
        public static List<T> GetData(string str)
        {
            var dt = GetTable(str);
            return JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(dt));
        }
    }
}