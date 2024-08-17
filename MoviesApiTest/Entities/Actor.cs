namespace MoviesApiTest.Entities
{
	public class Actor
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? Picture { get; set; }
    }
}
