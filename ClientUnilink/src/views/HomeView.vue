<template>
  <div class="container" style=" margin-top: 3%;" v-show="show">
    <div class="row">
      <div class="col">
        <!-- Name input -->
        <div class="form-outline">
          <input type="text" v-model="form.username"  class="form-control" />
          <label class="form-label" >Nombre Usuario</label>
        </div>
      </div>
      <div class="col">
        <!-- Email input -->
        <div class="form-outline">
          <input type="number" v-model="form.initial_Balance"  class="form-control" />
          <label class="form-label" >Valor Inicial</label>
        </div>
      </div>
      <div class="col">
        <!-- Email input -->
        <div class="form-outline">
          <MDBCheckbox label="Guardar Apuesta" v-model="form.is_Save" />
        </div>
      </div>
    </div>
    <hr />
    <div class="d-grid gap-2">
      <button class="btn btn-primary" @click.prevent="startGame()" type="button">Iniciar</button>
    </div>
  </div>
  <div class="container" style=" margin-top: 3%;" v-show="show==false">
    <br>
    <div class="row">
      <div class="col">
        <!-- Email input -->
        <div class="form-outline">
          <label class="form-label">Valor Inicial</label>
          <input type="number" v-model="form.initial_Balance" disabled class="form-control" />
        </div>
      </div>
      <div class="col">
        <!-- Email input -->
        <div class="form-outline">
          <label class="form-label" > </label>
          <input type="number" v-model="form.amount"  class="form-control" />
          <label class="form-label">Apuesta</label>
        </div>
      </div>
    </div>
    <br>
    <div class="row">
      <div class="col">
        <!-- Email input -->
        <div class="form-outline">
          <input type="number" v-model="form.number" max="36" min="0" class="form-control" />
          <label class="form-label" >Numero</label>
        </div>
      </div>
      <div class="col">
        <!-- Email input -->
        <div class="form-outline">
          <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio1"  checked  v-model="form.color" value="Rojo" />
            <label class="form-check-label" for="inlineRadio1">Rojo</label>
          </div>
          <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2" v-model="form.color" value="Negro" />
            <label class="form-check-label" for="inlineRadio2">Negro</label>
          </div>
        </div>
      </div>
    </div>
    <hr />
    <div class="d-grid gap-2">
      <button class="btn btn-primary" @click.prevent="play()" type="button">Jugar</button>
    </div>
  </div>
  <MDBModal
    id="exampleModal"
    tabindex="-1"
    labelledby="exampleModalLabel"
    v-model="exampleModal"
  >
    <MDBModalHeader>
      <MDBModalTitle id="exampleModalLabel"> Modal title </MDBModalTitle>
    </MDBModalHeader>
    <MDBModalBody>
      <div class="card">
        <div class="card-header">
          <h2 class="card-title"><span :class="`${result_class}`" style="text-align: center;">{{ result }}</span></h2>
        </div>
          <div class="card-body">
            <table class="table">
              <thead>
                <tr>
                  <th scope="col">Numero</th>
                  <th scope="col">Color</th>
                  <th scope="col">{{ retrunResul.gain<0 ? 'Total Perdida': 'Total Ganado'  }}</th>
                </tr>
              </thead>
              <tbody>
                <tr  class="table-info">
                  <th scope="row" >{{ retrunResul.number }}</th>
                  <td>{{ retrunResul.color }}</td>
                  <td>{{ retrunResul.gain }}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
    </MDBModalBody>
  </MDBModal>
</template>
  <script>
   import axios from "axios";
   import {MDBBtn,MDBModal,MDBModalTitle,MDBModalHeader,MDBModalBody,MDBModalFooter,MDBCheckbox} from 'mdb-vue-ui-kit'
   const axiosInstance = axios.create({
    headers: {
      "Access-Control-Allow-Origin": "*",
      "Content-Type": "	application/json"
    }
  });
    export default {
      components: {
        MDBBtn,MDBModal,MDBModalTitle,MDBModalHeader,MDBModalBody,MDBModalFooter,MDBCheckbox
       },
      directives: {   },
      data() {
        return {
          form: {
            username: null,
            initial_Balance: null,
            amount: null,
            number: null,
            color: true,
            is_Save: false
          },
          result: null,
          result_class: "badge badge-primary",
          show: true,
          exampleModal: false,
          retrunResul: {
            type: Object,
            required: false,
            default: () => {}
          }
        }
       
      },
      methods: {
        startGame(){
          //this.exampleModal= true
          if( this.form.username!=null && this.form.initial_Balance > 0 && this.form.initial_Balance != null){
            this.show=false;
            this.form.initial_Balance= this.form.initial_Balance
          }
        },
        play(){
          const isNumber = n => (typeof(n) === 'number' || n instanceof Number);
          if(this.form.amount<=0 || this.form.amount===null || isNumber(this.form.amount)==false ){
            alert("Ingrese el monto correcto");
            exit;
          }
          if(this.form.number<=0 || this.form.number===null || isNumber(this.form.number)==false ){
            alert("Ingrese el numero correcto");
            exit;
          }
          this.playGame(this.form);
        },
        playGame(data) {
          axiosInstance.post(import.meta.env.VITE_API_URL+"game/rule",data).then((response) => {
            this.retrunResul=response.data;
            this.form.initial_Balance=this.form.initial_Balance+response.data.gain
            this.result= response.data.gain<=0? "Loser": "Winer";
            this.result_class= response.data.gain<=0? "badge badge-danger": "badge badge-primary";
            this.exampleModal= true
          }).catch((error) =>{
            console.log(error.response.data)
            alert(error.response.data.detail);
          });
        },
        onSubmit(event) {
          event.preventDefault()
          alert(JSON.stringify(this.form))
        },
        onReset(event) {
          event.preventDefault()
          // Reset our form values
          this.form.email = ''
          this.form.name = ''
          this.form.food = null
          this.form.checked = []
          // Trick to reset/clear native browser form validation state
          this.show = false
          this.$nextTick(() => {
            this.show = true
          })
        }
      }
    }
  </script>