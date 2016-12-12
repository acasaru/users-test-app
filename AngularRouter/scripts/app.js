var myApp = angular.module('helloworld', ['ui.router']);

angular.module('helloworld').component('hello', {
    template: '<h3>{{$ctrl.greeting}} solar sytem!</h3>' +
               '<button ng-click="$ctrl.toggleGreeting()">toggle greeting</button>',

    controller: function () {
        this.greeting = 'hello';

        this.toggleGreeting = function () {
            this.greeting = (this.greeting == 'hello') ? 'whats up' : 'hello'
        }
    }
})

angular.module('helloworld').component('people', {
    bindings: { people: '<' },

    template: '<h3>Some people:</h3>' +
              '<ul>' +
              '  <li ng-repeat="person in $ctrl.people">' +
              '    <a ui-sref="person({ gogu: person.id })">' +
              '      {{person.name}}' +
              '    </a>' +
              '  </li>' +
              '</ul>' +
              '<ui-view></ui-view>'
})

angular.module('helloworld').component('person', {
    bindings: { person: '<' },
    template: '<h3>A person!</h3>' +

              '<div>Name: {{$ctrl.person.name}}</div>' +
              '<div>Id: {{$ctrl.person.id}}</div>' +
              '<button ui-sref="people">Close</button>'
});

angular.module('helloworld').service('PeopleService', function ($http) {
    var service = {
        getAllPeople: function () {
            return $http.get('data/people.json').then(function (response) {
                return response.data;
            });
        },

        getPerson: function (personId) {
            return service.getAllPeople().then(function (people) {
                return people.find(function (person) {
                    return person.id === personId;
                })
            })
        }
    };
    return service;
});


myApp.config(function ($stateProvider) {
    var helloState = {
        name: 'hello',
        url: '/hello',
        component: 'hello'
    };
    var aboutState = {
        name: 'about',
        url: '/about',
        template: '<h3>Its the UI-Router<br>Hello Solar System app!</h3>'
    };
    
    var peopleState = {
        url: '/people',
        name: 'people',
        component: 'people',
        resolve: {
            people: function (PeopleService) {
                return PeopleService.getAllPeople();
            }
        }
    }

    var personState = {
        name: 'person',
        parent:'people',
        url: '/{gogu}',
        component: 'person',
        resolve: {
            person: function (people, $transition$) {
                return people.find(function (person) {
                    return person.id === $transition$.params().gogu;
                });
            }
        }
    }
    $stateProvider.state(helloState);
    $stateProvider.state(aboutState);
    $stateProvider.state(peopleState);
    $stateProvider.state(personState);
});



