<?
    define("CHARSET",           "windows-1251");

    // Login page messages
    define("MSG_LOGIN",              "Login");
    define("MSG_PASSWORD",           "Password");
    define("MSG_ENTER",              "Enter");
    define("ERR_INVALID_LOGIN",      "Invalid login.");
    define("ERR_INVALID_PASSWORD",   "Invalid password.");
    define("ERR_LOGIN_ALREADY_BUSY", "This login is already busy.");
    define("ERR_USER_INACTIVE",      "This login is not active.");

    // Owner page messages
    define("MSG_EDIT_INFO",                     "Edit Info");
    define("MSG_SAVE",                          "Save");
    define("MSG_ADD_RENTAL",                    "Add Rental");
    define("MSG_LOGOUT",                        "Logout");
    define("MSG_SPECIFY_LOCATION",              "Specify location");
    define("MSG_ANOTHER_LOCATION",              "If you have not found any location your rental is located in, specify it below and click Next.");
    define("ERR_LOCATION_DESCRIPTION_REQUIRED", "Location Description required.");
    define("MSG_NEXT",                          "Next");
    define("MSG_BACK",                          "Back");

    define("MSG_SPECIFY_BASEINFO",                "Enter information about your rental");
    define("MSG_NOT_DISPLAYED",                   "This information will be hidden");
    define("MSG_RENTAL_NAME",                     "Rental Name");
    define("MSG_RENTAL_ADDRESS",                  "Rental Address");
    define("MSG_BEDROOMS_NUMBER",                 "Number of Bedrooms");
    define("MSG_ADDITIONAL_SLEEPING",             "Additional Optional Sleeping Areas");
    define("MSG_BATHS_NUMBER",                    "Number of Baths");
    define("MSG_SLEEPING_PLACES_NUMBER",          "Number of Sleeping Places");
    define("MSG_PET_FRIENDLY",                    "Pet Friendly");
    define("MSG_NO_SMOKING",                      "No Smoking");
    define("MSG_FIELDS_MARKED_WITH_ASTERISK",     "Fields marked with red asterisk are required.");
    define("ERR_RENTAL_NAME_REQUIRED",            MSG_RENTAL_NAME . " required.");
    define("ERR_RENTAL_ADDRESS_REQUIRED",         MSG_RENTAL_ADDRESS . " required.");
    define("ERR_BEDROOMS_NUMBER_REQUIRED",        MSG_BEDROOMS_NUMBER . " required.");
    define("ERR_BATHS_NUMBER_REQUIRED",           MSG_BATHS_NUMBER . " required.");
    define("ERR_SLEEPING_PLACES_NUMBER_REQUIRED", MSG_SLEEPING_PLACES_NUMBER . " required.");
    define("ERR_BEDROOMS_NUMBER_INVALID",         MSG_BEDROOMS_NUMBER . " invalid.");
    define("ERR_BATHS_NUMBER_INVALID",            MSG_BATHS_NUMBER . " invalid.");
    define("ERR_SLEEPING_PLACES_NUMBER_INVALID",  MSG_SLEEPING_PLACES_NUMBER . " invalid.");

    define("MSG_SELECT_AMENITIES",   "Please select amenities those your rental has.");
    define("MSG_SELECT_ACTIVITIES",  "Please select your rental's nearby activities.");

    define("MSG_SPECIFY_ADDITIONAL",          "Type an additional information about your rental");
    define("MSG_RENTAL_DESCRIPTION",          "Rental Description");
    define("ERR_RENTAL_DESCRIPTION_REQUIRED", MSG_RENTAL_DESCRIPTION . " required.");
    define("MSG_RATES_DESCRIPTION",           "Rates Description");
    define("ERR_RATES_DESCRIPTION_REQUIRED",  MSG_RATES_DESCRIPTION . " required.");

    define("MSG_RENTAL_ADDITION_FINAL", "So it looks like you have entered all we need to publish information about your rental. Just click Finish button below to finish the process. Also you can add a photo of your rental and/or photo of area nearby your rental.");
    define("MSG_FILE_NAME",             "File name");
    define("MSG_SELECT_PHOTO",          "Select photo");
    define("MSG_DESCRIPTION",           "Description");
    define("MSG_ADD_PHOTO",             "Add photo");
    define("ERR_FILE_NAME_REQUIRED",    MSG_FILE_NAME . " required.");
    define("MSG_FINISH",                "Finish");

    // Register owner page messages
    define("MSG_AGAIN"               , " again");
    define("MSG_EMAIL"               , "Email");
    define("MSG_PHONE_NUMBER"        , "Phone Number");
    define("MSG_MOBILE_PHONE_NUMBER" , "Mobile Phone Number");
    define("MSG_FIRST_NAME"          , "First Name");
    define("MSG_LAST_NAME"           , "Last Name");
    define("MSG_ADDRESS"             , "Address");
    define("MSG_COMMENTS"            , "Comments");
    
    define("ERR_LOGIN_REQUIRED",               MSG_LOGIN                . " required.");
    define("ERR_PASSWORD_REQUIRED",            MSG_PASSWORD             . " required.");
    define("ERR_PASSWORD_AGAIN_REQUIRED",      MSG_PASSWORD . MSG_AGAIN . " required.");
    define("ERR_EMAIL_REQUIRED",               MSG_EMAIL                . " required.");
    define("ERR_PHONE_NUMBER_REQUIRED",        MSG_PHONE_NUMBER         . " required.");
    define("ERR_MOBILE_PHONE_NUMBER_REQUIRED", MSG_MOBILE_PHONE_NUMBER  . " required.");
    define("ERR_FIRST_NAME_REQUIRED",          MSG_FIRST_NAME           . " required.");
    define("ERR_LAST_NAME_REQUIRED",           MSG_LAST_NAME            . " required.");
    define("ERR_ADDRESS_REQUIRED",             MSG_ADDRESS              . " required.");
    define("ERR_PHONE_NUMBER_INVALID",         MSG_PHONE_NUMBER         . " invalid format.");
    define("ERR_FIRST_NAME_OVERFLOW",          MSG_FIRST_NAME           . " cannot be so long.");
    define("ERR_LAST_NAME_OVERFLOW",           MSG_LAST_NAME            . " cannot be so long.");

    define("MSG_PASSWORDS",            "Passwords");
    define("ERR_PASSWORDS_NOT_AGREE",  MSG_PASSWORDS . " not agree.");

    // FindByBrowsing.html
    define("MSG_FIND_BY_BROWSING",     "Find By Browsing");

    // ShowBrowsed.html 
    define("MSG_FOUND_RENTALS",        "Found rentals");

    define("MSG_NOT_FOUND_RENTALS",    "Any rentals not found");

    // CompanyUser.html
    define("MSG_NOT_FOUND_INACTIVE_RENTALS", "Any inactive rentals not found");
    define("MSG_INACTIVE_RENTALS",           "Inactive rentals list");

    define("MSG_ACCEPT",           "Accept");
    define("MSG_ACCEPT_BASEINFO",  "Accept information about rental");
?>
