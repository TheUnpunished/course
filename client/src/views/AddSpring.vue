<template>
  <div style="margin: 10pt 15pt;" >
      <label>
        Имя<br>
        <input type="text" v-model="springForm.name"><br>
      </label>
      <label>
        Описание<br>
        <input type="text" v-model="springForm.description"><br>
      </label>
      <label>
        Статус
        <select v-model="springForm.status">
          <option value="In Progress">В прогрессе</option>
          <option value="Closed">Закрыта</option>
          <option value="Open">Открыта</option>
        </select>
      </label>
      <label>
        Вики:
      </label>
      <label>
        Имя<br>
        <input type="text" v-model="springForm.wikiName"><br>
      </label>
      <label>
        Описание<br>
        <input type="text" v-model="springForm.wikiDesc"><br>
      </label>
      <label>
        Файл<br>
        <input type="text" v-model="springForm.wikiFPath"><br>
      </label>
      <div v-show="badForm()" class="redMessage"  >
        Все поля должны быть заполнены!
      </div>
      <input type="submit" v-on:click="addThisSpring()" :disabled="badForm()">
    </div>
</template>

<script>
    import {mapActions} from "vuex";

    export default {
      async mounted() {
        await this.unauthorised(1);
      },
        name: "AddSpring",
        data() {
          return {
            springForm:{
              name: "",
              description: "",
              status: "Open",
              wikiName: "",
              wikiDesc: "",
              wikiFPath: ""
            }
          }
        },
      methods:{
          ...mapActions([
            "addSpring",
            "unauthorised"
          ]),
        async addThisSpring(){
            await this.addSpring(this.springForm);
            this.$router.push({name: "springs"});
            this.$router.go();
        },
        badForm(){
            return this.springForm.name=="" || this.springForm.description == "" || this.springForm.wikiName == ""
          || this.springForm.wikiDesc == "" || this.springForm.wikiFPath == ""
        }
      }
    }
</script>

<style scoped>

</style>
