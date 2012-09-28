// Global/Common Models
// ****************************************************
var Asset = function () {

    // todo

};

// Viewmodels
// ****************************************************
var LayoutViewModel = function () {
    var self = this;

    // Properties
    // ****************************************************

    // the selected asset type of a new resource
    self.assetType = ko.observable();

    // the list of asset types
    self.assetTypes = ko.observableArray([]);

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

        // get list of asset types and allow ko binding
        $.getJSON('/api/assettype', function (response) {

            // populate bindable asset types and let ko take over
            self.assetTypes(response.Data);
        });
    };

    // Initialization
    // ****************************************************

    self.populateAssetTypes();
};

var LearnIndexViewModel = function () {
    var self = this;

    // Properties
    // ****************************************************

    // the list of assets (search results)
    self.assets = ko.observableArray([]);

    // the search term for assets
    self.assetSearchTerm = ko.observable();

    // the list of subjects (search results)
    self.subjects = ko.observableArray([]);

    // the search term for subjects
    self.subjectSearchTerm = ko.observable();

    // Methods
    // ****************************************************

    // search assets
    self.searchAssets = function () {
        
        // get list of asset types from search criteria and allow ko binding
        $.getJSON('/api/asset/search/' + self.assetSearchTerm(), function (response) {

            // populate bindable assets and let ko take over
            self.assets(response.Data);
        });
    };

    // search subjects
    self.searchSubjects = function () {
        
        // get list of subjects from search criteria and allow ko binding
        $.getJSON('/api/subject/search/' + self.subjectSearchTerm(), function (response) {

            // populate bindable subjects and let ko take over
            self.subjects(response.Data);
        });
    };

    // Initialization
    // ****************************************************

    // nothing yet
};

var LearnDetailViewModel = function (id) {
    var self = this;

    // Properties
    // ****************************************************

    // the learning asset
    self.asset = ko.observable();

    // comments on the learning asset
    self.comments = ko.observableArray([]);

    // store the passed param id for specific learning asset
    self.id = id;


    // Methods
    // ****************************************************

    // load asset
    self.loadAsset = function () {

        // call rest service to get asset by specified id
        $.getJSON('/api/asset/' + self.id, function (response) {

            // populate bindable asset and let ko take over
            self.asset(response.Data);
        });
    };

    // load associated comments to the asset
    self.loadComments = function () {

        // call rest service to get comments associated with this asset
        $.getJSON('/api/asset/comments/' + self.id, function (response) {

            // populate bindable comments and let ko take over
            self.comments(response.Data);
        });
    };

    // Initialization
    // ****************************************************

    self.loadAsset();
    self.loadComments();
};

var HomeEnrollViewModel = function () {
    var self = this;

    // Properties
    // ****************************************************


    // Methods
    // ****************************************************


    // Initialization
    // ****************************************************

};

var HomeHowItWorksViewModel = function () {
    var self = this;

    // Properties
    // ****************************************************


    // Methods
    // ****************************************************


    // Initialization
    // ****************************************************

};

var HomeIndexViewModel = function () {
    var self = this;

    // Properties
    // ****************************************************


    // Methods
    // ****************************************************


    // Initialization
    // ****************************************************

};