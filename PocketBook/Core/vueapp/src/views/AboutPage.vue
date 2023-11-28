<template>
    <main class="about-page">
        <h1>Траты по категориям</h1>
      <div class="table">
        <div class="title">
          История трат
        </div>
        <table>
          <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Категория</th>
            <th scope="col">Сумма трат</th>
            <th scope="col">Лимит трат</th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="(item, index) in table.date" :key="index">
            <td data-label="#">{{ index + 1}}</td>
            <td data-label="Категория">{{table.category[index]}}</td>
            <td data-label="Сумма">{{table.value[index]}}</td>
          </tr>
          </tbody>
        </table>
      </div>
    </main>
</template>
<script  lang="js">
export default {
  name: 'GraphicsPge',
  components: {

  },
  data(){
    return{
      loaded: false,
      loaded2: false,
      table: {
        date:[],
        category:[],
        value:[],
      },
    }
  },
  mounted() {
    fetch('OperationWithMoney/GetFiveLastedConsumption', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json'
      },
    })
        .then(response => response.json())
        .then(response =>{
          this.table={
            date:response.map((responseData)=>responseData.date.toString("dd.mm.yyyy").substr(0, 10)),
            category:response.map((responseData)=>responseData.category),
            value:response.map((responseData)=>responseData.value),
          }
          // var nameLengths = response.map(function(name) {
          //     return name.category;
          // });
          console.log(this.table.date);

        })
  }
}
</script>
<style scoped>
h1{
  color: var(--light);
}
</style>
<script setup lang="ts">
</script>