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
    created: function() {
        this.authorizationHandler.setAuthorizationNotifier(this.notifier);
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
        async exchangeCodeForAccessToken() {
            const result = await this.authorizationHandler.completeAuthorizationRequest();
            if (!result) {
                return Promise.reject("No pending authorization request");
            }
            console.log(
                "Authorization request complete",
                result.request,
                result.response,
                result.error
            );
            if (result.error) {
                return Promise.reject(result.error.error);
            }
            if (!result.request && !result.request.internal) {
                return Promise.reject("Error: Could not authorize access");
            }
            console.log(`Authorization Code: ${result.response.code}`);
            const tokenRequest = new TokenRequest({
                client_id: this.config.client_id,
                redirect_uri: this.config.redirect_uri,
                grant_type: GRANT_TYPE_AUTHORIZATION_CODE,
                code: result.response.code,
                extras: {
                    code_verifier: result.request.internal.code_verifier
                }
            });
            try {
                const tokenResponse = await this.tokenHandler.performTokenRequest(
                    this.serviceConfig,
                    tokenRequest
                );
                console.log(`Access Token: ${tokenResponse.accessToken}`);
                return Promise.resolve(tokenResponse);
            } catch (error) {
                return Promise.reject(error);
            }
        }
    }
});

export default {
    install: function(Vue) {
        Vue.prototype.$auth = auth_app;
    }
};
