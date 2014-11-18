//
//  Values are 32 bit values layed out as follows:
//
//   3 3 2 2 2 2 2 2 2 2 2 2 1 1 1 1 1 1 1 1 1 1
//   1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0
//  +---+-+-+-----------------------+-------------------------------+
//  |Sev|C|R|     Facility          |               Code            |
//  +---+-+-+-----------------------+-------------------------------+
//
//  where
//
//      Sev - is the severity code
//
//          00 - Success
//          01 - Informational
//          10 - Warning
//          11 - Error
//
//      C - is the Customer code flag
//
//      R - is a reserved bit
//
//      Facility - is the facility code
//
//      Code - is the facility's status code
//
//
// Define the facility codes
//


//
// Define the severity codes
//


//
// MessageId: ATF_DEBUG_MC
//
// MessageText:
//
//  %1
//
#define ATF_DEBUG_MC                     0x4000FFFFL

//
// MessageId: ATF_WARNING_MC
//
// MessageText:
//
//  %1
//
#define ATF_WARNING_MC                   0x8000FFFEL

//
// MessageId: ATF_INFO_MC
//
// MessageText:
//
//  %1
//
#define ATF_INFO_MC                      0x0000FFFDL

//
// MessageId: ATF_ASSERT_MC
//
// MessageText:
//
//  %1
//
#define ATF_ASSERT_MC                    0xC0000001L

//
// MessageId: ATF_INVALID_CFG_MC
//
// MessageText:
//
//  %1
//
#define ATF_INVALID_CFG_MC               0xC0000002L

//
// MessageId: ATF_SYSTEM_MC
//
// MessageText:
//
//  %1
//
#define ATF_SYSTEM_MC                    0xC0000003L

//
// MessageId: ATF_MQ_MC
//
// MessageText:
//
//  %1
//
#define ATF_MQ_MC                        0xC0000004L

//
// MessageId: ATF_NOTFOUND_MC
//
// MessageText:
//
//  %1
//
#define ATF_NOTFOUND_MC                  0xC0000005L

//
// MessageId: ATF_NOMEMORY_MC
//
// MessageText:
//
//  %1
//
#define ATF_NOMEMORY_MC                  0xC0000006L

//
// MessageId: ATF_SOCKET_MC
//
// MessageText:
//
//  %1
//
#define ATF_SOCKET_MC                    0xC0000007L

//
// MessageId: ATF_NO_MORE_DATA_MC
//
// MessageText:
//
//  %1
//
#define ATF_NO_MORE_DATA_MC              0xC0000008L

//
// MessageId: ATF_UNKNOWN_MC
//
// MessageText:
//
//  %1
//
#define ATF_UNKNOWN_MC                   0xC0000009L

//
// MessageId: ATF_WRONG_TRANSID_MC
//
// MessageText:
//
//  %1
//
#define ATF_WRONG_TRANSID_MC             0xC000000AL

//
// MessageId: ATF_SERVICE_MC
//
// MessageText:
//
//  %1
//
#define ATF_SERVICE_MC                   0xC0001001L

//
// MessageId: ATF_NO_IMPLEMENT_MC
//
// MessageText:
//
//  %1
//
#define ATF_NO_IMPLEMENT_MC              0xC0001002L

//
// MessageId: ATF_USER_MC
//
// MessageText:
//
//  %1
//
#define ATF_USER_MC                      0x00005000L

