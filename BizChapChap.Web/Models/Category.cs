using System.Collections.Generic;

namespace BizChapChap.Web.Models
{
    public class Category : IEntityBase
    {
        public Category()
        {
                SubCategories = new List<SubCategory>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}