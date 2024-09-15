﻿using BookAudioSystem.Repositories.IRepositories;
using BookAudioSystem.Services.IService;

namespace BookAudioSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string GetEmailByUsername(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            return user?.Email;
        }
    }
}
