<template>    
  <div>
    <div class="rem10">
        <vue-monthly-picker name="monthSelect" @selected="refreshOnSelect" v-model="date" dateFormat="MM-YYYY"></vue-monthly-picker>
    </div>
      <table>
          <thead>
              <tr>
                  <th class="rem10">Start location</th>
                  <th class="rem10">End location</th>
                  <th class="rem10">Start date</th>
                  <th class="rem10">End date</th>
                  <th class="rem15">Car</th>
                  <th class="rem20">Employees</th>
                  <th class="rem10">Actions</th>
              </tr>
          </thead>
          <tbody>
              <tr v-for="ride in rides" :key="ride.id">
                  <td>{{ride.startLocation}}</td>
                  <td>{{ride.endLocation}}</td>
                  <td>{{ride.startDate}}</td>
                  <td>{{ride.endDate}}</td>
                  <td>{{ride.carName}}</td>
                  <td>{{ride.employeeNames}}</td>
                  <td>
                      <router-link :to="{ name: 'modify', params: { id: ride.id }}">Edit</router-link>|
                      <a href="#" @click.prevent="deleteRide(ride.id)">Delete</a>
                </td>
              </tr>
          </tbody>
      
      </table>
      
  </div>
</template>

<script type="text/javascript">
import moment from 'moment'
    
    export default {
        components: {
        },
        data() {
            return{
                rides: [],
                date: moment()
            }
            
        },
        mounted() {
            this.$axios.get("/api/ridesharing")
            .then(response => {this.rides = response.data})
        },
        methods: {
            moment: function () {
                return moment.locale();
            },
            deleteRide : function(id){
                this.$axios.delete("/api/ridesharing", { params: { id: id } })
                .then(response => {
                    this.$axios.get("/api/ridesharing")
                    .then(resp => {this.rides = resp.data})
                })
                
            },
            refreshOnSelect : function(){
                this.$axios.get("/api/ridesharing", { params: { date: this.date } })
                .then(response => {this.rides = response.data})
            }
        }
    };
    
    
</script>

<style>
    .rem10{
        width:10rem;
    }
    .rem15 {
        width: 15rem;
    }
    .rem20 {
        width: 20rem;
    }

</style>
       
