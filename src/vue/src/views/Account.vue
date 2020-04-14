<template>
  <div>
    <h1>Account</h1>
    <div class="userInfo">
      <p>ID: {{ userInfo.sub }}</p>
      <p>First Name: {{ userInfo.given_name }}</p>
      <p>Last Name: {{ userInfo.family_name }}</p>
      <p>Email: {{ userInfo.email }}</p>
    </div>
  </div>
</template>
<script>
import { mapState } from "vuex";

export default {
  computed: mapState({
    userInfo: state => state.user.userInfo
  })
};
</script>
<style lang="sass" scoped></style>
