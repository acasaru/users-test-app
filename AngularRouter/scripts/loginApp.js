var myApp = angular.module('loginApp', ['ui.router', 'ngMessages', 'ui.bootstrap']);

myApp.config(function ($stateProvider) {
    var states = [
    {
        url: '/login',
        name: 'login',
        component: 'login'
    },
    {
        name: 'users',
        abstract: true,
        url:'/users',
        templateUrl: 'views/users/main.html',
        defaultSubstate:'users.list'
    },
     {
         name: 'users.list',
         url: '/list',
         templateUrl: 'views/users/list.html',
         controller: 'usersCtrl',
         controllerAs: 'usersCtrl'
     },
     {
         name: "users.edit",
         url: '/edit/{id}',
         templateUrl: 'views/users/edit.html',
         controller: 'editCtrl',
         controllerAs: 'editCtrl'
     },
     {
         name: "users.delete",
         url: '/delete/{id}',
         templateUrl: 'views/users/delete.html',
         controller: 'deleteCtrl',
         controllerAs: 'deleteCtrl'
     },
     {
          name: 'users.create',
          url: '/create',
          templateUrl: 'views/users/create.html',
          controller: 'createCtrl',
          controllerAs: 'createCtrl',
          resolve: {
              roles: function (roleServices) { return roleServices.getRoles(); }
          }
      }
    ];

    states.forEach(function (state) {
        $stateProvider.state(state);
    });
});



myApp.component('login', {
    templateUrl: 'views/login.html',
    controller: function ($rootScope, $state) {
        this.submit = function () {
            $rootScope.isAuthenticated = true;
            $rootScope.username = this.username;
            $state.go('users.list');
        }
    }
});

myApp.controller('editCtrl', function ($stateParams, $rootScope, $state) {
    var vm = this;
    vm.id = $stateParams.id;
    vm.user = $rootScope.users.find(function (user) {
        return user.Id === vm.id
    })

    vm.save = function () {
        $state.go('users.list');
    }
});

myApp.controller('deleteCtrl', function ($uibModal, $stateParams, $rootScope, $state, $rootScope, $log) {
    var vm = this;
    vm.id = $stateParams.id;
    vm.user = $rootScope.users.find(function (user) {
        return user.Id === vm.id
    });
    vm.open = open;
    vm.open();

    function open() {
        var modalInstance = $uibModal.open({
            templateUrl: 'views/users/deleteModal.html',
            controller: 'deleteModalInstanceCtrl',
            controllerAs: '$ctrl',
            resolve: {
                user: function () {
                    return vm.user;
                }
            }
        });

        modalInstance.result.then(function () {
            $state.go('users.list');
        }, function () {
            $state.go('users.list');
        });

        return modalInstance;
    };   
})

myApp.controller('deleteModalInstanceCtrl', function ($uibModalInstance, $rootScope, user) {
    var $ctrl = this;
    $ctrl.user = user;

    $ctrl.ok = function () {
        $uibModalInstance.close('success');
    };

    $ctrl.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
})

myApp.controller('usersCtrl', function (userServices, $rootScope) {
    var vm = this;

    userServices.getUsers().then(function (users) {
        vm.users = $rootScope.users || users;
        $rootScope.users = vm.users;
    });
    console.log('List ctrl');
});

myApp.controller('createCtrl', function (roles, $state, $rootScope) {
    var vm = this;

    vm.roles = roles;
    vm.username = "";
    vm.firstName = "";
    vm.lastName = "";
    vm.selectedRole = "";
    vm.email = "";
    vm.password = "";

    vm.saveUser = function () {
        $rootScope.users.push({
            FirstName: vm.firstName,
            LastName: vm.lastName,
            UserName: vm.username
        });
        $state.go('users.list');
    }
});

myApp.factory('userServices', ['$http', function ($http) {
    var service = {
        getUsers: function() {
            return $http.get('data/users.json').then(
                function (response) { return response.data; });

        },
       
        getUser: function (id) {
            return getUsers().then(function (users) {
                users.find(function (user) {
                    return user.Id === id;
                })
            })
        },
    }
    return service;
}]);

myApp.factory('roleServices', ['$http', function($http){
    var factory = {
        getRoles: function(){
            return $http.get('data/roles.json').then(function(response){return response.data;});
        }
    };

    return factory;
}]);




myApp.run(function ($rootScope, $state, $location, $transitions) {
    $rootScope.isAuthenticated = false;
    var criteria = {
        to: function(state) {
            return state.name != 'login';
        }
    }
    $transitions.onBefore( criteria, function(trans) {
        var shouldLogin = $rootScope.isAuthenticated === false;

        console.log(shouldLogin);

        if (shouldLogin) {
            return trans.router.stateService.target('login');
        }
    })
});

