<template>
    <nav id="flexmenu">
        <div class="nav-inner">
            <div id="mobile-toggle" class="mobile-btn"></div>
            <ul id="menu-main-menu" class="main-menu">
                <router-link
                    class="menu-item"
                    to="/"
                    v-slot="{ href, route, navigate, isExactActive }"
                >
                    <li :class="[isExactActive && 'active']">
                        <a :href="href" @click="navigate">Home</a>
                    </li>
                </router-link>
                <router-link
                    class="menu-item"
                    to="/events"
                    v-slot="{ href, route, navigate, isExactActive }"
                >
                    <li :class="[isExactActive && 'active']">
                        <a :href="href" @click="navigate">Events</a>
                    </li>
                </router-link>
                <li class="menu-item"><a href="#">About</a></li>

                <router-link
                    v-if="isLoggedIn"
                    class="menu-item menu-item-has-children"
                    to="/account"
                    v-slot="{ href, route, navigate, isExactActive }"
                >
                    <li :class="[isExactActive && 'active']">
                        <a :href="href" @click="navigate">Account</a>
                        <ul class="sub-menu">
                            <li class="menu-item">
                                <a href="" @click.prevent="logout">Log out</a>
                            </li>
                        </ul>
                    </li>
                </router-link>
            </ul>
        </div>
    </nav>
</template>
<script>
import { mapGetters } from "vuex";
export default {
    methods: {
        logout() {
            this.$store.commit("logout");
            this.$router.push("/");
        }
    },
    computed: mapGetters(["isLoggedIn"])
};
</script>
<style lang="sass">
#flexmenu
  margin: 0 auto
  .menu-item
    padding-left: 30px !important
    padding-right: 30px !important
</style>
