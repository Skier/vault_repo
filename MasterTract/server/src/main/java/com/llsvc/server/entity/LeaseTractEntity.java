package com.llsvc.server.entity;

import java.io.Serializable;
import java.util.Date;
import java.util.Collection;

import java.math.BigDecimal;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.ManyToOne;
import javax.persistence.JoinColumn;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;
import javax.persistence.Transient;

@Entity
@Table(name = "DOC_LEASE_TRACT")
@SequenceGenerator(
        name="DocumentLeaseTractSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_LEASE_TRACT_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=LeaseTractEntity.FIND_BY_TRACT_MASK,
        query="select o from LeaseTractEntity as o join o.qqs as qq join qq.tract as t where t.township=:township and t.range=:range and t.section=:section and (:qq='!' or t.qq=:qq)")
})

public class LeaseTractEntity 
    implements Serializable 
{
    public static final String FIND_BY_TRACT_MASK = "findLeaseTractsByTractMask";

    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentLeaseTractSequence")
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="LEASE_ID", nullable=false)
    public LeaseEntity lease;
    
    @Column(name="TOWNSHIP", nullable=false)
    public String township; 
    public String getTownship() {
        return township;
    }

    @Column(name="RANGE", nullable=false)
    public String range;
    public String getRange() {
        return range;
    }

    @Column(name="SECTION", nullable=false)
    public String section;
    public String getSection() {
        return section;
    }

    @Column(name="TRACT", nullable=false )
    public String tract;
    public String getTract() {
        return tract;
    }

    @Transient
    public Boolean tractDescriptionChanged = Boolean.FALSE;

    @Column(name="GROSS_ACRES", nullable=false )
    public BigDecimal grossAcres;
    public BigDecimal getGrossAcres() {
        return grossAcres;
    }

    @Column(name="NET_ACRES", nullable=true )
    public BigDecimal netAcres;
    public BigDecimal getNetAcres() {
        return netAcres;
    }

    @Column(name="NOTE", nullable=true )
    public String note;

    @OneToMany(mappedBy="leaseTract", cascade = CascadeType.REMOVE)
    public Collection<LeaseTractQQEntity> qqs;

    @OneToMany(mappedBy="tract", cascade = CascadeType.REMOVE)
    public Collection<LeaseBreakdownEntity> breakdown;

    @OneToMany(mappedBy="tract", cascade = CascadeType.REMOVE)
    public Collection<LeaseTractHistoryEntity> history;

    @Column(name="NRI", nullable=true )
    public BigDecimal nri;
    public BigDecimal getNRI() {
        return nri;
    }
    
    @Column(name="CWI", nullable=true )
    public BigDecimal cwi;
    public BigDecimal getCWI() {
        return cwi;
    }
    
    @Column(name="BURDEN", nullable=true )
    public BigDecimal burden;
    public BigDecimal getBurden() {
        return burden;
    }
    
    @Column(name="CNRI", nullable=true )
    public BigDecimal cnri;
    public BigDecimal getCNRI() {
        return cnri;
    }
    
    @Column(name="LEASE_INTEREST", nullable=true )
    public BigDecimal leaseInterest;
    public BigDecimal getLeaeInterest() {
        return leaseInterest;
    }
    
    @Column(name="LEASE_BURDEN", nullable=true )
    public BigDecimal leaseBurden;
    public BigDecimal getLeaseBurden() {
        return leaseBurden;
    }
    
    @Column(name="C_NET_ACRES", nullable=true )
    public BigDecimal cNetAcres;
    public BigDecimal getCNetAcres() {
        return cnri;
    }

    @Column(name="UNITS", nullable=true )
    public String units;
    
    @Column(name="SURF_OWNER", nullable=false )
    public Boolean isSurfaceOwner;

    @Column(name="SO_CONTACT", nullable=true )
    public String surfaceOwnerContact;

    @ManyToOne
    @JoinColumn(name="STATE_ID", nullable=true)
    public StateEntity state;
    
    @ManyToOne
    @JoinColumn(name="COUNTY_ID", nullable=true)
    public CountyEntity county;
    
}
