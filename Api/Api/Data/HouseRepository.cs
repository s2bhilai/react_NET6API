using Api.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data
{

    public interface IHouseRepository
    {
        Task<List<HouseDto>> GetAll();
        Task<HouseDetailDto?> Get(int id);
        Task<HouseDetailDto> Add(HouseDetailDto dto);
        Task<HouseDetailDto> Update(HouseDetailDto dto);
        Task Delete(int id);
    }

    public class HouseRepository: IHouseRepository
    {
        private readonly HouseDbContext context;

        public HouseRepository(HouseDbContext context)
        {
            this.context = context;
        }

        private static void DtoToEntity(HouseDetailDto dto,HouseEntity e)
        {
            e.Address = dto.Address;
            e.Country = dto.Country;
            e.Description = dto.Description;
            e.Price = dto.Price;
            e.Photo = dto.Photo;
        }

        public async Task<List<HouseDto>> GetAll()
        {
            return await context.Houses
                .Select(h => new HouseDto(h.Id, h.Address, h.Country, h.Price))
                .ToListAsync();
        }

        public async Task<HouseDetailDto?> Get(int id)
        {
            var e = await context.Houses.SingleOrDefaultAsync(h => h.Id == id);

            if (e == null)
                return null;

            return new HouseDetailDto(e.Id, e.Address, e.Country, e.Price,
                e.Description, e.Photo);
        }

        public async Task<HouseDetailDto> Add(HouseDetailDto dto)
        {
            var entity = new HouseEntity();
            DtoToEntity(dto, entity);

            //In the case of Add, dbcontext starts tracking entity automatically,
            // even if entity tracking is not enabled.
            context.Houses.Add(entity);
            await context.SaveChangesAsync();

            return new HouseDetailDto(entity.Id, entity.Address, entity.Country, entity.Price,
               entity.Description, entity.Photo);
        }

        public async Task<HouseDetailDto> Update(HouseDetailDto dto)
        {
            var entity = await context.Houses.FindAsync(dto.Id);
            if (entity == null)
                throw new ArgumentException($"Error updating house {dto.Id}");

            DtoToEntity(dto, entity);
            //In the case of Update, We have to mention explicitly to start tracking.
            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return new HouseDetailDto(entity.Id, entity.Address, entity.Country, entity.Price,
              entity.Description, entity.Photo);

        }

        public async Task Delete(int id)
        {
            var entity = await context.Houses.FindAsync(id);

            if (entity == null)
                throw new ArgumentException($"Error deleting house {id}");

            context.Houses.Remove(entity);

            await context.SaveChangesAsync();
        }
    }
}
