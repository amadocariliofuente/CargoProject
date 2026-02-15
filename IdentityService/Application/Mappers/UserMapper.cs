using IdentityService.Application.Models;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Mappers;

public class UserMapper
{
    public static List<UsersModel> EntityToModelList(List<Users> users)
    {
        var result = new List<UsersModel>();
        foreach (var user in users)
        {
            var userModel = new UsersModel()
            {
                FirstName =  user.FirstName,
                SecondName =  user.SecondName,
                Age = user.Age,
                Email = user.Email!,
                CreatedDate =  user.CreatedDate,
                UserType = user.UserType
            };
            result.Add(userModel);
        }
        
        return result;
    }

    public static List<Users> ModelToEntityList(List<UsersModel> userModels)
    {
        var result = new List<Users>();
        foreach (var userModel in userModels)
        {
            var user = new Users()
            {
                FirstName =  userModel.FirstName,
                SecondName =  userModel.SecondName,
                Age = userModel.Age,
                Email = userModel.Email!,
                CreatedDate =  userModel.CreatedDate,
                UserType = userModel.UserType
            };
            result.Add(user);
        }
        
        return result;
    }
    
    public static Users ModelToEntity(UsersModel userModel)
    {
        var user = new Users()
        {
            FirstName =  userModel.FirstName,
            SecondName =  userModel.SecondName,
            Age = userModel.Age,
            Email = userModel.Email!,
            CreatedDate =  userModel.CreatedDate,
            UserType = userModel.UserType
        };

        return user;
    }
    
    public static UsersModel EntityToModel(Users user)
    {
        var userModel = new UsersModel()
        {
            FirstName =  user.FirstName,
            SecondName =  user.SecondName,
            Age = user.Age,
            Email = user.Email!,
            CreatedDate =  user.CreatedDate,
            UserType = user.UserType
        };
        return userModel;
    }
}