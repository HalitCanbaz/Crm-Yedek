using CrmApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace CrmApp.Models
{
    public class Varlik
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string VarlikName { get; set; }
        public ICollection<VarlikCategories> VarlikCategories { get; set; } = new HashSet<VarlikCategories>();

    }
}
