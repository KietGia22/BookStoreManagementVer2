using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Repository.IRepository;

namespace BookStoreAPIVer2.Helper;

public class CheckBookDTO
{
    public async static Task CheckBookDTOBeforeModify(BookDTO bookDto, IBookRepository _bookRepository)
    {
        if (await _bookRepository.GetAsync(u => u.BookName.ToLower() == bookDto.BookName.ToLower()) != null)
        {
            throw new Exception($"{bookDto.BookName} already exists");
        }    
    }
}