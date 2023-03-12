namespace CASS_Portal.Models
{

    public class Quoteable
    {
        public string _id { get; set; }
        public string content { get; set; }
        public string author { get; set; }
        public List<string> tags { get; set; }
        public string authorSlug { get; set; }
        public int length { get; set; }
        public string dateAdded { get; set; }
        public string dateModified { get; set; }
    }


    public class BulkQuotes
    {
        public int count { get; set; }
        public int totalCount { get; set; }
        public int page { get; set; }
        public int totalPages { get; set; }
        public object lastItemIndex { get; set; }
        public List<Quoteable> results { get; set; }
        public List<Quoteable> Short { get; set; }
        public List<Quoteable> Medium { get; set; }
        public List<Quoteable> Long { get; set; }
    
    }
    public class DefaultResponse{
        public bool IsSuccess  { get; set; }
        public string? Msg { get; set; }
        public string? Data { get; set; }

    }
    
 
}
