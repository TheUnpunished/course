<template>
  <div style="margin: 10pt 15pt;">
      <label>
        Имя проекта<br>
        <input name="projectName" type="text" v-model="projectForm.name"><br>
      </label>
      <label>
        Описание проекта<br>
        <input name="projectDesc" type="text" v-model="projectForm.description"><br>
      </label>
      <label>
        Публичный
        <input name="projectPub"type="checkbox" v-model="projectForm.public"><br>
      </label>
      <label>
        Репозиторий GitHub<br>
        <input name="projectGit" type="text" v-model="projectForm.repo"><br>
      </label>
      <a class="redMessage" v-show="badData()">Обязательные поля: имя проекта, его описание<br></a>
      <input name="submit" type="submit" v-on:click="addProjectAndReturn()" :disabled="badData()" value="Добавить">
    </div>
</template>

<script>
    import {mapActions} from "vuex";

    export default {
      async mounted(){
        await this.unauthorised(2);
      },
        name: "AddProject",
      data() {
        return {
          projectForm:{name: "", description: "", public: false, repo: ""}
        }
      },
      methods:{
        badData(){
          return this.projectForm.name === "" || this.projectForm.description === ""
        },
        ...mapActions([
          "addProject",
          "unauthorised"
        ]),
        addProjectAndReturn(){
          this.addProject(this.projectForm);
          this.$router.push({name:"projects"});
          this.$router.go();
        }
      }
    }
</script>

<style scoped>

</style>
