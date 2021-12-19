using ListingApi.Model.Auth;

namespace ListingApi.Model.List
{
    public class ListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; } 
        public string Host { get; set; }
         public string HostName { get; set; }
         public string Origin { get; set; }
         public string Link { get; set; }
         public string PathName { get; set; }
         public string UserName { get; set; }
         public string SearchParams { get; set; } 
        public int UserId { get; set; }
        public AuthUser User { get; set; }
        public int MasterListId { get; set; }
        public MasterList MasterList { get; set; }
    }
}