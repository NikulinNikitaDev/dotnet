namespace StudentsApp.API.Resources
{
    public class MarkResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StudentResource Student { get; set; }
    }
}