using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserEncrypt.Models;

namespace UserEncrypt.Services
{
    public interface IAuthorizationService
    {
        AuthResults Auth(string username, string password, out User user);
    }
    public enum AuthResults
    {
        Success,
        PasswordNotMatch,
        NotExists,
        Error
    }
}