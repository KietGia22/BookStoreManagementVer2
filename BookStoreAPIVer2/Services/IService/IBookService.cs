using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Entities;
using CloudinaryDotNet.Actions;

namespace BookStoreAPIVer2.Services.IService;

public interface IBookService : IService<BookDTO>
{
    Task<ImageUploadResult> UploadPhotoAsync(IFormFile file);
    Task<ImageDTO> AddImageToDbAsync(ImageUploadResult image, int bookId);
}