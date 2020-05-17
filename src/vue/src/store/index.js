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
        accessTokenResponse: {}
    },
    getters: {
        isLoggedIn: state => state.user.isLoggedIn,
        userInfo: state => state.user.userInfo,
        authorization: state =>
            `${state.accessTokenResponse.tokenType} ${state.accessTokenResponse.accessToken}`,
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
            state.accessTokenResponse = {};
        },
        setAccessTokenResponse(state, response) {
            state.accessTokenResponse = response;
        },
        setSignupEventId(state, id) {
            localStorage.setItem("signupEventId", id);
        }
    },
    actions: {
        async login({ commit, getters }) {
            try {
                const response = await fetch(
                    `${process.env.VUE_APP_ENV_SERVICE_ENDPOINT}/oauth/userinfo`,
                    {
                        method: "GET",
                        headers: {
                            Authorization: getters.authorization
                        }
                    }
                );
                const userInfo = await response.json();
                return commit("login", userInfo);
            } catch (error) {
                return console.log("ui err", error);
            }
        }
    }
});
