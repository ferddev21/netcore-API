// using System;
// using System.Linq;
// using System.Net;
// using Microsoft.AspNetCore.Mvc;
// using netcore.Models;
// using netcore.Repository;
// using netcore.Repository.Data;

// namespace netcore.Controllers
// {

//     [ApiController]
//     [Route("api/[controller]")]
//     public class PersonsController : ControllerBase
//     {
//         private readonly PersonRepository personRepository;
//         private string message;

//         public PersonsController(PersonRepository personRepository)
//         {
//             this.personRepository = personRepository;
//         }

//         [HttpPost]
//         public ActionResult Insert(Person person)
//         {
//             try
//             {
//                 personRepository.Insert(person);
//                 return StatusCode((int)HttpStatusCode.Created, new
//                 {
//                     status = (int)HttpStatusCode.Created,
//                     message = "Success created"
//                 });

//             }
//             catch (System.Exception e)
//             {
//                 return StatusCode((int)HttpStatusCode.InternalServerError, new
//                 {
//                     status = (int)HttpStatusCode.InternalServerError,
//                     message = e.Message
//                 });
//             }
//         }

//         [HttpGet]
//         public ActionResult Get()
//         {
//             var person = personRepository.Get();
//             if (person == null)
//             {
//                 return StatusCode((int)HttpStatusCode.NotFound, new
//                 {

//                     status = (int)HttpStatusCode.NoContent,
//                     result = personRepository.Get(),
//                     message = "Data tidak ditemukan",

//                 });
//             }
//             else
//             {
//                 return StatusCode((int)HttpStatusCode.OK, new
//                 {
//                     status = (int)HttpStatusCode.OK,
//                     result = personRepository.Get(),
//                     message = "Success",
//                 });
//             }

//         }



//         [HttpGet("{NIK}")] //BASEURL/api/persons/18120571
//         public ActionResult Get(string NIK)
//         {
//             var person = personRepository.Get(NIK);
//             if (person == null)
//             {
//                 return StatusCode((int)HttpStatusCode.NotFound, new
//                 {

//                     status = (int)HttpStatusCode.NoContent,
//                     result = personRepository.Get(NIK),
//                     message = "Data tidak ditemukan",

//                 });
//             }
//             else
//             {
//                 return StatusCode((int)HttpStatusCode.OK, new
//                 {
//                     status = (int)HttpStatusCode.OK,
//                     result = personRepository.Get(NIK),
//                     message = "Success",
//                 });
//             }

//         }


//         [HttpPut]
//         public ActionResult Update(Person person)
//         {

//             try
//             {
//                 personRepository.Update(person);
//                 return StatusCode((int)HttpStatusCode.OK, new
//                 {
//                     status = (int)HttpStatusCode.OK,
//                     message = "Success updated"
//                 });

//             }
//             catch (System.Exception e)
//             {
//                 return StatusCode((int)HttpStatusCode.InternalServerError, new
//                 {
//                     status = (int)HttpStatusCode.InternalServerError,
//                     message = e.Message
//                 });
//             }

//         }

//         [HttpDelete("{NIK}")]
//         public ActionResult Delete(string NIK)
//         {
//             try
//             {
//                 personRepository.Delete(NIK);
//                 return StatusCode((int)HttpStatusCode.OK, new
//                 {
//                     status = (int)HttpStatusCode.OK,
//                     message = "Success deleted"
//                 });

//             }
//             catch (System.Exception e)
//             {
//                 return StatusCode((int)HttpStatusCode.InternalServerError, new
//                 {
//                     status = (int)HttpStatusCode.InternalServerError,
//                     message = e.Message
//                 });
//             }

//         }
//     }

// }