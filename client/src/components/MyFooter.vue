<template>
    <footer class="footer">
      <div class="element">
        © Система управления проектами
      </div>
    </footer>
</template>

<script>
    export default {
        name: "MyFooter"
    }
</script>

<style scoped>
  .footer{
    background-color: #cbcbcb;
    color: black;
    font-family: "Open Sans";
    margin-top: 250pt;
  }
  .footer .element{
    padding: 15pt 15pt;
    font-size: 8pt;
    display: inline-block;
    vertical-align: top;
  }
</style>
