﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{

    [Authorize]
    public class FollowupController : BaseWebController
    {
        //
        // GET: /Followup/

        public ActionResult Index()
        {
            return View();
        }

    }
}
