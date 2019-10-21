<template>
  <div class="form">

    <form>
      <input class="form-control" type="hidden" v-model="form.id" />
      <div class="form-group rem15">
          <label for="form.startLocation">Start location</label>
          <input v-model="form.startLocation" class="form-control" />
      </div>
      <div class="form-group rem15">
          <label for="form.endLocation">End Location</label>
          <input v-model="form.endLocation" class="form-control" />
      </div>
      <div class="form-group rem15">
          <label for="modelStartDate">Start date</label>
          <datepicker v-model="form.startDate" name="startDate" class="form-control"></datepicker>
      </div>
      <div class="form-group rem15">
          <label for="form.endDate">End date</label>
          <datepicker v-model="form.endDate" name="endDate" class="form-control"></datepicker>
      </div>
      <div class="form-group rem15">
          <label for="form.carId">Select car</label>
          <multiselect v-model="form.carId" track-by="value" label="text" :options="form.cars" class="form-control">
          </multiselect>
      </div>
      <div class="form-group rem15">
          <label for="form.employeeIds">Select employees</label>
          <multiselect v-model="form.employeeIds" track-by="value" label="text" class="form-control" :multiple="true" :options="form.employees" :close-on-select="false"></multiselect>
      </div>
      <div class="form-group rem15">
          <button v-on:click="submitForm">Save</button>
      </div>
    </form>
  </div>
</template>

<script>
export default {

  data(){
    return{
      form:{
        employeeIds: [],
        employees: [],
        cars: [],
        car: undefined
      }
    }
  },
  mounted() {
    this.$route.params.id > 0 ? 
      this.$axios.get("/api/ridesharing/edit", { params: { id: this.$route.params.id }})
      .then(response => {this.form = response.data}) 
      :
      this.$axios.get("/api/ridesharing/add")
      .then(response => {this.form = response.data})
  },
  methods: {
    submitForm: function(){
      this.$route.params.id > 0 ? 
        this.$axios.put("/api/ridesharing/edit", this.form)
        .then(response => {this.$router.push({name : "home"})})
        
      :  
        this.$axios.post("/api/ridesharing/add", this.form )
        .then(response => {this.$router.push({name : "home"})})
        
    }
  }
}
</script>

<style scoped>

</style>
