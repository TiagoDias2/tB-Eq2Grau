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
        /// M�todo para calcular as raizes de uma equa��o de 2�grau
        /// </summary>
        /// <param name="A">Parametro do x^2</param>
        /// <param name="B">Parametro do x</param>
        /// <param name="C">parametro independente</param>
        /// <returns></returns>
        public IActionResult Index(string A, string B, string C)
        {
            // Vari�veis para armazenar os coeficientes convertidos de string para double
            double a, b, c;

            // Tente converter os coeficientes de string para double
            if (!double.TryParse(A, out a) || !double.TryParse(B, out b) || !double.TryParse(C, out c))
            {
                // Se a convers�o falhar, envie uma mensagem de erro para a view
                ViewBag.ErrorMessage = "Os coeficientes devem ser n�meros v�lidos.";
                return View();
            }

            // Verifique se a � diferente de zero para garantir que seja uma equa��o de segundo grau v�lida
            if (a == 0)
            {
                // Se 'a' for zero, n�o � uma equa��o de segundo grau v�lida, envie uma mensagem de erro para a view
                ViewBag.ErrorMessage = "O coeficiente 'a' deve ser diferente de zero para uma equa��o de segundo grau.";
                return View();
            }

            // Calcula o discriminante (delta)
            double delta = b * b - 4 * a * c;

            // Vari�veis para armazenar as ra�zes
            double x1, x2;

            // Verifica o valor do discriminante para determinar o tipo de ra�zes
            if (delta > 0)
            {
                // Duas ra�zes reais e distintas
                x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                x2 = (-b - Math.Sqrt(delta)) / (2 * a);
            }
            else if (delta == 0)
            {
                // Duas ra�zes reais e iguais
                x1 = x2 = -b / (2 * a);
            }
            else
            {
                // Duas ra�zes complexas conjugadas
                double realPart = -b / (2 * a);
                double imaginaryPart = Math.Sqrt(-delta) / (2 * a);
                // Construindo a parte real e imagin�ria
                string x1Complex = $"{realPart} + {imaginaryPart}i";
                string x2Complex = $"{realPart} - {imaginaryPart}i";
                // Enviando as ra�zes complexas para a view
                ViewBag.ResultMessage = $"As ra�zes s�o complexas: {x1Complex} e {x2Complex}";
                return View();
            }

            // Enviar as ra�zes reais para a view
            ViewBag.ResultMessage = $"As ra�zes reais s�o: x1 = {x1}, x2 = {x2}";
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
