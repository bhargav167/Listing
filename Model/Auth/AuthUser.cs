using System;

namespace ListingApi.Model.Auth
{
    public class AuthUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } 
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string ModOfRegistration { get; set; } 
        public string Token { get; set; }
        public string ImageUrl { get; set; }
        public string Otp { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public AuthUser()
        {
            CreatedDate=DateTime.Now;
            ImageUrl="https://mpng.subpng.com/20180429/bdq/kisspng-money-bag-computer-icons-dollar-sign-clip-art-5ae64a6cf14455.5668874915250417729882.jpg";
        }
    }
}