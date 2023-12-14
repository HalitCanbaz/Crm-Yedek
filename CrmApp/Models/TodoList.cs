using CrmApp.Models.Entities;

namespace CrmApp.Models
{
    public class TodoList
    {
        public int Id { get; set; }

        public string ToDoDescription { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
