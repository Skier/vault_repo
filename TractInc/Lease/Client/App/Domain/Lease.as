package App.Domain
	{
		import App.Domain.Codegen.*;        
        
        [Bindable]
        [RemoteClass(alias="TractInc.Lease.Domain.Lease")]
        public dynamic class Lease extends _Lease
        {
        	public static const LEASE_STATUS_EDIT:String = "EDIT";
        	public static const LEASE_STATUS_COMPLETE:String = "COMPLETE";
        	
        	public function FillIn(lease:Lease):void {
        	
		        LCN = lease.LCN;
		        DocumentNumber = lease.DocumentNumber;
		        Volume = lease.Volume;
		        PAGE = lease.PAGE;
		        LeaseeName = lease.LeaseeName;
		        AssigneeName = lease.AssigneeName;
		        LeassorName = lease.LeassorName;
		        AssignorName = lease.AssignorName;
		        StateFips = lease.StateFips;
		        CountyFips = lease.CountyFips;
		        UnitDepth = lease.UnitDepth;
		        FromDepth = lease.FromDepth;
		        FromFrom = lease.FromFrom;
		        ToDepth = lease.ToDepth;
		        ToFrom = lease.ToFrom;
		        WorkInt = lease.WorkInt;
		        OrrInt = lease.OrrInt;
		        NetAcres = lease.NetAcres;
		        GrossAcres = lease.GrossAcres;
		        NriAssign = lease.NriAssign;
		        RcdDate = lease.RcdDate;
		        Term = lease.Term;
		        HBR = lease.HBR;
		        Encumbrances = lease.Encumbrances;
		        EffDate = lease.EffDate;
		        PughClause = lease.PughClause;
		        DepthLimitation = lease.DepthLimitation;
		        ShutInClau = lease.ShutInClau;
		        PoolingClau = lease.PoolingClau;
		        MinimumPmt = lease.MinimumPmt;
		        Author = lease.Author;
		        Status = lease.Status;
        	}
        
        }
    }
    