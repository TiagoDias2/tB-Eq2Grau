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
            /* ALGORITMO:
             * 1- ler parâmetros A, B, C
             * 2- validar se se pode fazer operações com eles
             *    2.1- têm de ser números
             *    2.2- A != 0
             * 3- se posso calcular,
             *    3.1- determinar as raízes
             *         x=(-b +/- sqrt(b^2-4ac))/2/a
             *         3.1.1- calcular o delta: b^2-4ac
             *         3.1.2- se delta > 0, há 2 raízes reais distintas
             *                              x1 e x2
             *                se delta = 0, há 2 raízes reais iguais
             *                              x1=x2
             *                se delta < 0, há duas raízes complexas conjugadas
             *                              x1 = -b/(2a) + sqrt(-delta)/(2a) i
             *                              x2 = -b/(2a) - sqrt(-delta)/(2a) i
             *   3.2- enviar a resposta para o cliente
             * se não posso calcular (else)
             *    enviar mensagem de erro para o cliente (browser)
             */

            // vars. auxiliares
            double auxA = 0, auxB = 0, auxC = 0;
            ViewBag.mensagem = "";

            // 1.
            if (string.IsNullOrWhiteSpace(A) || string.IsNullOrWhiteSpace(B) || string.IsNullOrWhiteSpace(C))
            {
                // enviar mensagem para o utilizador

                ViewBag.mensagem = "Insira todos os valores";
                // devolver controlo à View
                return View();
            }

            // 1.
            // Tente converter os coeficientes de string para double
            if (!double.TryParse(A, out auxA) || !double.TryParse(B, out auxB) || !double.TryParse(C, out auxC))
            {
                // Se a conversão falhar, envie uma mensagem de erro para a view
                ViewBag.mensagem = "Os coeficientes devem ser números válidos.";
                return View();
            }

            
            // 2.
            if (auxA == 0)
            {
                // o A é ZERO.
                // enviar mensagem para o utilizador
                ViewBag.mensagem = "O coeficiente 'a' deve ser diferente de zero para uma equação de segundo grau.";

                // devolver controlo à View
                return View();
            }


            // 3.
            double delta = Math.Pow(auxB, 2) - 4 * auxC;
            // 3.1
            if (delta > 0)
            {
                string x1 = Math.Round((-auxB + Math.Sqrt(delta)) / 2 / auxA, 2) + "";
                string x2 = Math.Round((-auxB - Math.Sqrt(delta)) / 2 / auxA, 2) + "";
                // enviar mensagem para o utilizador
                ViewBag.mensagem = "A equação tem duas raizes mais distintas";
                ViewBag.x1 = x1;
                ViewBag.x2 = x2;

                // devolver controlo à View
                return View();
            }

            if (delta == 0)
            {
                string x = Math.Round(-auxB / 2 / auxA, 2) + "";

                // enviar mensagem para o utilizador

                ViewBag.mensagem = "A equação tem duas raizes mais distintas";
                ViewBag.x1 = x;
                // devolver controlo à View
                return View();
            }

            if (delta < 0)
            {
                string x1 = Math.Round((-auxB) / 2 / auxA, 2) + " + " + Math.Round(Math.Sqrt(-delta) / 2 / auxA, 2) + " i";
                string x2 = Math.Round((-auxB) / 2 / auxA, 2) + " - " + Math.Round(Math.Sqrt(-delta) / 2 / auxA, 2) + " i";
                // enviar mensagem para o utilizador
                ViewBag.mensagem = "A equação tem duas raizes mais distintas";
                ViewBag.x1 = x1;
                ViewBag.x2 = x2;

                // devolver controlo à View
                return View();
            }
            // se chegar aqui, alguma coisa correu muito mal...
            // devolver controlo à View
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
