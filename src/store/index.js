import Vue from "vue";
import Vuex from "vuex";

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
    },
    accessToken: undefined
  },
  getters: {
    isLoggedIn: state => state.user.isLoggedIn,
    accessToken: state => state.accessToken || localStorage.accessToken
  },
  mutations: {
    login(state, userInfo) {
      state.user.isLoggedIn = true;
      state.user.userInfo = { ...userInfo };
    },
    logout(state) {
      state.user.isLoggedIn = false;
      state.user.userInfo = {};
      state.accessToken = undefined;
      localStorage.accessToken = "";
    },
    setAccessToken(state, token) {
      state.accessToken = token;
    }
  },
  actions: {
    login({ commit, getters }) {
      return fetch("https://account.unidays.mk.dev/oauth/userinfo", {
        method: "GET",
        headers: {
          Authorization: "Bearer " + getters.accessToken
        }
      })
        .then(response => response.json())
        .then(userInfo => commit("login", userInfo))
        .catch(error => console.log("ui err", error));
    }
  }
});
//martin.kovac+1586044613236@myunidays.com heslo1234
