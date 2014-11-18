using System;
using System.Collections;
using DPI.Interfaces;

namespace DPI.Components
{	
	[Serializable]
	public class IdentityMap : IMap //, Hashtable
	{
	#region Data
		Hashtable hashtable;
		int addedCnt;
		int foundCnt;
		int deletedCnt;
	#endregion

	#region	Properties
		public int Count   { get { return hashtable.Count; }}
		public int Found   { get { return foundCnt; }}
		public int Added   { get { return addedCnt; }}
		public int Removed { get { return deletedCnt; }}
	#endregion

	#region	Constructor
		public IdentityMap()
		{
			hashtable = new Hashtable();
		}
		public IdentityMap(int size)
		{
			hashtable = new Hashtable(size);
		}
	#endregion

	#region Methods
		public void Compress()
		{
			hashtable = new Hashtable(hashtable);
		}
		public IMapObj[] syncIM(IMapObj[] found) 	// replaces data objects which are already in memory
		{
			for (int i = 0; i < found.Length; i++)
			{
				if (keyExists(found[i].IKey))
				{
					found[i] = find(found[i].IKey);
					foundCnt ++;
				}
				else
				{
					add(found[i]);
					addedCnt++;
				}
			}
			return found; 
		}
		public IMapObj   find(IDomKey key)
		{
			IMapObj obj = null;
			if (keyExists(key))
			{
				obj =  (IMapObj)hashtable[key];
				if ((obj.RowState == RowState.Deleted) || (obj.RowState == RowState.Remove))
					throw new ArgumentException("The row you are trying to retrieve has been deleted", "Identity Map");
			}

			foundCnt++;	
			return obj;
		}
		
		public IMapObj   find(IMapObj rec)
		{
			if (!keyExists(rec.IKey))
				return null;

			foundCnt++;
			return find(rec.IKey);
		}
		public bool      keyExists(IDomKey key)
		{
			return hashtable.ContainsKey(key);
		}
		public bool      objExists(IMapObj obj)
		{
			return hashtable.ContainsValue(obj);
		}
		public void      add(IMapObj obj)  
		{		
			if (keyExists(obj.IKey))
				return;

			hashtable.Add(obj.IKey, obj);
			addedCnt ++;
		}
		public void      remove(IDomKey key)
		{
			hashtable.Remove(key);
			deletedCnt++;
		}
		public void      save(IUOW uow)  // itterates through collection and saves to database as needed
		{
			ArrayList ar = new ArrayList();
		
			IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
			while(enumerator.MoveNext())
			{
				if (enumerator.Value != null)
					if (enumerator.Value is IMapObj)
						switch (((IMapObj)enumerator.Value).RowState)
						{
							case RowState.Clean   :
							case RowState.Deleted :
								break;

							case RowState.Dirty :
							{
								IMapObj domObj  = (IMapObj)enumerator.Value;
								domObj.Uow = uow;
								if (domObj is IDomObj)
									ar.Add(domObj);
								else
									domObj.save();
								break;
							}
							case RowState.New :
							{
								if (enumerator.Value is IDomObj)
									((IDomObj)enumerator.Value).RefreshForeignKeys();
								
								ar.Add(enumerator.Value);
								break;
							}
							case RowState.Remove :
							{
								if (enumerator.Value is IDomObj)
									ar.Add(enumerator.Value);
								break;

							}
							default :
								throw new ApplicationException(
									"Unknowed state: " + ((IDomObj)enumerator.Value).RowState.ToString());
						}
			}
			ar.Sort();
			IDomObj[] doms = new IDomObj[ar.Count];
			ar.CopyTo(doms);

			for (int i = 0; i < doms.Length; i++)  
			{
				doms[i].Uow = uow;
				switch (doms[i].RowState)
				{
					case RowState.Dirty :
					{
						doms[i].RefreshForeignKeys();
						doms[i].save();
						break;
					}
					case RowState.New :
					{
						doms[i].RefreshForeignKeys();
						doms[i].add();	
						break;
					}
					case RowState.Remove :
					{
						doms[i].deleteIt();	
						break;
					}
					default :
						break;
				}
			}
		}
		public IMapObj[] getObjets()
		{
			ArrayList ar  = new ArrayList();
			IDictionaryEnumerator enumerator = hashtable.GetEnumerator();

			while(enumerator.MoveNext())
				if (enumerator.Value != null)
					ar.Add(enumerator.Value);

			IMapObj[] objs = new IMapObj[ar.Count];
			ar.CopyTo(objs);
			foundCnt += objs.Length; 
			return objs;
		}
		public void      clear()
		{
			hashtable.Clear();
			clearCnts();
		}
		public void clearCnts()
		{
			foundCnt = addedCnt = deletedCnt = 0;
		}
		public void ClearDomainObjs()
		{
			ArrayList ar  = new ArrayList();
			IDictionaryEnumerator enumerator = hashtable.GetEnumerator();

			while(enumerator.MoveNext())
				if (enumerator.Value != null)
					if (enumerator.Value is DomainObj)
						if (((DomainObj)enumerator.Value).RowState == RowState.Clean)
							ar.Add(enumerator.Value);

			for (int i = 0; i < ar.Count; i++)
				remove(((IMapObj)ar[i]).IKey);
			
			Compress();
		}
	#endregion
	}
	[Serializable]
	public class Key : IDomKey
	{
		int hashCode;
		public readonly string Identity;
		public Key(string part1, string part2)
		{
			Identity  = part1 + part2;
			if(Identity == null)
				throw new ArgumentException("Both parts of a key cannot be null");

			hashCode = Identity.GetHashCode();
		}
		public override int GetHashCode()
		{
			return hashCode;
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (obj == this)
				return true;
			
			if (!(obj is Key))
				return false;
			
			if (((Key)obj).hashCode == this.hashCode)
				return true;

			return false;
		}
	}
}