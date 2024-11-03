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

        // Registers a new user in the system
        public async Task<UserResponseDto> RegisterAsync(RegisterModel registrationDto)
        {
            // Check if the user already exists with the given email
            var existingUser = await _userRepository.GetUserByEmailAsync(registrationDto.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists with this email.");
            }

            // Create a new User object with the provided registration details
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

            // Return the response DTO with the user details
            return new UserResponseDto
            {
                UserID = user.UserID,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        // Authenticates a user and generates a JWT token
        public async Task<UserResponseDto> LoginAsync(LoginModel loginDto)
        {
            // Find the user by their email
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            // Check if the user exists and the password is correct
            if (user == null || !PasswordHasher.VerifyPassword(loginDto.Password, user.Password))
            {
                throw new Exception("Invalid email or password.");
            }

            // Get the user's roles
            var roles = await _userRepository.GetUserRolesAsync(user.UserID);

            // Generate JWT token
            var token = _jwtTokenHelper.GenerateToken(user, roles);

            // Save the token in the database
            user.Token = token;
            await _userRepository.UpdateUserAsync(user);

            // Return the response DTO with the user details and token
            return new UserResponseDto
            {
                UserID = user.UserID,
                Email = user.Email,
                FullName = user.FullName,
                Token = user.Token
            };
        }

        // Changes the role of a user to "Owner" based on their email
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

        // Retrieves user information based on their email
        public async Task<UserResDto> GetUserInfoByEmailAsync(string email)
        {
            // Find the user by their email
            var user = await _userRepository.GetUserByEmailAsync(email);

            // If the user does not exist, return null
            if (user == null)
            {
                return null;
            }

            // Create and return the response DTO with the user details
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
                Token = user.Token
            };
        }

        // Retrieves user information based on their user ID
        public async Task<UserResDto> GetUserInfoByIdAsync(int userId)
        {
            // Find the user by their user ID
            var user = await _userRepository.GetUserByIdAsync(userId);

            // If the user does not exist, return null
            if (user == null)
            {
                return null;
            }

            // Create and return the response DTO with the user details
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
                Token = user.Token
            };
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task UpdateWalletBalanceAsync(int userId, decimal amount)
        {
            await _userRepository.UpdateWalletBalanceAsync(userId, amount);
        }
    }

}
