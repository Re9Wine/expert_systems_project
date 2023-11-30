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
          <tr v-for="(item, index) in table.category" :key="index">
            <td data-label="#">{{ index + 1}}</td>
            <td data-label="Категория">{{table.category[index]}}</td>
            <td data-label="Сумма">{{table.sum[index]}}</td>
            <td data-label="Сумма">{{table.limit[index]}}</td>
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
        category:[],
        sum:[],
        limit:[],
      },
    }
  },
  mounted() {
    fetch('MoneyTransaction/GetMonthlyConsumption', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json'
      },
    })
        .then(response => response.json())
        .then(response =>{
          this.table={
            category:response.map((responseData)=>responseData.category),
            sum:response.map((responseData)=>responseData.sum),
            limit:response.map((responseData)=>responseData.limit),
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