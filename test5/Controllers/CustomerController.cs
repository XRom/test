using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using test5.Models;

namespace test5.Controllers
{
    public class CustomerController : ApiController
    {
        private CustEntities db = new CustEntities();

        [HttpGet]
        public PagedList GetCustomers(string searchtext, int page = 1, int pageSize = 10, string sortBy = "Id", string sortDirection = "asc")
        {
            var pagedRecord = new PagedList();
            
            pagedRecord.Content = db.Customers
                        .Where(x => searchtext == null ||//поиск по текстовым полям
                                ((x.Country.Contains(searchtext)) ||
                                (x.City.Contains(searchtext)) ||
                                (x.Street.Contains(searchtext)) 
                            ))
                        .OrderBy(sortBy + " " + sortDirection)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            pagedRecord.TotalRecords = db.Customers
                        .Where(x => searchtext == null ||
                                ((x.Country.Contains(searchtext)) ||
                                (x.City.Contains(searchtext)) ||
                                (x.Street.Contains(searchtext))
                            )).Count();

            pagedRecord.CurrentPage = page;
            pagedRecord.PageSize = pageSize;

            return pagedRecord;
        }



        // GET api/Customer/5
        public Customers GetCustomer(string id)
        {
            Customers customer = db.Customers.Find(id);
            if (customer == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return customer;
        }

        // PUT api/Customer/5
        public HttpResponseMessage PutCustomer(string id, Customers customer)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != Convert.ToString(customer.Id))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Customer
        public HttpResponseMessage PostCustomer(Customers customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, customer);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = customer.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Customer/5
        public HttpResponseMessage DeleteCustomer(string id)
        {
            Customers customer = db.Customers.Find(id);
            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Customers.Remove(customer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
