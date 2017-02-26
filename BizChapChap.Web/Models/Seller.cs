using System.ComponentModel.DataAnnotations;

namespace BizChapChap.Web.Models
{
    public class Seller : IEntityBase
    {
        public int Id { get; set; }

        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        public string Phone { get; set; }

        public virtual SellerType Type { get; set; }

        public string Password { get; set; }
    }
}