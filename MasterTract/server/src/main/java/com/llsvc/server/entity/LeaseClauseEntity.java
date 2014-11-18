package com.llsvc.server.entity;

import java.io.Serializable;

import java.math.BigDecimal;

import java.util.Date;
import java.util.Collection;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToOne;
import javax.persistence.OneToMany;
import javax.persistence.ManyToOne;
import javax.persistence.JoinColumn;
import javax.persistence.PrimaryKeyJoinColumn;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name = "DOC_LEASE_CLAUSE2")
@SequenceGenerator(
        name = "DocumentLeaseClauseSequence",
        initialValue = 1,
        allocationSize = 1,
        sequenceName = "DOC_LEASE_CLAUSE_SQC")
public class LeaseClauseEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(
            strategy=GenerationType.SEQUENCE, 
            generator="DocumentLeaseClauseSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="LEASE_ID", nullable=false)
    public LeaseEntity lease;
    
    @Column(name="CODE", nullable=false )
    public String code;

    @Column(name="NAME", nullable=true )
    public String name;

    @Column(name="DESCR", nullable=true )
    public String description;

    @Column(name="CREATED", nullable=false )
    public Date created;

    @Column(name="MODIFIED", nullable=false )
    public Date modified;

    @Column(name="IS_ACTIVE", nullable=false )
    public Boolean isActive;

    @Column(name="DETAILS", nullable=true )
    public String details;

    @Column(name="TERM", nullable=true )
    public Integer term;
    
    @Column(name="ROYALTY", nullable=true )
    public BigDecimal royalty;
    
    @Column(name="BONUS_RATE", nullable=true )
    public BigDecimal bonusRate;
    
    @Column(name="BONUS_AMT", nullable=true )
    public BigDecimal bonusAmount;
    
    @OneToMany(mappedBy="clause", cascade = CascadeType.REMOVE)
    public Collection<LeaseAlarmEntity> alarms;
}
