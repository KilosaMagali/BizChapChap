using System.ComponentModel.DataAnnotations;

namespace BizChapChap.Web.Models
{
    public class Currency : IEntityBase
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Symbol { get; set; }
    }
}