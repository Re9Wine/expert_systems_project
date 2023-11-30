<template>
    <main class="plan-page">
        <div class=" title-container">
            <h1 class="page-title">
                Создание/редактирование категорий 
            </h1>
        </div>

        <div class="form-container">
            <div class="add-container">
                <div class=" title-container">
                    <h2 class="page-title">
                        Создать
                    </h2>
                </div>
                <form @submit.prevent="handleSubmit" id="createForm" class="createForm">
                    <div class="input-block">
                        <input v-model="addData.Name" name="Name" type="text" placeholder="Название категории"/>
                        <input v-model="addData.Limit" name="Limit" type="number" placeholder="Лимит"/>
                    </div>
            
                    <div class="button-block add-button">
                        <button type="submit" class="submitButton">
                            <span>Создать</span>
                        </button>
                    </div>
                </form>
        </div>

        <div class="edit-container">
            <div class=" title-container">
                <h2 class="page-title">
                    Редактировать
                </h2>
            </div>
            <form @submit.prevent="redactSubmit" class="form-example">
                <div class="select">
                    <select v-model="redactData.Name" name="select">
                        <option disabled>Выберите категорию</option>
                        <option v-for="(item, index) in category" :key="index" :value="item">{{ item }}</option>
                    </select>
                </div>
                <div class="input-block">
                    <input v-model="redactData.Limit" type="number" placeholder="Лимит"/>
                </div>
            
                <div class="button-block edit-button">
                    <button>
                        <span>Редактировать</span>
                    </button>
                </div>
            </form>
        </div>
        </div>

    </main>
</template>



<script>
    export default {
        data() {
            return {
                category:[],
                addData: {
                    Name: '',
                    Limit: ''
                },
                redactData: {
                    Name: '',
                    Limit: ''
                },
            };
        },
        methods: {
            async handleSubmit() {
                try {
                    const response = await fetch('TransactionCategory', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.addData)
                    });
                    if (response.ok) {
                        console.log('Data sent successfully');
                        this.addData.Name = '';
                        this.addData.Limit = '';
                    } else {
                        console.log('Error sending data', response);
                    }
                } catch (error) {
                    console.log('Error sending data', error);
                }
            },
            async redactSubmit() {
                try {
                    const response = await fetch('TransactionCategory', {
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.redactData)
                    });
                    if (response.ok) {
                        console.log('Data sent successfully');
                        console.log(this.redactData);
                        this.redactData.Name = '';
                        this.redactData.Limit = '';
                    } else {
                        console.log('Error sending data', response);
                    }
                } catch (error) {
                    console.log('Error sending data', error);
                }
            },
        },
        mounted(){
            fetch('TransactionCategory?isConsumption=true', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
            })
            .then(response => response.json())
            .then(response =>{
                this.category=response.map((responseData)=>responseData.name)
            // var nameLengths = response.map(function(name) {
            //     return name.category;
            // });
                // console.log(this.table.date);
            })
        }
    };



//import axios from 'axios';

//export default {
//  data() {
//    return {
//      formData: {
//        Name: '',
//        Limit: ''
//      }
//    };
//  },
//  methods: {
//    handleSubmit(event) {
//      event.preventDefault(); // Предотвращаем перезагрузку страницы

//      // Отправка данных на сервер
//      axios.post('values', this.formData)
//        .then(response => {
//          // Обработка успешного ответа от сервера
//          console.log(response.data);
//          // Дополнительные действия при необходимости
//        })
//        .catch(error => {
//          // Обработка ошибки при отправке данных на сервер
//          console.error(error);
//          // Дополнительные действия при необходимости
//        });

//        // fetch('values',{
//        //     headers:{
//        //         'Content-Type':'application/json'
//        //     },
//        //     method:'POST',
//        //     body:JSON.stringify(this.formData)
//        // })
//        // .then(response => {
//        // // Обработка успешного ответа от сервера
//        //     console.log('Data sent successfully!');
//        // })

//      // Очищаем данные формы
//      this.formData.Name = '';
//      this.formData.Limit = '';
//    }
//  }
//};


// import '../js/test'

    //fetch('values', {
    //    method: 'POST',
    //    body: new FormData(document.getElementById('createForm'))

    //});

    // var formdata = new FormData(document.getElementById('createForm'))
    // console.log(formdata)

    //fetch('values')
    //    .then(response => response.json())
    //    .then(data => {
    //        console.log(data)
    //        return;
    //    });
</script>

<style lang="scss">

.form-container{
    display: flex;
    gap: 60px;
    width: 100%;

    .add-container,.edit-container{
        width: 40%;
    }

    .input-block{
        width: 70%;
    }

    .select{
        width: 40%;
    }
}


</style>