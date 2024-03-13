<template>
  <main class="home-page">
    <div class=" title-container">
      <h1 class="page-title">
        Рекомендации
      </h1>
    </div>
    <div class="message-container" v-for="(item, index) in messages" :key="index">
      <div v-for="(i, j) in item" :key="j">
        <div v-if="i.status === 1" :style="{color: good}">
          {{i.recommendation}}
        </div>
        <div v-else-if="i.status === 2" :style="{color: warning}">
          {{i.recommendation}}
        </div>
        <div v-else :style="{color: dangerous}">
          {{i.recommendation}}
        </div>
      </div>
    </div>
  </main>
</template>

<script lang="js">
export default {
  name: 'HomePge',
  data() {
    return {
      loaded: false,
      messages: { },
      good: 'green',
      warning: 'yellow',
      dangerous: 'red',
    }
  },
  mounted() {
    fetch('MoneyTransaction/MonthlyRecommendations', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json'
      },
    })
        .then(response => response.json())
        .then(response => {
          this.messages = response
          console.log(this.messages)
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