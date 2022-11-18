using Api.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data
{

    public interface IBidRepository
    {
        Task<List<BidDto>> Get(int houseId);
        Task<BidDto> Add(BidDto dto);
    }

    public class BidRepository: IBidRepository
    {
        private HouseDbContext _context;

        public BidRepository(HouseDbContext context)
        {
            _context = context;
        }

        public async Task<List<BidDto>> Get(int houseId)
        {
            return await _context.Bids.Where(b => b.HouseId == houseId)
                .Select(b => new BidDto(b.Id, b.HouseId, b.Bidder, b.Amount))
                .ToListAsync();
        }

        public async Task<BidDto> Add(BidDto dto)
        {
            var entity = new BidEntity();
            entity.HouseId = dto.HouseId;
            entity.Bidder = dto.Bidder;
            entity.Amount = dto.Amount;

            _context.Bids.Add(entity);
            await _context.SaveChangesAsync();

            return new BidDto(entity.Id, entity.HouseId,
                entity.Bidder, entity.Amount);
        }
    }
}
