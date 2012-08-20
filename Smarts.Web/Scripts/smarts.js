// Models
// ****************************************************

// Viewmodels
// ****************************************************
var LayoutViewModel = function () {
    var self = this;

    // Properties
    // ****************************************************

    // the selected asset type of a new resource
    self.assetType = ko.observable();

    // the list of asset types
    self.assetTypes = ko.observableArray();

    // the description of a new resource
    self.description = ko.observable();

    // the title of a new resource
    self.title = ko.observable();

    // the url of a new resource
    self.url = ko.observable();

    // Methods
    // ****************************************************

    // add a resource
    self.addResource = function () {
        $.ajax({
            url: '/api/asset/',
            data: { title: self.title(), url: self.url(), description: self.description() },
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {

                // todo: add success message

            },
            error: function (response) {

                // todo: add error handling and failure message to user if necessary

            }
        });
    };

    // populate list of asset types
    self.populateAssetTypes = function () {

        // todo: add json get call to appropriate rest method and fill self.assetTypes with results
    };

    // Initialization
    // ****************************************************

    // todo
};