using System;
using System.Text;
using System.Collections;

using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.ClientComp
{
	public class Roles 
	{
		IPermission[] perms;
		static string separator = "%";
	
		/*		Constructors		*/
		public Roles(IPermission[] perms)
		{
			this.perms = perms;
		}
		public Roles(string saved)
		{
			if (saved.Length == 0)
			{
				perms = new Permission[0];
				return;
			}
			string[] pers = saved.Split(separator.ToCharArray());
		
			perms = new IPermission[pers.Length];
			for (int i = 0 ; i < perms.Length; i++)
			{
				perms[i] = PermissionCol.GetPermission(pers[i]);
			}
		}
		/*		Methods		*/
		public override string ToString()
		{
			if (perms == null)
				return "";

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < perms.Length; i++)
			{	
				sb.Append(perms[i].PermsName);
				if (i < perms.Length - 1)
					sb.Append(separator);
			}
			return sb.ToString();
		}
		public string[] GetRoles()
		{
			if (perms == null)
				return new string[0];
 
			string[] roles = new string[perms.Length];
			for(int i = 0; i < perms.Length; i++)
				roles[i] = perms[i].PermsName;
			
			return roles;
		}
	}	
}	