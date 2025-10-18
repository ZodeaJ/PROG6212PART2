namespace LecturerClaimSystem.Models
{
    public class FileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;
        private readonly string[] _allowed = new[] { ".pdf", ".docx", ".xlsx" };
        //convert MB to bytes
        private const long MaxBytes = 100 * 1024 * 1024; // 100 MB

        public FileStorage(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            if (file.Length > MaxBytes)
                throw new InvalidOperationException("File size too large");

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (Array.IndexOf(_allowed, ext) < 0)
                throw new InvalidOperationException("Invalid file type. Add a valid file type: \".pdf\", \".docx\", \".xlsx\" ");

            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var unique = $"{Guid.NewGuid():N}{ext}";
            var savePath = Path.Combine(uploads, unique);

            using (var fs = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return unique; 
        }

        public Task DeleteFile(string filename)
        {
            var path = Path.Combine(_env.WebRootPath, "uploads", filename);
            if (File.Exists(path)) File.Delete(path);
            return Task.CompletedTask;
        }
    }
}