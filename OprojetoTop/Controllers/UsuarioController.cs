using Microsoft.AspNetCore.Mvc;
using OprojetoTop.Models;
using OprojetoTop.Repositorio;

namespace OprojetoTop.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Login()
        {
            // retorna a página Login
            return View();
        }
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {

            var usuario = _usuarioRepositorio.ObterUsuario(email);

            if (usuario != null && usuario.Senha == senha)
            {
                return RedirectToAction("Index", "Produto");
            }

            ModelState.AddModelError("", "Email ou senha inválidos.");
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(string nome, string email, string senha)
        {
            Usuario usuario = new Usuario { Nome = nome, Email = email, Senha = senha };

            _usuarioRepositorio.Cadastrar(usuario);

            return RedirectToAction("Login", "Usuario");
        }
    }
}