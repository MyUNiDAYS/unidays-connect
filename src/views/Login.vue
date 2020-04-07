<template>
  <div>
    <div class="login">
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
    <Loader v-if="isLoading" class="loader" />
  </div>
</template>
<script>
import Loader from "@/components/Loader";
import { mapActions, mapMutations, mapGetters } from "vuex";
export default {
  components: {
    Loader
  },
  data() {
    return {
      isLoading: false,
      hasAccessToken: this.$auth.accessToken == true
    };
  },
  computed: {
    ...mapGetters(["accessToken"])
  },
  watch: {
    accessToken: function() {
      this.login().then(() => this.$router.push({ name: "Account" }));
    }
  },
  methods: {
    redirect() {
      this.$auth.redirect();
    },
    checkForAccessToken() {
      while (!this.hasAccessToken) {
        console.log("waiting");
      }
      console.log("done", this.$auth.accessToken);
    },
    ...mapActions(["login"]),
    ...mapMutations(["setAccessToken"])
  },

  async mounted() {
    if (this.$route.query.code) {
      this.isLoading = true;
      this.$auth.handleCodeAndAuthorization();
      this.$auth.closures = this.$auth.closures(this.setAccessToken);
    }
  }
};
</script>
<style lang="sass" scoped>
.loader
  position: absolute
  top: 35%
  left: 50%
  transform: translate(-50%, 0)
.login
  margin-top: 64px
.card
  padding: 20px
  max-width: 90%
  border: 1px solid #ccc
  display: flex
  flex-wrap: nowrap
  flex-direction: row
  justify-content: center
  margin: 0 auto

  h2
    margin-bottom: 24px
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
