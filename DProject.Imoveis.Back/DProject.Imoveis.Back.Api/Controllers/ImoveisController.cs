using DProject.Imoveis.Back.Services.Enums;
using DProject.Imoveis.Back.Services.Interfaces;
using DProject.Imoveis.Back.Services.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DProject.Imoveis.Back.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImoveisController : ControllerBase
    {
        private readonly IImovelWebService _imovelWebService;
        private readonly IZapImoveisService _zapImoveisService;
        public ImoveisController(IImovelWebService imovelWebService, IZapImoveisService zapImoveisService)
        {
            _imovelWebService = imovelWebService;
            _zapImoveisService = zapImoveisService;
        }

        [HttpGet(Name = "GetImoveis")]
        public List<ImoveisViewModel> GetAsync(OpcaoBuscaEnum opcaoBuscaEnum = OpcaoBuscaEnum.Aluguel, string cidade = "contagem", string estado = "mg")
        {
            try
            {
                var listRet = new List<ImoveisViewModel>();


                // Retorno Site: ZapImoveis
                var listZapImoveis = _zapImoveisService.GetImoveisAluguel(opcaoBuscaEnum, cidade, estado);
                if (listZapImoveis != null)
                    listRet.AddRange(from item in listZapImoveis select item);


                // Retorno Site: Imoveis Web
                var listImoveisViewModel = _imovelWebService.GetImoveisAluguel(opcaoBuscaEnum, cidade, estado);
                if (listImoveisViewModel != null)
                    listRet.AddRange(from item in listImoveisViewModel select item);

                return listRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpPost(Name = "GetImageUrl")]
        public List<ImagensUrlViewModel> GetImageUrl(string url, string webSite)
        {
            // TODO: Criar get image, por web site e tipo de anuncio (aluguel ou compra)
            var listRet = new List<ImagensUrlViewModel>();
            try
            {
                return listRet;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}