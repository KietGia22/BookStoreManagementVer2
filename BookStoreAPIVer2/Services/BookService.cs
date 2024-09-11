using AutoMapper;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Helper;
using BookStoreAPIVer2.Repository.IRepository;
using BookStoreAPIVer2.Services.IService;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BookStoreAPIVer2.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly Cloudinary _cloudinary;

    public BookService(IBookRepository bookRepository, IMapper mapper, Cloudinary cloudinary)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _cloudinary = cloudinary;
    }
    
    public async Task<List<BookDTO>> GetAllAsync()
    {
        var results = await _bookRepository.GetAllAsync();
        var books = _mapper.Map<List<BookDTO>>(results);
        return books;
    }

    public async Task<BookDTO> GetAsync(int bookId)
    {
        var result = await _bookRepository.GetAsync(c => c.BookId == bookId);
        var book = _mapper.Map<BookDTO>(result);
        return book;
    }

    public async Task CreateAsync(BookDTO bookDto)
    {
        CheckBookDTO.CheckBookDTOBeforeModify(bookDto, _bookRepository);
        
        var bookToCreate = _mapper.Map<Book>(bookDto);

        await _bookRepository.CreateAsync(bookToCreate);
    }

    public async Task<BookDTO> UpdateAsync(BookDTO bookDto)
    {
        
        CheckBookDTO.CheckBookDTOBeforeModify(bookDto, _bookRepository);
        
        var bookToUpdate = _mapper.Map<Book>(bookDto);

        var bookReturn = await _bookRepository.UpdateAsync(bookToUpdate);
        var result = _mapper.Map<BookDTO>(bookReturn);
        return result;
    }

    public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();

        if(file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        return uploadResult;
    }

    public async Task<ImageDTO> AddImageToDbAsync(ImageUploadResult image,  int bookId)
    {
        
        var existingBook = await _bookRepository.GetAsync(c => c.BookId == bookId);

        if (existingBook == null)
        {
            throw new Exception($"Book with id {bookId} not found");
        }
        
        var img = new Image
        {
            BookId = bookId,
            ImageUrl = image.Url.ToString()
        };

        var imageReturn = await _bookRepository.AddImageAsync(img);

        if (imageReturn == null)
        {
            throw new Exception("Upload image failed");
        }
        var imageDTO = _mapper.Map<ImageDTO>(imageReturn);
        
        return imageDTO;
    }
    
}