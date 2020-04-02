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
        <li class="menu-item"><a href="about.html">About</a></li>
        <li class="menu-item menu-item-has-children">
          <a href="#">Causes</a>
          <ul class="sub-menu">
            <li class="menu-item">
              <a href="causes1.html">Causes One</a>
            </li>
            <li class="menu-item">
              <a href="causes2.html">Causes Two</a>
            </li>
            <li class="menu-item">
              <a href="causes-details.html">Causes Details</a>
            </li>
          </ul>
        </li>
        <li class="menu-item menu-item-has-children">
          <a href="#">Pages</a>
          <ul class="sub-menu">
            <li class="menu-item menu-item-has-children">
              <a href="#">Blog</a>
              <ul class="sub-menu">
                <li class="menu-item">
                  <a href="blog1.html">Blog One</a>
                </li>
                <li class="menu-item">
                  <a href="blog2.html">Blog Two</a>
                </li>
                <li class="menu-item">
                  <a href="blog-details.html">Blog Details</a>
                </li>
              </ul>
            </li>
            <li class="menu-item"><a href="events.html">Events</a></li>
            <li class="menu-item">
              <a href="event-details.html">Event Details</a>
            </li>
            <li class="menu-item">
              <a href="volunteers.html">Volunteers</a>
            </li>
            <li class="menu-item">
              <a href="volunteer-form.html">Volunteer Form</a>
            </li>
            <li class="menu-item">
              <a href="donate1.html">Donate One</a>
            </li>
            <li class="menu-item">
              <a href="donate2.html">Donate Two</a>
            </li>
          </ul>
        </li>
        <li class="menu-item"><a href="contact.html">Contact</a></li>
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
        <router-link
          v-else
          class="menu-item"
          to="/login"
          v-slot="{ href, route, navigate, isExactActive }"
        >
          <li :class="[isExactActive && 'active']">
            <a :href="href" @click="navigate">Login/Register</a>
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
<style lang="sass"></style>
