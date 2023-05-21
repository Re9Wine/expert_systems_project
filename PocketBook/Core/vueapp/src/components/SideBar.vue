<template>
    <aside :class="`${is_expanded && 'is_expanded'}`">
        <div class="logo">
            <img src="../assets/coin.svg" alt="Vue">
        </div>

        <div class="menu-toggle-wrap">
            <button class="menu-toggle" @click="ToggleMenu">
                <span class="material-icons">keyboard_double_arrow_right</span>
            </button>
        </div>

        <h3>Menu</h3>
        <div class="menu">
            <router-link class="button" to="/">
                <span class="material-icons">home</span>
                <span class="text">Главная</span>
            </router-link>
            <router-link class="button" to="/newexpenses">
                <span class="material-icons">add</span>
                <span class="text">Добавить</span>
            </router-link>
            <router-link class="button" to="/expenses">
                <span class="material-icons">edit</span>
                <span class="text">Планирование</span>
            </router-link>
            <router-link class="button" to="/graphics">
                <span class="material-icons">donut_large</span>
                <span class="text">Графики</span>
            </router-link>
        </div>

        <div class="flex"></div>

        <div class="menu">
            <router-link class="button" to="/authorization">
                <span class="material-icons">login</span>
                <span class="text">Login</span>
            </router-link>
        </div>
    </aside>
</template>

<script setup>
import {ref} from 'vue'

const is_expanded = ref(localStorage.getItem("is_expanded") === "true")

const ToggleMenu = () => {
    is_expanded.value = !is_expanded.value

    localStorage.setItem("is_expended", is_expanded.value)
}

</script>

<style lang="scss" scoped>
aside{
    display: flex;
    flex-direction: column;
    width: calc(2rem + 32px);
    min-height: 100vh;
    overflow: hidden;
    padding: 1rem;



    background: var(--dark-alt);;
    color: var(--light);

    transition: 0.2s ease-out;

    .flex{
        flex: 1 1 0;
    }

    .logo {
        margin-bottom: 1rem;

        img {
            width: 2rem;
        }
    }

    .menu-toggle-wrap{
        display: flex;
        justify-content: flex-end;
        margin-bottom: 1rem;

        position: relative;
        top: 0;
        transition: 0.2s ease-out;

        .menu-toggle {
            transition: 0.2s ease-out;

            .material-icons{
                font-size: 2rem;
                color: var(--light);
                transition: 0.2s ease-out;
            }

            &:hover {
                .material-icons{
                    color: var(--primary);
                    transform: translateX(0.5rem);
                }
            }
        }
    }

    h3,.button .text {
        opacity: 0;
        transition: 0.3s ease-out;
    }

    h3 {
            color: var(--grey);
            font-size: 0.875rem;
            margin-bottom: 0.5rem;
            text-transform: uppercase;
        }

    .menu{
        margin: 0 -1rem;

        .button {
            display: flex;
            align-items: center;
            text-decoration: none;

            padding: 0.5rem 1rem;
            transition: 0.2 ease-out;

            .material-icons{
                font-size: 2rem;
                color: var(--light);
                transition: 0.2s ease-out;
            }

            .text{
                color: var(--light);
                transition: 0.2 ease-out;
            }

            &:hover, &.router-link-exact-active {
                background-color: var(--dark);

                .material-icons, .text {
                    color: var(--primary);
                }
            }

            &.router-link-exact-active {
                border-right: 5px solid var(--primary);
            }
        }
    }

    &.is_expanded {
        width: var(--sidebar-width);

        .menu-toggle-wrap{
            top: -3rem;
            .menu-toggle{
                transform: rotate(-180deg);
            }
        }

        h3,.button .text {
            opacity: 1;
        }

        h3 {
            color: var(--grey);
            font-size: 0.875rem;
            margin-bottom: 0.5rem;
            text-transform: uppercase;
        }

        .button{
            .material-icons{
                margin-right: 1rem;
            }
        }
    }

    @media (max-width: 768px){
        position: fixed;
        z-index: 99;
    }
}
</style>