using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmApp.Models.Entities
{
    public class AppUser : IdentityUser<int>
    {
        [StringLength(50)]
        public string NameSurName { get; set; }

        public string? Picture { get; set; }

        public DateTime? RegisterDate { get; set; }

        public bool IsActive { get; set; }


        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }        

        public ICollection<Works> Works { get; set; }= new HashSet<Works>();

        [ForeignKey("DepartmanId")]
        public int DepartmanId { get; set; }
        public Departman Departman { get; set; }

        public ICollection<Duty> Duty { get; set; } = new HashSet<Duty>();



    }
}
