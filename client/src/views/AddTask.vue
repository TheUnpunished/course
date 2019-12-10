<template>
  <div style="margin: 10pt 15pt;">
      <label>
        Тема<br>
        <input type="text" v-model="taskForm.theme"><br>
      </label>
      <label>
        Имя трекера<br>
        <input type="text" v-model="taskForm.trackerName"><br>
      </label>
      <label>
        Статус
        <select v-model="taskForm.trackerStat">
          <option value="Open">Открыта</option>
          <option value="Closed">Закрыта</option>
          <option value="In Progress">В прогрессе</option>
        </select><br>
      </label>
      <label>
        Приоритет
        <select v-model="taskForm.priority">
          <option value="Highest">Наивысший</option>
          <option value="High">Высокий</option>
          <option value="Medium">Средний</option>
          <option value="Low">Низкий</option>
          <option value="Lowest">Наименьший</option>
        </select><br>
      </label>
      <label>
        Описание<br>
        <input type="text" v-model="taskForm.description"><br>
      </label>
      <label>
        Назначить человека
        <select v-model="taskForm.whoCan">
          <option v-for="person in people" v-bind:value="person.id">{{person.name}} {{person.surname}}</option>
        </select><br>
      </label>
      <label>
        Назначить веху
        <select v-model="taskForm.spring">
          <option v-for="spring in springs" v-bind:value="spring.id">{{spring.name}}</option>
        </select><br>
      </label>
      <label>
        Дата окончания<br>
        <input type="date" v-model="taskForm.date_over"><br>
      </label>
      <label>
        Дней на окончание<br>
        <input type="number" v-model="taskForm.time"><br>
      </label>
      <label>
        Комментарий о выполнении<br>
        <input type="text" v-model="taskForm.percent"><br>
      </label>
      <label>
        Ссылка на файл<br>
        <input type="text" v-model="taskForm.file_path"><br>
      </label>
      <input type="submit" v-on:click="addThisTask()" value="Добавить">
    </div>
</template>

<script>
  import {mapActions, mapState} from "vuex";
  import cookies from 'js-cookie';
    export default {
      async mounted(){
        await this.setPeople();
        await this.setSprings();
        let projectId = cookies.get("projectId");
        this.taskForm.project_id = projectId;
        await this.unauthorised(1);
      },
        name: "AddTask",
      data() {
        return {
          taskForm:{
            theme: "",
              trackerStat: "Open",
            trackerName: "",
            status: "Open",
            priority: "Medium",
            description: "",
            whoCan: 1,
            spring: 1,
            date_over: new Date(),
            time: "",
            percent: "",
            file_path: "",
            project_id: ""
          }
        }
      },
      computed:{
          ...mapState({
            people: state => state.people,
            springs: state => state.springs
          })
      },
      methods:{
        ...mapActions([
          "setPeople",
          "setSprings",
          "addTask",
          "setProjectId",
          "unauthorised"
        ]),
        async addThisTask(){
          await this.addTask(this.taskForm);
          this.$router.push({name: "tasks"});
          this.$router.go();
        }
      }
    }
</script>

<style scoped>

</style>
