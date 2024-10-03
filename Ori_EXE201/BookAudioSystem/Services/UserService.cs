using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.Helper;
using BookAudioSystem.Repositories.IRepositories;
using BookAudioSystem.Services.IService;
using RentalBook.BusinessObjects.Models;

namespace BookAudioSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTTokenHelper _jwtTokenHelper;

        public UserService(IUserRepository userRepository, JWTTokenHelper jwtTokenHelper)
        {
            _userRepository = userRepository;
            _jwtTokenHelper = jwtTokenHelper;
        }

        public async Task<UserResponseDto> RegisterAsync(RegisterModel registrationDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(registrationDto.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            var user = new User
            {
                Email = registrationDto.Email,
                Password = PasswordHasher.HashPassword(registrationDto.Password),
                FullName = registrationDto.FullName,
                createdDate = DateTime.UtcNow,
                Address=registrationDto.Address,
                BankAccountNumber=registrationDto.BankAccountNumber,
                Province=registrationDto.Province,
                PhoneNumber=registrationDto.PhoneNumber,
                birthDate=registrationDto.BirthDate,
                BankName=registrationDto.BankName,
                District=registrationDto.District,
                
            };
            // Retrieve the "Renter" role from the database
            var renterRole = await _userRepository.GetRoleByNameAsync("Renter");
            if (renterRole == null)
            {
                throw new Exception("Renter role not found.");
            }

            // Create the relationship between the user and the "Renter" role
            var userRole = new UserRole
            {
                User = user,
                Role = renterRole
            };

            // Add the user to the database
            await _userRepository.AddUserAsync(user);

            // Add the user's role to the database
            await _userRepository.AddUserRoleAsync(userRole);
            await _userRepository.AddUserAsync(user);

            return new UserResponseDto
            {
                UserID = user.UserID,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        public async Task<UserResponseDto> LoginAsync(LoginModel loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            if (user == null || !PasswordHasher.VerifyPassword(loginDto.Password, user.Password))
            {
                throw new Exception("Invalid email or password.");
            }

            // Generate JWT token
            var token = _jwtTokenHelper.GenerateJwtToken(user);

            // Save token in the database
            user.Token = token;
            await _userRepository.UpdateUserAsync(user); // Add this method in the repository

            return new UserResponseDto
            {
                UserID = user.UserID,
                Email = user.Email,
                FullName = user.FullName,
                Token = token
            };
        }
    }

}

