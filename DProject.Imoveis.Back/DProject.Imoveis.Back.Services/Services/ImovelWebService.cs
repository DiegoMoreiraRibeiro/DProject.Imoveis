using DProject.Imoveis.Back.Services.Enums;
using DProject.Imoveis.Back.Services.Interfaces;
using DProject.Imoveis.Back.Services.ViewModel;
using DProject.Imoveis.Back.Utils.Geral;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DProject.Imoveis.Back.Services.Services
{
    public class ImovelWebService : IImovelWebService
    {
        public List<ImoveisViewModel> GetImoveisAluguel(OpcaoBuscaEnum opcaoBuscaEnum = OpcaoBuscaEnum.Aluguel, string cidade = "contagem", string estado = "mg")
        {
            var browser = new OpenQA.Selenium.Chrome.ChromeDriver("C:\\Users\\dimor\\Downloads\\Win_1000015_chromedriver_win32\\chromedriver_win32\\");
            var listImoveis = new List<ImoveisViewModel>();
            try
            {
                var enumOpcaoBusca = EnumDPrject.GetDescriptionFromEnumValue(opcaoBuscaEnum);

                // TODO: Criar enum ou configuração com URLs
                var url = "https://www.imovelweb.com.br";

                var urlFinal = $"{url}/imoveis-{enumOpcaoBusca}-{cidade}-{estado}.html";

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

                    valores.AddRange(doc.DocumentNode.SelectNodes("//div[@class='components__Price-sc-12dh9kl-4 inzZeR']").Select(node => Convert.ToDecimal(node.GetDirectInnerText().Split(" ")[1])));
                    desc.AddRange(doc.DocumentNode.SelectNodes("//div[@class='components__LocationAddress-ge2uzh-0 bBVWCw']").Select(node => node.GetDirectInnerText() != null ? node.GetDirectInnerText().ToString() : ""));
                    endereco.AddRange(doc.DocumentNode.SelectNodes("//div[@class='components__LocationLocation-ge2uzh-2 cGROxm']").Select(node => node.GetDirectInnerText() != null ? node.GetDirectInnerText().ToString() : ""));
                    link.AddRange(doc.DocumentNode.SelectNodes("//div[@class='postingCardstyles__PostingCardLayout-i1odl-0 kbwSaF']").Select(node => url + node.Attributes["data-to-posting"].Value));


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
            catch (Exception)
            {
                browser.Close();
                return listImoveis;
            }

        }
    }
}
