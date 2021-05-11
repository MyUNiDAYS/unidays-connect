<template>
    <nav id="flexmenu">
        <div class="nav-inner">
            <div id="mobile-toggle" class="mobile-btn"></div>
            <ul id="menu-main-menu" class="main-menu">
                <router-link
                    v-slot="{ href, route, navigate, isExactActive }"
                    class="menu-item"
                    to="/"
                >
                    <li :class="[isExactActive && 'active']">
                        <a :href="href" @click="navigate">Home</a>
                    </li>
                </router-link>
                <router-link
                    v-slot="{ href, route, navigate, isExactActive }"
                    class="menu-item"
                    to="/events"
                >
                    <li :class="[isExactActive && 'active']">
                        <a :href="href" @click="navigate">Events</a>
                    </li>
                </router-link>
                <li class="menu-item"><a href="#">About</a></li>
            </ul>
        </div>
    </nav>
</template>
<script>
export default {};
</script>
<style lang="sass">
#flexmenu
  margin: 0 auto
  .menu-item
    padding-left: 30px !important
    padding-right: 30px !important
</style>
