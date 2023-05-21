using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    //we are annotaing that its an api controller and not a mvc controller
    // Another way to name controller
    // [Route("api/contacts")]
    [ApiController]
    [Route("api/[controller]")]
   

    public class ContactsController : Controller
    {
        private readonly ContactsAPIDBContext dbContext;//we use this datafield to talk to the in memory db


        //Now we need to inject the dbcontext becoz we need to talk to the db

        public ContactsController(ContactsAPIDBContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpGet]
        //this method is returing an Iactionresult
        public async Task<IActionResult> GetContactsAsync()//this would return a ok respose
            //so we have to wrap inside a ok statement or change into a ienumerable type

            //we use the dbcontext to talk to the Contacts table,becoz we
              //have given the dbset propety inside the table and it knows contacts
              //is a table in our db and return a list
        {
           return Ok(await dbContext.Contacts.ToListAsync());
           
        }

        //we need another model to get the values from the db, so we create a class
        //we are going to use the new addcontactsrequest
        [HttpPost]
        //we are making this a Task type to make it asynchronous
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest) {

            //we are gonng to create a new contact object
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone,
                Email = addContactRequest.Email,
            };

            //now we talk to the db to add the contact
            //we can use the Async methoad and we need to use await
            //since we are awaiting on a method we need to declare the methoad as Async
            //With EF we need to add and then savechanges to see the changes in DB
           await dbContext.Contacts.AddAsync(contact);
           await dbContext.SaveChangesAsync();
           return Ok(contact);
        }


        //we are unsing put her becoz its an update method
        //we are using the id to find the customer and update it so the route is defined
        //for type safe id:guid is used
        [HttpPut]
        [Route("{id:guid}")]

        //we are defining a task of Iactionresult and it takes the from route as 
        // paramenter which is of type guid and name of parameter is id fromroute
        //we are also going to create a new class for this like the previous
        // add contact 
        //we are using a different class here becoz the name is different and the 
            //usecasse is also different
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {//check if the id is present in the db 
            // if presernt update it else return not found

            var contact=await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }
            else
            {
                
                contact.Address = updateContactRequest.Address;
                contact.FullName = updateContactRequest.FullName;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;

                await dbContext.SaveChangesAsync();

                return Ok(contact);
            }

        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute]Guid id) {
        

            var contact=await dbContext.Contacts.FindAsync(id);

            if(contact == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(contact);
            }
        
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact=  await dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound(contact);
        }
    }
}
