import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import {MDBBtn,MDBModal,MDBModalTitle,MDBModalHeader,MDBModalBody,MDBModalFooter,MDBCheckbox} from 'mdb-vue-ui-kit'

const app = createApp(App)

import 'mdb-vue-ui-kit/css/mdb.min.css';
axios.defaults.headers.common["Authorization"];

app.use(router,axios,MDBBtn,MDBModal,MDBModalTitle,MDBModalHeader,MDBModalBody,MDBModalFooter)
app.mount('#app')
