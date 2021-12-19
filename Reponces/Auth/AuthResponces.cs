using System.ComponentModel.DataAnnotations;
using ListingApi.Dtos;
using ListingApi.Model.Auth;

namespace ListingApi.Reponces.Auth {
    public class AuthResponces {
        public int? Status { get; set; }
        public bool Success { get; set; }

        [Display (Name = "Status_Message")]
        public string Status_Message { get; set; }
        public UserDtos data { get; set; }
    }
}