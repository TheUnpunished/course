<template>
  <div style="margin: 10pt">
    <label>
      Дата начала
      <input type="date" v-model="dateForm.dateOne">
    </label>
    <label>
      Дата конца
      <input type="date" v-model="dateForm.dateTwo">
    </label>
    <input type="submit" value="Подтвердить" v-on:click="makeDiagram()">
  </div>
</template>

<script>
  import myDiagram from './Diagram'
  import {mapActions} from "vuex";
    export default {
      async mounted(){
        await this.unauthorised(1);
      },
        name: "Diagram",
      components:{
        myDiagram
      },
      data() {
        return {
          dateForm:{dateOne: new Date(), dateTwo: new Date()},
        }
      },
      methods:{
          ...mapActions([
            "getDiagramData",
            "unauthorised"
          ]),
        async makeDiagram() {
          await this.getDiagramData(this.dateForm);
          this.$router.push({name:"diagram"})
        }
      }

    }
</script>

<style scoped>

</style>
