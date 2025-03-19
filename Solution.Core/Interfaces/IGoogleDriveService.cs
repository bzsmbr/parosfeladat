namespace Solution.Core.Interfaces;

public interface IGoogleDriveService
{
    ErrorOr<Success> DeleteFile(string fileId);
    Task<ErrorOr<byte[]>> DownloadFileAsync(string fileId);
    Task<ErrorOr<ImageUploadResponse>> UploadFileAsync(FileResult file);
}
