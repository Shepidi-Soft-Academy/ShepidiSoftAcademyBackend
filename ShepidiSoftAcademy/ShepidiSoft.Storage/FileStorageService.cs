using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShepidiSoft.Application.Contracts;

namespace ShepidiSoft.Storage;

public sealed class FileStorageService(IWebHostEnvironment env) : IFileStorageService
{
    private static readonly string[] AllowedExtensions = [".pdf", ".docx", ".doc", ".xlsx", ".png", ".jpg"];
    private const long MaxBytes = 10 * 1024 * 1024; // 10 MB

    public async Task<string> SaveAsync(
        IFormFile file,
        string folder,
        CancellationToken cancellationToken = default)
    {
        Validate(file);

        var uploadPath = Path.Combine(env.WebRootPath, folder);
        Directory.CreateDirectory(uploadPath); // yoksa oluşturur, varsa geçer

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var safeName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "_");
        var uniqueName = $"{safeName}_{Guid.NewGuid():N}{extension}";
        var fullPath = Path.Combine(uploadPath, uniqueName);

        await using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        await file.CopyToAsync(stream, cancellationToken);

        // "documents/report_abc123.pdf"  →  URL olarak kullanılabilir
        return $"{folder}/{uniqueName}";
    }

    public void Delete(string relativeUrl)
    {
        if (string.IsNullOrWhiteSpace(relativeUrl)) return;

        var fullPath = Path.Combine(env.WebRootPath, relativeUrl.Replace("/", Path.DirectorySeparatorChar.ToString()));
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    private static void Validate(IFormFile file)
    {
        if (file is null || file.Length == 0)
            throw new ArgumentException("Dosya boş olamaz.");

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(ext))
            throw new ArgumentException($"Geçersiz dosya formatı: {ext}");

        if (file.Length > MaxBytes)
            throw new ArgumentException("Dosya boyutu 10 MB'ı aşamaz.");
    }
}