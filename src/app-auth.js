/*eslint no-unused-vars: "off"*/
import { BasicQueryStringUtils } from "./utils";
import Vue from "vue";
import {
  AuthorizationServiceConfiguration,
  AuthorizationRequest,
  AuthorizationNotifier,
  RedirectRequestHandler,
  BaseTokenRequestHandler,
  TokenRequest,
  GRANT_TYPE_AUTHORIZATION_CODE,
  FetchRequestor,
  LocalStorageBackend
} from "@openid/appauth";

const auth_app = new Vue({
  data: {
    error: undefined,
    serviceConfig: new AuthorizationServiceConfiguration({
      authorization_endpoint: process.env.VUE_APP_ENV_AUTH_ENDPOINT,
      token_endpoint: process.env.VUE_APP_ENV_TOKEN_ENDPOINT
    }),
    config: {
      client_id: process.env.VUE_APP_ENV_CLIENT_ID,
      redirect_uri: process.env.VUE_APP_ENV_REDIRECT_URI,
      scope: process.env.VUE_APP_ENV_SCOPE,
      state: undefined
    },
    notifier: new AuthorizationNotifier(),
    authorizationHandler: new RedirectRequestHandler(
      new LocalStorageBackend(),
      new BasicQueryStringUtils()
    ),
    tokenHandler: new BaseTokenRequestHandler(new FetchRequestor()),
    accessToken: undefined,
    closures: function(mutationFunction) {
      console.log("received fun", mutationFunction);
      return function(token) {
        console.log("received token", token);
        return mutationFunction(token);
      };
    }
  },
  methods: {
    redirect() {
      this.authorizationHandler.performAuthorizationRequest(
        this.serviceConfig,
        this.request
      );
    },
    handleCodeAndAuthorization() {
      return this.authorizationHandler.completeAuthorizationRequestIfPossible();
    },
    getToken() {
      return window.localStorage.accessToken;
    },
    saveToken(token) {
      window.localStorage.accessToken = token.accessToken;
    },
    removeToken() {
      this.isAuthenticatedSubject.next(false);
      window.localStorage.removeItem("accessToken");
    },
    getUserInfo() {
      return fetch(this.serviceConfig.userInfoEndpoint, {
        method: "GET",
        headers: {
          Authorization: "Bearer " + this.accessToken
        }
      })
        .then(response => response.json())
        .catch(error => console.log("ui err", error));
    }
  },
  created: function() {
    const vm = this;
    vm.request = new AuthorizationRequest({
      response_type: AuthorizationRequest.RESPONSE_TYPE_CODE,
      client_id: vm.config.client_id,
      redirect_uri: vm.config.redirect_uri,
      scope: vm.config.scope,
      state: undefined,
      extras: { prompt: "consent", access_type: "offline" }
    });

    vm.authorizationHandler.setAuthorizationNotifier(vm.notifier);
    vm.notifier.setAuthorizationListener((request, response, error) => {
      console.log("Authorization request complete ", request, response, error);
      if (response) {
        vm.request = request;
        vm.response = response;
        const code = response.code;
        console.log(`Authorization Code  ${response.code}`);

        let extras;
        if (vm.request && vm.request.internal) {
          extras = {};
          extras.code_verifier = vm.request.internal.code_verifier;
        }
        const tokenRequest = new TokenRequest({
          client_id: vm.config.client_id,
          redirect_uri: vm.config.redirect_uri,
          grant_type: GRANT_TYPE_AUTHORIZATION_CODE,
          code,
          refresh_token: undefined,
          extras
        });

        vm.accessTokenPromise = vm.tokenHandler
          .performTokenRequest(vm.serviceConfig, tokenRequest)
          .then(resp => {
            console.log("have token", resp);
            vm.accessToken = resp.accessToken;
            vm.saveToken(resp);

            this.closures = this.closures(resp.accessToken);
          })
          .catch(oError => {
            vm.error = oError;
            console.log("error", oError);
          });
      }
    });
  }
});

export default {
  install: function(Vue) {
    Vue.prototype.$auth = auth_app;
  }
};
