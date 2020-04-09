<template>
  <div>
    <h2>
      Thank you for signing up
      <span class="first-name">{{ userInfo.given_name.toUpperCase() }}</span
      >!
    </h2>
    <p>
      We have sent email to {{ userInfo.email }} with details about this event.
    </p>
  </div>
</template>
<script>
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters(["userInfo"])
  }
};
</script>
<style lang="sass" scoped>
.first-name
  text-transform: capitalize
</style>
