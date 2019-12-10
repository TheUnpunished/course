<template>
  <div class="color-changer" v-on:click="selectProject()">
    <div class="main">
      <div  class="name" v-show="admin()"  style="float: right; vertical-align: top; cursor: pointer" v-on:click.stop="deleteThisProject()">
        X
      </div>
      <div  class="name" v-show="admin() || public" style="float: right; vertical-align: top; cursor: pointer " v-on:click.stop="editProject()">
        P
      </div>
      <div class="name">
        {{name}}
      </div>
      <div class="description">
        {{description}}
      </div>
    </div>
  </div>
</template>

<script>
  import {mapActions} from "vuex";
  import cookies from 'js-cookie'
    export default {
      name: "Project",
      props: {
        id: '',
        userId: '',
        name: "",
        description: "",
        public: false,
        repo: ""
      },
      data() {
        return {
        }
      },
      methods: {
        ...mapActions([
          'setProjectId',
          "deleteProject",
        ]),
        async selectProject() {
          await this.setProjectId(this.id);
          this.$router.push({name: "logs"});
          this.$router.go();
        },
        async deleteThisProject() {
          await this.deleteProject(this.id);
          this.$router.go();
        },
        editProject(){
            this.setProjectId(this.id);
            this.$router.push({name:"editProject"})
        },
        admin(){
          let userId = cookies.get("userId");
          return this.userId == userId;
        }
      },
      computed:{
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
    width: 400pt;
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
