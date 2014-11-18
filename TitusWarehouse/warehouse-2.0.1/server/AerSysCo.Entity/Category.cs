using System;

namespace AerSysCo.Entity
{
public class Category : Traceable 
{
   public int CategoryId = 0;
   public int BrandId = 0;
   public int ParentCategoryId = 0;
   public string Name = "";
};
};