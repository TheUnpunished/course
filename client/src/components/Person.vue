<template>
    <div class="color-changer">
      <div class="main">
        <div class="name">
          {{name}} {{surname}}
        </div>
        <div class="description">
          E-Mail: {{email}}<br>
          Должность: {{category}}<br>
          Язык: {{resolveLang(language)}}
        </div>
      </div>
    </div>
</template>

<script>
    import {mapActions} from "vuex";

    export default {
      async mounted(){
        let category = await this.getCategoryById(this.id);
        this.category = category.name;
      },
      props:{
        id: "",
        surname: "",
        name: "",
        email: "",
        language: "",
        password: "",
        admin: false
      },
      data() {
        return {
          category: ""
        }
      },
        name: "Person",
      methods:{
        ...mapActions([
          "getCategoryById",
          "setPersonId"
        ]),
        resolveLang(){
          switch (this.language) {
            case "en":{
              return "Английский"
            }
            case "cz":{
              return "Чешский"
            }
            case "de":{
              return "Немецкий"
            }
            case "ru":
              return "Русский"
          }
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
    width: 300pt;
    height: 100pt;
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
</style>
