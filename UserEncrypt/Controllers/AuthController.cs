using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserEncrypt.Context;
using UserEncrypt.Models;
using UserEncrypt.Security;
using UserEncrypt.Services;

namespace UserEncrypt.Controllers
{
    public class AuthController : Controller
    {
        // Instancia del context para la base de datos.
        private readonly UserEncryptContext db = new UserEncryptContext();
        // Instancia de la clase PasswordEncripter que se utiliza para encriptar la contraseña
        private readonly IPasswordEncripter _paswordEncripter = new PasswordEncripter();
        // Instancia de la clase AuthorizationService que se utiliza para la autenticacion de un usuario
        private readonly IAuthorizationService _authorizationService = new AuthorizationService();

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost] // Se indica que metodo http se usa
        [ValidateAntiForgeryToken] // Se usa para proteger contra ataques CSRF(Cross-Site Request Forgery)
        [Authorize]
        public ActionResult Register(User user)
        {
            try
            {
                var userExist = db.Users.FirstOrDefault(u => u.Username == user.Username);
                if (userExist != null)
                {
                    ModelState.AddModelError("", "El usuario ya existe.");
                    return View(user);
                }
                // Se generan las claves para el cifrado
                var key = new byte[32];
                var iv = new byte[16];
                // Se usa RNGCryptoServiceProvider para generar valores aleatorios seguros
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(key);
                    rng.GetBytes(iv);
                }
                // se encripta la contraseña utilizando la Key y IV generados
                var passwordEncrypted = _paswordEncripter.Encript(user.PasswordHash, new List<byte[]> { key, iv });

                // Se crea un nuevo objeto de User donde se pone la contraseña cifrada y las claves generadas
                var newUser = new User
                {
                    Username = user.Username,
                    PasswordHash = passwordEncrypted,
                    HashKey = key,
                    HashIV = iv,
                    CreatedAt = DateTime.UtcNow
                };

                // Se añade el nuevo usuario a la base de datos
                db.Users.Add(newUser);
                // Se guardan los cambios en la base de datos
                db.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                // Log de errores relacionados con la base de datos como problemas al guardar los datos etc...
                Log.Error(dbEx, "Error to save user in database");
                // Mensaje de error para el usuario.
                ModelState.AddModelError("", "Hubo un problema al guardar los datos en la base de datos. Intentalo nuevamente");
            }
            catch (CryptographicException cryptoEx)
            {
                // Log de error de cifrado.
                Log.Error(cryptoEx, "Error to encript password");
                // Mensaje de error para el usuario.
                ModelState.AddModelError("", "Hubo un problema. Intentalo nuevamente");
            }
            catch (Exception ex)
            {
                // Log de error desconocido.
                Log.Error(ex, "Unknown error saving user");
                // Mensaje de error para el usuario.
                ModelState.AddModelError("", "Un error ha ocurrido mientras se creaba el usuario, intenta nuevamente.");
                return View(user);
            }

            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel userData)
        {
            try
            {
                // Se intenta autenticar al usuario utilizando el servicio de AuthorizationService
                User user;
                var result = _authorizationService.Auth(userData.Username, userData.Password, out user);

                // Dependiendo del resultado de la autenticacion se toman diferentes casos
                switch (result)
                {
                    // En caso que el resultado sea vacano
                    case AuthResults.Success:
                        // Si ya existe una cookie de autenticacion se elimina
                        if (Request.Cookies["AuthCookie"] != null)
                        {
                            var existingCookie = new HttpCookie("AuthCookie")
                            {
                                Expires = DateTime.Now.AddDays(-1)
                            };
                            Response.Cookies.Add(existingCookie);
                        }

                        var authTicket = new FormsAuthenticationTicket(
                            1,
                            userData.Username,
                            DateTime.Now,
                            DateTime.Now.AddHours(1),
                            false,
                            userData.Username
                            );

                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                        {
                            HttpOnly = true,
                            Expires = authTicket.Expiration
                        };

                        Response.Cookies.Add(authCookie);
                        return RedirectToAction("", "");

                    // En caso de que el usuario no exista
                    case AuthResults.NotExists:
                        // Se envia un mensaje de error al usuario
                        ModelState.AddModelError("", "El usuario no existe.");
                        break;

                    // En caso de que la contraseña no coincida
                    case AuthResults.PasswordNotMatch:
                        // Se envia un mensaje de error al usuario
                        ModelState.AddModelError("", "Contraseña incorrecta.");
                        break;

                    // En caso de que el error sea desconocido
                    default:
                        // Se envia un mensaje de error al usuario
                        ModelState.AddModelError("", "Error desconocido intenta nuevamente");
                        break;
                }
            }
            catch (Exception ex)
            {
                // Log de error de cualquier excepcion inesperada
                Log.Error(ex, "Error during authentication process");

                ModelState.AddModelError("", "Error desconocido intenta nuevamente");
            }
            // Si todo cool se redirige al usuario
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            ViewBag.SuccessMessage = "";
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var user = db.Users.FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    ModelState.AddModelError("", "El usuario no existe.");
                    return View(model);
                }

                var passwordEncrypted = _paswordEncripter.Encript(model.CurrentPassword, new List<byte[]> { user.HashKey, user.HashIV });

                if (passwordEncrypted != user.PasswordHash)
                {
                    ModelState.AddModelError("", "La contraseña actual es incorrecta.");
                    return View(model);
                }

                if (model.NewPassword != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "La nueva contraseña y la confirmación no coinciden.");
                    return View(model);
                }

                try
                {
                    var key = new byte[32];
                    var iv = new byte[16];
                    using (var rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(key);
                        rng.GetBytes(iv);
                    }

                    var newPasswordEncrypted = _paswordEncripter.Encript(model.NewPassword, new List<byte[]> { key, iv });

                    user.PasswordHash = newPasswordEncrypted;
                    user.HashKey = key;
                    user.HashIV = iv;

                    db.SaveChanges();

                    ViewBag.SuccessMessage = "Contraseña cambiada exitosamente.";
                    return RedirectToAction("", "");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error al cambiar la contraseña.");
                    ModelState.AddModelError("", "Hubo un problema al cambiar la contraseña. Intenta nuevamente.");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            // Eliminar la cookie de autenticación (si se usa)
            if (Request.Cookies["AuthCookie"] != null)
            {
                var cookie = new HttpCookie("AuthCookie")
                {
                    Expires = DateTime.Now.AddDays(-1) // Establecer la cookie para que expire
                };
                // Eliminar la cookie
                Response.Cookies.Add(cookie);
            }
            FormsAuthentication.SignOut();
            // Redirigir al usuario a la pagina de inicio de sesión o a la pagina principal
            return RedirectToAction("Login", "Auth");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
