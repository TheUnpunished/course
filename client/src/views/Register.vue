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
      <label>
        Подтвердите пароль<br>
        <input type="password" v-model="userForm.confirmPassword" name="confPass"><br>
      </label>
      <div v-show="badPass()" class="redMessage">
        Пароль должен быть длиной не менее 4 символов
      </div>
      <div v-show="badConfirm()" class="redMessage">
        Подтверждение пароля и пароль не совпадают
      </div>
      <div v-show="badEmail()" class="redMessage">
        E-Mail должен иметь примерную стуткуру: example@domain.ru
      </div>
      <input type="submit"  value="Зарегистрироваться" v-on:click="registerAndReturn()"
             :disabled="readyToSubmit()"
      name="submit"><br>
      <div style="font-family: 'Open Sans',serif; font-size: 10pt">
        <a>Уже зарегистрированы? </a><a href="Auth"> Войти</a>
      </div>
    </div>
</template>

<script>
    import {mapActions} from "vuex";

    export default {
        name: "Register",
      data() {
        return {
          userForm: {email: "", password: "", confirmPassword:""}
        }
      },
      methods:{
        badPass(){
          return this.userForm.password.length < 4 && this.userForm.confirmPassword.length < 4;
        },
        badConfirm(){
          return this.userForm.confirmPassword !== this.userForm.password
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
        },
        readyToSubmit(){
          return this.badPass() || this.badConfirm() || this.badEmail()
        },
        ...mapActions([
          'register'
        ]),
        async registerAndReturn(){
          await this.register(this.userForm);
          this.$router.push({name: "auth"})
        }
      }
    }
</script>

<style scoped>

</style>
