<template>
  <div class="content-wrapper">
    <div class="content">
      <h2>
        Thank you for signing up
        <span class="first-name">{{ userInfo.given_name }}</span
        >!
      </h2>
      <p>
        We have sent email to {{ userInfo.email }} with more details about our
        <strong>{{ event.title }}</strong
        >.
      </p>
    </div>
  </div>
</template>
<script>
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters(["userInfo", "eventData", "signupEventId"]),
    event() {
      return this.eventData.find(e => e.id == this.signupEventId);
    }
  }
};
</script>
<style lang="sass" scoped>
.first-name
  text-transform: capitalize

.content-wrapper
  max-width: 800px
  margin: 0 auto
  border-radius: 0px
  padding-top: 0px
  padding-right: 0px
  padding-bottom: 0px
  padding-left: 40px
  box-shadow: 0px 0px 13px rgba(22, 50, 79, 0.1)
  .content
    padding: 30px
</style>
