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
import javax.persistence.OneToOne;
import javax.persistence.ManyToOne;
import javax.persistence.JoinColumn;
import javax.persistence.PrimaryKeyJoinColumn;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name = "DOC_LEASE_EXT")
@SequenceGenerator(
        name = "DocumentLeaseExtentionSequence",
        initialValue = 1,
        allocationSize = 1,
        sequenceName = "DOC_LEASE_EXT_SQC")
        
public class LeaseExtentionEntity 
    implements Serializable 
{

    @Id
    @GeneratedValue(
            strategy=GenerationType.SEQUENCE, 
            generator="DocumentLeaseExtentionSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @OneToOne
    @JoinColumn(name="LEASE_ID", nullable=false)
    public LeaseEntity lease;
    
    @Column(name="TERM", nullable=false )
    public Integer term;
    
    @Column(name="ROYALTY", nullable=false )
    public BigDecimal royalty;
    
    @Column(name="BONUS_RATE", nullable=false )
    public BigDecimal bonusRate;
    
    @Column(name="BONUS_AMT", nullable=false )
    public BigDecimal bonusAmount;
    
    @Column(name="NOTE", nullable=true )
    public String note;

}
