namespace MockHttpMessageHandler.WebApi.Models
{
    public class Todo
    {       
        public long UserId { get; set; }        
        public long Id { get; set; }        
        public string Title { get; set; }        
        public bool Completed { get; set; }
    }
}
