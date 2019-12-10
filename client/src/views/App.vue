<template>
	<div>
		<main class="container">
      <my-menu></my-menu>
			  <router-view></router-view>
      <my-footer></my-footer>
		</main>
	</div>
</template>

<script>
	import { mapState } from 'vuex';
	import MyFooter from '../components/MyFooter';
	import MyMenu from '../components/MyMenu'

	export default {
		computed: {
			...mapState({
				alertText: state => state.alertText
			})
		},
		components: {
      MyFooter,
      MyMenu
		}
	}
</script>
