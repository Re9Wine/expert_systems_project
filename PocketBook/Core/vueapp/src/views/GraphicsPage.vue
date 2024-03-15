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
          График 2
        </div>
        <div class="bar-chart">
          <Bar
              v-if="loaded2"
              :style="BarStyle"
              id="my-chart-id"
              :options="chartOptions"
              :data="BarData"
          />
        </div>
      </div>
      <div class="stacked-container" v-if="loaded3">
        <div class="title" >
          Динамика трат за {{names.nameFirst}} и {{names.nameSec}}
        </div>
        <div class="stacked-chart">
          <Bar
              :style="BarStyle"
              id="my-chart-id"
              :options="stackedOptions"
              :data="StackedData"
          />
        </div>
      </div>
    </div>
    <!--        <div class="table">-->
    <!--            <div class="title">-->
    <!--                История трат-->
    <!--            </div>-->
    <!--            <table>-->
    <!--                <thead>-->
    <!--                    <tr>-->
    <!--                        <th scope="col">#</th>-->
    <!--                        <th scope="col">Дата</th>-->
    <!--                        <th scope="col">Категория</th>-->
    <!--                        <th scope="col">Сумма</th>-->
    <!--                    </tr>-->
    <!--                </thead>-->
    <!--                <tbody>-->
    <!--                    <tr v-for="(item, index) in table.date" :key="index">-->
    <!--                        <td data-label="#">{{ index + 1}}</td>-->
    <!--                        <td data-label="Дата">{{table.date[index]}}</td>-->
    <!--                        <td data-label="Категория">{{table.category[index]}}</td>-->
    <!--                        <td data-label="Сумма">{{table.value[index]}}</td>-->
    <!--                    </tr>-->
    <!--                </tbody>-->
    <!--            </table>-->
    <!--        </div> -->

  </main>
</template>


<script>

import {Doughnut, Bar} from 'vue-chartjs'


import {
  Chart as ChartJS, Title, ArcElement,
  PointElement, LineElement, Tooltip, Legend, BarElement, CategoryScale, LinearScale
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, ArcElement,
    PointElement, LineElement, BarElement, CategoryScale, LinearScale)

export default {
  name: 'GraphicsPge',
  components: {
    Doughnut, Bar
  },
  data() {
    return {
      loaded: false,
      loaded2: false,
      loaded3: false,
      names:{
        nameFirst: '',
        nameSec: '',
      },
      table: {
        date: [],
        category: [],
        value: [],
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false,
      },
      stackedOptions: {
        responsive: true,
        maintainAspectRatio: false,
      }
    }
  },
  methods: {
    // formatDate(date) {
    //     // Здесь можно использовать библиотеки для форматирования даты, например, moment.js
    //     return moment(date).format('YYYY-MM-DD'); // Форматирование даты в нужный формат
    // }
  },
  mounted() {
    this.loaded = false;
    this.loaded2 = false;
    this.loaded3 = false;
    fetch('MoneyTransaction/ForDoughnut', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json'
      },
    })
        .then(response => {
          if (response.status === 204) {
            console.log("hello")
          }
          return response.json()
        })
        .then(response => {
          this.DoughnutData = {
            labels: response.map((responseData) => responseData.category),
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
              data: response.map((responseData) => responseData.sum),
              tension: 0.1
            },],
          }
          this.loaded = true

        }),
        fetch('MoneyTransaction/FiveLastedConsumption', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json'
          },
        })
            .then(response => response.json())
            .then(response => {
              this.table = {
                date: response.map((responseData) => responseData.date.toString("dd.mm.yyyy").substr(0, 10)),
                category: response.map((responseData) => responseData.category),
                value: response.map((responseData) => responseData.value),
              }
              // var nameLengths = response.map(function(name) {
              //     return name.category;
              // });

            }),

        fetch('MoneyTransaction/ForBarChar', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json'
          },
        })
            .then(response => response.json())
            .then(response => {
              console.log(response.map((responseData) => responseData.date.toString("dd.mm.yyyy").substr(0, 10)),)
              this.BarData = {
                labels: response.map((responseData) => responseData.date.toString("dd.mm.yyyy").substr(0, 10)),
                datasets: [{
                  data: response.map((responseData) => responseData.sum),
                  backgroundColor:
                      [
                        'rgba(28, 222, 202, 1)',
                        'rgba(28, 222, 202, 0.66)',
                        'rgba(28, 222, 202, 0.33)'
                      ],
                  borderWidth: 0,
                  fill: false,
                  borderColor: 'rgb(75, 192, 192)',
                  tension: 0.1
                },
                ],
              }
              this.loaded2 = true

            }),
        fetch('MoneyTransaction/SpendingTrends', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json'
          },
        })
            .then(response => response.json())
            .then(response => {
              this.names.nameFirst = response["previousMonth"].name[0].toUpperCase() + response["previousMonth"].name.slice(1)
              this.names.nameSec = response["previousPreviousMonth"].name[0].toUpperCase() + response["previousPreviousMonth"].name.slice(1)
              console.log(response["previousMonth"])
              this.StackedData = {
                labels: response["categories"],
                datasets: [{
                  label: this.names.nameFirst,
                  data: response["previousMonth"].values,
                  backgroundColor: 'rgba(187, 134, 252, 1)',
                },
                  {
                    label: this.names.nameSec,
                    data: response["previousPreviousMonth"].values,
                    backgroundColor: 'rgba(28, 222, 202, 1)',
                  }
                ]
              }

              this.loaded3 = true
            })
  }

}


//* Новый график

// const labels = Utils.months({count: 7});
// const data = {
//   labels: labels,
//   datasets: [
//     {
//       label: 'Dataset 1',
//       data: [123,123,324,512,2,4,634,12],
//       backgroundColor: [
//         'rgba(255, 99, 132, 0.2)',
//         'rgba(255, 159, 64, 0.2)',
//         'rgba(255, 205, 86, 0.2)',
//         'rgba(75, 192, 192, 0.2)',
//         'rgba(54, 162, 235, 0.2)',
//         'rgba(153, 102, 255, 0.2)',
//         'rgba(201, 203, 207, 0.2)'
//       ],
//       borderColor: [
//         'rgb(255, 99, 132)',
//         'rgb(255, 159, 64)',
//         'rgb(255, 205, 86)',
//         'rgb(75, 192, 192)',
//         'rgb(54, 162, 235)',
//         'rgb(153, 102, 255)',
//         'rgb(201, 203, 207)'
//       ],
//       borderWidth: 1
//     },
//     {
//       label: 'Dataset 2',
//       data: [123,435,12,453,6,35,7,8,9,],
//     },
//   ],
//   options:{
//     scales:{
//       x:{
//         stacked: true
//       },
//       y:{
//         stacked: true
//       }
//     }
//   }
// };


</script>

<style>
.main-container {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
}

.doughnut-container, .bar-container, .stacked-container {
  border-radius: 12px;
  background: var(--dark-alt);
  padding: 30px;
}

.title-container {
  margin-bottom: 2%;
  padding: 6px;
  color: var(--light);
  font-weight: 700;
  font-size: 16px;
}

.title {
  color: var(--light);
  font-weight: 700;
  margin-bottom: 20px;
}

.doughnut-container {
  height: 30%;
  width: 30%;
}

.doughnut-chart {
  position: relative;
  height: 30vh;

}

.stacked-container {
  margin-top: 30px;
  height: 30%;
  width: 100%;
}

.stacked-chart {
  position: relative;
  height: 30vh;
}

.bar-container {
  height: 30%;
  width: 68%;
}

.bar-chart {
  position: relative;
  height: 30vh;
}

/* .line-chart{
    width: 70%;
    margin-top: 1%;
}  */

.table {
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

table tbody {
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