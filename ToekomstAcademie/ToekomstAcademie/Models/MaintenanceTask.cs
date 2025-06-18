namespace ToekomstAcademie.Models
{
    public class MaintenanceTask
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public string Status { get; set; } // "ToDo", "InProgress", "Done"
        public DateTime Deadline { get; set; }
    }
}
