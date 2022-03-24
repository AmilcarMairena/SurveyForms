
using Authenticacion_surveyForm.Models;
using Authenticacion_surveyForm.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticacion_surveyForm.Repository
{
    public class UserRepository : IUserRepository
    {
        public List<UserDto> users = new List<UserDto>();

        public UserRepository()
        {
            users.Add(new UserDto
            {
                UserName = "Admin@admin.com",
                Password = "Admin",
                Role = "Admin"
            });
            users.Add(new UserDto
            {
                UserName = "RecursosHumanos@gmail.com",
                Password = "boneice",
                Role = "operator"
            });
            
        }
        public UserDto GetUser(Credentials userModel)=>users.Where(x => x.UserName.ToLower() == userModel.email.ToLower() && x.Password.ToLower() == userModel.Password.ToLower()).FirstOrDefault();
        
    }
}
