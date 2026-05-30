using Microsoft.AspNetCore.Http;

namespace ShepidiSoft.Application.Contracts;

public interface IFileStorageService
{
    /// <summary>
    /// Dosyayı belirtilen alt klasöre kaydeder, relative URL döner.
    /// Örn: "documents/report_abc123.pdf"
    /// </summary>
    Task<string> SaveAsync(IFormFile file, string folder, CancellationToken cancellationToken = default);

    void Delete(string relativeUrl);
}
