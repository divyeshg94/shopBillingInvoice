﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceAPI.Controllers
{
    public class HomeController : Controller
    {
        public void Index()
        {
            ViewBag.Title = "Home Page";
        }
    }
}
