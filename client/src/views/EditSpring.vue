<template>
  <div style="margin: 10pt 15pt;">
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
    <div>
      Вики:
    </div>
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
    <div v-show="badForm()" class="redMessage">
      Все поля должны быть заполнены!
    </div>
    <input type="submit" :disabled="badForm()" v-on:click="editThisSpring()">
  </div>
</template>

<script>
  import {mapActions, mapState} from "vuex";
  import cookies from 'js-cookie';
    export default {
      async mounted(){
        let springId = cookies.get("springId");
        let spring = await this.getSpring(springId);
        let wiki = await this.getWiki(springId);
        this.springForm = {
          name: spring.name,
          description: spring.description,
          status: spring.status,
          wikiName: wiki.name,
          wikiDesc: wiki.description,
          springId: spring.id,
          wikiId: wiki.id,
          wikiFPath: wiki.file_path,
          springDate: spring.date
        };
        await this.unauthorised(1);
      },
        name: "EditSpring",
      data() {
        return {
          springForm:{
            name: "",
            description: "",
            status: "Open",
            wikiName: "",
            wikiDesc: "",
            wikiFPath: "",
            springId: "",
            wikiId: "",
            springDate: ""
          }
        }
      },
      computed:{
          ...mapState({
          })
      },
      methods:{
          ...mapActions([
            "getSpring",
            "editSpring",
            "getWiki",
            "unauthorised"
          ]),
        badForm(){
          return this.springForm.name=="" || this.springForm.description == "" || this.springForm.wikiName == ""
            || this.springForm.wikiDesc == "" || this.springForm.wikiFPath == ""
        },
        async editThisSpring(){
            await this.editSpring(this.springForm);
            this.$router.push({name:"springs"});
            this.$router.go();
        }
      }
    }
</script>

<style scoped>

</style>
