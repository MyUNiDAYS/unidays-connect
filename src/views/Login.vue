<template>
  <div>
    <div class="card">
      <div class="card-section section-left">
        <h2>Not registered yet?</h2>
        <p>Creating an account on our website is really easy and quick.</p>
        <p>Registering makes it easier for you to</p>
        <ul>
          <li>order publication</li>
          <li>sign up for events</li>
          <li>donate online</li>
          <li>book a furniture collection</li>
        </ul>
        <div class="element login">
          <a href="#" class="power_button effect_1"
            ><span class=" w-50 button_value">Register &gt;&gt;</span></a
          >
        </div>
      </div>
      <div class="vertical-divider"></div>
      <div class="card-section">
        <h2>Log in to your account</h2>
        <form method="post" action="#" class="login-form">
          <input
            type="email"
            name="email"
            class="form-control"
            placeholder="Your E-mail"
          />
          <input
            type="password"
            name="password"
            class="form-control"
            placeholder="Your Password"
          />
          <div class="element login">
            <a href="#" class="power_button effect_1"
              ><span class="button_value">Log in &gt;&gt;</span></a
            >
          </div>
        </form>
        <div>Already have a UNiDAYS account?</div>
        <a href="" @click.prevent="redirect"
          ><img src="../assets/btn-log in with UNiDAYS-green.png" alt="" />
        </a>
      </div>
    </div>
  </div>
</template>
<script>
import { mapActions } from "vuex";
export default {
  methods: mapActions(["redirect"])
};
</script>
<style lang="sass" scoped>
.card
  padding: 20px
  max-width: 90%
  border: 1px solid #ccc
  display: flex
  flex-wrap: nowrap
  flex-direction: row
  justify-content: center
  margin: 0 auto

  .card-section
    width: 45%
  .section-left
    text-align: left
  .login-form
    margin: 0 auto
    width: 350px
    > *
      margin-bottom: 16px



.vertical-divider
  border: 1px solid #ccc
  width: 1px !important
  margin: 0 20px
</style>
