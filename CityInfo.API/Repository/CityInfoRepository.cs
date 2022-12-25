using CityInfo.API.Context;
using CityInfo.API.Models_Entity;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Repository
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context)); // کنترل خطا در صورت خالی بودن کانتکست
        }

        public async Task<bool> CityExistAsync(int cityId)
        {
            return await _context.City.AnyAsync(x => x.Id == cityId);
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

        public async Task<PointOfIntrest?> GetPointAsync(int cityId, int pointId)
        {
            return await _context.PointOfIntrest.
                Where(p=>p.CityId == cityId && p.Id==pointId).FirstOrDefaultAsync();

        }

        public async Task<bool> PointOfIntrestExistAsync(int cityId, int pointId)
        {
            return await _context.PointOfIntrest.AnyAsync(p => p.Id == pointId && p.CityId == cityId);
        }

        public async Task AddPointOfIntrestAsync(int cityId, PointOfIntrest pointOfIntrest)
        {
            
            if (! await CityExistAsync(cityId))
            {
                pointOfIntrest.CityId = cityId;
                _context.PointOfIntrest.Add(pointOfIntrest);
            }
        }

        public async Task<bool> SaveChangAsync()
        {
           return (await _context.SaveChangesAsync()>0);
        }

        public async Task UpdatePointOfIntrestAsync(int cityId, PointOfIntrest pointOfIntrest)
        {
           
            if(!await CityExistAsync(cityId))
            {

                _context.Update(pointOfIntrest);
            }
        }

        public  void DeletePointOfIntrestAsync(PointOfIntrest pointOfIntrest)
        {


             _context.PointOfIntrest.Remove(pointOfIntrest);
            
        }
    }
}
