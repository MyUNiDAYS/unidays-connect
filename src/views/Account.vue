<template>
<div>
    <h1>Account</h1>
    <div class="userInfo">
        <p>{{ userInfo.sub}}</p>
        <p>{{ userInfo.given_name}}</p>
        <p>{{ userInfo.family_name}}</p>
        <p>{{ userInfo.email}}</p>
    </div> 
</div>
</template>
<script>
import { mapState } from 'vuex';

export default {
   computed: mapState({
       userInfo: state => state.user.userInfo
   })
}
</script>
<style lang="sass" scoped>

</style>