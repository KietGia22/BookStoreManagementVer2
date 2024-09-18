using AutoMapper;
using BookStoreAPIVer2.DTOs.TimeKeeping;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Repository.IRepository;
using BookStoreAPIVer2.Services.IService;

namespace BookStoreAPIVer2.Services;

public class TimeService : ITimeService
{
    private readonly ITimeRepository _timeRepository;
    private readonly IMapper _mapper;

    public TimeService(ITimeRepository timeRepository, IMapper mapper)
    {
        _timeRepository = timeRepository;
        _mapper = mapper;
    }

    public async Task<List<TimeKeepingDTO>> GetAllAsync()
    {
        var timeKeepingList = await _timeRepository.GetAllAsync();
        
        var timeKeepingDTOsList = _mapper.Map<List<TimeKeepingDTO>>(timeKeepingList);
        
        return timeKeepingDTOsList;
    }

    public async Task<TimeKeepingDTO> AddAsync(int accId)
    {
        var timeKeeping = new TimeKeeping
        {
            AccId = accId,
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow,
            TotalTime = 0
        };

        await _timeRepository.AddAsync(timeKeeping);
        
        var newTimeKeeping = _mapper.Map<TimeKeepingDTO>(timeKeeping);
        
        return newTimeKeeping;
    }

    public async Task<TimeKeepingDTO> UpdateAsync(int accId)
    {
        var timeKeeping = await _timeRepository.GetAsync(time => time.AccId == accId);
        
        if (timeKeeping == null)
        {
            throw new Exception("TimeKeeping record not found.");
        }
        
        if (timeKeeping.TotalTime > 0)
        {
            throw new Exception("You have already timed it");
        }
        
        timeKeeping.EndTime = DateTime.UtcNow;
        timeKeeping.TotalTime = (timeKeeping.EndTime - timeKeeping.StartTime).TotalSeconds;

        await _timeRepository.UpdateAsync(timeKeeping);
        
        var updateTimeKeeping = _mapper.Map<TimeKeepingDTO>(timeKeeping);
        
        return updateTimeKeeping;
    }
    
}