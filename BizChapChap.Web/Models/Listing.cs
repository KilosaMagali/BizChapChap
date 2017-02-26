using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizChapChap.Web.Models
{
    public class Listing : IEntityBase
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateEdit { get; set; }

        [Required]
        public double Price { get; set; }

        public int CurrencyRefId { get; set; }

        [ForeignKey("CurrencyRefId")]
        public virtual Currency Currency { get; set; }

        public string Description { get; set; }

        public int SubCategoryRefId { get; set; }

        [ForeignKey("SubCategoryRefId")]
        public virtual SubCategory Category { get; set; }

        public bool IsPremium { get; set; }

        public string Photo1 { get; set; }

        public string Photo2 { get; set; }

        public string Photo3 { get; set; }

        public string Photo4 { get; set; }

        public string Photo5 { get; set; }

        public string Photo6 { get; set; }
    }
}