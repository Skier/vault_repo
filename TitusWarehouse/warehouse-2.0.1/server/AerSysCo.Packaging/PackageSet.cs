using System;
using System.Collections.Generic;
using System.Text;

namespace AerSysCo.Packaging
{
public class PackageSet {
    public PackageSet(Decimal weight, int length, int width, int height) {
        weightLB = weight;
        lengthIN = length;
        widthIN = width;
        heightIN = height;
    }

    public Decimal weightLB;
    public int     lengthIN;
    public int     widthIN;
    public int     heightIN;
}
}
