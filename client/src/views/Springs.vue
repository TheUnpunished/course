<template>
    <div>
      <input type="submit" value="Добавить веху..." v-on:click="addSpring()" style="margin: 10pt 15pt;">
      <div v-for="spring in springs">
        <my-spring :id="spring.id"
        :name="spring.name"
        :description="spring.description"
        :status="spring.status"
        :wiki="spring.wiki"
        :date="spring.date">
        </my-spring>
      </div>
    </div>
</template>

<script>
  import {mapActions, mapState} from "vuex";
  import mySpring from '../components/Spring'

    export default {
      async mounted(){
        this.setSprings();
      },
        name: "Springs",
      computed:{
          ...mapState({
            springs: state => state.springs
          })
      },
      methods:{
          ...mapActions([
            "setSprings"
          ]),
        addSpring(){
            this.$router.push({name: "addSpring"})
        }
    },
      components:{
        mySpring
      }
    }
</script>

<style scoped>

</style>
