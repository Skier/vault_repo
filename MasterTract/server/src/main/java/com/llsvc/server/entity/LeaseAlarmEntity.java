package com.llsvc.server.entity;

import java.io.Serializable;
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
@Table(name = "DOC_LEASE_ALARM")
@SequenceGenerator(
        name = "DocumentLeaseAlarmSequence",
        initialValue = 1,
        allocationSize = 1,
        sequenceName = "DOC_LEASE_ALARM_SQC")
public class LeaseAlarmEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(
            strategy=GenerationType.SEQUENCE, 
            generator="DocumentLeaseAlarmSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="LEASE_ID", nullable=false)
    public LeaseEntity lease;
    
    @ManyToOne
    @JoinColumn(name="CLAUSE_ID", nullable=true)
    public LeaseClauseEntity clause;
    
    @Column(name="ALARM_DATE", nullable=false )
    public Date alarmDate;

    @Column(name="IS_ACTIVE", nullable=false )
    public Boolean isActive;
}
