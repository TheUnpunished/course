<template>
  <div>
    <input name="addProject" v-show="admin()" type="submit" value="Добавить проект..." v-on:click="addProject()" style="margin: 10pt 15pt;">
    <div v-for="project in this.projects">
      <project-card :name="project.name" :description="project.description"
      :userId="project.userid" :id="project.id" :public="project.isPublic"
      :repo="project.repository"></project-card>
    </div>
  </div>
</template>

<script>
  import {mapActions, mapState} from "vuex";
  import projectCard from "../components/Project"
  import cookies from 'js-cookie'
    export default {
      async mounted(){
        await this.setProjects();
        cookies.remove("projectId");
        this.role = await this.getRole();
        await this.unauthorised(1);
      },
        name: "Projects",
      data() {
        return {
          role: ""
        }
      },
      methods: {
        ...mapActions([
          'setProjects',
          "getRole",
          "unauthorised"
        ]),
        addProject(){
          this.$router.push({name:"addProject"})
        },
        admin(){
          return this.role == "Admin"
        }
      },
        computed: {
        ...mapState({
          projects: state => state.projects,

        }),
      },
      components:{
        projectCard
      }
    }

</script>

<style scoped>

</style>
