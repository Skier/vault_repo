#ifndef __RXCLAIM_MESSAGES_H___
#define __RXCLAIM_MESSAGES_H___

// Main
#define FRAME_TITLE "RxClaim"
#define FRAME_LOG_TITLE "RxClaim Messenger"

#define CONFIGURATION   "config.xml"
#define HEADER_SUB_CONF "header"
#define DETAIL_SUB_CONF "detail"
#define MAIL_SUB_CONF   "http-agent"

#define DATA_FILE_KEY   "-df"
#define VERIFY_KEY      "-verify"
#define NOMAIL_KEY      "-nomail"
#define CONFIG_FILE_KEY "-cfg"
#define EDI_PATH        "-edi"

#define USAGE "usage: RxClaim.exe [-cfg=cfgfilename] [-verify] " \
    "[-nomail] -df=data_filename [-edi=edi_path | -edi=\"edi_path\"]\n"

// Loading configuration
#define LOAD_XML_FILE_CANT_OPEN "Fail to open file "
#define LOAD_XML_ROOT_NOT_FOUND "Root configuration node not found"
#define LOAD_XML_HEADER_NOT_FOUND "Header configuration not found"
#define LOAD_XML_DETAIL_NOT_FOUND "Detail configuration not found"
#define LOAD_XML_UNKNOWN_ERROR "Loading configuration error"
#define LOAD_XML_ERROR_TITLE "Problems with creating header or detail"

// Main grid messages
#define GRID_OPEN_TITLE "Open data file"
#define GRID_OPEN_PROGRESS_TITLE "Building grid"
#define GRID_OPEN_PROGRESS_MESSAGE "Building data table. Please wait..."
#define GRID_OPEN_ERROR_TITLE "Loading error"
#define GRID_OPEN_ERROR_NOT_EXIST "File does not exist "
#define GRID_OPEN_ERROR_CANT_OPEN "Fail to open file "
#define GRID_SAVE_TITLE "Save detail records to file"
#define GRID_SAVE_PROGRESS_TITLE "Save data"
#define GRID_SAVE_PROGRESS_MESSAGE "Preparing and saving data to file"
#define GRID_PASTE_ERROR_TITLE "Copy/Paste error"
#define GRID_PASTE_ERROR_MESSAGE "Fail to paste from clipboard.\n (Copied and pasted regions have different size)"
#define GRID_PASTE_WRONG_CONTENT "Fail to paste from clipboard. \n   (Unsupported data type)"
#define GRID_WRONG_CLIPBOARD "Fail to open clipboard"
#define GRID_SEARCH_END_TITLE "Search"
#define GRID_SEARCH_END_MESSAGE "Finished searching grid"
#define GRID_HELP_ABOUT_TITLE "About"
#define GRID_HELP_ABOUT_MESSAGE "AdvancePCS Client.\n© 2002"
#define GRID_CANCEL_TITLE "Do you want to exit?"
#define GRID_CANCEL_MESSAGE "Do you want to exit?"
#define GRID_CANCEL_WITHOUT_SAVE_TITLE "Whould you like to save the data?"
#define GRID_CANCEL_WITHOUT_SAVE_MESSAGE "File is modified. Save before exit?"

// Wizard messages
#define WIZARD_TITLE "Header Wizard"
#define WIZARD_OPEN_HEADER_TITLE "Choice file"
#define WIZARD_OPEN_ERROR_TITLE "Open error"
#define WIZARD_OPEN_ERROR_BLANK_NAME "File name can't be blank"
#define WIZARD_CANCEL_TITLE "Cancelling"
#define WIZARD_CANCEL_MESSAGE "Are you sure you want to cancel?"

// Descriptor messages
#define DESCRIPTOR_INVALID_INDEX "Invalid index"
#define DESCRIPTOR_DEPENDENCE_ONESELF "Circular dependency"
#define DESCRIPTOR_DEPENDENCE_RECURSIVE "Recursive dependency for"
#define DESCRIPTOR_WRONG_XML "Wrong xml node, not all variables specified"
#define DESCRIPTOR_WRONG_FIELD_NODE "Wrong fields parameter or default value for ["
#define DESCRIPTOR_WRONG_BOOLEAN_NODE "Wrong boolean value"
#define DESCRIPTOR_WRONG_CHOICE_NODE "Wrong choice node, value param not specified"
#define DESCRIPTOR_WRONG_DEPENDENCE_NODE "Wrong dependence node, field param not specified"

// Header messages
#define HEADER_PANEL_PROPOSE "Please fill the following fields"

// Validation messages
#define VALIDATING_HEADER_PANEL_ERROR "Not all the required fields filled correctly!\n"
#define VALIDATING_HEADER_PANEL_ERROR_TITLE "Warning!"
#define VALIDATING_HEADER_INVALID "Header is invalid. Please correct it and try again."
#define VALIDATING_DETAIL_INVALID "Please check selected cells and try to validate again."
#define VALIDATING_DETAIL_EMPTY "Grid is empty. Please add data."
#define VALIDATING_DETAIL_PROCESS_TITLE "Validating"
#define VALIDATING_DETAIL_PROCESS_MESSAGES "Please wait, validation in progress."
#define VALIDATING_DETAIL_TOO_MUCH_ERRORS_TITLE "Error"
#define VALIDATING_DETAIL_TOO_MUCH_ERRORS "Too many errors"
#define VALIDATING_ALL_OK "Validation successful"

// EDI encoding messages
#define COMPOSE_HEADER_INVALID "Header is invalid. Please correct and try again."
#define COMPOSE_DETAIL_INVALID "Please check selected cells and try again."
#define COMPOSE_DETAIL_EMPTY "Grid is empty. Empty file cannot be sent."
#define COMPOSE_ERROR_SETTING "Composer is not set."
#define COMPOSE_OVERWRITING_TITLE "Question"
#define COMPOSE_CANCEL "Composing cancelled"
#define COMPOSE_ERROR_OPEN_EDI_FILE "Cannot open output file ["
#define COMPOSE_WRONG_HEADER "Wrong header matrix for descriptor"
#define COMPOSE_HEADER_SKIP "Corrupt header was skipped.\n"
#define COMPOSE_WRONG_DETAIL "Wrong detail matrix for the descriptor."
#define COMPOSE_CHECKING_HEADER "Checking header"
#define COMPOSE_CHECKING_DETAIL "Checking detail"
#define COMPOSE_ADDING_TRAILER "Adding trailer"
#define COMPOSE_OK "Composed successfully"
#define VERIFY_OK "Verified successfully"
#define VERIFY_FAILED "Verification failed"

// Validation dialogs messages
#define MAILING_HEADER_INVALID "Header is invalid. Please correct and try again."
#define MAILING_DETAIL_INVALID "Please check selected cells and try again."
#define MAILING_DETAIL_EMPTY "Grid is empty.  Empty file cannot be sent."

// Internet Messages 
#define INET_ERROR "Internet Error: '%s'"
#define INET_OPENNED "Internet opened"
#define INET_MESSAGE_SENT "Trying to send request"
#define INET_MESSAGE_BODY_CREATED "Message body created"
#define INET_CANT_OPEN_FILE "Can't open file '%s'"
#define INET_REQUEST_CREATED "Request created"
#define INET_CONNECTION_OK "Connection established"
#define INET_TRANSACTION_COMPLETE "Transaction complete"
#define INET_TRANSACTION_START "--- Started ---"
#define INET_INVALID_PSWD_RESP "ERROR-2"
#define INET_INVALID_VERSION_RESP "ERROR-1"
#define INET_INVALID_PASSWORD "Invalid username or password"
#define INET_INVALID_VERSION "Version of application too old"
#define INET_UNKNOWN_ERROR "Unknown error"

// Log messages 
#define ERROR_MESSAGE_LEVEL     0
#define WARNING_MESSAGE_LEVEL   2
#define INFO_MESSAGE_LEVEL      3
#define VERIFY_MESSAGE_LEVEL    4
#define ERROR_MESSAGE_LOG    "ERROR"
#define WARNING_MESSAGE_LOG "WARNING"
#define INFO_MESSAGE_LOG    "INFO"
#define VERIFY_MESSAGE_LOG    "VERIFY"

#define VERIFY_MESSAGE_PATTERN "VERIFY: %s"
#define ERROR_MESSAGE_PATTERN "ERROR: %s\n"
#define WARNING_MESSAGE_PATTERN "WARNING: %s\n"
#define INFO_MESSAGE_PATTERN "INFO: %s\n"

#define SHOW_MESSAGE(PATTERN, DETAIL) { \
    wxString msg; \
    msg.Printf(PATTERN, DETAIL); \
    wxMessageBox(msg, "Error"); }

#define LOG_MESSAGE(LEVEL, DETAIL) { \
    LogMessage(LEVEL, wxString(DETAIL)); } 

void LogMessage(int level, wxString& message);



#endif //__RXCLAIM_MESSAGES_H___
