using System;
using MobileTech.Domain;

namespace MobileTech.Windows.UI
{

    #region MobileTechInvalidModelExeption

    public class MobileTechInvalidModelExeption : MobileTechException
    {
        public MobileTechInvalidModelExeption():base("Invalid model")
        {
        }
    }

    #endregion

    #region MobileTechInvalidItemTypeException

    public class MobileTechInvalidItemTypeException : MobileTechException
    {
        ItemTypeEnum m_allowedType;

        public ItemTypeEnum AllowedType
        {
            get { return m_allowedType; }
            set { m_allowedType = value; }
        }
        ItemTypeEnum m_passedType;

        public ItemTypeEnum PassedType
        {
            get { return m_passedType; }
            set { m_passedType = value; }
        }

        public MobileTechInvalidItemTypeException(ItemTypeEnum allowedType, ItemTypeEnum passedType)
            : base(
            String.Format("Invalid item type, allowed only {0} type, but {1} type was passed", 
            allowedType.ToString(),
            passedType.ToString()))
        {

            m_allowedType = allowedType;
            m_passedType = passedType;
        }
    }

    #endregion

    #region MobileTechAccessException
    public abstract class MobileTechAccessException : MobileTechException
    {
        PasswordFunctionality m_passwordFunctionality;

        public PasswordFunctionality PasswordFunctionality
        {
            get { return m_passwordFunctionality; }
            set { m_passwordFunctionality = value; }
        }

        public MobileTechAccessException(PasswordFunctionality action,
            String message):base(message)
        { 
        }

        public MobileTechAccessException(PasswordFunctionality action,
            String message,Exception innerExeption)
            : base(message, innerExeption)
        {
        }
    }

    #endregion

    #region MobileTechInvalidPasswordException

    public class MobileTechAccessInvalidPasswordException : MobileTechAccessException
    {

        public MobileTechAccessInvalidPasswordException(PasswordFunctionality action):
            base(action,"Invalid password")
        {
            PasswordFunctionality = action;
        }
    }

    #endregion

    #region MobileTechAccessDeniedException

    public class MobileTechAccessDeniedException : MobileTechAccessException
    {
        public MobileTechAccessDeniedException(PasswordFunctionality action):
            base(action,"Access is denied")
        {
            PasswordFunctionality = action;
        }


        public MobileTechAccessDeniedException(PasswordFunctionality action, Exception innerExeption):
            base(action,"Access is denied",innerExeption)
        {
            PasswordFunctionality = action;
        }
    }

    #endregion

    #region MobileTechInvalidCommandExeption

    public class MobileTechInvalidCommandExeption:MobileTechException
    {
        CommandName m_command;

        public CommandName Command
        {
            get { return m_command; }
            set { m_command = value; }
        }

        public MobileTechInvalidCommandExeption(CommandName command):
            base(String.Format("Invalid command {0}",command.ToString()))
        {
            m_command = command;
        }
    }

    #endregion

}