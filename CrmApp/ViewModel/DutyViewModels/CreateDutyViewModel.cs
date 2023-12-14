﻿namespace CrmApp.ViewModel.DutyViewModels
{
    public class CreateDutyViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; } = "beklemede";

        public byte Progress { get; set; } = 0;

        public DateTime Create { get; set; } = DateTime.Now;

        public DateTime Update { get; set; } = DateTime.Now;

        public DateTime DeadLine { get; set; }

        public DateTime Finished { get; set; } = DateTime.Now;

        public int AppUserId { get; set; }

        public int CategoriesId { get; set; }

        public int DepartmanId { get; set; }

        public int WorkOpenDepartmanId { get; set; }

    }
}
