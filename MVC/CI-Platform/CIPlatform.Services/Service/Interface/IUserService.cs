using CIPlatform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Services.Service.Interface;
public interface IUserService
{
    void Add(UserRegistrationVM user);
    bool IsEmailExists(string email);
    UserRegistrationVM ValidateUserCredential(UserLoginVM credential);
}
