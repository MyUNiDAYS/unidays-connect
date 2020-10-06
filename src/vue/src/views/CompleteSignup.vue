<template>
    <div
        class="content-wrapper"
        v-bind:class="{ 'thank-you': verifiedStudent }"
    >
        <div class="content">
            <template v-if="verifiedStudent">
                <h2>
                    Thank you for signing up
                    <span class="first-name"> {{ userInfo.given_name }} </span>!
                </h2>
                <p>
                    We have sent email to {{ userInfo.email }} with more details
                    about our <strong> {{ event.title }} </strong>.
                </p>
                <p>
                    We can see you are currenty studying in
                    <strong>{{ institutionInfo.name }}</strong
                    >. We are currently developing a support program for
                    <strong>{{ getFriendlyIsced }}</strong> students in the
                    <strong>{{ institutionInfo.country }}</strong> region and we've
                    emailed you all the details.
                </p>
            </template>
            <template v-if="!verifiedStudent">
                <h2>
                    Thank you for your interest
                    <span class="first-name"> {{ userInfo.given_name }} </span>!
                </h2>
                <p>
                    Unfortunately <strong> {{ event.title }} </strong> is only
                    open to Students.
                </p>
            </template>
            <p>
                Do you have an appetite for more good deeds?<br />
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
        ...mapGetters([
            "isLoggedIn",
            "userInfo",
            "eventData",
            "signupEventId",
            "institutionInfo"
        ]),
        event() {
            return this.eventData.find(e => e.id == this.signupEventId);
        },
        verifiedStudent() {
            return (
                this.userInfo.verification_status &&
                this.userInfo.verification_status.verified &&
                this.userInfo.verification_status.user_type === "student"
            );
        },
        getFriendlyIsced() {
            if (!this.institutionInfo.isced) return "all";

            switch (this.institutionInfo.isced[0]) {
                case 1:
                    return "primary education";
                case 2:
                    return "secondary education";
                case 3:
                    return "upper secondary education";
                case 4:
                    return "professional education";
                case 5:
                    return "professional education";
                case 6:
                    return "university";
                case 7:
                    return "master";
                case 8:
                    return "phd";
            }

            return "all";
        }
    },
    mounted() {
        if (!this.isLoggedIn) {
            this.$router.replace({ name: "Home" });
        }
    },
    beforeRouteLeave(to, from, next) {
        this.$store.commit("logout");
        next();
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

  &.thank-you
    background-image: url('../assets/images/object.png')
    background-repeat: no-repeat
    background-position: 90% 65%
    min-height: 500px
</style>
