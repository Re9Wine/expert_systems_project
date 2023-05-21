import {createRouter,createWebHistory} from 'vue-router'
import HomePage from '../views/HomePage.vue'
import GraphicsPage from '../views/GraphicsPage.vue'
import ExpensesPage from '../views/ExpensesPage.vue'
import AuthorizationPage from '../views/AuthorizationPage.vue'
import NewExpenses from '../views/NewExpenses.vue'


const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            component: HomePage
        },
        {
            path: '/graphics',
            component: GraphicsPage
        },
        {
            path: '/newexpenses',
            component: NewExpenses
        },
        {
            path: '/expenses',
            component: ExpensesPage
        },
        {
            path: '/authorization',
            component: AuthorizationPage
        }
    ]
})

export default router