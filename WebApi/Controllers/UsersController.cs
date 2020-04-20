using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public Users Login(string name,string pwd)
        {
            string sql = $"select * from Users where Uname='{name}' and Upwd='{pwd}'";
            return DBHelper<Users>.GetData(sql).FirstOrDefault();
        }
        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetUrl(string name)
        {
            string sql=$"select * from Users where Uname='{name}'";
            return DBHelper<Users>.GetData(sql).FirstOrDefault()?.Img;
        }

    }
}
