
namespace MoviesApiTest.Services
{
	public class LocalFileStorage : IFileStorage
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public LocalFileStorage(IWebHostEnvironment webHostEnvironment,
			IHttpContextAccessor httpContextAccessor)
		{
			_webHostEnvironment = webHostEnvironment;
			_httpContextAccessor = httpContextAccessor;
		}

		public Task Delete(string? path, string container)
		{
			if (string.IsNullOrEmpty(path))
			{
				return Task.CompletedTask;
			}

			var fileName = Path.GetFileName(path);
			var filePath = Path.Combine(_webHostEnvironment.WebRootPath, container, fileName);

			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}

			return Task.CompletedTask;
		}

		public async Task<string> Store(string path, IFormFile file)
		{
			var extension = Path.GetExtension(file.FileName);
			var fileName = $"{Guid.NewGuid()}{extension}";
			var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			var filePath = Path.Combine(directoryPath, fileName);
			using (var memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream);
				var content = memoryStream.ToArray();
				await File.WriteAllBytesAsync(filePath, content);
			}

			var url = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
			var urlFile = Path.Combine(url, path, fileName).Replace("\\", "/");

			return urlFile;
		}
	}
}
