import Vue from "vue";
import VueRouter from "vue-router";
import store from "../store";

import Home from "../views/Home.vue";
import Callback from "../views/Callback.vue";
import CompleteSignup from "../views/CompleteSignup.vue";
import Events from "../views/Events.vue";
import EventSignup from "../views/EventSignup.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/login",
    name: "Login",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/Login.vue")
  },
  {
    path: "/events",
    name: "Events",
    component: Events
  },
  {
    path: "/event-signup",
    name: "EventSignup",
    component: EventSignup
  },
  {
    path: "/callback",
    name: "Callback",
    component: Callback
  },
  {
    path: "/complete-signup",
    name: "CompleteSignup",
    component: CompleteSignup,
    meta: {
      requiresAuth: true
    }
  }
];

const router = new VueRouter({
  mode: "history",
  linkExactActiveClass: "active",
  routes
});

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (store.getters.isLoggedIn) {
      next();
      return;
    }
    next("/events");
  } else {
    next();
  }
});

export default router;
