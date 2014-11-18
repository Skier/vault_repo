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

@Entity
@Table(name = "DOC_LEASE_TRACT_HISTORY")
@SequenceGenerator(
        name="DocumentLeaseTractHistorySequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_LEASE_TRACT_HISTORY_SQC")

public class LeaseTractHistoryEntity 
    implements Serializable 
{

    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentLeaseTractHistorySequence")
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="TRACT_ID", nullable=false)
    public LeaseTractEntity tract;

    @Column(name="GEOMETRY_ID", nullable=false)    
    public Integer geometryId;

    @Column(name="CREATED", nullable=false )
    public Date created;

    @Column(name="USER_ID", nullable=true)    
    public Integer userId;

    @Column(name="USER_NAME", nullable=true )
    public String userName;

    @Column(name="NOTE", nullable=true )
    public String note;

}
