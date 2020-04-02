<template>
  <div class="callback">Callback</div>
</template>

<script>
import { mapActions } from 'vuex';
export default {
  name: "callback",
  methods: mapActions(['login']),
  async mounted() {
   // const vm = this;
   await this.login(this.$route.query.code);
  this.$router.push({ name: 'Account' })
  }
}
</script>