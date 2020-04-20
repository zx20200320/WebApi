using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class StudentController : ApiController
    {

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int AddStu(Student stu)
        {
            return DBHelper<Student>.Add(stu);
        }
        [HttpGet]
        public PageData<Student> GetDate(string name, string tel, int pageIndex = 1, int pageSize = 3)
        {
            PageData<Student> dt = new PageData<Student>();
            dt.PageSize = pageSize;
            int state = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            string sql = "from Student where 1=1";
            if (!string.IsNullOrWhiteSpace(name))
            {
                sql += $"and Name like '%{name}%'";
            }
            if (!string.IsNullOrWhiteSpace(tel))
            {
                sql += $"and Tel like '%{tel}%'";
            }
            //sql设置好查询条件，获取总条数
            dt.TotalRecord = DBHelper<Student>.GetData("select *" + sql).Count();

            var pagesql = "select * from  (select Row_number() over(order by Id) as No,*" +
                sql +
                $")T where T.No between {state} and {end}";
            dt.Data = DBHelper<Student>.GetData(pagesql);
            return dt;
        }
    }
}
