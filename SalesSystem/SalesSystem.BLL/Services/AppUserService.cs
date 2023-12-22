using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model;

namespace SalesSystem.BLL.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IGenericRepository<AppUser> _userRepository;
        private readonly IMapper _mapper;

        public AppUserService(IGenericRepository<AppUser> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to get all users List
        /// </summary>
        /// <returns></returns>
        public async Task<List<AppUserDTO>> List()
        {
            try
            {
                var userQuery = await _userRepository.Consult();
                var usersList = userQuery.Include(role => role.Role).ToList();

                return _mapper.Map<List<AppUserDTO>>(usersList);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to validate user credentials
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<SessionDTO> CredentialsValidation(string email, string password)
        {
            try
            {
                var userQuery = await _userRepository.Consult(u =>
                u.Email == email &&
                u.Pass == password
            );

                if (userQuery.FirstOrDefault() == null)
                    throw new TaskCanceledException("Invalid Credentials");

                AppUser response = userQuery.Include(role => role.Role).First();

                return _mapper.Map<SessionDTO>(response);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to create an user in DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AppUserDTO> Create(AppUserDTO model)
        {
            try
            {
                var createdUser = await _userRepository.Create(_mapper.Map<AppUser>(model));

                if(createdUser.UserId == 0)
                    throw new TaskCanceledException("User couldn't be created");

                var query = await _userRepository.Consult(u => u.UserId == createdUser.UserId);

                createdUser = query.Include(role => role.Role).First();

                return _mapper.Map<AppUserDTO>(createdUser);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to edit an user in DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Edit(AppUserDTO model)
        {
            try
            {
                var userModel = _mapper.Map<AppUser>(model);
                var userFound = await _userRepository.Get(u => u.UserId == userModel.UserId);

                if(userFound == null)
                    throw new TaskCanceledException("User doesn't exist");

                userFound.CompleteName = userModel.CompleteName;
                userFound.Email = userModel.Email;
                userFound.RoleId = userModel.RoleId;
                userFound.Pass = userModel.Pass;
                userFound.IsActive = userModel.IsActive;

                bool response = await _userRepository.Edit(userFound);

                if(!response)
                    throw new TaskCanceledException("User couldn't be updated");

                return response;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Delete an user from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            try
            {
                var userFound = await _userRepository.Get(u => u.UserId == id);

                if (userFound == null)
                    throw new TaskCanceledException("User doesn't exist");

                bool response = await _userRepository.Delete(userFound);

                if (!response)
                    throw new TaskCanceledException("User couldn't be deleted");

                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
