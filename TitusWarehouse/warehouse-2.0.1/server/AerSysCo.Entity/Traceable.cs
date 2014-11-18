using System;

namespace AerSysCo.Entity
{

public abstract class Traceable {
    public string createdByUser = "";
    public DateTime lastUpdateDate = DateTime.Now;
    public DateTime dateCreated = DateTime.Now; 
};

}