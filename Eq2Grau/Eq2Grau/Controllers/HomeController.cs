using Eq2Grau.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Eq2Grau.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Porta de entrada do pedidos feitos 
        /// Método para calcular as raizes de uma equação de 2ºgrau
        /// </summary>
        /// <param name="A">Parametro do x^2</param>
        /// <param name="B">Parametro do x</param>
        /// <param name="C">parametro independente</param>
        /// <returns></returns>
        public IActionResult Index(string A, string B, string C)
        {
            // Variáveis para armazenar os coeficientes convertidos de string para double
            double a, b, c;

            // Tente converter os coeficientes de string para double
            if (!double.TryParse(A, out a) || !double.TryParse(B, out b) || !double.TryParse(C, out c))
            {
                // Se a conversão falhar, envie uma mensagem de erro para a view
                ViewBag.ErrorMessage = "Os coeficientes devem ser números válidos.";
                return View();
            }

            // Verifique se a é diferente de zero para garantir que seja uma equação de segundo grau válida
            if (a == 0)
            {
                // Se 'a' for zero, não é uma equação de segundo grau válida, envie uma mensagem de erro para a view
                ViewBag.ErrorMessage = "O coeficiente 'a' deve ser diferente de zero para uma equação de segundo grau.";
                return View();
            }

            // Calcula o discriminante (delta)
            double delta = b * b - 4 * a * c;

            // Variáveis para armazenar as raízes
            double x1, x2;

            // Verifica o valor do discriminante para determinar o tipo de raízes
            if (delta > 0)
            {
                // Duas raízes reais e distintas
                x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                x2 = (-b - Math.Sqrt(delta)) / (2 * a);
            }
            else if (delta == 0)
            {
                // Duas raízes reais e iguais
                x1 = x2 = -b / (2 * a);
            }
            else
            {
                // Duas raízes complexas conjugadas
                double realPart = -b / (2 * a);
                double imaginaryPart = Math.Sqrt(-delta) / (2 * a);
                // Construindo a parte real e imaginária
                string x1Complex = $"{realPart} + {imaginaryPart}i";
                string x2Complex = $"{realPart} - {imaginaryPart}i";
                // Enviando as raízes complexas para a view
                ViewBag.ResultMessage = $"As raízes são complexas: {x1Complex} e {x2Complex}";
                return View();
            }

            // Enviar as raízes reais para a view
            ViewBag.ResultMessage = $"As raízes reais são: x1 = {x1}, x2 = {x2}";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
