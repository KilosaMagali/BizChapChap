using System.ComponentModel.DataAnnotations.Schema;

namespace BizChapChap.Web.Models
{
    public class SubCategory : IEntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryRefId { get; set; }

        [ForeignKey("CategoryRefId")]
        public virtual Category Category { get; set; }
    }
}