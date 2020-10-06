<template>
    <Loader loaderText="Getting your info" />
</template>
<script>
import Loader from "@/components/Loader";
import { mapActions, mapMutations, mapGetters } from "vuex";
export default {
    components: {
        Loader
    },
    computed: mapGetters(["authorization", "signupEventId"]),
    watch: {
        authorization: function() {
            this.getInstitutionInfo();
            this.login().then(
                () => this.$router.replace({ name: "CompleteSignup" }),
                () => this.$router.replace({ name: "SignupError" })
            );
        }
    },
    methods: {
        ...mapActions(["login", "getInstitutionInfo"]),
        ...mapMutations(["setAccessTokenResponse", "setSignupEventId"])
    },
    mounted() {
        this.$auth
            .exchangeCodeForAccessToken()
            .then(this.setAccessTokenResponse, error => {
                if (!error) {
                    this.$router.replace({ name: "Home" });
                    return;
                }
                if (error === "access_denied") {
                    this.$router.replace({
                        name: "EventSignup",
                        params: { id: this.signupEventId }
                    });
                    return;
                }
                console.log(error);
                this.$router.replace({
                    name: "SignupError"
                });
                this.setSignupEventId(null);
            });
    }
};
</script>
