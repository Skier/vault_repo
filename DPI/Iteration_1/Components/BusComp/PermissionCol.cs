using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class PermissionCol
	{
		/*        Data        */
		static Permission[] perms;
		static DateTime lastLoad;

		/*		Properties		*/
		static Permission[] Perms
		{
			get 
			{
				if (DateTime.Now.AddMinutes(- Const.REF_INTERVAL) > lastLoad)
					LoadData();
				
				return perms;
			}
		}
		/*		Constructors		*/
		PermissionCol() 
		{
			LoadData();
			OperationMessenger.RefreshData += new EventHandler(OnRefresh);
		}
		/*		Methods		*/
		public static Permission GetPermission(string perm)
		{	
			if (perms == null)
				new PermissionCol();
			
			for (int i = 0; i < perms.Length; i++)
				if (perms[i].PermsName.Trim().ToLower() == perm.Trim().ToLower())
					return perms[i];
			
			throw new ApplicationException("Can't find Permission '" + perm + "'") ;
		}
		public static Permission[] GetPerms()
		{
			if (perms == null)
				new PermissionCol();

			Permission[] res = new Permission[perms.Length];
			for (int i = 0; i < res.Length; i++)
				res[i] = perms[i];

			return res;
		}

		/*		Implementation		*/
		static void OnRefresh(object sender, EventArgs ea)
		{
			LoadData();
		}
		static void LoadData()
		{
			UOW uow = null;

			try
			{
				uow = new UOW();
				uow.Service = "PermissionCol.LoadData()";
				perms =   Permission.getAll(uow);
				lastLoad = DateTime.Now;
			}
			finally
			{
				uow.close();
			}
		}
	}
}