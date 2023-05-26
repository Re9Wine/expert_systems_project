<template>
    <main class="expenses-page">
        <div class=" title-container">
            <h1 class="page-title">
                Графики трат в разрезах
            </h1>
        </div>
        <div class="main-container">
            <div class="doughnut-container">
                <div class="title">
                    График 1
                </div>
                <div class="doughnut-chart">
                    <Doughnut
                    v-if="loaded"
                    id="my-chart-id"
                    :options="chartOptions"
                    :data="DoughnutData"
                    :style="DoughnutStyle"
                    />
                </div>       
            </div>
            
            <div class="bar-container">
                <div class="title">
                        График 1
                </div>
                <div class="bar-chart">
                    <Bar 
                        :style="BarStyle"
                        id="my-chart-id"
                        :options="chartOptions"
                        :data="BarData"
                    />
                </div>
                <!-- <div class="chart-canvas-div">
                    <canvas id="myChart" ref="chart"></canvas>
                </div>     -->
                <!-- <canvas id="myChart" ref="chart"></canvas> -->
            </div>
            <!-- <div class="line-chart">
                <Line
                    :style="LineStyle"
                    id="my-chart-id"
                    :options="chartOptions"
                    :data="LineData"
                />
            </div> -->
        </div>
        <div class="table">
            <div class="title">
                История трат
            </div>
            <table>
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Дата</th>
                        <th scope="col">Категория</th>
                        <th scope="col">Сумма</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in table.date" :key="index">
                        <td data-label="#">{{ index + 1}}</td>
                        <td data-label="Дата">{{table.date[index]}}</td>
                        <td data-label="Категория">{{table.category[index]}}</td>
                        <td data-label="Сумма">{{table.value[index]}}</td>
                    </tr>
                </tbody>
            </table>
        </div> 
        
    </main>
</template>


<script>

import { Doughnut,Bar,Line } from 'vue-chartjs'


import { Chart as ChartJS, Title,ArcElement,
  PointElement,LineElement, Tooltip, Legend, BarElement, CategoryScale, LinearScale } from 'chart.js'

ChartJS.register(Title, Tooltip, Legend,ArcElement,
  PointElement,LineElement, BarElement, CategoryScale, LinearScale)

export default {
    name: 'GraphicsPge',
    components: {
    Doughnut,Bar,Line
  },
//   computed: {
//     DoughnutStyle () {
//       return {
//         position: 'relative'
//       }
//     },
//     BarStyle () {
//       return {
//         position: 'relative'
//       }
//     },
//     // LineStyle () {
//     //   return {
//     //     height: '400px',
//     //     position: 'relative'
//     //   }
//     // }
//   },
  data() {
      return {
        loaded: false,
        table: {
            date:[],
            category:[],
            value:[],
        },
    //     DoughnutData: {
    //     // labels: [ 'Развлечения', 'Еда', 'Транспорт' ],
    //     // datasets: [ 
    //     //    { 
    //     //        data: [40, 20, 12],
    //     //        backgroundColor: 
    //     //        [
    //     //            'rgba(187, 134, 252, 1)',
    //     //            'rgba(187, 134, 252, 0.66)',
    //     //            'rgba(187, 134, 252, 0.33)'
    //     //        ],
    //     //        borderWidth:0,
    //     //        fill: false,
    //     //        borderColor: 'rgb(75, 192, 192)',
    //     //        tension: 0.1
    //     //    },
    //     // ],
    //   },
      BarData: {
        labels: [ 'Развлечения', 'Еда', 'Транспорт' ],
        datasets: [ 
            { 
                data: [40, 20, 12],
                backgroundColor: 
                [
                    'rgba(28, 222, 202, 1)',
                    'rgba(28, 222, 202, 0.66)',
                    'rgba(28, 222, 202, 0.33)'
                ],
                borderWidth:0,
                fill: false,
                borderColor: 'rgb(75, 192, 192)',
                tension: 0.1
            },
        ],
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false,
      }
    }
  },
  methods: {
    formatDate(date) {
        // Здесь можно использовать библиотеки для форматирования даты, например, moment.js
        return moment(date).format('YYYY-MM-DD'); // Форматирование даты в нужный формат
    }
},
  mounted() {
        this.loaded = false;
        this.loaded2 = false;
        fetch('OperationWithMoney/WeeklyConsumption', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        })
        .then(response => response.json())
        .then(response => {
            this.DoughnutData = {
                labels: response.map((responseData)=>responseData.category),
                datasets: [{
                    backgroundColor: [
                        'rgba(187, 134, 252, 1)',
                        'rgba(187, 134, 252, 0.77)',
                        'rgba(187, 134, 252, 0.55)',
                        'rgba(187, 134, 252, 0.33)',
                        'rgba(187, 134, 252, 0.11)'
                    ],
                    borderWidth: 0,
                    fill: false,
                    borderColor: 'rgb(75, 192, 192)',
                    data: response.map((responseData)=>responseData.value),
                    tension: 0.1
                },],
            }
            this.loaded = true
        }),
        fetch('OperationWithMoney/FiveLatestConsumption', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        })
        .then(response => response.json())
        .then(response =>{
            this.table={
                date:response.map((responseData)=>responseData.date),
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

<style>
.main-container{
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
}

.doughnut-container,.bar-container,.line-chart,.table{
    border-radius: 12px;
    background: var(--dark-alt);
    padding: 30px;
}

.title-container{
    margin-bottom: 2%;
    padding: 6px;
    color: var(--light);
    font-weight: 700;
    font-size: 16px;
}

.title{
    color: var(--light);
    font-weight: 700;
    margin-bottom: 20px;
}

.doughnut-container{
    height:50%;
    width:30%;
}

.doughnut-chart{
    position: relative; 
    height:40vh;

}

.bar-container{
    height: 50%;
    width:68%;
}

.bar-chart{
    position: relative; 
    height: 40vh; 
}

/* .line-chart{
    width: 70%;
    margin-top: 1%;
}  */

.table{
    width: 100%;
    margin-top: 2%;
    padding: 30px;
    background-color: var(--dark-alt);
}

table {
  border-collapse: collapse;
  color: var(--light);
  border-spacing: 0;
  border-radius: 12px;
  margin: 0;
  padding: 0;
  width: 100%;
  table-layout: fixed;
}

table caption {
  font-size: 1.5em;
  margin: .5em 0 .75em;
}

table tr {
  background-color: var(--dark-alt);
  /* border-bottom: 1px solid var(--primary-alt); */
  padding: .35em;
}

table th,
table td {
  padding: .625em;
  /* border-bottom: 1px solid var(--primary-alt); */
  text-align: start;
}

table th {
  font-size: .85em;
  letter-spacing: .1em;
  text-transform: uppercase;
  border-bottom: 1px solid var(--table-border);
}

table tbody{
    color: var(--light-alt);
}

@media screen and (max-width: 600px) {
  table {
    border: 0;
  }

  table caption {
    font-size: 1.3em;
  }
  
  table thead {
    border: none;
    clip: rect(0 0 0 0);
    height: 1px;
    margin: -1px;
    overflow: hidden;
    padding: 0;
    position: absolute;
    width: 1px;
  }
  
  table tr {
    border: 3px solid var(--primary);
    display: block;
    margin-bottom: .625em;
  }
  
  table td {
    border: 1px solid var(--primary);
    display: block;
    font-size: .8em;
    text-align: right;
  }
  
  table td::before {
    /*
    * aria-label has no advantage, it won't be read inside a table
    content: attr(aria-label);
    */
    content: attr(data-label);
    float: left;
    font-weight: bold;
    text-transform: uppercase;
  }
  
  table td:last-child {
    border-bottom: 0;
  }
}



</style>