using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class PageData<T>
    {
        public List<T> Data { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalRecord { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotlePage
        {
            get { return TotalRecord / PageSize + (TotalRecord % PageSize == 0 ? 0 : 1); }
        }
    }
}