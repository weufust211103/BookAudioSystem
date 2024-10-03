using RentalBook.BusinessObjects.Models;

namespace BookAudioSystem.Services.IService
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterAsync(RegisterModel registrationDto);
        Task<UserResponseDto> LoginAsync(LoginModel loginDto);
    }
}
