using System;

namespace QuickBooksAgent.Windows.UI
{

    #region QuickBooksAgentInvalidModelExeption

    public class QuickBooksAgentInvalidModelExeption : QuickBooksAgentException
    {
        public QuickBooksAgentInvalidModelExeption():base("Invalid model")
        {
        }
    }

    #endregion

    #region QuickBooksAgentInvalidCommandExeption

    public class QuickBooksAgentInvalidCommandExeption:QuickBooksAgentException
    {
        CommandName m_command;

        public CommandName Command
        {
            get { return m_command; }
            set { m_command = value; }
        }

        public QuickBooksAgentInvalidCommandExeption(CommandName command):
            base(String.Format("Invalid command {0}",command.ToString()))
        {
            m_command = command;
        }
    }

    #endregion

}