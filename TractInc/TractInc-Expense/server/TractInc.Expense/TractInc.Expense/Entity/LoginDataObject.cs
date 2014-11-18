using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class LoginDataObject
    {

        public UserDataObject UserInfo;

        public AssetDataObject AssetInfo;

        public UserRoleDataObject UserRoleInfo;

        public UserAssetDataObject UserAssetInfo;

        public List<AFEDataObject> AFEs;

        public List<ProjectDataObject> Projects;

    }

}
