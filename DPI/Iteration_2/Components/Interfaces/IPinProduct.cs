namespace DPI.Interfaces
{
	public interface IPinProduct
	{
		int		Product_Id		{ get; set; }
		string  Product_Name	{ get; set; }		
		decimal Price			{ get; set; }
		string  Expiration		{ get; set; }
		int		Unlimited		{ get; set; }
		string	Upc				{ get; set; }
		string  ReqItems		{ get; set; }
	}	
}