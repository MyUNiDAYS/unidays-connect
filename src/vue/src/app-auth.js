import Vue from "vue";
import {
  AuthorizationServiceConfiguration,
  AuthorizationRequest,
  AuthorizationNotifier,
  BaseTokenRequestHandler,
  FetchRequestor,
  LocalStorageBackend,
  RedirectRequestHandler,
  TokenRequest,
  GRANT_TYPE_AUTHORIZATION_CODE
} from "@openid/appauth";
import { QueryStringUtils } from "./utils";

const auth_app = new Vue({
  data: {
    serviceConfig: new AuthorizationServiceConfiguration({
      authorization_endpoint: `${process.env.VUE_APP_ENV_SERVICE_ENDPOINT}/oauth/authorize`,
      token_endpoint: `${process.env.VUE_APP_ENV_SERVICE_ENDPOINT}/oauth/token`
    }),
    config: {
      client_id: process.env.VUE_APP_ENV_SERVICE_CLIENT_ID,
      redirect_uri: `${location.protocol}//${location.host}/oauth/callback`
    },
    notifier: new AuthorizationNotifier(),
    authorizationHandler: new RedirectRequestHandler(
      new LocalStorageBackend(),
      new QueryStringUtils()
    ),
    tokenHandler: new BaseTokenRequestHandler(new FetchRequestor())
  },
  methods: {
    redirect() {
      const request = new AuthorizationRequest({
        response_type: AuthorizationRequest.RESPONSE_TYPE_CODE,
        client_id: this.config.client_id,
        redirect_uri: this.config.redirect_uri,
        scope: "openid name email verification",
        extras: { prompt: "consent" }
      });
      this.authorizationHandler.performAuthorizationRequest(
        this.serviceConfig,
        request
      );
    },
    handleCodeAndAuthorization(callback) {
      this.notifier.setAuthorizationListener((request, response, error) => {
        console.log(
          "Authorization request complete ",
          request,
          response,
          error
        );
        if (response) {
          console.log(`Authorization Code: ${response.code}`);

          if (!request && !request.internal) {
            console.log("Error: Could not authorize access");
            return;
          }
          const tokenRequest = new TokenRequest({
            client_id: this.config.client_id,
            redirect_uri: this.config.redirect_uri,
            grant_type: GRANT_TYPE_AUTHORIZATION_CODE,
            code: response.code,
            extras: {
              code_verifier: request.internal.code_verifier
            }
          });

          this.tokenHandler
            .performTokenRequest(this.serviceConfig, tokenRequest)
            .then(resp => {
              console.log(`Access Token: ${resp.accessToken}`);
              callback(resp);
            })
            .catch(error => {
              console.log("Error", error);
            });
        }
      });
      return this.authorizationHandler.completeAuthorizationRequestIfPossible();
    }
  },
  created: function() {
    this.authorizationHandler.setAuthorizationNotifier(this.notifier);
  }
});

export default {
  install: function(Vue) {
    Vue.prototype.$auth = auth_app;
  }
};
