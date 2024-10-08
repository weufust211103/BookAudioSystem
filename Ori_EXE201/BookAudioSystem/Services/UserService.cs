using BookAudioSystem.BusinessObjects.Entities;
using BookAudioSystem.BusinessObjects.Models;
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
                throw new Exception("User already exists with this email.");
            }

            var user = new User
            {
                Email = registrationDto.Email,
                Password = PasswordHasher.HashPassword(registrationDto.Password),
                FullName = registrationDto.FullName,
                createdDate = DateTime.UtcNow,
                Address = registrationDto.Address,
                BankAccountNumber = registrationDto.BankAccountNumber,
                Province = registrationDto.Province,
                PhoneNumber = registrationDto.PhoneNumber,
                birthDate = registrationDto.BirthDate,
                BankName = registrationDto.BankName,
                District = registrationDto.District,
                IdentityCard = registrationDto.IdentityCard,
                Ward = registrationDto.Ward,
                Token = "default-token",
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

            var roles = await _userRepository.GetUserRolesAsync(user.UserID);

            // Generate JWT token
            var token = _jwtTokenHelper.GenerateToken(user, roles);

            // Save token in the database
            user.Token = token;
            await _userRepository.UpdateUserAsync(user); // Add this method in the repository

            return new UserResponseDto
            {
                UserID = user.UserID,
                Email = user.Email,
                FullName = user.FullName,
                Token = user.Token
            };
        }
        public async Task<bool> ChangeUserRoleToOwnerByEmailAsync(string email)
        {
            // Find the user by their email
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Find the "Owner" role
            var ownerRole = await _userRepository.GetRoleByNameAsync("Owner");
            if (ownerRole == null)
            {
                throw new Exception("Owner role not found.");
            }

            // Remove any existing roles for the user
            await _userRepository.RemoveUserRolesAsync(user.UserID);

            // Assign the "Owner" role to the user
            var userRole = new UserRole
            {
                UserID = user.UserID,
                RoleID = ownerRole.RoleID
            };
            await _userRepository.AddUserRoleAsync(userRole);

            return true;
        }
        public async Task<UserResDto> GetUserInfoByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            return new UserResDto
            {
                UserId = user.UserID,
                Email = user.Email,
                FullName = user.FullName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                BankName = user.BankName,
                BankAccountNumber = user.BankAccountNumber,
                IdentityCard = user.IdentityCard,
                Province = user.Province,
                District = user.District,
                Ward = user.Ward,
                BirthDate = user.birthDate,
                Token=user.Token
            };
        }

        public Task<UserResDto> GetUserInfoByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }

}

