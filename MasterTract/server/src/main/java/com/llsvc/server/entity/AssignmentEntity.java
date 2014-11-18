package com.llsvc.server.entity;

import java.io.Serializable;
import java.util.Date;
import java.util.Collection;

import java.math.BigDecimal;

import javax.persistence.Column;
import javax.persistence.CascadeType;
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
@Table(name = "DOC_ASSIGNMENT")
        
@NamedQueries({
    @NamedQuery(
        name=AssignmentEntity.FIND_ALL,
        query="select o from AssignmentEntity as o")
/*
,
    @NamedQuery(
        name=AssignmentEntity.FIND_BY_LEASE_ID,
        query="select o from AssignmentEntity as o join o.leaseSet as ls where ls.lease.document.id=:leaseId")
,
    @NamedQuery(
        name=AssignmentEntity.FIND_BY_STATE_AND_COUNTY,
        query="select o from AssignmentEntity as o join o.document as d join d.records as r where r.state.id=:stateId and r.county.id=:countyId"),
    @NamedQuery(
        name=AssignmentEntity.FIND_BY_VOLUME_AND_PAGE,
        query="select o from AssignmentEntity as o join o.document as d join d.records as r where r.state.id=:stateId and r.county.id=:countyId and r.volume=:volume and r.page=:page"),
    @NamedQuery(
        name=AssignmentEntity.FIND_BY_DOC_NO,
        query="select o from AssignmentEntity as o join o.document as d join d.records as r where r.state.id=:stateId and r.county.id=:countyId and r.docNo=:docNo and (:volume='!' or r.volume=:volume) and (:page='!' or r.page=:page)")
*/
})

public class AssignmentEntity 
    implements Serializable 
{
    public static final String FIND_ALL = "findAllAssignments";
    public static final String FIND_BY_LEASE_ID = "findAssignmentByLeaseId";
    public static final String FIND_BY_STATE_AND_COUNTY = "findAssignmentsByStateAndCounty";
    public static final String FIND_BY_VOLUME_AND_PAGE = "findAssignmentsByVolumeAndPage";
    public static final String FIND_BY_DOC_NO = "findAssignmentsByDocNo";

    @Id
    @Column(name="ID", nullable=false)    
    public Integer id;

    @OneToOne(cascade = CascadeType.ALL)
    @PrimaryKeyJoinColumn
    public DocumentEntity document;
    
    @Column(name="ROYALTY", nullable=false )
    public BigDecimal royalty;
    
    @Column(name="ASSIGN_DATE", nullable=false )
    public Date assignDate;
    
    @Column(name="EFFECT_DATE", nullable=false )
    public Date effectiveDate;
    
    @Column(name="IS_FULL_LEASE_SET", nullable=false )
    public Boolean isFullLeaseSet;
    
    @Column(name="DEPTH_SEV", nullable=true )
    public String depthSererances;
    
    @Column(name="BURDENS", nullable=true )
    public String burdens;
    
    @OneToMany(mappedBy="assignment")
    public Collection<AssignmentTractEntity> tracts;
    
}
