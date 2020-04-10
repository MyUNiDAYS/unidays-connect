import Vue from "vue";
import Vuex from "vuex";

import eventData from "@/data/eventData";

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
    userInfo: state => state.user.userInfo,
    accessToken: state => state.accessToken || localStorage.accessToken,
    eventData: () => eventData,
    signupEventId: () => localStorage.signupEventId
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
    },
    setSignupEventId(state, id) {
      localStorage.setItem("signupEventId", id);
    }
  },
  actions: {
    login({ commit, getters }) {
      return fetch(process.env.VUE_APP_ENV_USERINFO_ENDPOINT, {
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
