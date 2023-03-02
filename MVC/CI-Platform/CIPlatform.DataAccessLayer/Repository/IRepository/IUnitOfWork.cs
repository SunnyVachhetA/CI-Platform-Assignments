using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IUnitOfWork
{
    IUserRepository UserRepo { get; }
    IPasswordResetRepository PasswordResetRepo { get; }

    void Save();
}
