﻿using System.Web;
using System.Web.Mvc;

namespace Testing_Automate_Report
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
