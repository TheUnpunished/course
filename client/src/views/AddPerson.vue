<template>
    <div style="margin: 10pt 15pt;">
        <label>
          Имя<br>
          <input type="text" v-model="peopleForm.name"><br>
        </label>
        <label>
          Фамилия<br>
          <input type="text" v-model="peopleForm.surname"><br>
        </label>
        <label>
          Язык
          <select v-model="peopleForm.language">
            <option value="en">Английский</option>
            <option value="ru">Русский</option>
            <option value="cz">Чешский</option>
            <option value="de">Немецкий</option>
          </select>
          <br>
        </label>
        <label>
          E-Mail<br>
          <input type="text" v-model="peopleForm.email"><br>
        </label>
        <label>
          Пароль<br>
          <input type="password" v-model="peopleForm.password"><br>
        </label>
        <label>
          Отдел\категория<br>
          <input type="text" v-model="peopleForm.categoryName"><br>
        </label>
        <label>
          Админ
          <input type="checkbox" v-model="peopleForm.admin"><br>
        </label>
        <label>
          Роль
          <select v-model="peopleForm.status" :disabled="peopleForm.admin">
            <option value="Manager">Менеджер</option>
            <option value="Worker">Рабочий</option>
          </select>
        </label>
      <div v-show="badForm()" class="redMessage">
        Все поля должны быть заполнены!
      </div>
        <input type="submit" value="Добавить" v-on:click="addPerson()" :disabled="badForm()">
    </div>
</template>

<script>
    import {mapActions} from "vuex";

    export default {
      async mounted() {
        await this.unauthorised(1);
      },
        name: "AddPerson",
      data() {
        return {
          peopleForm:{
            name:"",
            surname:"",
            email:"",
            password:"",
            admin: false,
            status: "Manager",
            categoryName: "",
            language: "en"
          }
        }
      },
      methods:{
        ...mapActions([
          "addPeople",
          "unauthorised"
        ]),
        async addPerson() {
          await this.addPeople(this.peopleForm);
          this.$router.push({name: "people"});
          this.$router.go();
        },
        badForm(){
          return this.peopleForm.name == "" || this.peopleForm.password == "" || this.peopleForm.email == ""
          || this.peopleForm.password == "" || this.peopleForm.surname == "" || this.peopleForm.categoryName == ""
        }
      }
    }
</script>

<style scoped>

</style>
