<template>
  <main class="home-page">
    <div class=" title-container">
      <h1 class="page-title">
        Рекомендации
      </h1>
    </div>
    <div class="message-container" v-for="(item, index) in messages[0]" :key="index">
      {{item}}
    </div>
  </main>
</template>

<script lang="js">
export default {
  name: 'HomePge',
  data() {
    return {
      loaded: false,
      messages:[],
    }
  },
  mounted() {
    fetch('OperationWithMoney/GetWeeklyForDoughnut', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json'
      },
    })
        .then(response => response.json())
        .then(response => {
          this.messages = response.map((responseData) => responseData.errorMessages)

          this.loaded=true
        })
  }
}
</script>
<style>
.message-container{
  width: auto;
  padding: 30px;
  border-radius: 12px;
  height: auto;
  font-size: 24px;
  font-weight: bold;
  color: var(--light-alt);
  margin-bottom: 10px;
  background-color: var(--grey);
}
</style>