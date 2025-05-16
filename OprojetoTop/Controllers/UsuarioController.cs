using Microsoft.AspNetCore.Mvc;
using OprojetoTop.Models;
using OprojetoTop.Repositorio;

namespace OprojetoTop.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly LoginRepositorio _loginRepositorio;
        public UsuarioController(LoginRepositorio loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;
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

            var usuario = _loginRepositorio.ObterLogin(email);

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

            _LoginRepositorio.Logar(usuario);

            return RedirectToAction("Login", "Usuario");
        }
    }
}