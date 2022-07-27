const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  lintOnSave: false,
  configureWebpack: {
    devtool: 'source-map'
  },
  devServer: {
      proxy: 'https://servicodados.ibge.gov.br/',
  }
})
