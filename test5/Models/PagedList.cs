using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test5.Models;

namespace test5.Models
{
    public class PagedList
    {
        public IList<Customers> Content { get; set; }

        public Int32 CurrentPage { get; set; }
        public Int32 PageSize { get; set; }
        public int TotalRecords { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalRecords / PageSize); }
        }
    }
}