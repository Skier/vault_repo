
package com.dalworth.leadCentral.domain
{
    import com.dalworth.leadCentral.domain.codegen.*;
    
    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.TransactionType")]
    public class TransactionType extends _TransactionType
    {
        public static const INCOME_CALL:int = 1;
        public static const OUTCOME_CALL:int = 2;
        public static const INCOME_SMS:int = 3;
        public static const OUTCOME_SMS:int = 4;
        public static const CALLER_ID_LOOKUP:int = 5;
        public static const VOICE_TRANSCRIBE:int = 6;
        public static const APPLICATION_CHARGE:int = 7;
        public static const PHONE_NUMBER_CHARGE:int = 8;
        public static const RECURRING_PAYMENT:int = 9;
        public static const EXTRA_PAYMENT:int = 10;
        public static const INCOME_TOLL_FREE_CALL:int = 11;

        public function TransactionType()
        {
        }
        
        public static function getTypeNameById(id:int):String 
        {
        	if (id < 7)
        		return "Usage Charge";
        		
        	switch (id)
        	{
        		case APPLICATION_CHARGE :
        			return "Application Charge";
        			
        		case PHONE_NUMBER_CHARGE :
        			return "Phone Number Charge";
        			
        		case RECURRING_PAYMENT :
        			return "Recurring Payment";
        			
        		case EXTRA_PAYMENT :
        			return "Extra Payment";

        		default :
        			return "";
        	}
        }

        public static function getTypeDescriptionById(id:int):String 
        {
        	switch (id)
        	{
        		case INCOME_CALL :
        			return "Inbound Call";
        			
        		case OUTCOME_CALL :
        			return "Outbound Call";
        			
        		case INCOME_TOLL_FREE_CALL :
        			return "Inbound Tollfree Call";
        			
        		case INCOME_SMS :
        			return "Inbound SMS";
        			
        		case OUTCOME_SMS :
        			return "Outbound SMS";
        			
        		case CALLER_ID_LOOKUP :
        			return "Caller ID Lookup";
        			
        		case VOICE_TRANSCRIBE :
        			return "Transcribe Voice to Text";
        		
        		default :
        			return "";
        	}
       	}
    }
}
      