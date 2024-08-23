namespace Library.Models
{
    public class Issue
    {
        public int IssueID { get; set; }
        public string Ititle { get; set; }
        public string Iauthor { get; set; }

        public string IssuePeriod { get; set; }
        public bool Due { get; set; }
    }
}
