using System;
using Dalworth.SDK;

namespace Dalworth.Windows
{

    #region DalworthInvalidModelExeption

    public class DalworthInvalidModelExeption : DalworthException
    {
        public DalworthInvalidModelExeption()
            : base("Invalid model")
        {
        }
    }

    #endregion

    #region DalworthInvalidCommandExeption

    public class DalworthInvalidCommandExeption:DalworthException
    {
        CommandName m_command;

        public CommandName Command
        {
            get { return m_command; }
            set { m_command = value; }
        }

        public DalworthInvalidCommandExeption(CommandName command)
            :
            base(String.Format("Invalid command {0}",command.ToString()))
        {
            m_command = command;
        }
    }

    #endregion

}