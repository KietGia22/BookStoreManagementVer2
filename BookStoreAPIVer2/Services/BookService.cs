using AutoMapper;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Helper;
using BookStoreAPIVer2.Repository.IRepository;
using BookStoreAPIVer2.Services.IService;

namespace BookStoreAPIVer2.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }
    
    public async Task<List<BookDTO>> GetAllAsync()
    {
        var results = await _bookRepository.GetAllAsync();
        var books = _mapper.Map<List<BookDTO>>(results);
        return books;
    }

    public async Task<BookDTO> GetAsync(int categoryId)
    {
        var result = await _bookRepository.GetAsync(c => c.CategoryId == categoryId);
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
    
}