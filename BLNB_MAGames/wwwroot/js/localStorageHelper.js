window.localStorageHelper = {
    setProfile: function (profile) {
        localStorage.setItem("profile", profile);
    },
    getProfile: function () {
        return localStorage.getItem("profile");
    },
    //clearProfile: function () {
    //    localStorage.removeItem("profile");
    //}
};
