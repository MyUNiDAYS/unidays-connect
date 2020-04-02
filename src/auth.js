// import axios from "axios";
import qs from "qs";

const authConfig = {
    domain: "https://account.unidays.mk.dev",
    cliendID: "OYSPFRV1G8QZLZBSUTQ6KTPEQK-HRZ6RXAQFEK4F-H0",
    redirectURI: "http://localhost:8082/callback",
    reponseType: "code",
    scopes: "openid name verification email offline_access",
    code_challenge: "",
    code_challenge_method: "S256",
    refresh_token: ""
}
//code verifier 7b028833-cb2f-4c85-8676-a681ba3791a5
//code challenge ZmEwZmIyMWViMjMzOWI3ZTg2ZjQ4M2M3YzE4YjA5MTg1YWRiYTViY2IwNDJiZjg5YjcwNzVhZWI2NzEwNmVlMQ
// /-g-yHrIzm36G9IPHwYsJGFrbpbywQr-Jtwda62cQbuE
// function str2ab(str) {
//     var buf = new ArrayBuffer(str.length*2); // 2 bytes for each char
//     var bufView = new Uint16Array(buf);
//     for (var i=0, strLen=str.length; i < strLen; i++) {
//       bufView[i] = str.charCodeAt(i);
//     }
//     return buf;
//   }

const auth = {
      login() {
            console.log("trigger login");
            const redirect = `${authConfig.domain}/oauth/authorize?client_id=${authConfig.cliendID}&response_type=${authConfig.reponseType}&scope=${authConfig.scopes}&code_challenge_method=${authConfig.code_challenge_method}&code_challenge=-g-yHrIzm36G9IPHwYsJGFrbpbywQr-Jtwda62cQbuE&redirect_uri=http%3A%2F%2Flocalhost%3A8082%2Fcallback`;
           console.log(redirect);
           window.location = redirect;
        },
        async handleAuthentication(code){

            const body= {
                'code': code,
                'code_verifier': '7b028833-cb2f-4c85-8676-a681ba3791a5',
                'client_id': authConfig.cliendID,
                'redirect_uri': authConfig.redirectURI,
                "grant_type": "authorization_code"
            }
            return await fetch( `${authConfig.domain}/oauth/access_token`,{
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                  },
                body: qs.stringify(body)
            })
            .then(response => response.json())
            .then(async data => {
                console.log('show data');
                console.log(data);
                authConfig.refresh_token = data.refresh_token;
                const response = await fetch(`${authConfig.domain}/oauth/userinfo`, {
                    method: 'GET',
                    headers: {
                        Authorization: "Bearer " + data.access_token
                    }
                });
                console.log('response');
                const js = response.json();
                return js;
            })
        }
};

export default auth;