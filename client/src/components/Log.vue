<template>
    <div class="color-changer">
      <div class="main">
        <div class="name">
          {{name}}
        </div>
        <div class="description">
          {{description}}
        </div>
        <div class="description">
          Файл: <a v-on:click="goToUrl()" style="color: white; text-decoration: underline;
           cursor:pointer;">Cсылка</a>
        </div>
      </div>
    </div>
</template>

<script>
    export default {
        name: "Log",
      props:{
          name: "",
          description: "",
          file_path: ""
      },
      methods:{
          goToUrl(){
            window.location = this.file_path;
          }
      }
    }
</script>

<style scoped>
  .color-changer :hover{
    background-color: cornflowerblue;
  }
  .main{
    background-color: #4285F4;
    font-family: "Open Sans",serif;
    margin: 10pt 15pt;
    width: 550pt;
    color: white;
  }
  .main .name{
    font-size: 18pt;
    font-weight: bold;
    padding: 5pt 5pt;
  }
  .main .subname{
    font-size: 14pt;
    padding: 5pt 5pt;
  }
  .main .description{
    font-size: 10pt;
    padding: 5pt 5pt;
  }
</style>
