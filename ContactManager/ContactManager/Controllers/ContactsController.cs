using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactManager.Services;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class ContactsController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get(string status = "All")
        {
            ContactRepository contactRepository = new ContactRepository();
            switch (status.ToLower())
            {
                case "all":
                    return Request.CreateResponse(HttpStatusCode.OK, contactRepository.GetAllContacts.ToList());
                case "active":
                    return Request.CreateResponse(HttpStatusCode.OK, contactRepository.GetAllContacts.Where(e => e.Status.ToLower() == "active").ToList());
                case "inactive":
                    return Request.CreateResponse(HttpStatusCode.OK, contactRepository.GetAllContacts.Where(e => e.Status.ToLower() == "inactive").ToList());
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            "Value for the status must be All, Active or Inactive." + status + " is invalid.");
            }
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            ContactRepository contactRepository = new ContactRepository();
            var contact = contactRepository.GetAllContacts.FirstOrDefault(c => c.ID == id);

            if (contact != null)
                return Request.CreateResponse(HttpStatusCode.OK, contact);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with id = " + id.ToString() + " not found.");
        
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]Contact contact)
        {
            try
            {
                ContactRepository contactRepository = new ContactRepository();
                contactRepository.AddContact(contact);

                var message = Request.CreateResponse(HttpStatusCode.Created, contact);
                message.Headers.Location = new Uri(Request.RequestUri + contact.ID.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody]Contact contact)
        {
            try
            {
                ContactRepository contactRepository = new ContactRepository();
                var sContact = contactRepository.GetAllContacts.FirstOrDefault(c => c.ID == id);

                if (sContact == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with id = " + id.ToString() + " not found to update.");
                }
                else
                {
                    sContact.ID = contact.ID;
                    sContact.FirstName = contact.FirstName;
                    sContact.LastName = contact.LastName;
                    sContact.Email = contact.Email;
                    sContact.PhoneNumber = contact.PhoneNumber;
                    sContact.Status = contact.Status;
                    contactRepository.SaveContact(sContact);
                    return Request.CreateResponse(HttpStatusCode.OK, sContact);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                ContactRepository contactRepository = new ContactRepository();
                
                var contact = contactRepository.GetAllContacts.FirstOrDefault(c => c.ID == id);

                if (contact == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with id = " + id.ToString() + " not found.");
                }
                else
                {
                    contactRepository.DeleteContact(id);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}