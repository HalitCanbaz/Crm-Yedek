using System.ComponentModel.DataAnnotations;

namespace CrmApp.Models
{
    public class VarlikCategories
    {
        public int Id { get; set; }
        public int VarlikId { get; set; }
        public Varlik Varlik { get; set; }
        public  int CategoriesId { get; set; }
        public Categories Categories { get; set; }

        [StringLength(150)]
        public string VarlikCategoriesName { get; set; }

        public ICollection<Works> Works { get; set; } = new HashSet<Works>();

    }
}
