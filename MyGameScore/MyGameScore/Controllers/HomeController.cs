using MyGameScore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyGameScore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DBContext db = new DBContext();
            Jogo jogo = new Jogo();

            var listaJogos = db.jogo.OrderBy(x => x.data_jogo).ToList();

            ViewBag.listaJogos = listaJogos;
            ViewBag.dataDefault = DateTime.Now.ToString("yyyy-MM-dd");


            return View();

        }

        public bool SalvarJogo(DateTime data, int pontuacao)
        {
            DBContext db = new DBContext();
            Jogo jogo = new Jogo();

            try
            {
                jogo.pontuacao = pontuacao;
                jogo.data_jogo = data;

                db.jogo.Add(jogo);
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExcluirJogo(int Id)
        {
            DBContext db = new DBContext();
            Jogo jogo = new Jogo();

            try
            {
                bool retorno = false;
                var result = from r in db.jogo where r.id == Id select r;

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        db.jogo.Remove(item);
                    }

                    db.SaveChanges();
                    retorno = true;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public JsonResult SelecionarJogo(int Id)
        {
            DBContext db = new DBContext();
            Jogo jogo = new Jogo();

            try
            {
                var resultJogo = from r in db.jogo where r.id == Id select r;
                string json = "";

                if (resultJogo != null)
                {
                    foreach (var item in resultJogo)
                    {
                        json = JsonConvert.SerializeObject(new
                        {
                            results = new List<Jogo>()
                            {
                                new Jogo { id = item.id, data_jogo = item.data_jogo, pontuacao = item.pontuacao }
                            }
                        });

                        return Json(json);
                    }
                }

                var resultDado = new { Sucesso = false };
                return Json(resultDado);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool EditarJogo(int id, DateTime data, int pontuacao)
        {
            DBContext db = new DBContext();
            Jogo jogo = new Jogo();

            try
            {
                bool retorno = false;
                var result = db.jogo.SingleOrDefault(x => x.id == id);

                if (result != null)
                {
                    jogo.id = id;
                    jogo.data_jogo = data;
                    jogo.pontuacao = pontuacao;

                    db.Entry(result).CurrentValues.SetValues(jogo);
                    db.SaveChanges();
                    retorno = true;
                }

                return retorno;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ActionResult Resultado()
        {
            DBContext db = new DBContext();
            Jogo jogo = new Jogo();

            var listaJogos = db.jogo.OrderBy(x => x.data_jogo).ToList();

            if (listaJogos != null && listaJogos.Count > 0)
            {
                var jogosDisputados = listaJogos.Count();
                var totalPontos = listaJogos.Sum(x => x.pontuacao);
                var mediaPontoJogo = totalPontos / jogosDisputados;
                var maiorPontuacao = listaJogos.Max(x => x.pontuacao);
                var menorPontuacao = listaJogos.Min(x => x.pontuacao);
                var dataPrimeiroJogo = listaJogos.Min(x => x.data_jogo);
                var dataUltimoJogo = listaJogos.Max(x => x.data_jogo);

                //Lógica para verificar quantidade de vezes que bateu o próprio recorde

                var qtdRecorde = 0;
                int pontuacaoAnterior = 0;
                int recorde = 0;
                var primeiraPontuacao = listaJogos.Select(x => x.pontuacao).First();

                foreach (var item in listaJogos)
                {
                    if (pontuacaoAnterior > 0)
                    {
                        if (item.pontuacao > pontuacaoAnterior && item.pontuacao > recorde && item.pontuacao > primeiraPontuacao)
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