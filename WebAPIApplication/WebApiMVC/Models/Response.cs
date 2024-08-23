namespace WebApiMVC.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<Crud> cruds { get; set; }
        public Crud crud { get; set; }
    }
}
