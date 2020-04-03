/*eslint no-unused-vars: "off"*/

import Vue from "vue";
import Vuex from "vuex";
import auth from "../auth";

import {
  AuthorizationServiceConfiguration,
  AuthorizationRequest,
  AuthorizationNotifier,
  RedirectRequestHandler,
  BaseTokenRequestHandler,
  TokenRequest,
  TokenResponse,
  FetchRequestor,
  DefaultCrypto
} from "@openid/appauth";

let code_verifier;

const config = new AuthorizationServiceConfiguration({
  authorization_endpoint: "https://account.unidays.mk.dev/oauth/authorize",
  userinfo_endpoint: "https://account.unidays.mk.dev/oauth/userinfo",
  token_endpoint: "https://account.unidays.mk.dev/oauth/access_token"
});

// create a request
const request = new AuthorizationRequest({
  response_type: AuthorizationRequest.RESPONSE_TYPE_CODE,
  client_id: "OYSPFRV1G8QZLZBSUTQ6KTPEQK-HRZ6RXAQFEK4F-H0",
  redirect_uri: "http://localhost:8082/callback",
  scope: "openid name verification email offline_access",
  state: undefined,
  extras: { prompt: "consent", access_type: "offline" }
});

const notifier = new AuthorizationNotifier();
// uses a redirect flow
const authorizationHandler = new RedirectRequestHandler();
// set notifier to deliver responses
authorizationHandler.setAuthorizationNotifier(notifier);
// set a listener to listen for authorization responses
notifier.setAuthorizationListener((request, response, error) => {
  console.log("Authorization request complete ", request, response, error);
  if (response) {
    //code = response.code;
    console.log(`Authorization Code ${response.code}`);
  }
});

const GRANT_TYPE_AUTHORIZATION_CODE = "authorization_code";
const GRANT_TYPE_REFRESH_TOKEN = "refresh_token";

let tokenResponse = null; // = new TokenResponse();
const tokenHandler = new BaseTokenRequestHandler(new FetchRequestor());
function makeTokenRequest(code, state) {
  if (!config) {
    console.log("Please fetch service configuration.");
    return;
  }

  let tokenRequest;
  if (code) {
    let extras;
    console.log(JSON.stringify(request));
    console.log(code_verifier);
    const internal = request.internal;
    if (request && request.internal) {
      extras = {};
      extras["code_verifier"] = request.internal["code_verifier"];
    }
    console.log(extras);
    // use the code to make the token request.
    tokenRequest = new TokenRequest({
      client_id: "OYSPFRV1G8QZLZBSUTQ6KTPEQK-HRZ6RXAQFEK4F-H0",
      redirect_uri: "http://localhost:8082/callback",
      grant_type: GRANT_TYPE_AUTHORIZATION_CODE,
      code: code,
      refresh_token: undefined,
      extras: extras
    });
  } else if (tokenResponse) {
    // use the token response to make a request for an access token
    tokenRequest = new TokenRequest({
      client_id: "OYSPFRV1G8QZLZBSUTQ6KTPEQK-HRZ6RXAQFEK4F-H0",
      redirect_uri: "http://localhost:8082/callback",
      grant_type: GRANT_TYPE_REFRESH_TOKEN,
      code: undefined,
      refresh_token: tokenResponse.refreshToken,
      extras: undefined
    });
  }

  if (tokenRequest) {
    tokenHandler
      .performTokenRequest(config, tokenRequest)
      .then(response => {
        let isFirstRequest = false;
        if (tokenResponse) {
          // copy over new fields
          tokenResponse.accessToken = response.accessToken;
          tokenResponse.issuedAt = response.issuedAt;
          tokenResponse.expiresIn = response.expiresIn;
          tokenResponse.tokenType = response.tokenType;
          tokenResponse.scope = response.scope;
        } else {
          isFirstRequest = true;
          tokenResponse = response;
        }

        // unset code, so we can do refresh token exchanges subsequently
        code = undefined;
        if (isFirstRequest) {
          console.log(`Obtained a refresh token ${response.refreshToken}`);
        } else {
          console.log(`Obtained an access token ${response.accessToken}.`);
        }
      })
      .catch(error => {
        console.log("Something bad happened", error);
      });
  }
}

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
    async login({ commit }, payload) {
      makeTokenRequest(payload.code, payload.state);
      //console.log(user);
      //commit("login", user);
    },
    redirect() {
      //auth.login();
      // make the authorization request
      request.setupCodeVerifier().then(
        _ => {
          console.log("verifier setup");
          code_verifier = request.internal;
          console.log(JSON.stringify(request));
          authorizationHandler.performAuthorizationRequest(config, request);
        },
        error => console.log("code verifier err", error)
      );
    }
  }
});
