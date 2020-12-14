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

            var directorBD = context.Director.FirstOrDefault(x => x.CorreoElectronico.ToLower() == correo.ToLower());

            var maestroBD = context.Maestro.FirstOrDefault(x => x.CorreoElectronico.ToLower() == correo.ToLower());


            if (directorBD != null)
            {
                if (directorBD.CorreoElectronico.ToLower() == correo.ToLower() &&Hashear(password) == directorBD.Contrasena)
                {
                    List<Claim> cl = new List<Claim>();
                    cl.Add(new Claim(ClaimTypes.Name, $"{directorBD.Nombre}"));
                    cl.Add(new Claim(ClaimTypes.Role, "Director"));
                    cl.Add(new Claim("Nombre", directorBD.Nombre));
                    //informacion.Add(new Claim("Correo electronico", director.Correo));

                    var clIdentity = new ClaimsIdentity(cl, CookieAuthenticationDefaults.AuthenticationScheme);
                    var clPrincipal = new ClaimsPrincipal(clIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, clPrincipal,
                        new AuthenticationProperties { IsPersistent = true });
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "No coincide los datos ingresados con los registrados.");
                    return View();
                }
            }

            if (maestroBD != null)
            {
                if (maestroBD.Activo == 1)
                {
                    if (maestroBD.CorreoElectronico.ToLower() == correo.ToLower() && Hashear(password) == maestroBD.Contrasena)
                    {
                        List<Claim> informacion = new List<Claim>();
                        informacion.Add(new Claim(ClaimTypes.Name, $"{maestroBD.Nombre}"));
                        informacion.Add(new Claim(ClaimTypes.Role, "Maestro"));
                        informacion.Add(new Claim("Nombre", maestroBD.Nombre));

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
                    ModelState.AddModelError("", "Director  por favor active su cuenta.");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Es necesario registrarse con anticipación para poder ingresar.");
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


        public IActionResult VerAlumnos()
        {

            Repository<Alumno> repos = new Repository<Alumno>(context);
            if (User.IsInRole("Maestro"))
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


        [HttpPost]
        public IActionResult AgregarDocente(Maestro maestroP, string contrasena1, string contrasena2)
        {
            try
            {

                Repository<Maestro> repos = new Repository<Maestro>(context);

                if (context.Maestro.Any(x => x.CorreoElectronico == maestroP.CorreoElectronico))
                {
                    ModelState.AddModelError("", "Ya se encuentra registrado un maestro con el mismo correo.");
                    return View(maestroP);
                }
                else
                {
                    if (contrasena1 == contrasena2)
                    {
                        maestroP.Contrasena = Hashear(contrasena1);
                        maestroP.Activo = 1;
                       // mae.CorreoElectronico = correo;
                        repos.Insert(maestroP);
                        return RedirectToAction("VerMaestros");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Verificar la igualdad de contraseñas.");
                        return View(maestroP);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(maestroP);
            }
        }


        [Authorize(Roles = "Director")]
        public IActionResult EditarMaestro(int id)
        {
            
            Repository<Maestro> repos = new Repository<Maestro>(context);
            var maestro = repos.GetById(id);
            if (maestro == null)
            {
                return RedirectToAction("VerMaestros");
            }
            else
                return View(maestro);
        }

        [HttpPost]
        public IActionResult EditarMaestro(Maestro maestroP, string contrasena1, string contrasena2, bool activo, string correo)
        {
            try
            {             
                Repository<Maestro> repos = new Repository<Maestro>(context);
                var maestro = repos.GetById(maestroP.Id);
                if (contrasena1 == contrasena2)
                {
                    if (maestro != null)
                    {
                        maestro.Nombre = maestroP.Nombre;
                        maestro.CorreoElectronico = correo;
                        maestro.Grupo = maestroP.Grupo;

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
                    ModelState.AddModelError("", "Verificar la igualdad de las contraseñas.");
                    return View(maestroP);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(maestroP);
            }
            return RedirectToAction("VerMaestros");
        }


        [Authorize(Roles = "Director, Maestro")]
        public IActionResult AgregarAlumno()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarAlumno(Alumno alumnoParametro)
        {
            try
            {
               
                Repository<Alumno> repos = new Repository<Alumno>(context);

                if (context.Alumno.Any(x => x.Nombre == alumnoParametro.Nombre))
                {
                    ModelState.AddModelError("", "Alumno registrado.");
                    return View(alumnoParametro);
                }
                else
                {
                    if (User.IsInRole("Maestro")) 
                    {
                        var maestroSesion = User.Claims.FirstOrDefault(x => x.Type == "Nombre").Value;

                        var maestrobd = context.Maestro.FirstOrDefault(x => x.Nombre == maestroSesion.ToString());

                        if (maestrobd.Grupo == alumnoParametro.Grupo)
                        {
                            alumnoParametro.IdMaestro = maestrobd.Id;                            
                            repos.Insert(alumnoParametro);
                        }
                        else
                        {
                            ModelState.AddModelError("", "No puede agregar un alumno a otro grupo.");
                            return View(alumnoParametro);
                        }
                    }
                    else 
                    {
                        var temp = context.Maestro.FirstOrDefault(x => x.Grupo == alumnoParametro.Grupo).Id;
                        var activoTemp = context.Maestro.FirstOrDefault(x => x.Id == temp).Activo;

                        if (activoTemp == 1)
                        {
                            alumnoParametro.IdMaestro = temp;
                            repos.Insert(alumnoParametro);
                        }
                        else
                        {
                            ModelState.AddModelError("", "No puede agregar un alumno a un maestro no activo.");
                            return View(alumnoParametro);
                        }

                    }
                    return RedirectToAction("VerAlumnos");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(alumnoParametro);
            }
        }


        [Authorize(Roles = "Director, Maestro")]
        public IActionResult EditarAlumno(int id)
        {            
            Repository<Alumno> repos = new Repository<Alumno>(context);
            var alumnoBD = repos.GetById(id);
            if (alumnoBD == null)
            {
                return RedirectToAction("VerAlumnos");
            }
            else
                return View(alumnoBD);

        }

        [HttpPost]
        public IActionResult EditarAlumno(Alumno alumnoP)
        {
            try
            {               
                Repository<Alumno> repos = new Repository<Alumno>(context);
                var alumnobd = context.Alumno.FirstOrDefault(x => x.Id == alumnoP.Id);

                if (alumnobd != null)
                {
                    alumnobd.Nombre = alumnoP.Nombre;
                    repos.Update(alumnobd);
                }
                else
                {
                    ModelState.AddModelError("", "No se encontró el alumno seleccionado.");
                    return View(alumnoP);
                }
                return RedirectToAction("VerAlumnos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(alumnoP);
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
            var hasc = SHA256.Create();
            byte[] gtbytes = System.Text.Encoding.UTF8.GetBytes(cadena);
            byte[] hasheado = hasc.ComputeHash(gtbytes);
            string c = "";
            foreach (var b in hasheado)
            {
                c += b.ToString("X2");
            }
            return c;
        }
    }
}
