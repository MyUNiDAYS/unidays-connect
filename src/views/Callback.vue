<template>
  <Loader loaderText="Getting yout info" />
</template>
<script>
import Loader from "@/components/Loader";
import { mapActions, mapMutations, mapGetters } from "vuex";
export default {
  components: {
    Loader
  },
  computed: {
    ...mapGetters(["accessToken"])
  },
  watch: {
    accessToken: function() {
      this.login().then(() => this.$router.push({ name: "CompleteSignup" }));
    }
  },
  methods: {
    redirect() {
      this.$auth.redirect();
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
