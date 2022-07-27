using DProject.Imoveis.Back.Services.Enums;
using DProject.Imoveis.Back.Services.ViewModel;

namespace DProject.Imoveis.Back.Services.Interfaces
{
    public interface IZapImoveisService
    {
        List<ImoveisViewModel> GetImoveisAluguel(OpcaoBuscaEnum opcaoBuscaEnum = OpcaoBuscaEnum.Aluguel, string cidade = "contagem", string estado = "mg");
    }
}
