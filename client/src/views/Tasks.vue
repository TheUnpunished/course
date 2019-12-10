<template>
    <div>
      <input v-show="adminManager()" type="submit" value="Добавить задачу..." v-on:click="addTask()" style="margin: 10pt 15pt;">
      <div>
        <div v-for="task in this.tasks">
          <my-task :theme="task.theme" :description="task.description"
                   :id="task.id"
          :who-can="task.whoCan" :priority="task.priority" :category="task.category"
          :status="task.status" :project_id="task.project_id"
          :date_over="task.date_over" :time="task.time"
          :spring="task.spring"
          :percent="task.percent"></my-task>
        </div>
      </div>
    </div>
</template>

<script>
  import {mapActions, mapState} from "vuex";
  import myTask from '../components/Task'
    export default {
    async mounted(){
      this.role = await this.getRole();
      await this.setTasks();
      await this.unauthorised(0);
    },
        name: "Tasks",
      data() {
        return {
          role: ""
        }
      },
      methods:{
          ...mapActions([
            "setTasks",
            "unauthorised",
            "getRole"
          ]),
        addTask(){
            this.$router.push({name:"addTask"});
        },
        adminManager(){
            return this.role == "Admin" || this.role == "Manager";
        }

      },
      computed:{
          ...mapState({
            tasks: state => state.tasks
          })
      },
      components:{
          myTask
      },
    }
</script>

<style scoped>

</style>
