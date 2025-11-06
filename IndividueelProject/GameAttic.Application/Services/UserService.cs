using AutoMapper;
using GameAttic.Domain;

namespace GameAttic.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public bool AddUser(User user)
        {
            RegistrationUserDto userDTO = _mapper.Map<RegistrationUserDto>(user);
            bool addedUser = _userRepository.AddUser(userDTO);
            return addedUser;
        }

        public List<User> GetAllUsers()
        {
            List<UserDto> userDTOs = _userRepository.GetAllUsers();
            List<User> users = new List<User>();
            foreach (UserDto userDTO in userDTOs)
            {
                users.Add(_mapper.Map<User>(userDTO));
            }
            return users;
        }

        public User GetUserById(Guid id)
        {
            UserDto userDTO = _userRepository.GetUserById(id)!;
            User user = _mapper.Map<User>(userDTO);
            return user;
        }

        public bool EditUser(User user)
        {
            RegistrationUserDto userDTO = _mapper.Map<RegistrationUserDto>(user);
            bool updatedUser = _userRepository.EditUser(userDTO);
            return updatedUser;
        }

        public bool DeleteUser(Guid id)
        {
            bool deletedUser = _userRepository.DeleteUser(id);
            return deletedUser;
        }

        public Guid? Login(string username, string password)
        {
            Guid? loginId = _userRepository.Login(username, password);
            return loginId;
        }

        public bool IsAdmin(Guid id)
        {
            int role = _userRepository.GetRole(id);
            Role enumRole = (Role)role;
            if (enumRole == Role.Admin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
