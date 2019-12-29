var config = {
    authority: "https://localhost:44343/",
    client_id: "client_id_js",
    redirect_uri: "https://localhost:44314/home/signin",
    response_type: "id_token token",
    scope: "openid ApiOne ApiTwo rc.scope",
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage })
};

var mgr = new Oidc.UserManager(config);

var signIn = function () {
    mgr.signinRedirect();
};

var signOut = function() {
    mgr.signoutRedirect();
};

mgr.getUser().then(user => {
    if (user) {
        axios.defaults.headers.common["Authorization"] = `Bearer ${user.access_token}`;
    }
});

var callApi = function () {
    axios.get(`https://localhost:44356/secret`).then(res => {
        console.log(res);
    });
}

var refreshing = false;

axios.interceptors.response.use(function (response) {
    return response;
}, function (err) {

    console.log("axios error:", err.response);
    var axiosConfig = err.response.config;

    if (err.response.status === 401) {
        console.log("axios error status is 401");

        // if already refreshing don't refresh
        if (!refreshing) {
            refreshing = true;
            console.log("to refresh the token");
            return mgr.signinSilent().then(user => {
                console.log(user);

                axios.defaults.headers.common["Authorization"] = `Bearer ${user.access_token}`;
                axiosConfig.headers["Authorization"] = `Bearer ${user.access_token}`;

                refreshing = false;

                // retry the http request
                return axios(axiosConfig);
            }, error => {
                console.log(error);
                refreshing = false;
            });
        }
    }
    return Promise.reject(err);
});