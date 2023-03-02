import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import  mdb from 'mdb-ui-kit'; // lib
import { Input } from 'mdb-ui-kit'; // module
const app = createApp(App)

axios.defaults.headers.common["Authorization"];
app.use(router,axios,mdb,Input)
app.mount('#app')
