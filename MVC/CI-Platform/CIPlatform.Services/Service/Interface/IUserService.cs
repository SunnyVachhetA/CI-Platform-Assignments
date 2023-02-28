using CIPlatform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Services.Service.Interface;
public interface IUserService
{
    void Add(UserVM user);
    bool IsEmailExists(string email);
}
