using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
 
namespace DPI.Interfaces
{
	//	[Serializable]
	public interface ISqlGateway
	{
		void insert(IDomainObj rec);	
		void delete(IDomainObj rec);
		void update(IDomainObj rec);
	}
}