using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public record HouseDetailDto(int Id, 
        [property: Required] string? Address, 
        [property: Required] string? Country, 
        int Price,string? Description,string? Photo);
}
