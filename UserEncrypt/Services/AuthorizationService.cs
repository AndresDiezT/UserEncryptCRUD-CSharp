using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserEncrypt.Context;
using UserEncrypt.Models;
using UserEncrypt.Security;

namespace UserEncrypt.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        // Instancia del context para la base de datos.
        private readonly UserEncryptContext db = new UserEncryptContext();
        // Instancia de la clase PasswordEncripter que se utiliza para encriptar la contraseña
        private readonly IPasswordEncripter _passwordEncripter = new PasswordEncripter();

        // Metodo principal para autenticar a un usuario por el Username y Password
        public AuthResults Auth(string username, string password, out User user)
        {
            // Buscar al usuario en la base de datos por el Username
            user = db.Users.Where(x => x.Username.Equals(username)).FirstOrDefault();

            // Si el usuario no existe se retorna un resultado de que no existe pues
            if (user == null)
                return AuthResults.NotExists;

            // Se encripta la contraseña proporcionada usando las clave almacenadas en el objeto user
            // El metodo Encript toma la Password y una lista de dos elementos: el HashKey y el HashIV del usuario
            string encriptedPassword = _passwordEncripter.Encript(password, new List<byte[]>
            {
                user.HashKey,
                user.HashIV
            });

            // Se compara la contraseña encriptada generada con la contraseña almacenada en la base de datos (PasswordHash)
            // Si no coinciden la autenticación falla y se devuelve un resultado indicando que la contraseña no coincide
            if (encriptedPassword != user.PasswordHash)
                return AuthResults.PasswordNotMatch;

            // Si la contraseña encriptada coincide con la almacenada en la base de datos la autenticación es exitosa
            return AuthResults.Success;
        }
    }
}