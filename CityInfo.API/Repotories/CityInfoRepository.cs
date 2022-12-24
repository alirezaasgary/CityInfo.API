using CityInfo.API.Context;
using CityInfo.API.Models_Entity;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Repotories
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context)); // کنترل خطا در صورت خالی بودن کانتکست
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.City.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int id, bool pointInterest)
        {
            if (pointInterest)
            {
                return await _context.City.Include(c => c.PointOfIntrests).
                    Where(c => c.Id == id).FirstOrDefaultAsync();
            }
            
                return await _context.City.
                Where(c => c.Id == id).FirstOrDefaultAsync();
             
        }

        public async Task<IEnumerable<PointOfIntrest>> GetPointsAsync(int cityId)
        {
            return await _context.PointOfIntrest.
                Where(p=>p.CityId==cityId).ToListAsync();
        }

        public async Task<PointOfIntrest?> GetPointsAsync(int cityId, int pointId)
        {
            return await _context.PointOfIntrest.
                Where(p=>p.CityId == cityId && p.Id==pointId).FirstOrDefaultAsync();

        }
    }
}
