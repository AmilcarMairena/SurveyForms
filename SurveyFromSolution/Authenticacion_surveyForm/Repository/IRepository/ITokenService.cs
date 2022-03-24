
using Authenticacion_surveyForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticacion_surveyForm.Repository.IRepository
{
    public interface ITokenService
    {
        string BuildToken(string key, UserDto user, DateTime expiresAt);
        bool ValidateToken(string key, string token);
    }
}
