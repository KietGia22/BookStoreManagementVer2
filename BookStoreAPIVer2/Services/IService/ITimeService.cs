using BookStoreAPIVer2.DTOs.TimeKeeping;
using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2.Services.IService;

public interface ITimeService
{
    Task<List<TimeKeepingDTO>> GetAllAsync();
    
    Task<TimeKeepingDTO> AddAsync(int accId);
    
    Task<TimeKeepingDTO> UpdateAsync(int accId);
}