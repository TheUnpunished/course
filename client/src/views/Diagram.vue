<template>
  <div style="font-family: 'Open Sans',serif">
    <h2 align="center">Сгорание задач</h2>
    <div class="diagram">
    <my-diagram
      :datasets="[
        {
          data: counts,
          smooth: false,
          fill: true
        }
      ]"
      :grid="{
        verticalLines: true,
        horizontalLines: true
      }"
      :labels="{
        xLabels: dates,
        yLabels: maxValue,
        yLabelsTextFormatter: val => Math.round(val)
      }"
      :min="0">

    </my-diagram>
  </div>
  </div>
</template>

<script>
  import {mapActions, mapState} from "vuex";
    import myDiagram from 'vue-trend-chart'

    export default {
      beforeMount(){
        let value = this.maxVal();
        this.maxValue = value + 1;
      },
        name: "Diagram",
      data() {
        return {
          maxValue: ""
        }
      },
      computed:{
          ...mapState({
            counts: state => state.counts,
            dates: state => state.dates
          })
      },
      components:{
          myDiagram
      },
      methods:{
          maxVal(){
            let maxCount = 0;
            let i=0;
            for(i=0; i< this.counts.length; i++){
              if (this.counts[i] > maxCount){
                maxCount = this.counts[i]
              }
            }
            return maxCount;
          },
          ...mapActions([
            "unauthorised"
          ])
      }
    }
</script>

<style scoped>
my-diagram{
  font-family: "Open Sans",serif;
}
</style>
