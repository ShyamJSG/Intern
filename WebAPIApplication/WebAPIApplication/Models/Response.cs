namespace WebAPIApplication.Models
{
    public class Response
    {
        public int StatusCode {  get; set; }
        public String StatusMessage { get; set; }
        public Crud crud { get; set; } 
        public List<Crud> cruds { get; set; }
    }
}
