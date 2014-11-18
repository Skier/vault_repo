/*
 *  $RCSfile: Resources.h,v $
 *
 *  $Revision: 1.4 $
 *
 *  last change: $Date: 2003/10/17 16:12:48 $
 */

#ifndef __ADVPCS_RESOURCES_H__ 
#define __ADVPCS_RESOURCES_H__ 

/* -------------------------- header place ---------------------------------- */
#include <wx/colour.h> 
/* -------------------------- implementation place -------------------------- */

extern const wxString ADVPCS_CFG_FILE_NAME;
extern const wxString ADVPCS_HEADER_CFG;
extern const wxString ADVPCS_DETAIL_CFG;
extern const wxString ADVPCS_TRAILER_CFG;
extern const wxString ADVPCS_COUNTER_FILE_NAME;
extern const wxString ADVPCS_AGENT_CFG;
extern const wxString ADVPCS_AGENT_ENABLE_CFG;
extern const wxString ADVPCS_HTTP_AGENT_HOST_CFG;
extern const wxString ADVPCS_HTTP_AGENT_PORT_CFG;
extern const wxString ADVPCS_HTTP_AGENT_LOGIN_CFG;
extern const wxString ADVPCS_HTTP_AGENT_UPLOAD_CFG;
extern const wxString ADVPCS_HTTP_AGENT_STATUS_CFG;
extern const wxString ADVPCS_HTTP_AGENT_SECURE_CFG;
extern const wxString ADVPCS_HTTP_AGENT_CHANGEPWD_CFG;
extern const wxString ADVPCS_HTTP_AGENT_COMPRESED_CFG;
extern const wxString ADVPCS_ROW_SIZE_CFG;
extern const wxString ADVPCS_EDI_TYPE_CFG;
extern const wxString ADVPCS_EDI_TYPE_ZIP;
extern const wxString ADVPCS_EDI_TYPE_PLAIN;

extern const wxString ADVPCS_DEBUG_CFG;
extern const wxString ADVPCS_DEBUG_LOG_CFG;

extern const wxString ADVPCS_COMPOSER_CFG;
extern const wxString ADVPCS_COMPOSER_PREFIX_CFG;
extern const wxString ADVPCS_COMPOSER_SUFFIX_CFG;

extern const wxString ADVPCS_AGENT_NAME;
extern const wxString ADVPCS_INET_MODULE;

extern const wxString ADVPCS_CFG_ROOT_NOT_FOUND;
extern const wxString ADVPCS_CFG_HEADER_NOT_FOUND;
extern const wxString ADVPCS_CFG_DETAIL_NOT_FOUND;
extern const wxString ADVPCS_CFG_TRAILER_NOT_FOUND;

extern const wxString ADVPCS_FRAME_TITLE;

extern const wxString ADVPCS_ABOUT_TITLE;       //"About"
extern const wxString ADVPCS_ABOUT_MESSAGE; //"AdvancePCS Client.\né 2002"

extern const wxString ADVPCS_FATAL_TITLE;
extern const wxString ADVPCS_ERROR_TITLE;
extern const wxString ADVPCS_INTERNAL_ERROR;

extern const wxString ADVPCS_DESC_INVALID_INDEX;          // "Invalid index"
extern const wxString ADVPCS_DESC_DEPENDENCE_ONESELF;     //  "Circular dependency"
extern const wxString ADVPCS_DESC_DEPENDENCE_RECURSIVE;   //  "Recursive dependency for"
extern const wxString ADVPCS_DESC_WRONG_XML;              //  "Wrong xml node, not all variables specified"
extern const wxString ADVPCS_DESC_WRONG_FIELD_NODE;       //  "Wrong fields parameter or default value for ["
extern const wxString ADVPCS_DESC_WRONG_BOOLEAN_NODE;     //  "Wrong boolean value"
extern const wxString ADVPCS_DESC_WRONG_CHOICE_NODE;      //  "Wrong choice node, value param not specified"
extern const wxString ADVPCS_DESC_WRONG_DEPENDENCE_NODE;  //  "Wrong dependence node, field param not specified"
extern const wxString ADVPCS_DESC_WRONG_REGEXP_NODE;      //  "Wrong regexp node. Regular expression is invalid"
extern const wxString ADVPCS_DESC_WRONG_CHOICE_ID;        //  "Choice ID must be integer value"
extern const wxString ADVPCS_WRONG_CROSS_ID;              //  "Choice Cross Id nust be integer value"


extern const wxString ADVPCS_LOGIN_AUTH_ERR_MSG;          
extern const wxString ADVPCS_LOGIN_PSWD_EXPIRED_ERR_MSG ; 
extern const wxString ADVPCS_LOGIN_INITAL_SIGNON_ERR_MSG; 
extern const wxString ADVPCS_LOGIN_EXCEEDMAX_ERR_MSG;     
extern const wxString ADVPCS_LOGIN_INACIVE_ERR_MSG;       
extern const wxString ADVPCS_LOGIN_EXPIRED_ERR_MSG;       
extern const wxString ADVPCS_LOGIN_DISABLED_ERR_MSG;      
extern const wxString ADVPCS_PSWD_INVALID_ERR_MSG;
extern const wxString ADVPCS_PSWD_DUPLICATE_ERR_MSG;
extern const wxString ADVPCS_POST_AUTH_ERR_MSG;           
extern const wxString ADVPCS_POST_PERMISSION_ERR_MSG;     
extern const wxString ADVPCS_POST_FILE_NAME_ERR_MSG;      
extern const wxString ADVPCS_POST_FORMAT_ERR_MSG;         
extern const wxString ADVPCS_POST_DUPLICATE_ERR_MSG;      
extern const wxString ADVPCS_POST_TRANSMISSION_ERR_MSG;   
extern const wxString ADVPCS_STATUS_AUTH_ERR_MSG;         
extern const wxString ADVPCS_STATUS_PERMISSION_ERR_MSG;   
extern const wxString ADVPCS_STATUS_NOT_AVAILABLE_ERR_MSG; 
extern const wxString ADVPCS_STATUS_UNKNOWN_CODE_MSG;
extern const wxString ADVPCS_UNEXPECTED_REPLY_MSG; // "Unexpected server reply"
extern const wxString ADVPCS_REPLY_FORMAT_ERR_MSG;

extern const wxString ADVPCS_COMPOSER_FORMAT_ERR_MSG;
extern const wxString ADVPCS_UNKNOWN_FIELD_ERR_MSG;
extern const wxString ADVPCS_UNKNOWN_FIELD_NAME_ERR_MSG; // "Field name %s is unknown"

extern const wxString ADVPCS_RESP_TAG;
extern const wxString ADVPCS_RESP_STATUS_TAG;
extern const wxString ADVPCS_RESP_CODE_TAG;
extern const wxString ADVPCS_RESP_MESSAGE_TAG;

extern const wxString ADVPCS_RESP_STATUS_HISTORY_TAG;
extern const wxString ADVPCS_RESP_STATUS_USERID_TAG;
extern const wxString ADVPCS_RESP_STATUS_FILENAME_TAG;
extern const wxString ADVPCS_RESP_STATUS_FILESIZE_TAG;
extern const wxString ADVPCS_RESP_STATUS_DATETIME_TAG;
extern const wxString ADVPCS_RESP_STATUS_TRACKINGREF_TAG;
extern const wxString ADVPCS_RESP_STATUS_RECORDCOUNT_TAG;
extern const wxString ADVPCS_RESP_STATUS_STATUS_TAG;

extern const wxString ADVPCS_LOG_PAGE_TITLE;
extern const wxString ADVPCS_LOG_COL_LEVEL_TITLE;
extern const wxString ADVPCS_LOG_COL_TIME_TITLE;
extern const wxString ADVPCS_LOG_COL_MSG_TITLE;

extern const wxString ADVPCS_STATUS_PAGE_TITLE;
extern const wxString ADVPCS_STATUS_COL_USERID_TITLE;
extern const wxString ADVPCS_STATUS_COL_FILENAME_TITLE;
extern const wxString ADVPCS_STATUS_COL_FILESIZE_TITLE;
extern const wxString ADVPCS_STATUS_COL_DATETIME_TITLE;
extern const wxString ADVPCS_STATUS_COL_TRACKINGREF_TITLE;
extern const wxString ADVPCS_STATUS_COL_RECORDCOUNT_TITLE;
extern const wxString ADVPCS_STATUS_COL_STATUS_TITLE;

extern const wxString ADVPCS_GRID_SAVE_TITLE; // "Save detail records to file"
extern const wxString ADVPCS_GRID_CANCEL_TITLE; // "Do you want to exit?"
extern const wxString ADVPCS_GRID_CANCEL_MESSAGE; // "Do you want to exit?"
extern const wxString ADVPCS_GRID_CANCEL_WITHOUT_SAVE_TITLE; // "Whould you like to save the data?"
extern const wxString ADVPCS_GRID_CANCEL_WITHOUT_SAVE_MESSAGE; // "File is modified. Save before exit?"
extern const wxString ADVPCS_GRID_ASK_SAVE_MESSAGE; //(_("File is modified. Whould you like to save the data?"));

extern const wxString ADVPCS_CSV_EXT;
extern const wxString ADVPCS_EDI_EXT;
extern const wxString ADVPCS_ZIP_EXT;
extern const wxString ADVPCS_NEW_FILE_NAME;    

extern const wxString ADVPCS_WIZARD_TITLE;
extern const wxString ADVPCS_WIZARD_OPEN_HEADER_TITLE;
extern const wxString ADVPCS_WIZARD_OPEN_ERROR_TITLE;
extern const wxString ADVPCS_WIZARD_OPEN_ERROR_BLANK_NAME;
extern const wxString ADVPCS_WIZARD_CANCEL_TITLE;
extern const wxString ADVPCS_WIZARD_CANCEL_MESSAGE;

// Main grid messages
extern const wxString ADVPCS_GRID_OPEN_TITLE; // "Open data file"
extern const wxString ADVPCS_GRID_OPEN_PROGRESS_TITLE; // "Building grid"
extern const wxString ADVPCS_GRID_OPEN_PROGRESS_MESSAGE; // "Building data table. Please wait..."
extern const wxString ADVPCS_GRID_OPEN_ERROR_TITLE; // "Loading error"
extern const wxString ADVPCS_GRID_OPEN_ERROR_NOT_EXIST; // "File does not exist "
extern const wxString ADVPCS_GRID_OPEN_ERROR_CANT_OPEN; // "Fail to open file "
extern const wxString ADVPCS_GRID_SAVE_TITLE; // "Save detail records to file"
extern const wxString ADVPCS_GRID_SAVE_PROGRESS_TITLE; // "Save data"
extern const wxString ADVPCS_GRID_SAVE_PROGRESS_MESSAGE; // "Preparing and saving data to file"
extern const wxString ADVPCS_GRID_PASTE_ERROR_TITLE; // "Copy/Paste error"
extern const wxString ADVPCS_GRID_PASTE_ERROR_MESSAGE; // "Fail to paste from clipboard.\n (Copied and pasted regions have different size)"
extern const wxString ADVPCS_GRID_PASTE_WRONG_CONTENT; // "Fail to paste from clipboard. \n   (Unsupported data type)"
extern const wxString ADVPCS_GRID_WRONG_CLIPBOARD; // "Fail to open clipboard"
extern const wxString ADVPCS_GRID_SEARCH_END_TITLE; // "Search"
extern const wxString ADVPCS_GRID_SEARCH_END_MESSAGE; // "Finished searching grid"
extern const wxString ADVPCS_GRID_HELP_ABOUT_TITLE; // "About"
extern const wxString ADVPCS_GRID_HELP_ABOUT_MESSAGE; // "AdvancePCS Client.\n© 2002"

extern const wxString ADVPCS_COMPOSE_ERROR_OPEN_EDI_FILE; // "Cannot open output file ["
extern const wxString ADVPCS_COMPOSE_BUILD_HEADER; // "Composing header"
extern const wxString ADVPCS_COMPOSE_BUILD_DETAIL; // "Composing detail"
extern const wxString ADVPCS_COMPOSE_ADDING_TRAILER; // "Adding trailer"
extern const wxString ADVPCS_COMPOSE_HEADER_INVALID; //"Header is invalid. Please correct and try again."
extern const wxString ADVPCS_COMPOSE_DETAIL_MORE_100_ERRORS; //"Too many errors in Detail.\nPlease check selected cells and tryagain."
extern const wxString ADVPCS_COMPOSE_DETAIL_INVALID; //"Please check selected cells and try again."
extern const wxString ADVPCS_COMPOSE_DETAIL_EMPTY; //"Grid is empty. Empty file cannot be sent."
extern const wxString ADVPCS_COMPOSE_OVERWRITING_TITLE; // "Question"
extern const wxString ADVPCS_COMPOSE_CANCEL; // "Composing cancelled"
extern const wxString ADVPCS_COMPOSE_OK; // "Composed OK"

extern const wxString ADVPCS_COMPOSED_OK; //(_("File '%s' is Uploaded OK"));
extern const wxString ADVPCS_LOGIN_WAITING; //(_("Logging is in progress. Please wait..."));
extern const wxString ADVPCS_UPLOAD_WAITING; //(_("File Upload is in progress. Please wait..."));
extern const wxString ADVPCS_UPLOAD_OK; //File '%s' is Uploaded OK
extern const wxString ADVPCS_UPLOAD_FAIL; //File '%s' Uploading FAIL"));
extern const wxString ADVPCS_GET_STATUS_WAITING; //(_("Status receiving is in progress. Please wait..."));
extern const wxString ADVPCS_GET_STATUS_OK; //Status is received OK"));
extern const wxString ADVPCS_GET_STATUS_FAIL; //Status receiving FAIL"));
extern const wxString ADVPCS_CHANGE_PSWD_WAITING; //(_("Changing Password is in progress. Please wait..."));
extern const wxString ADVPCS_CHANGE_PSWD_OK; // (_("Password is changed OK"));
extern const wxString ADVPCS_CHANGE_PSWD_FAIL;// (_("Password changing FAIL"));
extern const wxString ADVPCS_CANNOT_CREATE_MORE_FILES;

extern const wxString ADVPCS_VALIDATING_ALL_OK; // "Validating OK"
extern const wxString ADVPCS_VALIDATING_DETAIL_PROCESS_TITLE; // "Validating"
extern const wxString ADVPCS_VALIDATING_HEADER_PANEL_ERROR; // "Not all the required fields filled correctly!\n"
extern const wxString ADVPCS_VALIDATING_HEADER_PANEL_ERROR_TITLE; // "Warning!"

extern const wxString ADVPCS_HEADER_PANEL_PROPOSE; // "Please fill the following fields"

extern const wxString ADVPCS_EDIT_TYPE_CHOICE;
extern const wxString ADVPCS_EDIT_TYPE_DATE;
extern const wxString ADVPCS_EDIT_TYPE_LONGDATE;
extern const wxString ADVPCS_EDIT_TYPE_MONEY;
extern const long     ADVPCS_DATE_MAX_SIZE;

#if ( wxUSE_GUI == 1 )

extern const wxColour REQUIRED_NAVY;
extern const wxColour REQUIRED_COL;
extern const wxColour VALID_COL;
extern const wxColour NOT_VALID_COL;

#endif

#endif /* __ADVPCS_RESOURCES_H__ */
