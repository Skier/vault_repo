package com.llsvc.server.doc;

import java.math.BigDecimal;

import java.util.Collection;

public class LeaseSummary
{
    public BigDecimal grossAc;
    
    public BigDecimal netAc;

    public BigDecimal interest;

    public BigDecimal leaseBurden;

    public BigDecimal leaseNri;

    public BigDecimal wi;

    public BigDecimal additionalBurden;

    public BigDecimal nri;

    public BigDecimal net;

    public Collection<String> leaseNames;

    public String toString() {
        return "LeaseSummary:"
                + "\ngrossAc=" + grossAc
                + "\nnetAc=" + netAc
                + "\ninterest=" + interest
                + "\nleaseBurden=" + leaseBurden
                + "\nleaseNri=" + leaseNri
                + "\nwi=" + wi
                + "\nadditionalBurden=" + additionalBurden
                + "\nnri=" + nri
                + "\nnet=" + net;
    }
}
