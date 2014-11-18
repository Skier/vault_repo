<?
    define("CHARSET", "windows-1251");

    // Login page messages
    define("MSG_LOGIN",              "Логин");
    define("MSG_PASSWORD",           "Пароль");
    define("MSG_ENTER",              "Войти");
    define("ERR_INVALID_LOGIN",      "Неправильный логин.");
    define("ERR_INVALID_PASSWORD",   "Неправильный пароль.");
    define("ERR_LOGIN_ALREADY_BUSY", "Этот логин уже занят.");
    define("ERR_USER_INACTIVE",      "Этот логин неактивен.");

    // Owner page messages
    define("MSG_EDIT_INFO",                     "Редактировать");
    define("MSG_SAVE",                          "Сохранить");
    define("MSG_ADD_RENTAL",                    "Добавить предложение");
    define("MSG_LOGOUT",                        "Выйти");
    define("MSG_SPECIFY_LOCATION",              "Укажите местоположение");
    define("MSG_ANOTHER_LOCATION",              "Если Вы не нашли местоположение, где расположено Ваше владение, укажите его ниже и нажмите Далее.");
    define("ERR_LOCATION_DESCRIPTION_REQUIRED", "Описание местоположения не указано.");
    define("MSG_NEXT",                          "Далее");
    define("MSG_BACK",                          "Назад");

    define("MSG_SPECIFY_BASEINFO",                "Введите данные о Вашем владении");
    define("MSG_NOT_DISPLAYED",                   "Эта информация будет скрыта");
    define("MSG_RENTAL_NAME",                     "Название владения");
    define("MSG_RENTAL_ADDRESS",                  "Адрес владения");
    define("MSG_BEDROOMS_NUMBER",                 "Количество спален");
    define("MSG_ADDITIONAL_SLEEPING",             "Дополнительные опциональные спальные места");
    define("MSG_BATHS_NUMBER",                    "Количество ванн");
    define("MSG_SLEEPING_PLACES_NUMBER",          "Количество спальных мест");
    define("MSG_PET_FRIENDLY",                    "Разрешены домашние животные");
    define("MSG_NO_SMOKING",                      "Не курить");
    define("MSG_FIELDS_MARKED_WITH_ASTERISK",     "Поля, помеченные красной звездочкой, обязательны.");
    define("ERR_RENTAL_NAME_REQUIRED",            MSG_RENTAL_NAME . " не указано.");
    define("ERR_RENTAL_ADDRESS_REQUIRED",         MSG_RENTAL_ADDRESS . " не указан.");
    define("ERR_BEDROOMS_NUMBER_REQUIRED",        MSG_BEDROOMS_NUMBER . " не указано.");
    define("ERR_BATHS_NUMBER_REQUIRED",           MSG_BATHS_NUMBER . " не указано.");
    define("ERR_SLEEPING_PLACES_NUMBER_REQUIRED", MSG_SLEEPING_PLACES_NUMBER . " не указано.");
    define("ERR_BEDROOMS_NUMBER_INVALID",         MSG_BEDROOMS_NUMBER . " неверно.");
    define("ERR_BATHS_NUMBER_INVALID",            MSG_BATHS_NUMBER . " неверно.");
    define("ERR_SLEEPING_PLACES_NUMBER_INVALID",  MSG_SLEEPING_PLACES_NUMBER . " неверно.");

    define("MSG_SELECT_AMENITIES",  "Пожалуйста укажите удобства, имеющиеся в Вашем владении.");
    define("MSG_SELECT_ACTIVITIES", "Пожалуйста укажите виды времяпровождения, возможные поблизости Вашем владения.");

    define("MSG_SPECIFY_ADDITIONAL",          "Введите дополнительную информацию про ваше владение");
    define("MSG_RENTAL_DESCRIPTION",          "Описание владения");
    define("ERR_RENTAL_DESCRIPTION_REQUIRED", MSG_RENTAL_DESCRIPTION . " не указано.");
    define("MSG_RATES_DESCRIPTION",           "Описание налогов");
    define("ERR_RATES_DESCRIPTION_REQUIRED",  MSG_RATES_DESCRIPTION . " не указано.");

    define("MSG_RENTAL_ADDITION_FINAL", "Похоже, Вы ввели все необходимое, что нужно нам для публикации информации про Ваше предложение. Просто нажмите кнопку Готово ниже для завершения процесса. Также Вы можете добавить фотографию Вашего владения и/или фотографию близлежащей местности.");
    define("MSG_FILE_NAME",             "Имя файла");
    define("MSG_SELECT_PHOTO",          "Выберите фотографию");
    define("MSG_DESCRIPTION",           "Описание");
    define("MSG_ADD_PHOTO",             "Добавить фотографию");
    define("ERR_FILE_NAME_REQUIRED",    MSG_FILE_NAME . " не указано.");
    define("MSG_FINISH",                "Готово");

    // Register owner page messages
    define("MSG_AGAIN"               , " снова");
    define("MSG_EMAIL"               , "Email");
    define("MSG_PHONE_NUMBER"        , "Номер Телефона");
    define("MSG_MOBILE_PHONE_NUMBER" , "Номер Мобильного Телефона");
    define("MSG_FIRST_NAME"          , "Имя");
    define("MSG_LAST_NAME"           , "Фамилия");
    define("MSG_ADDRESS"             , "Адрес");
    define("MSG_COMMENTS"            , "Комментарии");
    
    define("ERR_LOGIN_REQUIRED",               MSG_LOGIN               . " не указан.");
    define("ERR_PASSWORD_REQUIRED",            MSG_PASSWORD            . " не указан.");
    define("ERR_PASSWORD_AGAIN_REQUIRED",      MSG_PASSWORD . MSG_AGAIN . " не указан.");
    define("ERR_EMAIL_REQUIRED",               MSG_EMAIL. " required");
    define("ERR_PHONE_NUMBER_REQUIRED",        MSG_PHONE_NUMBER        . " не указан.");
    define("ERR_MOBILE_PHONE_NUMBER_REQUIRED", MSG_MOBILE_PHONE_NUMBER . " не указан.");
    define("ERR_FIRST_NAME_REQUIRED",          MSG_FIRST_NAME          . " не указано.");
    define("ERR_LAST_NAME_REQUIRED",           MSG_LAST_NAME           . " не указана.");
    define("ERR_ADDRESS_REQUIRED",             MSG_ADDRESS             . " не указан.");
    define("ERR_PHONE_NUMBER_INVALID",         MSG_PHONE_NUMBER         . " в неверном формате.");
    define("ERR_FIRST_NAME_OVERFLOW",          MSG_FIRST_NAME           . " не может быть таким длинным.");
    define("ERR_LAST_NAME_OVERFLOW",           MSG_LAST_NAME            . " не может быть таким длинной.");

    define("MSG_PASSWORDS",            "Пароли");
    define("ERR_PASSWORDS_NOT_AGREE",  MSG_PASSWORDS . " не совпадают.");

    // FindByBrowsing.html
    define("MSG_FIND_BY_BROWSING",     "Искать по расположению");

    // ShowBrowsed.html 
    define("MSG_FOUND_RENTALS",        "Найденные предложения");

    define("MSG_NOT_FOUND_RENTALS",    "Ни одного предложения не найдено");

    // CompanyUser.html
    define("MSG_NOT_FOUND_INACTIVE_RENTALS", "Ни одного неактивного предложения не найдено");
    define("MSG_INACTIVE_RENTALS",           "Список неактивных предложений");

    define("MSG_ACCEPT",           "Принять");
    define("MSG_ACCEPT_BASEINFO",  "Принять данные о владении");
?>
