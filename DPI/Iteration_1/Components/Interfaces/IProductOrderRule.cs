using System;

namespace DPI.Interfaces
{
	public interface IProductOrderRule
	{
		int Id						{ get;	}
		int Product					{ get;	}
		string DmdType				{ get;	}
		decimal MinAmt				{ get;	}
		decimal MaxAmt				{ get;	}
		int ExpirationPeriod		{ get;	}
	}
}