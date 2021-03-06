﻿using BusinessLMS.ActionFilters;
using BusinessLMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessLMS.Controllers
{
	[BasicAuthentication]
	public class DreamsController : ApiController
	{
		private BusinessLMSContext db = new BusinessLMSContext();

		public IEnumerable<Dream> GetDreams()
		{
			return db.Dreams.AsEnumerable();
		}

		public Dream GetDream(int id)
		{
			Dream dream = db.Dreams.Find(id);
			if (dream == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}

			return dream;
		}

		public IEnumerable<Dream> GetDreamsUserLevel(string id, int level)
		{
			return (from dream in db.Dreams where dream.IBONum == id && dream.dreamLevel == level orderby dream.timeframeId select dream);
		}

		public IEnumerable<Dream> GetDreamsUser(string id)
		{
			return (from dream in db.Dreams where dream.IBONum == id orderby dream.timeframeId select dream);
		}

		public HttpResponseMessage PutDream(int id, Dream dream)
		{
			if (ModelState.IsValid && id == dream.dreamId)
			{
				db.Entry(dream).State = EntityState.Modified;

				try
				{
					db.SaveChanges();
				}
				catch (DbUpdateConcurrencyException)
				{
					return Request.CreateResponse(HttpStatusCode.NotFound);
				}

				return Request.CreateResponse(HttpStatusCode.OK);
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		public HttpResponseMessage PostDream(Dream dream)
		{
			if (ModelState.IsValid)
			{
				db.Dreams.Add(dream);
				db.SaveChanges();

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, dream);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = dream.dreamId }));
				return response;
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		public HttpResponseMessage DeleteDream(int id)
		{
			Dream dream = db.Dreams.Find(id);
			if (dream == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			db.Dreams.Remove(dream);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, dream);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}