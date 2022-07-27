using DProject.Imoveis.Back.Services.Enums;
using DProject.Imoveis.Back.Services.Interfaces;
using DProject.Imoveis.Back.Services.ViewModel;
using DProject.Imoveis.Back.Utils.Geral;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;

namespace DProject.Imoveis.Back.Services.Services
{
    public class ZapImoveisService : IZapImoveisService
    {
        public List<ImoveisViewModel> GetImoveisAluguel(OpcaoBuscaEnum opcaoBuscaEnum, string cidade, string estado)
        {

            var browser = new OpenQA.Selenium.Chrome.ChromeDriver("C:\\Users\\dimor\\Downloads\\Win_1000015_chromedriver_win32\\chromedriver_win32\\");
            var listImoveis = new List<ImoveisViewModel>();
            try
            {
                var enumOpcaoBusca = EnumDPrject.GetDescriptionFromEnumValue(opcaoBuscaEnum);

                // TODO: Criar enum ou configuração com URLs
                var url = "https://www.zapimoveis.com.br";

                var urlFinal = $"{url}/{enumOpcaoBusca}/imoveis/{estado}+{cidade}/.html";

                var options = new ChromeOptions();

                browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                browser.Navigate().GoToUrl(urlFinal);
                browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                var results = browser.FindElement(By.TagName("body"));
                var doc = new HtmlDocument();
                doc.LoadHtml(results.GetAttribute("innerHTML"));




                if (opcaoBuscaEnum.Equals(OpcaoBuscaEnum.Aluguel))
                {
                    List<decimal> valores = new List<decimal>();
                    List<string> desc = new List<string>();
                    List<string> endereco = new List<string>();
                    List<string> link = new List<string>();
                    List<string> imagens = new List<string>();

                    valores.AddRange(from item in doc.DocumentNode.SelectNodes("//div[@class='simple-card__prices simple-card__listing-prices']") select Convert.ToDecimal(Regex.Replace(Regex.Replace(item.FirstChild.FirstChild.FirstChild.InnerText, "[A-Za-z ]", "").Replace("\r\n$", "").Replace("/ê", ""), "[A-Za-z ]", "")));

                    desc.AddRange(from item in doc.DocumentNode.SelectNodes("//div[@class='simple-card__title simple-card__listing-title']")
                                  let dsc = "" + item.InnerText
                                  select dsc);
                    endereco.AddRange(doc.DocumentNode.SelectNodes("//p[@class='color-dark text-regular text-margin-zero']").Select(node => node.InnerText));
                    link.AddRange(doc.DocumentNode.SelectNodes("//div[@class='card-container js-listing-card']").Select(node => "https://www.zapimoveis.com.br/imovel/" + node.Attributes["data-id"].Value));



                    for (int i = 0; i < desc.Count; i++)
                    {
                        listImoveis.Add(new()
                        {
                            Descricao = desc[i],
                            Valor = valores[i],
                            Endereco = endereco[i],
                            Link = link[i],
                            Imagem = "http://portal.ufvjm.edu.br/a-universidade/cursos/grade_curricular_ckan/loading.gif"

                        });
                    }
                }

                browser.Close();
                return listImoveis;
            }
            catch (Exception ex)
            {
                browser.Close();
                return listImoveis;
            }

        }
    }
}
