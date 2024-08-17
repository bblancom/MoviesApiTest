namespace MoviesApiTest.Dtos
{
	public class CreateActorDto
	{
		public string Name { get; set; } = null!;
		public DateTime BirthDate { get; set; }
		public IFormFile? Picture { get; set; }
	}
}
