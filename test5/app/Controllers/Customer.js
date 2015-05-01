app.controller('CustomerController', function ($scope, CustomerService, $routeParams, $log) {
    $scope.data = CustomerService.data;

    $scope.$watch('data.sortOptions', function (newVal, oldVal) {
        $log.log("sortOptions changed: " + newVal);
        if (newVal !== oldVal) {
            $scope.data.pagingOptions.currentPage = 1;
            CustomerService.find();
        }
    }, true);

    $scope.$watch('data.filterOptions', function (newVal, oldVal) {
        $log.log("filters changed: " + newVal);
        if (newVal !== oldVal) {
            $scope.data.pagingOptions.currentPage = 1;
            CustomerService.find();
        }
    }, true);

    $scope.$watch('data.pagingOptions', function (newVal, oldVal) {
        $log.log("page changed: " + newVal);
        if (newVal !== oldVal) {
            CustomerService.find();
        }
    }, true);

    $scope.gridOptions = {
        data: 'data.customers.Content',
        showFilter: false,
        multiSelect: false,
        selectedItems: $scope.data.selected,
        enablePaging: true,
        showFooter: true,
        totalServerItems: 'data.customers.TotalRecords',
        pagingOptions: $scope.data.pagingOptions,
        filterOptions: $scope.data.filterOptions,
        useExternalSorting: true,
        sortInfo: $scope.data.sortOptions,
        plugins: [new ngGridFlexibleHeightPlugin()],
        columnDefs: [
                    { field: 'Id', displayName: 'ID' },
                    { field: 'Country', displayName: 'Country' },
                    { field: 'City', displayName: 'City' },
                    { field: 'Street', displayName: 'Street' },
                    { field: 'HouseNumber', displayName: 'HouseNumber' },
                    { field: 'Zip', displayName: 'Zip' },
                    { field: 'Date', displayName: 'Date' }
        ],
        afterSelectionChange: function (selection, event) {
            $log.log("selection: " + selection.entity.Id);
             $location.path("comments/" + selection.entity.commentId);
        }
    };

});