<template>
    <div>
      <input type="submit" v-on:click="addPerson()" value="Добавить сотрудника..." style="margin: 10pt 15pt;">
      <div v-for="person in people">
        <my-person :id="person.id" :name="person.name"
        :surname="person.surname" :admin="person.admin"
        :email="person.email" :language="person.language"
        :password="person.password"></my-person>
      </div>
    </div>
</template>

<script>
  import {mapActions, mapState} from "vuex";
  import myPerson from '../components/Person'

    export default {
      async mounted(){
        await this.setPeople();
        await this.unauthorised(1);
      },
        name: "People",
      data(){
        return{
        role:""
        }},
      computed:{
          ...mapState({
            people: state => state.people
          })
      },
      methods:{
          ...mapActions([
            "setPeople",
            "unauthorised"
          ]),
        addPerson(){
          this.$router.push({name: "addPeople"});
        }

      },
      components:{
        myPerson
      }
    }
</script>

<style scoped>

</style>
