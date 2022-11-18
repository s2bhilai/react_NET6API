using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public record HouseDto(int Id,string? Address,string? Country,int Price);
}
