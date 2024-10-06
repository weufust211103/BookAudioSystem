using BookAudioSystem.BusinessObjects.Models;
using RentalBook.BusinessObjects.Models;

namespace BookAudioSystem.Services.IService
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterAsync(RegisterModel registrationDto);
        Task<UserResponseDto> LoginAsync(LoginModel loginDto);

        Task<UserResDto> GetUserInfoByIdAsync(int userId);

        Task<UserResDto> GetUserInfoByEmailAsync(string email);
    }
}
