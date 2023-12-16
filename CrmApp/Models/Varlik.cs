using CrmApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace CrmApp.Models
{
    public class Varlik
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string VarlikName { get; set; }


        [StringLength(100)]
        public string VarlikCode { get; set; }


        [StringLength(250)]
        public string VarlikDescription { get; set; }



        public ICollection<VarlikCategories> VarlikCategories { get; set; } = new HashSet<VarlikCategories>();


        public int AppUserId { get; set; }

    }
}
