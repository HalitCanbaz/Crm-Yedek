
using System.ComponentModel.DataAnnotations;

namespace CrmApp.Models
{
    public class Categories
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        public ICollection<VarlikCategories> Varlikcategories { get; set; } = new HashSet<VarlikCategories>();
        public ICollection<Duty> Duty { get; set; } = new HashSet<Duty>();

    }
}
