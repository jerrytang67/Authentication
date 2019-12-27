
var createState = function () {
    return "SessionValueMakeItAbnadfhaskdflahkjdlioefiandsnvcoiudsfhoaiusdfiasnfldkasdfaksdfsdf";
}

var createNonce = function () {
    return "NonceValueasfdjoihepfoansijfbvcziujshfgvuyioegirfoS";
}

var signIn = function () {
    let redirectUri = "https://localhost:44314/home/signin";
    let responseType = "id_token token";
    let scope = "openid ApiOne";
    let authUrl =
        "/connect/authorize/callback" +
        "?client_id=client_id_js" +
        `&redirect_uri=${encodeURIComponent(redirectUri)}` +
        `&response_type=${encodeURIComponent(responseType)}` +
        `&scope=${encodeURIComponent(scope)}` +
        `&nonce=${createNonce()}` +
        `&state=${createState()}`;

    var returnUrl = encodeURIComponent(authUrl);

    window.location.href = "https://localhost:44343/Account/Login?ReturnUrl=" + returnUrl;

    console.log(authUrl);
    console.log(returnUrl);
}