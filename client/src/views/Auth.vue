<template>
  <div style="margin: 10pt 15pt;">
    <label>
      E-Mail<br>
      <input type="text" v-model="userForm.email" name="email"><br>
    </label>
    <label>
      Пароль<br>
      <input type="password" v-model="userForm.password" name="pass"><br>
    </label>
    <div v-show="badEmail()" class="redMessage">
      E-Mail должен иметь примерную стуткуру: example@domain.ru
    </div>
    <div v-show="badPass()" class="redMessage">
      Пароль должен быть длиной не менее 4 символов
    </div>
    <input name="submit" type="submit" value="Войти" v-on:click="login()" :disabled="badPass()"><br>
    <div style="font-family: 'Open Sans',serif; font-size: 10pt">
      <a>Впервые на сайте? </a><a href="Register"> Регистрация</a>
    </div>
  </div>
</template>

<script>
	import {mapActions} from "vuex";

  export default {
	  data() {
      return {
        userForm: {email: "", password: ""},
        disabled: true,
      }
    },
		methods: {
	    ...mapActions([
	      'authorise'
      ]),
      async login() {
        await this.authorise(this.userForm);
        this.$router.push({name: "loader"});
      },
	    badPass(){
	      return this.userForm.password.length < 4
      },
      badEmail(){
        try{
          const login = this.userForm.email;
          if (login == ""){
            return true;
          }
          let firstTwo = login.split('@');
          if (firstTwo.length != 2){
            return true;
          }
          let secondTwo = firstTwo[1].split('.');
          if (secondTwo.length < 2 || secondTwo[secondTwo.length-1] == ""){
            return true;
          }
        }
        catch (e) {
          return true;
        }
      }
		},
		computed: {
		}
	}
</script>
