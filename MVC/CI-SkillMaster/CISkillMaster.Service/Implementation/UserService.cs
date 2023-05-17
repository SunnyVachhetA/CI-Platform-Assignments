using CISkillMaster.DataAccessLayer.Abstract;
using CISkillMaster.Entities.DataModels;
using CISkillMaster.Entities.DTO;
using CISkillMaster.Entities.Extension;
using CISkillMaster.Services.Abstract;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Text.Json;

namespace CISkillMaster.Services.Implementation;

public class UserService: IUserService
{
    #region Property
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;
    #endregion

    #region Constructor
    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        Console.WriteLine("here user service;");
        _userRepository = userRepository;
        _logger = logger;
    }
    #endregion

    #region Methods
    public async Task<UserInformationDTO?> SignInUser(UserLoginDTO credential)
    {
        _logger.LogInformation("Executing {Action}", nameof(SignInUser));
        var user = await _userRepository.GetFirstOrDefaultAsync(IsValidCredential(credential.Email, credential.Password));
        if (user is null)
        {
            _logger.LogWarning("Invalid user credential for {Param}", JsonSerializer.Serialize(credential.Email));
            return null;
        }
        return user.ToUserInformationDTO();
        
    }
    #endregion 

    #region Helper Filter
    private Expression<Func<User, bool>> IsValidCredential(string email, string password) =>
        (user) => user.Email == email && user.Password == password;
    #endregion 
}
