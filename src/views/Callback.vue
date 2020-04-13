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
  computed: mapGetters(["authorization"]),
  watch: {
    authorization: function() {
      this.login().then(() => this.$router.push({ name: "CompleteSignup" }));
    }
  },
  methods: {
    ...mapActions(["login"]),
    ...mapMutations(["setAccessTokenResponse"])
  },
  mounted() {
    if (this.$route.query.code) {
      this.isLoading = true;
      this.$auth.handleCodeAndAuthorization(this.setAccessTokenResponse);
    }
  }
};
</script>
