using System;
 
namespace DPI.Interfaces
{	
	public interface IMap
	{
		/*		Properties		*/
		int Count  { get; } 
		/*		Methods		*/
		IMapObj[] syncIM(IMapObj[] found); 
		IMapObj[] getObjets();
		IMapObj   find(IDomKey key);
		IMapObj   find(IMapObj rec);
		bool      keyExists(IDomKey key);
		bool      objExists(IMapObj obj);
		void      add(IMapObj obj);  
		void      remove(IDomKey key); 
		void      save(IUOW uow);
		void      clear();
		void	  ClearDomainObjs();
	}
}