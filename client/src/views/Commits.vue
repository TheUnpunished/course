<template>
    <div>
      <my-github v-for="element in gitData.data"
      :author="element.commit.author.name"
      :comment="element.commit.message"
      :url="element.html_url">
      </my-github>
    </div>
</template>

<script>
  import {mapActions, mapState} from "vuex";
  import myGithub from "../components/GitHub"
    export default {
      async mounted(){
        await this.setCommits();
        await this.unauthorised(1);
      },
        name: "Commits",
      computed:{
          ...mapState({
            gitData: state => state.gitData
          })
      },
      methods:{
          ...mapActions([
            "setCommits",
            "unauthorised"
          ])
      },
      components:{
        myGithub
      }
    }
</script>

<style scoped>

</style>
