using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using U4_ControlUsuario.Models;
using U4_ControlUsuario.Models.ViewModels;
using U4_ControlUsuario.Repositories;

namespace U4_ControlUsuario.Controllers
{
    public class HomeController : Controller
    {
        controlCuentasContext context;

        public HomeController(controlCuentasContext ctx)
        {
            context = ctx;

        }

        [Authorize(Roles = "Usuario")]
        public IActionResult Index()
        {

            return View();
        }

        [AllowAnonymous]
        public IActionResult IniciarSesion()
        {            
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> IniciarSesion(string correo, string contraseña, bool persistencia)
        {
            Usuario user = context.Usuario.FirstOrDefault(x => x.CorreoElectronico.ToUpper() == correo.ToUpper());
            if (user != null)
            {
                if (user.Activo == 0)
                {
                    ModelState.AddModelError("", "No ha activado su cuenta.");
                    return View();
                }
                else
                {
                    if (user.CorreoElectronico.ToLower() == correo.ToLower() && user.Contrasena == Hasheo(contraseña))
                    {
                        List<Claim> cl = new List<Claim>();
                        cl.Add(new Claim(ClaimTypes.Name, "Usuario"));
                        cl.Add(new Claim(ClaimTypes.Role, "Usuario"));
                        cl.Add(new Claim("NombreUsuario", user.Username));
                        cl.Add(new Claim("IdUsuario", user.Id.ToString()));
                        var clIdentity = new ClaimsIdentity(cl, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(clIdentity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties { IsPersistent = persistencia });
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Se ha producido un error al proporcionar los datos");
                        return View();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "No hay ningun usuario registado con ese correo electronico.");
                return View();
            }
        }


        [AllowAnonymous]
        public IActionResult Registrarse()
        {
            //RegistrarseViewModel vm = new RegistrarseViewModel();
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Registrarse(RegistrarseViewModel vm)
        {
            //Condicionar si ya está el correo o el username agregados
            try
            {
                
                if(context.Usuario.Any(x=>x.CorreoElectronico==vm.NuevoUsuario.CorreoElectronico))
                {
                    ModelState.AddModelError("", "Ya existe un usario registrado con esa dirección de correo. Recupere su contraseña..");
                    return View(vm);
                }
                else
                {
                    if (vm.ConfirmarContraseña == vm.NuevoUsuario.Contrasena)
                    {
                        Repository repos = new Repository(context);
                        //Creo un random code 4 digitos
                        Random r = new Random();
                        int _min = 0000;
                        int _max = 9999;
                        vm.NuevoUsuario.Codigo = r.Next(_min, _max).ToString("0000");
                        vm.NuevoUsuario.Activo = 0;
                        vm.NuevoUsuario.Contrasena = Hasheo(vm.NuevoUsuario.Contrasena);
                        repos.Insert(vm.NuevoUsuario);
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("testingcodesistemas@gmail.com", "SheinAssociates");
                        message.To.Add(vm.NuevoUsuario.CorreoElectronico);
                        message.Subject = "Confirma tu identidad. Código de seguridad";
                        message.IsBodyHtml = true;
                        message.Body = $"<b>¡Gracias por unirte a nuestra comunidad! {vm.NuevoUsuario.Username}<b><br/>Con la finalidad de proteger tu cuenta te enviamos la clave {vm.NuevoUsuario.Codigo} que te ayudará a comprobar que eres tú." +
                            $"<br> ¿Dudas? <a>Contactanos aquí.<a>";
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("testingcodesistemas@gmail.com", "correofake1");
                        client.Send(message);

                        return RedirectToAction("IngresarCodigo", "Home", new { Id = vm.NuevoUsuario.Id });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ha ocurrido un error al registrar la contraseña.");
                        return View(vm);
                    }
                }
                   
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
            
            
        }



        
        public IActionResult ResetContraseña(int Id)
        {
            Repository repos = new Repository(context);
            RegistrarseViewModel vm = new RegistrarseViewModel();
            vm.NuevoUsuario = repos.GetById(Id);
            return View(vm);
        }

        
        [HttpPost]        
        public IActionResult ResetContraseña(RegistrarseViewModel vm)
        {
            Repository repos = new Repository(context);
            var original = repos.GetById(vm.NuevoUsuario.Id);
            var passHasheado = Hasheo(vm.NuevoUsuario.Contrasena);
            if (original != null)
            {
                if (vm.NuevoUsuario.Contrasena == vm.ConfirmarContraseña)
                {
                    original.Contrasena = passHasheado;
                    repos.Update(original);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Ha ocurrido un error en la verificación de passwords");
                    return View(vm);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]

        public IActionResult Rechazado()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult IngresarCodigo(int Id)
        {
            IngresarCodigoViewModel vm = new IngresarCodigoViewModel();
            vm.Id = Id;
            return View(vm);
        }

        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult IngresarCodigo(IngresarCodigoViewModel vm)
        {
            Repository repos = new Repository(context);
            var objectBD = repos.GetById(vm.Id);
            try
            {
                if(objectBD!=null)
                {
                    if(objectBD.Codigo==vm.codigoConfirmar)
                    {
                        if (objectBD.Activo == 1)
                        {
                            return RedirectToAction("ResetContraseña", "Home", new { id = objectBD.Id });
                        }
                        else
                        {
                            objectBD.Activo = 1;
                            repos.Update(objectBD);                          
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Código incorrecto.");
                        return View(vm);
                    }
                    
                }
                else
                {
                    return RedirectToAction("IniciarSesion");
                }

            }
            catch (Exception m)
            {
                ModelState.AddModelError("", m.Message);
                return View(vm);
            }
        }


        [Authorize(Roles = "Usuario")]
        public IActionResult EliminarPerfil(int Id)
        {
            Repository repos = new Repository(context);
            Usuario user= repos.GetById(Id);
            if(user!=null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }


        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public IActionResult EliminarPerfil(Usuario user)
        {
            Repository repos = new Repository(context);
            Usuario usuario = repos.GetById(user.Id);
            if (usuario != null)
            {
                HttpContext.SignOutAsync();
                repos.Delete(usuario);
                return RedirectToAction("Registrarse");
            }
            else
            {
                return View();
            }
        }

        
        [AllowAnonymous]
        public IActionResult RecuperarContraseña()
        {
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult RecuperarContraseña(Usuario user)
        {
            
            try
            {
                Repository repos = new Repository(context);
                
                if (user.CorreoElectronico != null)
                {
                    
                    Usuario userBD = repos.GetByCorreo(user.CorreoElectronico);
                    Random r = new Random();
                    int _min = 0000;
                    int _max = 9999;
                    userBD.Codigo = r.Next(_min, _max).ToString("0000");
                    repos.Update(userBD);                    
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("testingcodesistemas@gmail.com", "SheinAssociates");
                    message.To.Add(userBD.CorreoElectronico);
                    message.Subject = "A un solo paso de recuperar tu cuenta";
                    message.Body = $"Tu nuevo código es: {userBD.Codigo}";
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("testingcodesistemas@gmail.com", "correofake1");
                    client.Send(message);
                    return RedirectToAction("IngresarCodigo", "Home", new { id = userBD.Id });
                }
                else
                {
                    ModelState.AddModelError("", "Introduzca el correo.");
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(user);
            }
        }

        public string Hasheo(string cadena)
        {            
            byte[] datoCodificado = Encoding.UTF8.GetBytes(cadena);
            byte[] h = SHA256.Create().ComputeHash(datoCodificado);

            string stringHaseado = "";
            foreach (var item in h)
            {
                stringHaseado += item.ToString("x2");
            }
            return stringHaseado;
        }

        
    }
}
