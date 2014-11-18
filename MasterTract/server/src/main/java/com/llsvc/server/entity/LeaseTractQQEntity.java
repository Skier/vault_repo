package com.llsvc.server.entity;

import java.io.Serializable;
import java.util.Date;
import java.util.Collection;

import java.math.BigDecimal;

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
@Table(name = "DOC_LEASE_TRACT_QQ")
@SequenceGenerator(
        name="DocumentLeaseTractQQSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_LEASE_TRACT_QQ_SQC")
        
public class LeaseTractQQEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentLeaseTractQQSequence")
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="LT_ID", nullable=false)
    public LeaseTractEntity leaseTract;
    
    @ManyToOne
    @JoinColumn(name="GT_ID", nullable=false)
    public TractEntity tract;
}
