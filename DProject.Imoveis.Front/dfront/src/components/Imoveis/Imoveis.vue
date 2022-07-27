<template>
  <b-container fluid class="main">
    <b-row>
      <b-col>
        <h1>D'Imovéis</h1>

        <h3 class="title-desc">
          Maneira facíl de buscar imovéis em um lugar só!
        </h3>
      </b-col>
      <b-col style="float: right; text-align: right">
        <div class="div-image">
          <b-img
            src="https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Zap_im%C3%B3veis_2021.svg/1200px-Zap_im%C3%B3veis_2021.svg.png"
            fluid
            alt="Fluid image"
            style="max-width: auto; height: 40px"
          ></b-img>
        </div>
        &nbsp;&nbsp;
        <div class="div-image">
          <b-img
            src="https://produto.imovelweb.com.br/2016/WImoveis/imovelweb-logotipo.png"
            fluid
            alt="Fluid image"
            style="max-width: auto; height: 40px"
          ></b-img>
        </div>
      </b-col>
      <hr />
      <br />
      <br />
    </b-row>
    <b-row>
      <b-col>
        <b-form-select
          v-model="selectedImovel"
          :options="tipoImoveis"
          aria-disabled="compra"
          class="select-customn"
        ></b-form-select>
      </b-col>

      <b-col>
        <b-form-select
          v-model="selectedEstado"
          :options="estados"
          @change="listarCidade($event)"
          class="select-customn"
        ></b-form-select>
      </b-col>

      <b-col>
        <b-form-select
          v-model="selectedCidade"
          :options="cidades"
          @change="getCidade($event)"
          class="select-customn"
        ></b-form-select>
      </b-col>

      <b-col>
        <b-button
          class="btn-pesquisa"
          variant="success"
          @click="pesquisarImoveis()"
          >Pesquisar</b-button
        >
      </b-col>
    </b-row>
    <b-row>
      <div
        class="content-imo"
        v-for="(item, index) in imoveis"
        :key="item.id"
        :tabindex="index"
        @click="getUrl(item.Link)"
      >
        <b-card
          :img-src="getImgUrl(item.Imagem)"
          img-alt="Card image"
          img-left
          class="mb-3"
          style="max-width: auto; height: 250px"
        >
          <b-card-text>
            <div>
              <b>R$ {{ item.Valor }}</b> &nbsp;&nbsp;&nbsp; | &nbsp;&nbsp;&nbsp;
              {{ item.Descricao }}
            </div>
            <br />
            <div>
              {{ item.Endereco }}
            </div>
          </b-card-text>
        </b-card>
      </div>
      <div class="isLoadingContent" v-if="isLoadingContent">
        <b-img
          src="http://portal.ufvjm.edu.br/a-universidade/cursos/grade_curricular_ckan/loading.gif"
          fluid
          alt="Fluid image"
          style="max-width: auto; height: 100px"
        ></b-img>
      </div>
      <div class="isLoadingContent" v-if="isNotFound">
        <h3>Ops, não encontramos nenhum imovél</h3>
      </div>
    </b-row>
  </b-container>
</template>

<script>
import "./ImoveisStyle.css";
import ImoveisService from "./ImoveisService";

var listEstados = new Array();
var listCidades = new Array({ value: "", text: "Selecione uma cidade" });
var listImoveis = new Array();

async function getImoveis(tipo, estado, cidade) {
  if (tipo == "") tipo = "aluguel";

  if (estado == "") estado = "mg";

  if (cidade == "") cidade = "contagem";

  tipo = tipo.toLowerCase();
  estado = estado.toLowerCase();
  cidade = cidade.toLowerCase();
  cidade = retiraAcentos(cidade);
  let listCidade = cidade.split(" ");
  let retCidade = "";
  debugger;
  if (listCidade.length == 1) {
    retCidade = cidade;
  } else {
    listCidade.forEach((element) => {
      retCidade += "-" + element;
    });
    retCidade = retCidade.substring(1);
  }

  await ImoveisService.getImoveisAluguel(tipo, retCidade, estado)
    .then((response) => {
      return (response = response);
    })
    .then((data) => {
      listImoveis = data.data;
      return listImoveis;
    })
    .catch((err) => {
      console.log(err.response);
    });
}
function retiraAcentos(str) {
  let com_acento =
    "ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝŔÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿŕ";
  let sem_acento =
    "AAAAAAACEEEEIIIIDNOOOOOOUUUUYRsBaaaaaaaceeeeiiiionoooooouuuuybyr";
  let novastr = "";
  for (let i = 0; i < str.length; i++) {
    let troca = false;
    for (let a = 0; a < com_acento.length; a++) {
      if (str.substr(i, 1) == com_acento.substr(a, 1)) {
        novastr += sem_acento.substr(a, 1);
        troca = true;
        break;
      }
    }
    if (troca == false) {
      novastr += str.substr(i, 1);
    }
  }
  return novastr;
}

async function loadCidades(estado, init = false) {
  if (init) {
    listCidades = new Array();
    return await ImoveisService.getCidades(estado)
      .then((response) => {
        return (response = response);
      })
      .then((data) => {
        data.forEach((element) => {
          listCidades.push({ value: element.text, text: element.text });
        });
        return listCidades.sort((a, b) =>
          a.text.toLowerCase() > b.text.toLowerCase() ? 1 : -1
        );
      })
      .catch((err) => {
        console.log(err.response);
      });
  } else {
    return listCidades;
  }
}

function loadEstados() {
  listEstados = new Array();
  listEstados.push({ value: "", text: "Selecione um Estado" });
  ImoveisService.getEstados()
    .then((result) => {
      let estados = result;
      estados.forEach((element) => {
        listEstados.push({ value: element.value, text: element.text });
      });
      listEstados.sort((a, b) =>
        a.text.toLowerCase() > b.text.toLowerCase() ? 1 : -1
      );
    })
    .catch((err) => {
      console.log(err.response);
    });
  return listEstados.sort((a, b) =>
    a.text.toLowerCase() > b.text.toLowerCase() ? 1 : -1
  );
}

export default {
  name: "Imoveis",
  props: {
    msg: String,
  },

  data() {
    return {
      selected: "",
      selectedImovel: "aluguel",
      selectedEstado: "",
      selectedCidade: "",
      estados: loadEstados(),
      cidades: listCidades,
      isLoadingContent: false,
      isNotFound: false,
      tipoImoveis: new Array(
        { value: "aluguel", text: "Aluguel" },
        { value: "compra", text: "Compra" }
      ),
      imoveis: listImoveis,
    };
  },

  methods: {
    async listarCidade(event) {
      this.selectedEstado = event;
      this.cidades = await loadCidades(event, true);
    },

    getCidade(event) {
      this.selectedCidade = event;
    },

    async pesquisarImoveis() {
      this.isLoadingContent = true;
      this.isNotFound = false;
      await getImoveis(
        this.selectedImovel,
        this.selectedEstado,
        this.selectedCidade
      );
      this.imoveis = listImoveis;
      if (this.imoveis.length == 0) this.isNotFound = true;
      this.isLoadingContent = false;
    },

    getImgUrl: function (path) {
      return path;
    },

    getUrl(link) {
      var a = document.createElement("a");
      a.target = "_blank";
      a.href = link;
      a.click();
    },
  },
};
</script>
