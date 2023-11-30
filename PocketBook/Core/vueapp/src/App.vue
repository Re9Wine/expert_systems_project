<template>
  <div class="app">
    <Sidebar />
    <router-view />
  </div>
</template>

<script setup>
import Sidebar from './components/SideBar.vue'



</script>

<style lang="scss">
:root {
  --primary: #bb86fc;
  --temp: #6c4d92;
  --primary-alt: #5b427a;
  --secondary: #1cdeca; 
  --secondary-alt: #0a4943; 
  --grey: #464646;
  --grey-alt: #808080;
  --table-border: #363636;
  --dark: #212121;
  --dark-alt: #2e2e2e;
  --light: #e2e2e2;
  --light-alt: #cccccc;
  --sidebar-width: 300px;
}

*{
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: 'Open sans', sans-serif;
}

body{
  background: var(--dark);
}

button{
  cursor: pointer;
  appearance: none;
  border: none;
  outline: none;
  background: none;
}

.app{
  display: flex;
  

  main{
    flex: 1 1 0;
    width: calc(100% - (2rem + 32px));
    padding: 2rem;

    @media (max-width: 768px) {
      padding-left: 6rem;
    }
  }
}
</style>