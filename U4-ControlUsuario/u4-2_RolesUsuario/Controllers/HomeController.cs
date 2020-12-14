using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using u4_2_RolesUsuario.Models;
using u4_2_RolesUsuario.Repositories;

namespace u4_2_RolesUsuario.Controllers
{
    public class HomeController : Controller
    {
        sistemadirectorContext context;

        public HomeController(sistemadirectorContext ctx)
        {
            context = ctx;

        }

        [Authorize(Roles = "Director, Maestro")]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string password)
        {
           
            Repository<Director> repos = new Repository<Director>(context);
            Repository<Maestro> reposMaestro = new Repository<Maestro>(context);

            var dir = context.Director.FirstOrDefault(x => x.CorreoElectronico.ToLower() == correo.ToLower());

            var mae = context.Maestro.FirstOrDefault(x => x.CorreoElectronico.ToLower() == correo.ToLower());


            if (dir != null)
            {
                if (dir.CorreoElectronico.ToLower() == correo.ToLower() &&Hashear(password) == dir.Contrasena)
                {
                    List<Claim> informacion = new List<Claim>();
                    informacion.Add(new Claim(ClaimTypes.Name, $"{dir.Nombre}"));
                    informacion.Add(new Claim(ClaimTypes.Role, "Director"));
                    informacion.Add(new Claim("Nombre", dir.Nombre));
                    //informacion.Add(new Claim("Correo electronico", director.Correo));

                    var claimidentity = new ClaimsIdentity(informacion, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimprincipal = new ClaimsPrincipal(claimidentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimprincipal,
                        new AuthenticationProperties { IsPersistent = true });
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "El correo o la contraseña están incorrectas.");
                    return View();
                }
            }

            if (mae != null)
            {
                if (mae.Activo == 1)
                {
                    if (mae.CorreoElectronico.ToLower() == correo.ToLower() && Hashear(password) == mae.Contrasena)
                    {
                        List<Claim> informacion = new List<Claim>();
                        informacion.Add(new Claim(ClaimTypes.Name, $"{mae.Nombre}"));
                        informacion.Add(new Claim(ClaimTypes.Role, "Maestro"));
                        informacion.Add(new Claim("Nombre", mae.Nombre));

                        var claimidentity = new ClaimsIdentity(informacion, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimprincipal = new ClaimsPrincipal(claimidentity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimprincipal,
                            new AuthenticationProperties { IsPersistent = true });
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "El correo o la contraseña están incorrectas.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "El director no ha activado su cuenta.");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "El correo no se encuentra registrado.");
                return View();
            }

        }

        [AllowAnonymous]
        
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult VerMaestros()
        {
           
            Repository<Maestro> repos = new Repository<Maestro>(context);
            return View(repos.GetAll().OrderBy(x => x.Nombre));
        }

        public IActionResult Denegado()
        {
            return View();
        }

        [Authorize(Roles = "Director")]
        public IActionResult AgregarDocente()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AgregarDocente(Maestro mae, string contrasena1, string contrasena2)
        {
            try
            {

                Repository<Maestro> repos = new Repository<Maestro>(context);

                if (context.Maestro.Any(x => x.CorreoElectronico == mae.CorreoElectronico))
                {
                    ModelState.AddModelError("", "Ya se encuentra registrado un maestro con el mismo correo.");
                    return View(mae);
                }
                else
                {
                    if (contrasena1 == contrasena2)
                    {
                        mae.Contrasena = Hashear(contrasena1);
                        mae.Activo = 1;
                       // mae.CorreoElectronico = correo;
                        repos.Insert(mae);
                        return RedirectToAction("VerMaestros");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Verificar la igualdad de contraseñas.");
                        return View(mae);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(mae);
            }
        }


        [Authorize(Roles = "Director")]
        public IActionResult EditarMaestro(int id)
        {
            
            Repository<Maestro> repos = new Repository<Maestro>(context);
            var mae = repos.GetById(id);
            if (mae == null)
            {
                return RedirectToAction("VerMaestros");
            }
            else
                return View(mae);
        }

        [HttpPost]
        public IActionResult EditarMaestro(Maestro mae, string contrasena1, string contrasena2, bool activo, string correo)
        {
            try
            {             
                Repository<Maestro> repos = new Repository<Maestro>(context);
                var maestro = repos.GetById(mae.Id);
                if (contrasena1 == contrasena2)
                {
                    if (maestro != null)
                    {
                        maestro.Nombre = mae.Nombre;
                        maestro.CorreoElectronico = correo;
                        maestro.Grupo = mae.Grupo;

                        maestro.Contrasena = Hashear(contrasena1);

                        if (activo == false)
                        {
                            maestro.Activo = 0;
                        }
                        else
                        {
                            maestro.Activo = 1;
                        }

                        repos.Update(maestro);
                    }
                    else
                    {
                        ModelState.AddModelError("", "No existe el maestro seleccionado.");
                        return RedirectToAction("VerMaestros");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Las contraseñas no coinciden.");
                    return View(mae);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(mae);
            }
            return RedirectToAction("VerMaestros");
        }


        public IActionResult VerAlumnos()
        {
          
            Repository<Alumno> repos = new Repository<Alumno>(context);
            if (User.IsInRole("VerMaestro"))
            {
                var currentMaestro = User.Claims.FirstOrDefault(x => x.Type == "Nombre").Value;
                var maestrobd = context.Maestro.FirstOrDefault(x => x.Nombre == currentMaestro.ToString());

                return View(repos.GetAll().OrderBy(x => x.Grupo).Where(x => x.IdMaestro == maestrobd.Id));
            }
            else
            {
                return View(repos.GetAll().OrderBy(x => x.Grupo));
            }
        }

        [Authorize(Roles = "Director, Maestro")]
        public IActionResult AgregarAlumno()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarAlumno(Alumno alu)
        {
            try
            {
               
                Repository<Alumno> repos = new Repository<Alumno>(context);

                if (context.Alumno.Any(x => x.Nombre == alu.Nombre))
                {
                    ModelState.AddModelError("", "Ya se encuentra registrado un alumno con el mismo nombre.");
                    return View(alu);
                }
                else
                {
                    if (User.IsInRole("Maestro")) //Claim Maestro
                    {
                        var currentMaestro = User.Claims.FirstOrDefault(x => x.Type == "Nombre").Value;

                        var maestrobd = context.Maestro.FirstOrDefault(x => x.Nombre == currentMaestro.ToString());

                        if (maestrobd.Grupo == alu.Grupo)
                        {
                            alu.IdMaestro = maestrobd.Id;                            
                            repos.Insert(alu);
                        }
                        else
                        {
                            ModelState.AddModelError("", "No puede agregar un alumno a otro grupo.");
                            return View(alu);
                        }
                    }
                    else //Claim Director
                    {
                        var temp = context.Maestro.FirstOrDefault(x => x.Grupo == alu.Grupo).Id;
                        var activoTemp = context.Maestro.FirstOrDefault(x => x.Id == temp).Activo;

                        if (activoTemp == 1)
                        {
                            alu.IdMaestro = temp;
                            repos.Insert(alu);
                        }
                        else
                        {
                            ModelState.AddModelError("", "No puede agregar un alumno a un maestro no activo.");
                            return View(alu);
                        }

                    }
                    return RedirectToAction("VerAlumnos");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(alu);
            }
        }


        [Authorize(Roles = "Director, Maestro")]
        public IActionResult EditarAlumno(int id)
        {            
            Repository<Alumno> repos = new Repository<Alumno>(context);
            var alu = repos.GetById(id);
            if (alu == null)
            {
                return RedirectToAction("VerAlumnos");
            }
            else
                return View(alu);

        }

        [HttpPost]
        public IActionResult EditarAlumno(Alumno alu)
        {
            try
            {               
                Repository<Alumno> repos = new Repository<Alumno>(context);
                var alumnobd = context.Alumno.FirstOrDefault(x => x.Id == alu.Id);

                if (alumnobd != null)
                {
                    alumnobd.Nombre = alu.Nombre;
                    repos.Update(alumnobd);
                }
                else
                {
                    ModelState.AddModelError("", "No se encontró el alumno seleccionado.");
                    return View(alu);
                }
                return RedirectToAction("VerAlumnos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(alu);
            }
        }

            public IActionResult EliminarAlumno(int id)
        {
            
            Repository<Alumno> repos = new Repository<Alumno>(context);
            var alu = repos.GetById(id);

            if (alu != null)
            {
                repos.Delete(alu);
                return RedirectToAction("VerAlumnos");
            }
            else
            {
                ModelState.AddModelError("", "No se ha podido completar la eliminación del alumno.");
                return RedirectToAction("VerAlumnos");
            }
        }

        public static string Hashear(string cadena)
        {
            var alg = SHA256.Create();
            byte[] codificar = System.Text.Encoding.UTF8.GetBytes(cadena);
            byte[] hash = alg.ComputeHash(codificar);
            string res = "";
            foreach (var b in hash)
            {
                res += b.ToString("X2");
            }
            return res;
        }
    }
}
