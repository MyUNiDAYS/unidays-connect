<template>
  <div class="callback">Callback</div>
</template>

<script>
import { mapActions } from "vuex";
export default {
  name: "callback",
  methods: mapActions(["login"]),
  async mounted() {
    // const vm = this;
    console.log(this.$route.query);
    await this.login({
      code: this.$route.query.code,
      state: this.$route.query.state
    });
    // this.$router.push({ name: "Account" });
  }
};
</script>
