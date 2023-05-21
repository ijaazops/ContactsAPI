namespace ContactsAPI.Models
{
    //we dont need the ID becoz we are giving it
    //and we wont to save to the db
    public class AddContactRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }

    }
}
