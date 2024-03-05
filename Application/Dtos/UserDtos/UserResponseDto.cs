using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDtos;
public class UserResponseDto {
    public required string JWT { get; set; } = string.Empty;
}
