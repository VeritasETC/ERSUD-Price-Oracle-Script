using DTO.ViewModel.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITokenService
    {
        JwtTokenModel ValidateToken(string token);
    }
}
