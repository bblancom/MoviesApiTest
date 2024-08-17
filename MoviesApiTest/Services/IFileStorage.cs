namespace MoviesApiTest.Services
{
	public interface IFileStorage
	{
		Task Delete(string? path, string container);
		Task<string> Store(string path, IFormFile file);

		// Default implementation. Could be done at interface level to set the default.
		async Task<string> Edit(string path, string container,IFormFile file)
		{
			await Delete(path, container);

			return await Store(path, file);
		}
	}
}
