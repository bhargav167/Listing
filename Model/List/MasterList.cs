using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ListingApi.Model.Auth;
namespace ListingApi.Model.List {
    public class MasterList {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public AuthUser User { get; set; }
        public ICollection<ListItem> list { get; set; }
        public bool CanDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        [NotMapped]
        public string DateFormat { get; set; }
        
        
        public MasterList () {
            CreatedDate = DateTime.Now;
            CanDelete = true;
        }
    }
}