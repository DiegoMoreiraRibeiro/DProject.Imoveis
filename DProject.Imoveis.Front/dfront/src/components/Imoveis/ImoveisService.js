import axios from 'axios';


export default {
  async getEstados() {
    
    return await axios.get("https://servicodados.ibge.gov.br/api/v1/localidades/estados")
              .then((result) => {
                if (result.status == 200) {
                  
                  let estados = result.data;
                  let listEstados = new Array();
                  estados.forEach((element) => {
                    listEstados.push({ value: element.sigla, text: element.nome });
                  });
                  return listEstados
                }

              })
              .catch((err) => {
                
                console.log(err.response);
    });

  },

  async getCidades(estado){
    return await axios
      .get(
        "https://servicodados.ibge.gov.br/api/v1/localidades/estados/" +
          estado +
          "/distritos"
      )
      .then((response) => {
        return (response = response.data);
      })
      .then((data) => { 
        let listCidades = new Array();
        data.forEach((element) => {
          listCidades.push({ value: element.id, text: element.nome });
        });
        return listCidades;
      })
      .catch((err) => {
        
        console.log(err.response);
      });
  },

  async getImoveisAluguel(tipo, cidade, estado){
    return await axios
      .get(
      "https://localhost:5001/Imoveis?opcaoBuscaEnum="+tipo+"&cidade="+cidade+"&estado=" + estado
      )
      .then((response) => {
        return (response = response);
      })
      .then((data) => { 
        return data;
      })
      .catch((err) => {
        console.log(err.response);
      });
  }
};
