using System;
using System.Collections.Generic;

using AerSysCo.Entity;

namespace AerSysCo.Server
{

public class CatalogPackage
{
    public List<AerSysCo.Entity.Warehouse> warehouseList = null;
    public List<Category> categoryList = null;
    public List<Model> modelList = null;
    public string defaultCategory = null;
}

}
