<template>
  <div style="margin: 10pt 15pt;">
    <label>
      Имя проекта<br>
      <input type="text" v-model="projectForm.name"><br>
    </label>
    <label>
      Описание проекта<br>
      <input type="text" v-model="projectForm.description"><br>
    </label>
    <div v-show="editable">
      <label>
        Публичный
        <input type="checkbox" v-model="projectForm.public"><br>
      </label>
    </div>
    <label>
      Репозиторий GitHub<br>
      <input type="text" v-model="projectForm.repo"><br>
    </label>
    <a v-show="badData()" class="redMessage">Обязательные поля: имя проекта, его описание<br></a>
    <input type="submit" v-on:click="editProjectAndReturn()" :disabled="badData()" value="Изменить">
  </div>
</template>

<script>
  import {mapActions, mapState} from "vuex";
  import cookies from 'js-cookie';

    export default {
      async mounted(){
        let projectId = cookies.get("projectId");
        if (projectId != undefined){
          let project = await this.getProject(projectId);
          let userId = cookies.get("userId");
          this.editable = userId == project.userid;
          this.projectForm = {name: project.name, description: project.description,
            public: project.isPublic, repo: project.repository};
        }
        else{
          this.$router.push({name: "loader"});
        }
        await this.unauthorised(2);
      },
        name: "EditProject",
      data() {
        return {
          projectForm:{name: "", description: "", public: false, repo: ""},
          editable: false
        }
      },
      computed:{
          ...mapState({
          })
      },
      methods:{
        ...mapActions([
          "getProject",
          "editProject",
          "unauthorised"
        ]),
        badData(){
          return this.projectForm.name === "" || this.projectForm.description === ""
        },
        editProjectAndReturn(){
          let obj = {id: this.id, name: this.projectForm.name,
          description: this.projectForm.description,
          public: this.projectForm.public,
          repository: this.projectForm.repo};
          this.editProject(obj);
          this.$router.push({name:"projects"});
          this.$router.go();
        }
      }
    }
</script>

<style scoped>
a{
  font-family: "Open Sans",serif;
  color: red;
}
</style>
