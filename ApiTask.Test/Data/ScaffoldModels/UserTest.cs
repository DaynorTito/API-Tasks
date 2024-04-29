using ApiTask.Data.ScaffoldModels;

namespace ApiTask.Test.Data.ScaffoldModels;
using Xunit;
using ApiTask.Test.Data.ScaffoldModels;

public class UserTest
{
    [Fact]
    public void User_Initialization_PropertiesAreSetCorrectly()
    {
        string id = "1";
        string username = "test_user";
        string email = "test@example.com";
        string password = "password123";
        
        var user = new User
        {
            Id = id,
            Username = username,
            Email = email,
            Passwd = password
        };

        Assert.Equal(id, user.Id);
        Assert.Equal(username, user.Username);
        Assert.Equal(email, user.Email);
        Assert.Equal(password, user.Passwd);
    }

    [Fact]
    public void User_GroupTasks_InitializedEmpty()
    {
        var user = new User();
        Assert.NotNull(user.GroupTasks);
        Assert.Empty(user.GroupTasks);
    }

    [Fact]
    public void User_Tasks_InitializedEmpty()
    {
        var user = new User();
        Assert.NotNull(user.Tasks);
        Assert.Empty(user.Tasks);
    }
}