<template>
	<div class="loader"></div>
</template>

<script>
	import { wait } from '../utils';
  import {mapActions, mapState} from "vuex";

	export default {
		mounted() {
			this.$nextTick(async () => {
				await wait(1500);
				let role = await this.getRole();
				if (role == "Admin" || role == "Manager"){
				  this.$router.push({name: "projects"})
        }
				else{
				  if (role == "Worker"){
				    this.$router.push({name:"logs"});
          }
				  else{
				    this.$router.push({name: "auth"})
          }
        }
			});
		},
		methods: {
		  ...mapActions([
		    'getRole',
        "setProjects"
      ])
		},
    computed: {
		  ...mapState({
      })
    }
	}
</script>

<style scoped lang="scss">
	$primary-color: #2196F3;

	.loader {
		z-index: 1;
		height: 4px;
		width: 100%;
		position: absolute;
		top: 0;
		left: 0;
		overflow: hidden;
		background-color: #ddd;

		&:before {
			display: block;
			position: absolute;
			content: "";
			left: -200px;
			width: 200px;
			height: 4px;
			background-color: $primary-color;
			animation: loading 2s linear infinite;
		}
	}

	@keyframes loading {
		from {left: -200px; width: 30%;}
		50% {width: 30%;}
		70% {width: 70%;}
		80% { left: 50%;}
		95% {left: 120%;}
		to {left: 100%;}
	}
</style>
