using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MobileTech.Data;
using System.Collections;

namespace MobileTech.Domain
{

    public enum PasswordFunctionality
    {
        StartDay,
        EndDay,
        Load,
        Unload,
        SetupRoute,
        ChangeDate,
        Exit,
        Null
    }

    public partial class Password
    {
        #region Fields

        public const String EmptyValue = "empty";

        #endregion

        public Password() { }

        #region CheckPassword

        const String SqlCheckPassword = "Select * From Password Where (Functionality = @Functionality OR Functionality = 'Master') and PasswordValue = @PasswordValue";

        public static bool CheckAccess(PasswordFunctionality functionality, String password)
        {
            bool exist = false;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlCheckPassword))
            {
                Database.PutParameter(dbCommand, "@Functionality", functionality.ToString());
                Database.PutParameter(dbCommand, "@PasswordValue", password);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    exist = dataReader.Read();
                }
            }

            return exist;
        }

        #endregion

        #region IsPasswordExists
        const String SqlIsPasswordExists = "Select * From Password Where Functionality = @Functionality";

        public static bool IsPasswordExists(PasswordFunctionality passwordFunctionality)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlIsPasswordExists))
            {
                Database.PutParameter(dbCommand,"@Functionality", passwordFunctionality.ToString());

                using (IDataReader dataReader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
                {
                    return dataReader.Read();
                }
            }
        }
        #endregion

    }
}
