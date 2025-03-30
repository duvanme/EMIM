namespace EMIM.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderPath);
        bool DeleteFile(string filePath);
    }
}
