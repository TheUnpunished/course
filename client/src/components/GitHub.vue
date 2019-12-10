<template>
    <div class="color-changer" style="cursor: pointer" v-on:click="goToUrl()" v-bind:href="url">
      <div class="main">
        <div class="name">
          {{author}}
        </div>
        <div class="description">
          {{comment}}
        </div>
      </div>
    </div>
</template>

<script>
    export default {
        name: "GitHub",
      props:{
          url: "",
          author: "",
          comment: ""
      },
      methods:{
          goToUrl(){
            window.location = this.url;
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
    width: 500pt;
    color: white;
  }
  .main .name{
    font-size: 18pt;
    font-weight: bold;
    padding: 5pt 5pt;
  }
  .main .description{
    font-size: 10pt;
    padding: 5pt 5pt;
  }
  .main .subname{
    font-size: 14pt;
    padding: 5pt 5pt;
  }
</style>
