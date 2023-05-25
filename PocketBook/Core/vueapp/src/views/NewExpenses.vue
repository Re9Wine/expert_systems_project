<template>
    <main class="about-page">
        <div class=" title-container">
            <h1 class="page-title">
                Добавление расходов/доходов
            </h1>
        </div>
        <form @submit.prevent="handleSubmit"  class="form-example">
            <div class="form_radio_btn">
	            <input v-model="formData.TypeOperation" id="radio-1" type="radio" name="radio" value='income'>
	            <label for="radio-1">Доходы</label>
            </div>
            <div class="form_radio_btn">
                <input v-model="formData.TypeOperation" id="radio-2" type="radio" name="radio" value='consumption'>
	            <label for="radio-2">Рассходы</label>
            </div>
            <div class="select">
                <select v-model="formData.addData.Name" name="select">
                    <option disabled>Выберите категорию</option>
                    <option value="value1">Еда</option>
                    <option value="value2">Развлечения</option>
                    <option value="value3">Транспорт</option>
                </select>
            </div>
            <div class="input-block">
                <input v-model="formData.addData.Value" type="number" placeholder="Сумма"/>
                <input v-model="formData.addData.Description" type="text" placeholder="Описание"/>
            </div>
            
            <div class="button-block">
                <button type="submit">
                    <span>Добавить</span>
                </button>
            </div>
        </form>
    </main>
</template>

<script>
    export default {
        data() {
            return {
                formData: {
                    TypeOperation: '',
                    addData: {
                        Name: '',
                        Value: '',
                        Description: ''
                    },
                }
            };
        },
        methods: {
            async handleSubmit() {
                try {
                    const response = await fetch('values', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.formData)
                    });
                    if (response.ok) {
                        console.log('Data sent successfully');
                        console.log(this.formData);
                        console.log(this.formData.addData);
                        this.formData.addData.Name = '';
                        this.formData.addData.Limit = '';
                        this.formData.addData.Value = '';
                        this.formData.addData.Description = '';
                    } else {
                        console.log('Error sending data', response);
                    }
                } catch (error) {
                    console.log('Error sending data', error);
                }
            },
        }
    }


</script>

<style lang="scss">

.title-container{
    margin-bottom: 2%;
    padding: 5px;
    color: var(--light);
    font-weight: 700;
    font-size: 16px;

    .title {
        color: var(--light);
        font-weight: 700;
        margin-bottom: 20px;
    }
}

.form_radio_btn {
	display: inline-block;
	margin-right: 10px;
    margin-bottom: 20px;
    color: var(--light);
    font-size: 14px;

    input[type=radio] {
	    display: none;
    }

    label {
	    display: inline-block;
	    cursor: pointer;
	    padding: 0px 16px;
	    line-height: 34px;
	    border-radius: 12px;
	    user-select: none;
        background-color: var(--dark-alt);
        transition: .3s;
        font-weight: 500;
    }

    /* Checked */
    input[type=radio]:checked + label {
	    background: var(--primary);
        color: var(--dark);
        font-weight: 600;
    }

    /* Hover */
     label:hover {
	    color: var(--light);
        background-color: var(--primary-alt);
    }

    /* Disabled */
    input[type=radio]:disabled + label {
	    background: #efefef;
	    color: #666;
    }

}
 
 
/* Disabled */
.form_radio_btn input[type=radio]:disabled + label {
	background: #efefef;
	color: #666;
}

.select {
  position: relative;
  display: flex;
  width: 20%;
  height: 3em;
  margin-bottom: 20px;
  border-radius: 12px;
  overflow: hidden;
    select {
        
        appearance: none;
        outline: 0;
        border: 0;
        box-shadow: none;

        flex: 1;
        padding: 0 1em;
        color: var(--light);
        background-color: var(--dark-alt);
        background-image: none;
        cursor: pointer;
    }
    
    select::-ms-expand {
        display: none;
    }
}
/* Arrow */
.select::after {
  content: '\25BC';
  position: absolute;
  top: 0;
  right: 0;
  padding: 1em;
  background-color: #383737;
  transition: .25s all ease;
  pointer-events: none;
  color: var(--light);
}

.select:hover::after {
  color: var(--primary);
}

.input-block{
    width: 30%;

    input {
        font-family: "Roboto", sans-serif;
        outline: 0;
        background-color: var(--dark-alt);
        color: var(--light);
        width: 100%;
        border: 1px solid var(--grey);
        border-radius: 12px;
        margin: 0 0 20px;
        padding: 16px;
        box-sizing: border-box;
        font-size: 14px;
        transition: .3s;
    }
    input::-webkit-input-placeholder {
        color: var(--grey-alt);
    }
    input:active, input:hover, input:focus{
        border: 1px solid var(--primary);
    }
}

.button-block{
    width: 10%;

    button {
        display: inline-block;
        border-radius: 12px;
        background-color: var(--secondary);
        border: none;
        color: var(--dark);
        text-align: center;
        font-size: 16px;
        padding: 16px 20px;
        transition: .3s;
        cursor: pointer;
    }

    button span {
        cursor: pointer;
        display: inline-block;
        position: relative;
        transition: .3s;
        font-weight: 600;
    }

    button:hover{
        background-color: var(--secondary-alt);
    }

    button:hover span{
        color: var(--light-alt);
    }

    button:hover span:after {
        opacity: 1;
        right: 0;
    }
}


</style>