using MyGameScore.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyGameScore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.dataDefault = DateTime.Now.ToString("yyyy-MM-dd");
            return View();

        }

        public ActionResult SalvarJogo(DateTime data, int pontuacao)
        {            
            DBContext db = new DBContext();
            Jogo jogo = new Jogo();

            jogo.pontuacao = pontuacao;
            jogo.data_jogo = data;

            db.jogo.Add(jogo);
            db.SaveChanges();

            return View();
        }

        public ActionResult Resultado()
        {
            
            DBContext db = new DBContext();
            Jogo jogo = new Jogo();            

            var listaJogos = db.jogo.ToList();

            if (listaJogos != null && listaJogos.Count > 0)
            {
                var jogosDisputados = listaJogos.Count();
                var totalPontos = listaJogos.Sum(x => x.pontuacao);
                var mediaPontoJogo = totalPontos / jogosDisputados;
                var maiorPontuacao = listaJogos.Max(x => x.pontuacao);
                var menorPontuacao = listaJogos.Min(x => x.pontuacao);
                var dataPrimeiroJogo = listaJogos.Min(x => x.data_jogo);
                var dataUltimoJogo = listaJogos.Max(x => x.data_jogo);

                var qtdRecorde = 0;
                int pontuacaoAnterior = 0;
                int recorde = 0;

                foreach (var item in listaJogos)
                {
                    if (pontuacaoAnterior > 0)
                    {
                        if (item.pontuacao > pontuacaoAnterior && item.pontuacao > recorde)
                        {
                            recorde = item.pontuacao;
                            qtdRecorde++;
                        }
                    }

                    pontuacaoAnterior = item.pontuacao;
                }

                ViewBag.totalJogos = jogosDisputados;
                ViewBag.totalPontos = totalPontos;
                ViewBag.mediaPontoJogo = mediaPontoJogo;
                ViewBag.maiorPontuacao = maiorPontuacao;
                ViewBag.menorPontuacao = menorPontuacao;
                ViewBag.dataPrimeiroJogo = dataPrimeiroJogo.ToString("dd/MM/yyyy");
                ViewBag.dataUltimoJogo = dataUltimoJogo.ToString("dd/MM/yyyy");
                ViewBag.qtdeRecorde = qtdRecorde;
                ViewBag.existeDados = true;
            }
            else
            {
                ViewBag.existeDados = false;
            }

            return View();
        }
    }
}