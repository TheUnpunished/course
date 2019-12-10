<template>

  <div>
    <div v-on:click="editThisSpring()">
      <div class="main" v-bind:style="{'background-color': springColor()}">
        <div style="display: inline-block;">
          <div class="name">
            {{name}}
          </div>
          <div class="description">
            {{description}}
          </div>
          <div class="description">
            Дата создания: {{dateShow}}
          </div>
          <div class="description">
            Статус: {{resolveStatus(status)}}
          </div>
        </div>
        <div style="display: inline-block; float: right;">
          <div v-show="goodWiki()">
            <div class="subname">

            </div>
            <div class="subname">
              Вики {{wikiShow.name}}
            </div>
            <div class="description">
              Описание: {{wikiShow.description}}
            </div>
            <div class="description">
              Файл: <a style="color: white; text-decoration: underline; cursor: pointer"
                       v-on:click.stop="goToUrl()">Ссылка</a>
            </div>
          </div>
        </div>
        <div class="description" v-show="this.tasks.length>0">
          <div class="noHover">
            Задания:
            <ul style="background-color: white">
              <li v-for="task in tasks" style="color:black"
              v-bind:style="{'color': resolveColor(task)}">{{task.theme}}</li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
    import {mapActions} from "vuex";

    export default {
    async mounted(){
      let wiki = await this.getWiki(this.id);
      this.tasks = await this.getTasksBySpring(this.id);
      if (this.wiki == undefined && wiki == undefined){
        this.wikiShow = {name: "", description: "", file_path: ""};
      }
      else{
        this.wikiShow = wiki;
      }
        let ts = (new Date(this.date));
        this.dateShow = ts.toDateString();
    },
      data() {
        return {
          wikiShow:{},
          dateShow: ""
        }
      },
      props:{
        id: "",
        name: "",
        description: "",
        status: "",
        wiki: {},
        date: "",
        tasks: []
    },
        name: "Spring",
      methods:{
      goodWiki(){
        return this.wiki != undefined;
      },
        ...mapActions([
          "setSpringId",
          "getWiki",
          "getTasksBySpring"
        ]),
        editThisSpring(){
          this.setSpringId(this.id);
          this.$router.push({name: "editSpring"});
        },
        goToUrl(){
          window.location = this.wikiShow.file_path;
        },
        resolveStatus(status){
          switch (status) {
            case "Open":{
              return "Открыта";
              break;
            }
            case "In Progress":{
              return "В прогрессе";
              break;
            }
            case "Closed":{
              return "Закрыта";
              break;
            }
          }
        },
        resolveColor(task){
          let color = "";
          switch (task.priority) {
            case "Highest":{
              color = "#d80404";
              break;
            }
            case "High":{
              color = "#d83503";
              break;
            }
            case "Medium":{
              color = "#d89402";
              break;
            }
            case "Low":
            {
              color = "#d8d001"
              break;
            }
            case "Lowest":
              color = "#9ed801";
              break;
          }
          if (task.status == "Closed"){
            color = "#999999";
          }
          return color;
        },
        springColor(){
          if (this.status == "Closed"){
            return "#999999"
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
    width: 500pt;
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
  .main .description .noHover :hover{
    background-color: white;
  }
</style>
