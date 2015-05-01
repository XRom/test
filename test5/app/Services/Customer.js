app.factory('CustomerService', function ($http, $q, $log, $rootScope) {

    var baseUrl = '/api/customer';

    var service = {
              data: {
                currentcustomer: {},
                customers: {
                Content: [],
                CurrentPage: 40,
                PageSize: 20,
                TotalRecords: 40,
                TotalPages: 2
             },
                selected: [],
                totalPages: 0,

            filterOptions: {
                filterText: '',
                externalFilter: 'searchText',
                useExternalFilter: true
            },
            sortOptions: {
                fields: ["Id"],
                directions: ["asc"]
            },
            pagingOptions: {
                pageSizes: [10, 20, 50, 100],
                pageSize: 10,
                currentPage: 1
            }
        },

        find: function () {
            var params = {
                searchtext: service.data.filterOptions.filterText,
                page: service.data.pagingOptions.currentPage,
                pageSize: service.data.pagingOptions.pageSize,
                sortBy: service.data.sortOptions.fields[0],
                sortDirection: service.data.sortOptions.directions[0]
            };

            var deferred = $q.defer();
            $http.get(baseUrl, { params: params })
            .success(function (data) {
                service.data.customers = data;
                deferred.resolve(data);
            }).error(function () {
                deferred.reject();
            });
            return deferred.promise;
        }
    }

    service.find();
    return service;
});