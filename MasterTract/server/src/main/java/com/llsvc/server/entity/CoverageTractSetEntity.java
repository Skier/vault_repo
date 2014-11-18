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
@Table(name = "GEO_COVERAGE_SET")
@SequenceGenerator(
        name="GeoCoverageTractSetSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="GEO_COVERAGE_SET_SQC")

public class CoverageTractSetEntity 
    implements Serializable 
{

    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="GeoCoverageTractSetSequence")
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @Column(name="LT_ID", nullable=false)    
    public Integer externalId;
/*
    @ManyToOne
    @JoinColumn(name="LT_ID", nullable=false)
    public LeaseTractEntity leaseTract;
*/

    @ManyToOne
    @JoinColumn(name="CT_ID", nullable=false)
    public CoverageTractEntity coverageTract;
}
