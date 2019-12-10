<template>
  <div class="menu">
    <div class="option" style="border: 0; padding-right: 0; padding-left: 0">
      <img src="../assets/img/logo.png" v-on:click="goToProjects()" class="logo" alt="https://www.google.com/url?sa=i&source=images&cd=&cad=rja&uact=8&ved=2ahUKEwi3wuL66ITiAhV9wsQBHe6rB0kQjRx6BAgBEAU&url=http%3A%2F%2Ft3.gstatic.com%2Fimages%3Fq%3Dtbn%3AANd9GcSkokrxxIX-EDiBFHl7z-gDrIscZSa2KDgl5Xm1-31053Zb1yvb&psig=AOvVaw1pGFVA6fkjwfyo0A5Yjdl2&ust=1557160478106703">
    </div>
    <div class="option">
      <a href="/app/logs" v-show="logged()">Логи</a>
    </div>
    <div class="option" v-show="managerOrAdmin() && logged()">
      <a href="/app/people">Персонал</a>
    </div>
    <div class="option" v-show="project_selected() && logged() || isWorker()">
      <a href="/app/tasks">Задачи</a>
    </div>
    <div class="option" v-show="project_selected() && managerOrAdmin() && logged()">
      <a href="/app/springs">Вехи</a>
    </div>
    <div class="option" v-show="project_selected() && managerOrAdmin() && logged()">
      <a href="/app/diagrams">Диаграмма</a>
    </div>
    <div class="option" style="border: 0px" v-show="project_selected() && managerOrAdmin() && logged()">
      <a href="/app/github">GitHub</a>
    </div>
    <div class="option" v-on:click="logout_redirect()" style="border: 0; float:right; vertical-align: middle; line-height: 26pt">
      <a href="" style="color: white; text-decoration: none">{{email}}</a>
    </div>
  </div>

</template>

<script>
  import {mapActions, mapState} from "vuex";
  import cookies from "js-cookie"
    export default {
      async mounted(){
        await this.setEmail();
        this.role = await this.getRole();
      },
        name: "MyMenu",
        computed:{
          ...mapState({
            email: state => state.email
          }),
      },
      data() {
        return {
          role:""
        }
      },
        methods: {
          ...mapActions([
            "setEmail",
            "logout",
            "getRole"
          ]),
          logout_redirect() {
            this.logout();
            this.$router.push({path: "/"});
            this.$router.go();
          },
          managerOrAdmin() {
            return this.role == 'Manager' || this.role == 'Admin';
          },
          logged() {
            return cookies.get("userId") != undefined;
          },
          async goToProjects() {
            this.$router.push({name: "loader"});
            this.$router.go();
          },
          project_selected() {
            let project_id = cookies.get("projectId");
            return project_id !== undefined;
          },
          isWorker() {
            return this.role == 'Worker';
          }
        }
    }

</script>

<style scoped>
  .menu {
    background-color: #353434;
    font-family: "Open Sans";
    color: white;
    padding: 5pt 10pt;
  }
  .menu .option{
    vertical-align: middle;
    display: inline-block;
    padding-right: 30pt;
    padding-left: 30pt;
    font-size: 10pt;
    border-right: 1.5pt white solid;
  }
  .menu .option:hover{
    text-decoration: underline;
  }
  .menu .option .logo{
    border: 0;
    height: 25pt;
    cursor: pointer;
  }
  a{
    color: white;
    text-decoration: none;
  }
</style>
