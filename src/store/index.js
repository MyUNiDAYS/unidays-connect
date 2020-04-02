import Vue from "vue";
import Vuex from "vuex";
import auth from "../auth";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    user: {
      isLoggedIn: false,
      userInfo: {
        //   sub: "",
        //   given_name: "",
        //   family_name: "",
        //   email: "",
        //   verification_status: {
        //     verified: true,
        //     user_type: "student"
        // }
      }
    }
  },
  getters: {
    isLoggedIn: state => state.user.isLoggedIn
  },
  mutations: {
    login({ user }, payload) {
      user.isLoggedIn = true;
      user.userInfo = { ...payload };
    },
    logout({ user }) {
      user.isLoggedIn = false;
      user.userInfo = {};
    }
  },
  actions: {
    async login({ commit }, code) {
      const user = await auth.handleAuthentication(code);
      console.log(user);
      commit("login", user);
    },
    redirect() {
      auth.login();
    }
  }
});
