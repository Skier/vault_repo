package com.llsvc.server.entity;

import java.io.Serializable;
import java.util.Date;
import java.util.Collection;
import java.util.Iterator;

import java.math.BigDecimal;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.OneToOne;
import javax.persistence.ManyToOne;
import javax.persistence.JoinColumn;
import javax.persistence.PrimaryKeyJoinColumn;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name = "DOC_LEASE")
        
/*
@NamedQueries({
    @NamedQuery(
        name=LeaseEntity.FIND_ALL,
        query="select o from LeaseEntity as o"),
    @NamedQuery(
        name=LeaseEntity.FIND_BY_STATE_AND_COUNTY,
        query="select o from LeaseEntity as o join o.document as d join d.records as r where r.state.id=:stateId and r.county.id=:countyId"),
    @NamedQuery(
        name=LeaseEntity.FIND_BY_VOLUME_AND_PAGE,
        query="select o from LeaseEntity as o join o.document as d join d.records as r where r.state.id=:stateId and r.county.id=:countyId and r.volume=:volume and r.page=:page"),
    @NamedQuery(
        name=LeaseEntity.FIND_BY_DOC_NO,
        query="select o from LeaseEntity as o join o.document as d join d.records as r where r.state.id=:stateId and r.county.id=:countyId and r.docNo=:docNo and (:volume='!' or r.volume=:volume) and (:page='!' or r.page=:page)"),
    @NamedQuery(
            name=LeaseEntity.FIND_COUNT,
            query="select count(o) from LeaseEntity as o")
})
*/

public class LeaseEntity 
    implements Serializable 
{
/*
    public static final String FIND_COUNT = "findLeasesCount";
    public static final String FIND_ALL = "findAllLeases";
    public static final String FIND_BY_STATE_AND_COUNTY = "findLeasesByStateAndCounty";
    public static final String FIND_BY_VOLUME_AND_PAGE = "findLeasesByVolumeAndPage";
    public static final String FIND_BY_DOC_NO = "findLeasesByDocNo";
*/
    @Id
    @Column(name="ID", nullable=false)    
    public Integer id;

    @OneToOne
    @PrimaryKeyJoinColumn
    public DocumentEntity document;
    
    @Column(name="PROSP_NAME", nullable=true )
    public String prospectName;
    public String getProspectName() {
        return prospectName;
    }
    
    @Column(name="LEASE_NAME", nullable=false )
    public String leaseName;
    public String getLeaseName() {
        return leaseName;
    }

    public String getRecordInfo() {
/*
        Iterator<DocumentRecordEntity> ri = document.records.iterator();
        while ( ri.hasNext() ) {
            DocumentRecordEntity r = ri.next();
            return r.county.name + ", " + r.volume + "/" + r.page;
        }
*/
        return "-";
    }

    public String getLessorName() {
        Iterator<DocumentActorEntity> ai = document.actors.iterator();
        while ( ai.hasNext() ) {
            DocumentActorEntity a = ai.next();
            if ( a.isGiver ) {
                return a.name;
            }
        }
        return "-";
    }

    public String getLesseeName() {
        Iterator<DocumentActorEntity> ai = document.actors.iterator();
        while ( ai.hasNext() ) {
            DocumentActorEntity a = ai.next();
            if ( !a.isGiver ) {
                return a.name;
            }
        }
        return "-";
    }
    
    @Column(name="LEASE_DATE", nullable=false )
    public Date leaseDate;
    public Date getLeaseDate() {
        return leaseDate;
    }
    
    @Column(name="EFFECT_DATE", nullable=false )
    public Date effectiveDate;
    public Date getEffectiveDate() {
        return effectiveDate;
    }

    public Date getExpirationDate() {
        return effectiveDate;
    }
    
    @Column(name="TERM", nullable=false )
    public Integer term;
    
    @Column(name="IS_PAID_UP", nullable=false )
    public Boolean isPaidUp;
    
    @Column(name="RENT_DUE_DATE", nullable=true )
    public Date rentDueDate;

    @Column(name="ROYALTY", nullable=false )
    public BigDecimal royalty;
    
    @Column(name="ROYALTY_INPUT", nullable=true )
    public String royaltyInput;
    
    @Column(name="BONUS_RATE", nullable=false )
    public BigDecimal bonusRate;
    
    @Column(name="BONUS_AMT", nullable=false )
    public BigDecimal bonusAmount;
    
    @Column(name="GROSS_ACRES", nullable=false )
    public BigDecimal grossAcres;
    
    @Column(name="NET_ACRES", nullable=false )
    public BigDecimal netAcres;
    
    @Column(name="NOTE", nullable=true )
    public String note;
/*    
    @OneToOne(mappedBy="lease", cascade = CascadeType.REMOVE)
    public LeaseExtentionEntity extention;
*/    
    @OneToMany(mappedBy="lease", cascade = CascadeType.REMOVE)
    public Collection<LeaseTractEntity> tracts;
    public Collection<LeaseTractEntity> getTracts() {
        return tracts;
    }

    @Column(name="LEASE_NUM", nullable=true, insertable=false, updatable=false)    
    public Integer leaseNum;
    public String getLeaseNum() {
        if ( null != leaseNum ) {
            return leaseNum.toString();
        } else {
            return "-";
        }
    }

    @Column(name="LOC_ID", nullable=true )
    public String locationId;
    
    @Column(name="VET", nullable=true )
    public Boolean vet;
    
    @Column(name="OPTIONS", nullable=true )
    public String options;
    
    @Column(name="TERM_STATUS", nullable=true )
    public String termStatus;
    
    @OneToMany(mappedBy="lease", cascade = CascadeType.REMOVE)
    public Collection<LeaseClauseEntity> clauses;

    @OneToMany(mappedBy="lease")
    public Collection<LeaseAlarmEntity> alarms;
}
