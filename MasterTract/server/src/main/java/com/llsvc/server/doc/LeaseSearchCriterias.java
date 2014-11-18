package com.llsvc.server.doc;

import java.math.BigDecimal;

import java.util.Date;
import java.util.List;

public class LeaseSearchCriterias
{
    public Integer leaseNo;

    public Integer projectId;

    public String leaseName;

    public String recordInfo;

    public DateRange expDate;

    public BigDecimalRange grossAc;
    
    public BigDecimalRange netAc;

    public BigDecimalRange interest;

    public BigDecimalRange leaseBurden;

    public BigDecimalRange leaseNri;

    public BigDecimalRange wi;

    public BigDecimalRange additionalBurden;

    public BigDecimalRange nri;

    public BigDecimalRange net;

    public List<TractSearchCriteria> tracts;
}
