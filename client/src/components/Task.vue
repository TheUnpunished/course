<template>
    <div class="color-changer" v-on:click="edit()">
      <div class="main" v-bind:style="{'background-color': resolveColor()}">
        <div>
          <div class="name" style="display: inline-block" >
            {{theme}}
          </div>
          <div class="name" v-show="!isClosed()" style="display: inline-block; float: right; cursor: pointer" v-on:click.stop="doThis()">
            x
          </div>
        </div>
        <div class="description">
          {{description}}
        </div>
        <div>
          <div class="description">
            Проект: {{project.name}}
          </div>
        </div>
        <div class="description" style="display: inline-block">
          Назначено: {{whoCanShow.name}} {{whoCanShow.surname}}
        </div>
        <div class="description" style="float: right; vertical-align: top; display: inline-block" >
          Отдел: {{categoryShow.name}}
        </div>
        <div>
          <div class="description" style="display: inline-block">
            Приоритет: {{resolvePriority(priority)}}
          </div>
          <div class="description" style="display: inline-block; float: right">
            Статус: {{resolveStatus(status)}}
          </div>

        </div>
        <div>
          <div class="description">
            Комментарий: {{percent}}
          </div>
          <div class="description">
            Дата завершения: {{date_show}}
          </div>
          <div class="description">
            Дней на исполнение: {{time}}
          </div>
        </div>
        <div v-show="goodSpring()">
          <div class="subname">
            Веха {{springShow.name}}
          </div>
          <div class="description">
            Описание: {{springShow.description}}
          </div>
          <div class = "description">
            Статус: {{resolveStatus(springShow.status)}}
          </div>
        </div>
      </div>
    </div>
</template>

<script>
    import {mapActions} from "vuex";
    export default {
      async mounted(){
        this.whoCanShow = await this.getPersonById(this.whoCan.id);
        this.springShow = await this.getSpring(this.spring.id);
        if (this.whoCanShow == undefined || this.whoCanShow.name == undefined || this.whoCanShow.surname == undefined){
          this.whoCanShow['name']= "\<нет\>";
          this.whoCanShow['surname'] = "";
        }
        if (this.category == undefined){
          this.categoryShow.name = "\<нет\>"
        }
        else{
          this.categoryShow.name = this.category.name;
        }
        let id = this.project_id.id;
        let newProject = await this.getProject(id);
        this.project = newProject;
        if (this.springShow == undefined){
          this.springShow == {name: "", description: "", status: ""}
        }
        if(this.date_over != undefined){
          this.date_show = new Date(this.date_over).toDateString();
        }
      },
      data() {
        return {
          whoCanShow: {name: "", surname: ""},
          categoryShow: {name: ""},
          project: {},
          springShow: {},
          date_show: "",
        }
      },
        name: "Task",
      props:{
          theme: "",
          tracker: "",
          status: "",
          priority: "",
          description: "",
          category: "",
          spring: "",
          date_over: "",
          time: "",
          percent: "",
          file_path: "",
          whoCan: {},
          project_id: "",
          id: ""
      },
      methods:{
        async edit() {
          let role = await this.getRole();
          if(role == "Worker"){
            let currentId = this.id;
            let messange = window.prompt("Введите URL для файла", "");
            let confirmForm = {id: currentId,
            file_path: message};
            this.$router.push({name:"loader"});
          }
          else{
            await this.setTaskId(this.id);
            this.$router.push({name: "editTask"})
          }
        },
        async doThis(){
          let currentId = this.id;
          let message = window.prompt("Введите URL для файла", "");
          let confirmForm = {id: currentId,
            file_path: message};
          await this.completeTask(confirmForm);
          this.$router.push({name:"loader"});
        },
        ...mapActions([
          "getProject",
          "deleteTask",
          "setTaskId",
          "getRole",
          "completeTask",
          "getPersonById",
          "getSpring"
        ]),
        goodSpring(){
          return this.spring != undefined;
        },
        isClosed(){
          return this.status == "Closed";
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
        resolvePriority(priority){
          switch (priority) {
            case "Highest":{
              return "Наивысший";
              break;
            }
            case "High":{
              return "Высокий";
              break;
            }
            case "Medium":{
              return "Средний";
              break;
            }
            case "Low":{
              return "Низкий";
              break;
            }
            case "Lowest":{
              return "Наименьший";
              break;
            }
          }
        },
        resolveColor(){
          let color = "";
          switch (this.priority) {
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
          if (this.status == "Closed"){
            color = "#999999";
          }
          return color;
        }
      },
      computed:{

      }
    }
</script>

<style scoped>
  .color-changer :hover{
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
