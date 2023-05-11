using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    IUserRepository _userRepository; 
    IMapper _mapper;

    public UserEFController(IConfiguration config, IUserRepository userRepository)
    {
        _userRepository = userRepository;

        _mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.CreateMap<UserToAddDto, User>();
        }));
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _userRepository.GetUsers();
        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    // public IEnumerable<WeatherForecast> Get()
    public User GetSingleUser(int userId)
    {
        return _userRepository.GetSingleUser(userId);
    }


    [HttpPut("EditUser")]
    // public IEnumerable<WeatherForecast> Get()
    public IActionResult EditUser(User user)
    {
        User? userData = _userRepository.GetSingleUser(user.UserId);
        if (userData != null) {
            userData.FirstName = user.FirstName;
            userData.LastName = user.LastName;
            userData.Email = user.Email;
            userData.Gender = user.Gender;
            userData.Active = user.Active;
            
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
        }
        throw new Exception ("User does not exist");
    }

    [HttpPost("AddUser")]
    // public IEnumerable<WeatherForecast> Get()
    public IActionResult AddUser(UserToAddDto user)
    {
        User userData = _mapper.Map<User>(user);

        _userRepository.AddEntity(userData);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception ("User does not exist");
    }


    [HttpDelete("DeleteUser/{userId}")]
    // public IEnumerable<WeatherForecast> Get()
    public IActionResult DeleteUser(int userId)
    {
        User? userData = _userRepository.GetSingleUser(userId);
        if (userData != null) {
            _userRepository.RemoveEntity<User>(userData);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
        }
        throw new Exception ("User does not exist");
    }


    [HttpGet("UserSalary/{userId}")]
    public UserSalary GetUserSalaryEF(int userId)
    {
        return _userRepository.GetUserSalary(userId);
    }

    [HttpPost("UserSalary")]
    public IActionResult PostUserSalaryEf(UserSalary userForInsert)
    {
        _userRepository.AddEntity(userForInsert);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Adding UserSalary failed on save");
    }


    [HttpPut("UserSalary")]
    public IActionResult PutUserSalaryEf(UserSalary userForUpdate)
    {
        UserSalary? userToUpdate = _userRepository.GetUserSalary(userForUpdate.UserId);

        if (userToUpdate != null)
        {
            _mapper.Map(userToUpdate, userForUpdate);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Updating UserSalary failed on save");
        }
        throw new Exception("Failed to find UserSalary to Update");
    }


    [HttpDelete("UserSalary/{userId}")]
    public IActionResult DeleteUserSalaryEf(int userId)
    {
        UserSalary? userToDelete = _userRepository.GetUserSalary(userId);

        if (userToDelete != null)
        {
            _userRepository.RemoveEntity<UserSalary>(userToDelete);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Deleting UserSalary failed on save");
        }
        throw new Exception("Failed to find UserSalary to delete");
    }


    [HttpGet("UserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfoEF(int userId)
    {
        return _userRepository.GetUserJobInfo(userId);
    }

    [HttpPost("UserJobInfo")]
    public IActionResult PostUserJobInfoEf(UserJobInfo userForInsert)
    {
        _userRepository.AddEntity(userForInsert);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Adding UserJobInfo failed on save");
    }


    [HttpPut("UserJobInfo")]
    public IActionResult PutUserJobInfoEf(UserJobInfo userForUpdate)
    {
        UserJobInfo? userToUpdate = _userRepository.GetUserJobInfo(userForUpdate.UserId);

        if (userToUpdate != null)
        {
            _mapper.Map(userToUpdate, userForUpdate);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Updating UserJobInfo failed on save");
        }
        throw new Exception("Failed to find UserJobInfo to Update");
    }


    [HttpDelete("UserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfoEf(int userId)
    {
        UserJobInfo? userToDelete = _userRepository.GetUserJobInfo(userId);

        if (userToDelete != null)
        {
            _userRepository.RemoveEntity<UserJobInfo>(userToDelete);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Deleting UserJobInfo failed on save");
        }
        throw new Exception("Failed to find UserJobInfo to delete");
    }
}
