<template>
    <div>
        <div class=" signup-card">
            <div class="signup-card-content">
                <div class="urgent_cause_heading">
                    <div class="power_highlight_tag">Student only!</div>
                    <h3>You are almost on board!</h3>
                    <p class="condition">
                        <strong>{{
                            this.$store.getters.eventData.find(
                                e => e.id == this.$route.params.id
                            ).title
                        }}</strong>
                        is student only event. <br />To verify that you are a
                        student Log in with UNiDAYS
                    </p>
                    <a class="redirect-link" @click.prevent="redirect">
                        <img
                            src="https://cdn.unidays.world/assets/buttons/login-green.png"
                            alt=""
                        />
                    </a>
                </div>
            </div>
        </div>

        <Loader v-if="isRedirecting" loaderText="Redirecting" />
    </div>
</template>
<script>
import Loader from "@/components/Loader";
import { mapMutations } from "vuex";
export default {
    components: {
        Loader
    },
    data() {
        return {
            isRedirecting: false
        };
    },
    methods: {
        ...mapMutations(["setSignupEventId"]),
        redirect() {
            this.setSignupEventId(this.$route.params.id || 1);
            this.isRedirecting = true;
            this.$auth.redirect();
        }
    }
};
</script>
<style lang="sass">
.signup-card
  padding: 60px 100px
  border-radius: 15px
  box-shadow: 0px 0px 13px rgba(253, 27, 91, 0.1);
  max-width: 800px
  margin: 64px auto
  background-image: url('../assets/layer1.png');
  background-repeat: no-repeat;
  background-position: bottom right;
  h3
    padding: 20px 0
  .signup-card-content
    padding: 20px
  .redirect-link
    cursor: pointer
  .condition
    padding: 0 45px 30px 45px
</style>
