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
                <form action="" method="get" class="form-example">
                    <div class="input-block">
                        <input type="text" placeholder="Название категории"/>
                        <input type="number" placeholder="Лимит"/>
                    </div>
            
                    <div class="button-block add-button">
                        <button>
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
            <form action="" method="get" class="form-example">
                <div class="select">
                    <select name="select">
                        <option disabled>Выберите категорию</option>
                        <option value="value1">Еда</option>
                        <option value="value2">Развлечения</option>
                        <option value="value3">Транспорт</option>
                    </select>
                </div>
                <div class="input-block">
                    <input type="number" placeholder="Лимит"/>
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