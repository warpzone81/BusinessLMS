﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLMS.ActionFilters;
using BusinessLMS.Models;

namespace BusinessLMS.Controllers
{
    [BasicAuthentication]
    public class LocationsController : ApiController
    {
        private BusinessLMSContext db = new BusinessLMSContext();

        #region States

        public IEnumerable<State> GetStates()
        {
            return (from st in db.States where st.StateCode != String.Empty select st).AsEnumerable();
        }

        #endregion

        #region ZipCode

        public ZIPCode GetZIPCode(string id)
        {
            ZIPCode zipcode = db.ZIPCodes.Find(id);
            if (zipcode == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return zipcode;
        }
        
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}