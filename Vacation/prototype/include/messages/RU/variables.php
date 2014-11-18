<?
    define("CHARSET", "windows-1251");

    // Login page messages
    define("MSG_LOGIN",              "�����");
    define("MSG_PASSWORD",           "������");
    define("MSG_ENTER",              "�����");
    define("ERR_INVALID_LOGIN",      "������������ �����.");
    define("ERR_INVALID_PASSWORD",   "������������ ������.");
    define("ERR_LOGIN_ALREADY_BUSY", "���� ����� ��� �����.");
    define("ERR_USER_INACTIVE",      "���� ����� ���������.");

    // Owner page messages
    define("MSG_EDIT_INFO",                     "�������������");
    define("MSG_SAVE",                          "���������");
    define("MSG_ADD_RENTAL",                    "�������� �����������");
    define("MSG_LOGOUT",                        "�����");
    define("MSG_SPECIFY_LOCATION",              "������� ��������������");
    define("MSG_ANOTHER_LOCATION",              "���� �� �� ����� ��������������, ��� ����������� ���� ��������, ������� ��� ���� � ������� �����.");
    define("ERR_LOCATION_DESCRIPTION_REQUIRED", "�������� �������������� �� �������.");
    define("MSG_NEXT",                          "�����");
    define("MSG_BACK",                          "�����");

    define("MSG_SPECIFY_BASEINFO",                "������� ������ � ����� ��������");
    define("MSG_NOT_DISPLAYED",                   "��� ���������� ����� ������");
    define("MSG_RENTAL_NAME",                     "�������� ��������");
    define("MSG_RENTAL_ADDRESS",                  "����� ��������");
    define("MSG_BEDROOMS_NUMBER",                 "���������� ������");
    define("MSG_ADDITIONAL_SLEEPING",             "�������������� ������������ �������� �����");
    define("MSG_BATHS_NUMBER",                    "���������� ����");
    define("MSG_SLEEPING_PLACES_NUMBER",          "���������� �������� ����");
    define("MSG_PET_FRIENDLY",                    "��������� �������� ��������");
    define("MSG_NO_SMOKING",                      "�� ������");
    define("MSG_FIELDS_MARKED_WITH_ASTERISK",     "����, ���������� ������� ����������, �����������.");
    define("ERR_RENTAL_NAME_REQUIRED",            MSG_RENTAL_NAME . " �� �������.");
    define("ERR_RENTAL_ADDRESS_REQUIRED",         MSG_RENTAL_ADDRESS . " �� ������.");
    define("ERR_BEDROOMS_NUMBER_REQUIRED",        MSG_BEDROOMS_NUMBER . " �� �������.");
    define("ERR_BATHS_NUMBER_REQUIRED",           MSG_BATHS_NUMBER . " �� �������.");
    define("ERR_SLEEPING_PLACES_NUMBER_REQUIRED", MSG_SLEEPING_PLACES_NUMBER . " �� �������.");
    define("ERR_BEDROOMS_NUMBER_INVALID",         MSG_BEDROOMS_NUMBER . " �������.");
    define("ERR_BATHS_NUMBER_INVALID",            MSG_BATHS_NUMBER . " �������.");
    define("ERR_SLEEPING_PLACES_NUMBER_INVALID",  MSG_SLEEPING_PLACES_NUMBER . " �������.");

    define("MSG_SELECT_AMENITIES",  "���������� ������� ��������, ��������� � ����� ��������.");
    define("MSG_SELECT_ACTIVITIES", "���������� ������� ���� ����������������, ��������� ���������� ����� ��������.");

    define("MSG_SPECIFY_ADDITIONAL",          "������� �������������� ���������� ��� ���� ��������");
    define("MSG_RENTAL_DESCRIPTION",          "�������� ��������");
    define("ERR_RENTAL_DESCRIPTION_REQUIRED", MSG_RENTAL_DESCRIPTION . " �� �������.");
    define("MSG_RATES_DESCRIPTION",           "�������� �������");
    define("ERR_RATES_DESCRIPTION_REQUIRED",  MSG_RATES_DESCRIPTION . " �� �������.");

    define("MSG_RENTAL_ADDITION_FINAL", "������, �� ����� ��� �����������, ��� ����� ��� ��� ���������� ���������� ��� ���� �����������. ������ ������� ������ ������ ���� ��� ���������� ��������. ����� �� ������ �������� ���������� ������ �������� �/��� ���������� ����������� ���������.");
    define("MSG_FILE_NAME",             "��� �����");
    define("MSG_SELECT_PHOTO",          "�������� ����������");
    define("MSG_DESCRIPTION",           "��������");
    define("MSG_ADD_PHOTO",             "�������� ����������");
    define("ERR_FILE_NAME_REQUIRED",    MSG_FILE_NAME . " �� �������.");
    define("MSG_FINISH",                "������");

    // Register owner page messages
    define("MSG_AGAIN"               , " �����");
    define("MSG_EMAIL"               , "Email");
    define("MSG_PHONE_NUMBER"        , "����� ��������");
    define("MSG_MOBILE_PHONE_NUMBER" , "����� ���������� ��������");
    define("MSG_FIRST_NAME"          , "���");
    define("MSG_LAST_NAME"           , "�������");
    define("MSG_ADDRESS"             , "�����");
    define("MSG_COMMENTS"            , "�����������");
    
    define("ERR_LOGIN_REQUIRED",               MSG_LOGIN               . " �� ������.");
    define("ERR_PASSWORD_REQUIRED",            MSG_PASSWORD            . " �� ������.");
    define("ERR_PASSWORD_AGAIN_REQUIRED",      MSG_PASSWORD . MSG_AGAIN . " �� ������.");
    define("ERR_EMAIL_REQUIRED",               MSG_EMAIL. " required");
    define("ERR_PHONE_NUMBER_REQUIRED",        MSG_PHONE_NUMBER        . " �� ������.");
    define("ERR_MOBILE_PHONE_NUMBER_REQUIRED", MSG_MOBILE_PHONE_NUMBER . " �� ������.");
    define("ERR_FIRST_NAME_REQUIRED",          MSG_FIRST_NAME          . " �� �������.");
    define("ERR_LAST_NAME_REQUIRED",           MSG_LAST_NAME           . " �� �������.");
    define("ERR_ADDRESS_REQUIRED",             MSG_ADDRESS             . " �� ������.");
    define("ERR_PHONE_NUMBER_INVALID",         MSG_PHONE_NUMBER         . " � �������� �������.");
    define("ERR_FIRST_NAME_OVERFLOW",          MSG_FIRST_NAME           . " �� ����� ���� ����� �������.");
    define("ERR_LAST_NAME_OVERFLOW",           MSG_LAST_NAME            . " �� ����� ���� ����� �������.");

    define("MSG_PASSWORDS",            "������");
    define("ERR_PASSWORDS_NOT_AGREE",  MSG_PASSWORDS . " �� ���������.");

    // FindByBrowsing.html
    define("MSG_FIND_BY_BROWSING",     "������ �� ������������");

    // ShowBrowsed.html 
    define("MSG_FOUND_RENTALS",        "��������� �����������");

    define("MSG_NOT_FOUND_RENTALS",    "�� ������ ����������� �� �������");

    // CompanyUser.html
    define("MSG_NOT_FOUND_INACTIVE_RENTALS", "�� ������ ����������� ����������� �� �������");
    define("MSG_INACTIVE_RENTALS",           "������ ���������� �����������");

    define("MSG_ACCEPT",           "�������");
    define("MSG_ACCEPT_BASEINFO",  "������� ������ � ��������");
?>
