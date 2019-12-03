using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApIDay1.Models;
using WebApIDay1.NewDTO;

namespace WebApIDay1.Controllers
{
    public class WebApiController : ApiController
    {
       
        //public List<string> cities = new List<string> { "Pondicherry", "Chennai", "Bangalore","Hydrabad" };
        //[Route("api/WebApi/getCities")]
        //[HttpGet]
        //public IEnumerable<string> getCities()
        //{
        //    return cities;
        //}

        //[Route("api/WebApi/GetCustomerById/{id}")]
        //[HttpGet]
        //public Customer GetCustomerById(int id)
        //{
        //    IEnumerable<Customer> customers = GetCustomers();
        //    var customer = customers.Where(c => c.Id == id).FirstOrDefault();
        //    return customer;
        //}
        //[Route("api/WebApi/GetCustomerByName/{name}")]
        //[HttpGet]
        //public Customer GetCustomerByName(string name)
        //{
        //    IEnumerable<Customer> customers = GetCustomers();
        //    var customer = customers.Where(c => c.Name==name).FirstOrDefault();
        //    return customer;
        //}

        //[Route("api/WebApi/GetCustomers")]
       // [HttpGet]
        //[NonAction]
        //public IEnumerable<Customer> GetCustomers()
        //{
        //    List<Customer> customers = new List<Customer>
        //    {
        //        new Customer{Id=1,Name="Kani",Age=22},
        //        new Customer{Id=2,Name="Kalpana",Age=22},
        //        new Customer{Id=3,Name="Arthi",Age=23},
        //        new Customer{Id=4,Name="usha",Age=24},
        //        new Customer{Id=5,Name="RagaPriya",Age=20}
        //    };
        //    return customers;
        //}

        [Route("api/WebApi/GetCustomer")]
        [HttpGet]
        public IEnumerable<CustomerDTO> GetCustomer()
        {
            Entities entities = new Entities();
            //IEnumerable<customer> cus = entities.customers.ToList();
            var cus = entities.customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                customerName = c.customerName,
                Gender = c.Gender,
                DateofBirth = c.DateofBirth,
                city = c.city,
                MemberShipTypeId = c.MemberShipTypeId

            }).ToList();
            return cus;
        }
        [Route("api/WebApi/GetCustomerById/{Id}")]
        [HttpGet]
        public IHttpActionResult GetCustomerById(int id)
        {
            Entities entities = new Entities();
            var cus = entities.customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                customerName = c.customerName,
                Gender = c.Gender,
                DateofBirth = c.DateofBirth,
                city = c.city,
                MemberShipTypeId = c.MemberShipTypeId

            }).FirstOrDefault(c=>c.Id==id);
            if(cus==null)
            {
                return NotFound();
            }
            return Ok(cus);


        }
        [Route("api/WebApi/GetCustomerByNameAndCity/{input}")]
        [HttpGet]
        public IHttpActionResult GetCustomerByNameAndCity(string input)
        {
            Entities entities = new Entities();
            var cus = entities.customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                customerName = c.customerName,
                Gender = c.Gender,
                DateofBirth = c.DateofBirth,
                city = c.city,
                MemberShipTypeId = c.MemberShipTypeId

            }).Where(c => c.customerName == input|| c.city == input).ToList();
            
            //Where(c => c.customerName==Name).Where(c => c.city == City).ToList();
            //if(cus.Count()==0)
            //{
            //    return NotFound();
            //}
            return Ok(cus);


        }
        [HttpPost]
        public IHttpActionResult CreateEmployee(CustomerDTO customerDTO)
        {
            Entities entities = new Entities();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                customer Customer = new customer
                {
                    customerName = customerDTO.customerName,
                    Gender = customerDTO.Gender,
                    DateofBirth = customerDTO.DateofBirth,
                    city = customerDTO.city,
                    MemberShipTypeId = customerDTO.MemberShipTypeId
                };


                entities.customers.Add(Customer);
                entities.SaveChanges();
                customerDTO.Id = Customer.Id;
                return Created(new Uri(Request.RequestUri + "/" + customerDTO.Id), customerDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(HttpStatusCode.InternalServerError);
               
            }
        }
        [Route("api/WebApi/UpdateCustomer/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateCustomer([FromUri]int id,[FromBody]CustomerDTO customerDTO)
        {
            Entities entities = new Entities();
            var customerInDb = entities.customers.FirstOrDefault(c => c.Id == id);
            if(customerInDb==null)
            {
                return NotFound();
            }
            if(id!=customerDTO.Id)
            {
                return BadRequest();
            }
            customerInDb.customerName = customerDTO.customerName;
            customerInDb.city = customerDTO.city;
            customerInDb.Gender = customerDTO.Gender;
            customerInDb.DateofBirth = customerDTO.DateofBirth;
            customerInDb.MemberShipTypeId = customerDTO.MemberShipTypeId;
            entities.Entry(customerInDb).State = System.Data.Entity.EntityState.Modified;
            entities.SaveChanges();
            return Ok();
        }
        [Route("api/WebApi/DeleteCustomer/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Entities entities = new Entities();
            var customer = entities.customers.FirstOrDefault(c => c.Id == id);
            if(customer==null)
            {
                return NotFound();
            }
            entities.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
            entities.SaveChanges();
            return Ok();

        }
    }
}
