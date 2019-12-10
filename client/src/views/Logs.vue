<template>
    <div>
      <div v-for="log in logs">
        <my-logs :name="log.name"
        :description="log.description"
        :file_path="log.file_path">
        </my-logs>
      </div>
    </div>
</template>

<script>
  import {mapActions, mapState} from "vuex";
  import myLogs from "../components/Log"
    export default {
      async mounted() {
        await this.setLogs();
        await this.unauthorised(0);
      },
        name: "Logs",
      computed:{
          ...mapState({
            logs: state => state.logs
          })
      },
      methods:{
          ...mapActions([
            "setLogs",
            "unauthorised"
            ])
      },
      components:{
        myLogs
      }
    }

</script>

<style scoped>

</style>
