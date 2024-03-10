using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibQTS
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        List<User> GetUsers();

        [OperationContract]
        User GetUserById(int id);

        [OperationContract]
        User GetUserByUserName(string username);

        [OperationContract]
        void DeleteUser(int id);

        [OperationContract]
        void UpdateUser(int id, User user);

        [OperationContract]
        void CreateUser(User user);
    }
}
