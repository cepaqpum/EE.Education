(function() {
  angular
    .module("EducationApp", ["ngRoute", "semantic-ui", "angular-md5", "ngStorage"])
    .config(config);
    
  config.$inject = ["$routeProvider", "$locationProvider", "$httpProvider"];
    
  function config($routeProvider, $locationProvider, $httpProvider) {

    $locationProvider.html5Mode(false);

      $routeProvider
          .when("/account/login",
              {
                  templateUrl: "js/pages/account/account-login-template.html",
                  controller: "AccountLoginController",
                  controllerAs: "accountLogin"
              })
          .when("/account/view",
              {
                  templateUrl: "js/pages/account/account-view-template.html",
                  controller: "AccountViewController",
                  controllerAs: "accountView"
              })
          .when("/task/registry",
              {
                  templateUrl: "js/pages/task/task-registry-template.html",
                  controller: "TaskRegistryController",
                  controllerAs: "taskRegistry"
              })
          .when("/lession/registry",
              {
                  templateUrl: "js/pages/lession/lession-registry-template.html",
                  controller: "LessionRegistryController",
                  controllerAs: "lessionRegistry"
              })
          .otherwise({
              redirectTo: "/account/view"
          });

    $httpProvider.interceptors.push("authInterceptor");
  }

  angular
    .module("EducationApp")
    .factory("authInterceptor", authInterceptor);

    authInterceptor.$inject = ["$rootScope", "$q", "$location", "account"];

    function authInterceptor($rootScope, $q, $location, account) {
        return {

            // intercept every request
            request: function(config) {
                config.headers = config.headers || {};
                if (account.isAuthenticated()) {
                    config.headers.Authorization = "Bearer " + account.getToken();
                }
                return config;
            },

            // Catch 404 errors
            responseError: function(response) {
                if (response.status === 401) {
                    account.logout();
                    $location.path("/accountLogin");
                    return $q.reject(response);
                } else if (response.status === 404) {
                    $location.path("/");
                    return $q.reject(response);
                } else {
                    return $q.reject(response);
                }
            }
        };
    }

  angular
    .module("EducationApp")
    .run(run);

  run.$inject = ["$rootScope", "$location"];

  function run($rootScope, $location) {
    // put here everything that you need to run on page load
  }
})();