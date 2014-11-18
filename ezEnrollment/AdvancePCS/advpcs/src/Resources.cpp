/*
 *  $RCSfile: Resources.cpp,v $
 *
 *  $Revision: 1.4 $
 *
 *  last change: $Date: 2003/10/17 16:12:48 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <advpcs/Resources.h>

/* -------------------------- implementation place -------------------------- */
const wxString ADVPCS_CFG_FILE_NAME(wxT("config.xml"));
const wxString ADVPCS_HEADER_CFG(wxT("header"));
const wxString ADVPCS_DETAIL_CFG(wxT("detail"));
const wxString ADVPCS_AGENT_CFG(wxT("http-agent"));
const wxString ADVPCS_AGENT_ENABLE_CFG(wxT("enable"));
const wxString ADVPCS_HTTP_AGENT_HOST_CFG(wxT("host"));
const wxString ADVPCS_HTTP_AGENT_PORT_CFG(wxT("port"));
const wxString ADVPCS_HTTP_AGENT_LOGIN_CFG(wxT("login"));
const wxString ADVPCS_HTTP_AGENT_UPLOAD_CFG(wxT("upload"));
const wxString ADVPCS_HTTP_AGENT_STATUS_CFG(wxT("status"));
const wxString ADVPCS_HTTP_AGENT_SECURE_CFG(wxT("secure"));
const wxString ADVPCS_HTTP_AGENT_CHANGEPWD_CFG(wxT("chgpwd"));
const wxString ADVPCS_HTTP_AGENT_COMPRESED_CFG(wxT("compressed"));
const wxString ADVPCS_ROW_SIZE_CFG(wxT("row_size"));
const wxString ADVPCS_EDI_TYPE_CFG(wxT("edi-type"));
const wxString ADVPCS_EDI_TYPE_ZIP(wxT("zip"));
const wxString ADVPCS_EDI_TYPE_PLAIN(wxT("txt"));

const wxString ADVPCS_DEBUG_CFG(wxT("debug"));
const wxString ADVPCS_DEBUG_LOG_CFG(wxT("file-name"));

const wxString ADVPCS_COMPOSER_CFG(wxT("composer"));
const wxString ADVPCS_COMPOSER_PREFIX_CFG(wxT("subject"));
const wxString ADVPCS_COMPOSER_SUFFIX_CFG(wxT("suffix"));


const wxString ADVPCS_AGENT_NAME(wxT("RxClaim"));
const wxString ADVPCS_INET_MODULE(wxT("wininet.dll"));

const wxString ADVPCS_CFG_ROOT_NOT_FOUND(_("Root configuration node not found"));
const wxString ADVPCS_CFG_HEADER_NOT_FOUND(_("Header configuration not found"));
const wxString ADVPCS_CFG_DETAIL_NOT_FOUND(_("Detail configuration not found"));

const wxString ADVPCS_FRAME_TITLE(_("RxClaim"));
const wxString ADVPCS_ABOUT_TITLE(_("About"));
const wxString ADVPCS_ABOUT_MESSAGE(_("AdvancePCS Client.\né 2002"));

const wxString ADVPCS_FATAL_TITLE(_("Fatal error"));
const wxString ADVPCS_ERROR_TITLE(_("Error"));
const wxString ADVPCS_INTERNAL_ERROR(_("Internal error"));

const wxString ADVPCS_DESC_INVALID_INDEX(_("Invalid index"));
const wxString ADVPCS_DESC_DEPENDENCE_ONESELF(_("Circular dependency"));
const wxString ADVPCS_DESC_DEPENDENCE_RECURSIVE(_("Recursive dependency for"));
const wxString ADVPCS_DESC_WRONG_XML(_("Wrong xml node, not all variables specified"));
const wxString ADVPCS_DESC_WRONG_FIELD_NODE(_("Wrong fields parameter or default value for ["));
const wxString ADVPCS_DESC_WRONG_BOOLEAN_NODE(_("Wrong boolean value"));
const wxString ADVPCS_DESC_WRONG_CHOICE_NODE(_("Wrong choice node, value param not specified"));
const wxString ADVPCS_DESC_WRONG_DEPENDENCE_NODE(_("Wrong dependence node, field param not specified"));
const wxString ADVPCS_DESC_WRONG_REGEXP_NODE(_("Wrong regexp node. Regular expression is invalid"));
const wxString ADVPCS_DESC_WRONG_CHOICE_ID(_("Choice ID must be integer value"));
const wxString ADVPCS_WRONG_CROSS_ID(_("Choice Cross Id nust be integer value"));


const wxString ADVPCS_LOGIN_AUTH_ERR_MSG(_("Invalid userid  or password"));          
const wxString ADVPCS_LOGIN_PSWD_EXPIRED_ERR_MSG(_("Password expired")); 
const wxString ADVPCS_LOGIN_INITAL_SIGNON_ERR_MSG(_("Initial signon not performed")); 
const wxString ADVPCS_LOGIN_EXCEEDMAX_ERR_MSG(_("Max login attempts exceed"));     
const wxString ADVPCS_LOGIN_INACIVE_ERR_MSG(_("Userid inactive"));       
const wxString ADVPCS_LOGIN_EXPIRED_ERR_MSG(_("Userid expired"));       
const wxString ADVPCS_LOGIN_DISABLED_ERR_MSG(_("Userid disabled"));      
const wxString ADVPCS_PSWD_INVALID_ERR_MSG(_("Invalid password. Please Re-enter"));
const wxString ADVPCS_PSWD_DUPLICATE_ERR_MSG(_("Duplicate password.Please Re-enter"));
const wxString ADVPCS_POST_AUTH_ERR_MSG(_("Not loggedin"));           
const wxString ADVPCS_POST_PERMISSION_ERR_MSG(_("No permission"));     
const wxString ADVPCS_POST_FILE_NAME_ERR_MSG(_("Invalid file name"));      
const wxString ADVPCS_POST_FORMAT_ERR_MSG(_("Invalid file format"));
const wxString ADVPCS_POST_DUPLICATE_ERR_MSG(_("Duplicate file name"));
const wxString ADVPCS_POST_TRANSMISSION_ERR_MSG(_("Transmission error"));
const wxString ADVPCS_STATUS_AUTH_ERR_MSG(_("Not Loggedin"));
const wxString ADVPCS_STATUS_PERMISSION_ERR_MSG(_("No permission")); 
const wxString ADVPCS_STATUS_NOT_AVAILABLE_ERR_MSG(_("No status records available")); 
const wxString ADVPCS_STATUS_UNKNOWN_CODE_MSG(_("Unknown status code '%ld'"));
const wxString ADVPCS_UNEXPECTED_REPLY_MSG(_("Unexpected server reply"));
const wxString ADVPCS_REPLY_FORMAT_ERR_MSG(_("Wrong reply format"));

const wxString ADVPCS_COMPOSER_FORMAT_ERR_MSG(_("Invalid file format"));
const wxString ADVPCS_UNKNOWN_FIELD_ERR_MSG(_("Unknown field '%s'"));
const wxString ADVPCS_UNKNOWN_FIELD_NAME_ERR_MSG(_("Field name %s is unknown"));

const wxString ADVPCS_RESP_TAG(wxT("ediresponse"));
const wxString ADVPCS_RESP_STATUS_TAG(wxT("edistatus"));
const wxString ADVPCS_RESP_CODE_TAG(wxT("edireturncode"));
const wxString ADVPCS_RESP_MESSAGE_TAG(wxT("edimessage"));

const wxString ADVPCS_RESP_STATUS_HISTORY_TAG(wxT("ediuploadhistory"));
const wxString ADVPCS_RESP_STATUS_USERID_TAG(wxT("ediuserid"));
const wxString ADVPCS_RESP_STATUS_FILENAME_TAG(wxT("edifilename"));
const wxString ADVPCS_RESP_STATUS_FILESIZE_TAG(wxT("edifilesize"));
const wxString ADVPCS_RESP_STATUS_DATETIME_TAG(wxT("edidatetime"));
const wxString ADVPCS_RESP_STATUS_TRACKINGREF_TAG(wxT("editrackingref"));
const wxString ADVPCS_RESP_STATUS_RECORDCOUNT_TAG(wxT("edirecordcount"));
const wxString ADVPCS_RESP_STATUS_STATUS_TAG(wxT("edistatusmsg"));


const wxString ADVPCS_LOG_PAGE_TITLE(_("Log"));
const wxString ADVPCS_LOG_COL_LEVEL_TITLE(_("Level"));
const wxString ADVPCS_LOG_COL_TIME_TITLE(_("Time"));
const wxString ADVPCS_LOG_COL_MSG_TITLE(_("Message"));

const wxString ADVPCS_STATUS_PAGE_TITLE(_("Status"));
const wxString ADVPCS_STATUS_COL_USERID_TITLE(_("Userid"));
const wxString ADVPCS_STATUS_COL_FILENAME_TITLE(_("File name"));
const wxString ADVPCS_STATUS_COL_FILESIZE_TITLE(_("File size"));
const wxString ADVPCS_STATUS_COL_DATETIME_TITLE(_("Date"));
const wxString ADVPCS_STATUS_COL_TRACKINGREF_TITLE(_("Tracking"));
const wxString ADVPCS_STATUS_COL_RECORDCOUNT_TITLE(_("Record count"));
const wxString ADVPCS_STATUS_COL_STATUS_TITLE(_("Status"));

const wxString ADVPCS_GRID_SAVE_TITLE(_("Save detail records to file"));
const wxString ADVPCS_GRID_CANCEL_TITLE(_("Do you want to exit?"));
const wxString ADVPCS_GRID_CANCEL_MESSAGE(_("Do you want to exit?"));
const wxString ADVPCS_GRID_CANCEL_WITHOUT_SAVE_TITLE(_("Whould you like to save the data?"));
const wxString ADVPCS_GRID_CANCEL_WITHOUT_SAVE_MESSAGE(_("File is modified. Save before exit?"));
const wxString ADVPCS_GRID_ASK_SAVE_MESSAGE(_("File is modified. Whould you like to save the data?"));

const wxString ADVPCS_CSV_EXT(wxT(".csv"));
const wxString ADVPCS_EDI_EXT(wxT(".txt"));
const wxString ADVPCS_ZIP_EXT(wxT(".zip"));

const wxString ADVPCS_NEW_FILE_NAME(wxT("new.csv"));    

const wxString ADVPCS_WIZARD_TITLE(_("Header Wizard"));
const wxString ADVPCS_WIZARD_OPEN_HEADER_TITLE(_("Choice file"));
const wxString ADVPCS_WIZARD_OPEN_ERROR_TITLE(_("Open error"));
const wxString ADVPCS_WIZARD_OPEN_ERROR_BLANK_NAME(_("File name can't be blank"));
const wxString ADVPCS_WIZARD_CANCEL_TITLE(_("Cancelling"));
const wxString ADVPCS_WIZARD_CANCEL_MESSAGE(_("Are you sure you want to cancel?"));

// Main grid messages
const wxString ADVPCS_GRID_OPEN_TITLE(_("Open data file"));
const wxString ADVPCS_GRID_OPEN_PROGRESS_TITLE(_("Building grid"));
const wxString ADVPCS_GRID_OPEN_PROGRESS_MESSAGE(_("Building data table. Please wait..."));
const wxString ADVPCS_GRID_OPEN_ERROR_TITLE(_("Loading error"));
const wxString ADVPCS_GRID_OPEN_ERROR_NOT_EXIST(_("File does not exist "));
const wxString ADVPCS_GRID_OPEN_ERROR_CANT_OPEN(_("Failed to open file "));
const wxString ADVPCS_GRID_SAVE_PROGRESS_TITLE(_("Save data"));
const wxString ADVPCS_GRID_SAVE_PROGRESS_MESSAGE(_("Preparing and saving data to file"));
const wxString ADVPCS_GRID_PASTE_ERROR_TITLE(_("Copy/Paste error"));
const wxString ADVPCS_GRID_PASTE_ERROR_MESSAGE(_("Failed to paste from clipboard.\n (Copied and pasted regions have different size)"));
const wxString ADVPCS_GRID_PASTE_WRONG_CONTENT(_("Failed to paste from clipboard. \n   (Unsupported data type)"));
const wxString ADVPCS_GRID_WRONG_CLIPBOARD(_("Failed to open clipboard"));
const wxString ADVPCS_GRID_SEARCH_END_TITLE(_("Search"));
const wxString ADVPCS_GRID_SEARCH_END_MESSAGE(_("Finished searching grid"));
const wxString ADVPCS_GRID_HELP_ABOUT_TITLE(_("About"));
const wxString ADVPCS_GRID_HELP_ABOUT_MESSAGE(_("AdvancePCS Client.\n© 2002"));

const wxString ADVPCS_COMPOSE_ERROR_OPEN_EDI_FILE(_("Cannot open output file ["));
const wxString ADVPCS_COMPOSE_BUILD_HEADER(_("Composing header"));
const wxString ADVPCS_COMPOSE_BUILD_DETAIL(_("Composing detail"));
const wxString ADVPCS_COMPOSE_ADDING_TRAILER(_("Adding trailer"));
const wxString ADVPCS_COMPOSE_HEADER_INVALID(_("Header is invalid. Please correct and try again."));
const wxString ADVPCS_COMPOSE_DETAIL_MORE_100_ERRORS(_("Too many errors in Detail.\nPlease check selected cells and try again."));
const wxString ADVPCS_COMPOSE_DETAIL_INVALID(_("Please check selected cells and try again."));
const wxString ADVPCS_COMPOSE_DETAIL_EMPTY(_("Grid is empty. Empty file cannot be sent."));
const wxString ADVPCS_COMPOSE_OVERWRITING_TITLE(_("Question"));
const wxString ADVPCS_COMPOSE_CANCEL(_("Composing cancelled"));
const wxString ADVPCS_COMPOSE_OK(_("File '%s' composed OK"));

const wxString ADVPCS_COMPOSED_OK(_("File '%s' is Composed OK"));
const wxString ADVPCS_LOGIN_WAITING(_("Logging is in progress. Please wait..."));
const wxString ADVPCS_UPLOAD_WAITING(_("File Upload is in progress. Please wait..."));
const wxString ADVPCS_UPLOAD_OK(_("File '%s' is Uploaded OK"));
const wxString ADVPCS_UPLOAD_FAIL(_("Failed to upload file '%s'"));
const wxString ADVPCS_GET_STATUS_WAITING(_("Status receiving is in progress. Please wait..."));
const wxString ADVPCS_GET_STATUS_OK(_("Status is received OK"));
const wxString ADVPCS_GET_STATUS_FAIL(_("Failed to receive status"));
const wxString ADVPCS_CHANGE_PSWD_WAITING(_("Changing Password is in progress. Please wait..."));
const wxString ADVPCS_CHANGE_PSWD_OK(_("Password is changed OK"));
const wxString ADVPCS_CHANGE_PSWD_FAIL(_("Failed to change password"));
const wxString ADVPCS_CANNOT_CREATE_MORE_FILES (_("Cannot create more files today. \n Please try tomorrow"));


const wxString ADVPCS_VALIDATING_ALL_OK(_("Validation successsful"));
const wxString ADVPCS_VALIDATING_DETAIL_PROCESS_TITLE(_("Validating"));
const wxString ADVPCS_VALIDATING_HEADER_PANEL_ERROR(_("Not all the required fields filled correctly!\n"));
const wxString ADVPCS_VALIDATING_HEADER_PANEL_ERROR_TITLE(_("Warning!"));


const wxString ADVPCS_HEADER_PANEL_PROPOSE(_("Please fill the following fields"));

const wxString ADVPCS_EDIT_TYPE_CHOICE(wxT("choice"));
const wxString ADVPCS_EDIT_TYPE_DATE(wxT("date"));
const wxString ADVPCS_EDIT_TYPE_MONEY(wxT("money"));
const wxString ADVPCS_EDIT_TYPE_LONGDATE(wxT("longdate"));
const long     ADVPCS_DATE_MAX_SIZE = 10;

#if ( wxUSE_GUI == 1 )
const wxColour REQUIRED_NAVY(35, 35, 142);
const wxColour REQUIRED_COL(230, 250, 255);
const wxColour VALID_COL(255, 255, 255);
const wxColour NOT_VALID_COL(255, 255, 135);
#endif

