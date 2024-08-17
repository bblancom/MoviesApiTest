namespace MoviesApiTest.Dtos
{
	public class ActorDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public DateTime BirthDate { get; set; }
		public string? Picture { get; set; }
	}
}
