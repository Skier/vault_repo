using System;

namespace AerSysCo.Entity {

public class OrderStatus  
{
    public const int DRAFT = -1;
    public const int SUBMITTED = 1;
    public const int CONFIRMED = 2;

    public int orderStatusId = 0;
    public string status = "";
}

}
