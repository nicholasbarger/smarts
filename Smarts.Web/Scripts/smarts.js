// Global/Common Models
// ****************************************************
var Asset = function () {
    
    this.Id;
    this.AssetTypeId;
    this.ContributorGuid;
    this.Cost;
    this.Created;
    this.Description;
    this.Difficulty;
    this.Importance;
    this.PictureUri;
    this.IsActive;
    this.IsScoreable;
    this.IsTestRequired;
    this.PassingScore;
    this.Title;
    this.Uri;

    this.AssetType;
    this.Comments;
    this.Contributor = new WebUser();
    this.AssetToSubjectAssociations;

    this.ImportanceAsPercent;
    this.DifficultyAsPercent;
};

var AssetToSubjectAssociation = function (assetId, hashtag) {
    this.AssetId = assetId;
    this.Hashtag = hashtag;
    this.ContributorGuid;
    this.Created;

    this.Asset = new Asset();
    this.Subject = new Subject();
    this.Contributor = new WebUser();
};

var Comment = function () {

    this.Id;
    this.AssetId;
    this.ContributorGuid;
    this.Text;

    this.Contributor = new WebUser();
};

var Subject = function () {
    this.Hashtag;
    this.ContributorGuid;
    this.Created;
    this.Description;
    this.Title;

    this.Assets;
    this.Contributor;
};

var WebUser = function () {
    
    this.Guid;
    this.City;
    this.Created;
    this.Country;
    this.Email;
    this.FirstName;
    this.IsActive;
    this.IsLockedOut;
    this.LastName;
    this.Phone;
    this.PictureUri;
    this.PostalCode;
    this.Province;
    this.Score;
    this.State;
    this.Street1;
    this.Street2;
    this.Title;
    this.Username;

    this.FullName;
    this.MemberSince;
    this.OneLineAddress;
    this.AssetsAddedCount;
    this.VotesCastCount;
    this.VotesReceivedCount;
};

// Viewmodels
// ****************************************************
var LayoutViewModel = function () {
    var self = this;

    // Properties
    // ****************************************************

    // the list of asset types
    self.assetTypes = ko.observableArray([]);

    // controls whether to hide/show the advanced add features
    self.isAdvancedAdd = ko.observable();

    // controls whether the user is considered logged in and toggles visible elements
    self.isLoggedIn = ko.observable(false);

    // the full user (populated upon profile request only)
    self.user = ko.observable(new WebUser());

    // the logged in users guid
    self.uid = ko.observable();

    // the logged in users username
    self.username = ko.observable();

    // **** Asset ko model properties ****
    // ----------------------------------------------------
    // the selected asset type of a new resource
    self.assetTypeId = ko.observable();

    // optionally specifies a cost
    self.cost = ko.observable();
    
    // the description of a new resource
    self.description = ko.observable();

    // specifies the passing score if it is scoreable
    self.passingScore = ko.observable();

    // specifies whether the asset costs money
    self.isCost = ko.observable();

    // specifies whether the asset is scoreable
    self.isScoreable = ko.observable();

    // specifies whether a test is required for completion
    self.isTestRequired = ko.observable();

    // picture uri for the asset
    self.pictureUri = ko.observable();

    // the title of a new resource
    self.title = ko.observable();

    // the uri of a new resource
    self.uri = ko.observable();

    // the individual subject being added
    self.subject = ko.observable();

    // the tagged subjects added to the new resource (in the form of the association)
    self.taggedSubjects = ko.observableArray([]);
    // ----------------------------------------------------

    // Methods
    // ****************************************************

    // add a resource
    self.addResource = function () {

        // call service
        $.ajax({
            url: '/api/asset/',
            data: {
                assetTypeId: self.assetTypeId(),
                title: self.title(),
                uri: self.uri(),
                description: self.description(),
                subjectAssociations: self.taggedSubjects(),
                isScoreable: self.isScoreable(),
                isTestRequired: self.isTestRequired(),
                passingScore: self.passingScore(),
                pictureUri: self.pictureUri(),
                isCost: self.isCost(),
                cost: self.cost()
            },
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {

                // handle response
                handleSuccess(response, $('#addMessage'), 'Success, you\'ve added a new resource!', 'Bummer, something is wrong: ', true);

                // clear obj if successful
                if (response.IsSuccess) {
                    self.clearResource();
                }
            },
            error: function (response) {
                handleError(response, $('#addMessage'), 'Bummer, something is wrong: ', true);
            }
        });
    };

    // clear the ko bindings for a resource that is being or has been added
    self.clearResource = function () {

        self.assetTypeId('');  // todo: this is not resetting to the proper default value
        self.cost('');
        self.description('');
        self.passingScore('');
        self.isCost(false);
        self.isScoreable(false);
        self.isTestRequired(false);
        self.title('');
        self.uri('');
        self.subject('');
        self.taggedSubjects().length = 0;  // todo: this is not clearing the array
    };

    // get the user to be displayed in the profile
    self.getUser = function () {

        // get full user and allow ko binding
        $.getJSON('/api/webuser/', { Guid: self.uid }, function (response) {

            // populate user and let ko take over
            self.user(response.Data);
        });
    };

    // logout and clear cookies
    self.logout = function () {

        // call service
        $.ajax({
            url: '/api/webuser/logout/',
            data: { Guid: self.uid },
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {
                handleSuccess(response, $('#accountMessage'), 'Feel safe, you\'ve been successfully logged out.', 'Doh! We malfunctioned: ', true);

                // handle successful login
                if (response.IsSuccess) {

                    // clear all cookies
                    clearCookies();

                    // redirect to homepage
                    window.location = '/';
                }
            },
            error: function (response) {
                handleError(response, $('#accountMessage'), 'Doh! We malfunctioned: ', true);
            }
        });
    };

    // populate list of asset types
    self.populateAssetTypes = function () {

        // get list of asset types and allow ko binding
        $.getJSON('/api/assettype/', function (response) {

            // populate bindable asset types and let ko take over
            self.assetTypes(response.Data);
        });
    };

    // hide or show the advanced add features
    self.showAdvancedAdd = function () {

        // toggle ko property which controls hide/show of advanced add div
        self.isAdvancedAdd(!self.isAdvancedAdd());
    };

    // remove a tagged subject
    self.removeSubject = function (subject) {

        // get hashtag of subject to remove
        self.taggedSubjects.remove(subject);
    };

    // add a subject to a new asset and clear the current subject
    self.tagSubject = function () {

        // add to tagged subjects collection
        var subject = new AssetToSubjectAssociation();
        subject.Hashtag = self.subject();

        self.taggedSubjects.push(subject);

        // clear current subject just added
        self.subject('');
    };

    // set visual elements if logged in
    self.toggleLoggedInUI = function () {

        // set isLoggedIn flag which is attached to UI elements through ko bindings
        var cookie = getCookie('uid');
        if (cookie != null) {
            self.isLoggedIn(true);
        }
    };

    // Initialization
    // ****************************************************

    self.populateAssetTypes();
    self.toggleLoggedInUI();
    self.username(getCookie('uname'));
    self.uid(getCookie('uid'));
};

var LearnIndexViewModel = function () {
    var self = this;

    // Properties
    // ****************************************************

    // the list of assets (search results)
    self.assets = ko.observableArray([]);

    // the search term for assets
    self.assetSearchTerm = ko.observable();

    // the throttled value for searching assets
    self.assetSearchTermThrottled = ko.computed(self.assetSearchTerm).extend({ throttle: 250 });

    // specifies whether the user has attempted to search for assets yet
    self.hasSearchedAlready = ko.observable(false);

    // specifies whether the user has attempted to search for subjects yet
    self.hasSearchedSubjectsAlready = ko.observable(false);

    // the list of subjects (search results)
    self.subjects = ko.observableArray([]);

    // the search term for subjects
    self.subjectSearchTerm = ko.observable();

    // the throttled value for searching subjects
    self.subjectSearchTermThrottled = ko.computed(self.subjectSearchTerm).extend({ throttle: 250 });
    

    // Methods
    // ****************************************************

    // search assets
    self.searchAssets = function () {
        
        // get list of assets from search criteria and allow ko binding
        $.getJSON('/api/asset/search/' + self.assetSearchTerm(), function (response) {

            // populate bindable assets and let ko take over
            self.assets(response.Data);
        });

        // set the flag that the user has searched now
        self.hasSearchedAlready(true);
    };

    // search assets by selected subject
    self.searchAssetsBySubject = function (data, event) {

        // clean hashtag - WebAPI route does not like the '#'
        var hashtag = data.Hashtag.replace('#', '');

        // get list of assets from selected hashtag subject
        $.getJSON('/api/asset/subject/' + hashtag, function (response) {

            // populate bindable assets and let ko take over
            self.assets(response.Data);
        });

        // set the flag that the user has searched now
        self.hasSearchedAlready(true);
    };

    // search subjects
    self.searchSubjects = function () {
        
        // get list of subjects from search criteria and allow ko binding
        $.getJSON('/api/subject/search/' + self.subjectSearchTerm(), function (response) {

            // populate bindable subjects and let ko take over
            self.subjects(response.Data);
        });

        // set the flag that the user has searched now
        self.hasSearchedSubjectsAlready(true);
    };

    // Initialization
    // ****************************************************

    // setup throttling to call searchAssets
    self.assetSearchTermThrottled.subscribe(function (val) {
        if (val != '') {
            self.searchAssets();
        }
    });

    // setup throttling to call searchSubjects
    self.subjectSearchTermThrottled.subscribe(function (val) {
        if (val != '') {
            self.searchSubjects();
        }
    });
};

var LearnDetailViewModel = function (id) {
    var self = this;

    // Properties
    // ****************************************************

    // the list of activities for the asset
    self.activities = ko.observableArray([]);

    // the learning asset
    self.asset = ko.observable(new Asset());

    // a new comment (text) being added
    self.comment = ko.observable();

    // comments on the learning asset
    self.comments = ko.observableArray([]);

    // store the passed param id for specific learning asset
    self.id = id;

    // subjects (tags) on the learning asset
    self.subjects = ko.observableArray([]);

    // Methods
    // ****************************************************

    // add comment
    self.addComment = function () {

        // save the comment
        $.ajax({
            url: '/api/asset/comment/',
            data: { assetId: self.id, text: self.comment() },
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {

                handleSuccess(response, $('#message'), 'Great, thank you for the contribution!', 'Yikes! There was an error while adding the comment: ', true);

                // if success, add to observable array and close modal popup
                if (response.IsSuccess) {
                    self.comments().push(response.Data);
                    $('#addCommentPopup').modal('hide');
                }
            },
            error: function (response) {

                handleError(response, $('#message'), 'Yikes! There was an error while adding the comment: ', true);
            }
        });
    };

    // edit asset
    self.edit = function () {
    
        // because the edit is fairly involved, let's redirect this to a new page until we come up with a clean way to do this inline
        window.location = '/Learn/Edit/' + self.id;
    };

    // load activities
    self.loadActivities = function () {

        // call rest service to get activities by asset
        $.getJSON('/api/asset/' + self.id + '/activity/', function (response) {

            // populate bindable list of activities
            self.activities(response.Data);
        });
    };

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
        $.getJSON('/api/asset/' + self.id + '/comments/', function (response) {

            // populate bindable comments and let ko take over
            self.comments(response.Data);
        });
    };

    // load associated subjects to the asset
    self.loadSubjects = function () {

        // call rest service to get subjects associated with this asset
        $.getJSON('/api/asset/' + self.id + '/subjects/', function (response) {

            // populate bindable subjects and let ko take over
            self.subjects(response.Data);
        });
    };

    // Initialization
    // ****************************************************

    self.loadAsset();
    self.loadActivities();
    self.loadComments();
    self.loadSubjects();
};

var LearnEditViewModel = function (id) {
    var self = this;

    // Properties
    // ****************************************************

    // the original read-only asset loaded prior to changes
    self.asset = ko.observable(new Asset());

    // store the passed param id for specific learning asset
    self.id = id;

    // **** Asset ko model properties ****
    // ----------------------------------------------------
    // the selected asset type of a new resource
    self.assetTypeId = ko.observable();

    // the description of a new resource
    self.description = ko.observable();

    // the title of a new resource
    self.title = ko.observable();

    // the uri of a new resource
    self.uri = ko.observable();

    // the picture uri for the resource
    self.pictureUri = ko.observable();

    // ----------------------------------------------------

    // Methods
    // ****************************************************

    // cancel and return to previous page
    self.cancel = function () {

        // generic redirect call
        returnToPreviousPage();
    };

    // load asset
    self.loadAsset = function () {

        // call rest service to get asset by specified id
        $.getJSON('/api/asset/' + self.id, function (response) {

            var data = response.Data;

            // populate the read-only asset
            self.asset(data);

            // populate bindable ko properties to allow for editing
            self.assetTypeId(data.AssetTypeId);
            self.title(data.Title);
            self.uri(data.Uri);
            self.description(data.Description);
            self.pictureUri(data.PictureUri);

            // HACK: knockoutjs and nicedit does not work together, having to manually read/set the description value
            // hopefully this will be fixed soon in the next knockoutjs release.
            nicEditors.findEditor('description').setContent(self.description());
        });
    };

    // save changes for the asset
    self.save = function () {

        // HACK: knockoutjs and nicedit does not work together, having to manually read/set the description value
        // hopefully this will be fixed soon in the next knockoutjs release.
        self.description(nicEditors.findEditor('description').getContent());

        // save the updates for this asset
        $.ajax({
            url: '/api/asset/',
            data: { id: self.id, assetTypeId: self.assetTypeId(), title: self.title(), uri: self.uri(), description: self.description() },
            type: 'PUT',
            dataType: 'JSON',
            success: function (response) {

                handleSuccess(response, $('#message'), 'Great, thank you for the contribution!', 'Whoops, there was an error while updating: ', true);
            },
            error: function (response) {

                handleError(response, $('#message'), 'Whoops, there was an error while updating: ', true);
            }
        });
    };

    // Initialization
    // ****************************************************

    // load
    self.loadAsset();

    // set wysiwig editors
    nicEditors.allTextAreas();
};

var HomeEnrollViewModel = function () {
    var self = this;

    // Properties
    // ****************************************************

    // form properties
    self.city = ko.observable();
    self.confirmPassword = ko.observable();
    self.country = ko.observable();
    self.firstName = ko.observable();
    self.lastName = ko.observable();
    self.password = ko.observable();
    self.state = ko.observable();
    self.username = ko.observable();


    // Methods
    // ****************************************************

    // cancel and return to previous page
    self.cancel = function () {

        // generic redirect call
        returnToPreviousPage();
    };

    // Save the user and enroll
    self.save = function () {

        // save 
        $.ajax({
            url: '/api/webuser/',
            data: { Username: self.username(), Password: self.password(), ConfirmPassword: self.confirmPassword(), FirstName: self.firstName(), LastName: self.lastName(), Country: self.country, State: self.state(), City: self.city() },
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {

                handleSuccess(response, $('#message'), 'Congratulations, you\'ve been enrolled.  You can start tracking your learning right away!', 'Uh oh, we ran into a minor problem: ', true);
            },
            error: function (response) {

                handleError(response, $('#message'), 'Uh oh, something bad happened: ', true);
            }
        });
    };

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

    // the email to login with
    self.email = ko.observable();

    // specifies whether to show the login panel or not
    self.isLoginPanelVisible = ko.observable(false);

    // the password to login with
    self.password = ko.observable();
    

    // Methods
    // ****************************************************

    // perform login
    self.login = function () {

        // call login service
        $.ajax({
            url: '/api/webuser/login/',
            data: { Email: self.email(), Password: self.password() },
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {
                handleSuccess(response, $('#message'), 'Success logging in, now redirecting...', 'Hmm... it looks like something went wrong: ', true);

                // handle successful login
                if (response.IsSuccess) {

                    // assign cookies
                    setCookie('uid', response.Data.Guid, 1);
                    setCookie('uname', response.Data.Username, 1);
                    setCookie('ufname', response.Data.FirstName, 1);
                    setCookie('ulname', response.Data.LastName, 1);

                    // redirect to learning page
                    window.location = '/Learn/';
                }
            },
            error: function (response) {
                handleError(response, $('#message'), 'Hmm... it looks like something went wrong: ', true);
            }
        });
    };

    // toggles visibility of login panel
    self.showLoginPanel = function () {

        // toggle flag and let ko binding takeover
        self.isLoginPanelVisible(true);

        // set focus to login control
        $('#email').focus();
    };

    // Initialization
    // ****************************************************

};

// Helper Utilities
// ****************************************************

// clear all cookies
function clearCookies() {
    var cookies = document.cookie.split(";");

    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        var eqPos = cookie.indexOf("=");
        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
    }
}

// get a browser cookie
function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}

// handle ajax error responses
function handleError(response, container, errorText, displayErrorDetail) {

    // get response data (parsed when errors occur)
    var data = jQuery.parseJSON(response.responseText);

    // add error handling and failure message to user if necessary/specified
    var msg = '';
    if (displayErrorDetail) {
        for (var key in data.Errors) {
            msg = data.Errors[key];
        }
    }

    // display error msg
    showMessage(container, errorText + msg, 'error');
}

// handle ajax success responses
function handleSuccess(response, container, successText, errorText, displayErrorDetail) {

    // get response data (direct on successful transmission)
    var data = response;

    // check the response to see if successful
    if (data.IsSuccess) {

        // display success message
        // todo: change this to load the success message from response and have it account for localization
        showMessage(container, successText, 'success');
    }
    else {

        // add error handling and failure message to user if necessary
        var msg = '';
        if (displayErrorDetail) {
            for (var key in data.Errors) {
                msg = data.Errors[key];
            }
        }

        // display error msg
        showMessage(container, errorText + msg, 'error');
    }
}

// hide message
function hideMessage(container) {

    container.html('').hide();
}

// return to previous page
function returnToPreviousPage() {

    // try return and refresh
    try {
        window.location = document.referrer;
    }
    catch (ex) {
    }

    try {
        // fall back to using the back button
        history.go(-1);
    }
    catch (ex) {
    }
}

// set a browser cookie
function setCookie(c_name, value, exdays) {
    var hostname = window.location.hostname.replace('business', '');
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString()) + ";path=/;";
    if (hostname != 'localhost') {
        c_value += 'domain=' + hostname;
    }
    document.cookie = c_name + "=" + c_value;
}

// show message
function showMessage(container, msg, type) {

    container.addClass(type).html(msg).show();
}