using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Repository.IRepository;

namespace BookStoreAPIVer2.Helper;

public class CheckBookDTO
{
    public async static Task CheckBookDTOBeforeModify(BookDTO bookDto, IBookRepository _bookRepository)
    {
        var check =  await _bookRepository.GetAsync(u => u.BookName.ToLower() == bookDto.BookName.ToLower());
        if (check != null)
        {
            throw new Exception($"{bookDto.BookName} already exists");
        }

        if (bookDto.Quantity <= 0)
        {
            throw new Exception($"{bookDto.Quantity} is empty");
        }
    }
}