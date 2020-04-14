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
      <p>
        Do you have appetite for more good deeds? <br />
        See other available events.
      </p>
      <router-link to="/events" class="power_button effect_1">
        <span class="button_value">Go to Events!</span>
      </router-link>
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
  margin: 36px auto
  border-radius: 0px
  padding: 60px
  box-shadow: 0px 0px 13px rgba(253, 27, 91, 0.1)
  .content
    h2, p
      padding-bottom: 36px
    p
      max-width: 450px
      margin: 0 auto

  background-image: url('../assets/images/object.png')
  background-repeat: no-repeat
  background-position: 90% 65%
  min-height: 500px;
</style>
