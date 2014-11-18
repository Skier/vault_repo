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
@Table(name = "GEO_COVERAGE_TRACT")
@SequenceGenerator(
        name="GeoCoverageTractSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="GEO_COVERAGE_TRACT_SQC")
        
public class CoverageTractEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="GeoCoverageTractSequence")
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @Column(name="TYPE", nullable=false)    
    public Integer type;
    
    @Column(name="NAME", nullable=true)    
    public String name;
    
    @Column(name="TOWNSHIP", nullable=true)
    public String township; 

    @Column(name="TDIR", nullable=true)
    public String tdir; 

    @Column(name="RANGE", nullable=true)
    public String range;

    @Column(name="RDIR", nullable=true)
    public String rdir;

    @Column(name="MERIDIAN", nullable=true)
    public Integer meridian;

    @Column(name="SECTION", nullable=true)
    public String section;

    @Column(name="TRACT_DESC", nullable=true)
    public String tractDescription;

    @Column(name="AC", nullable=false)
    public BigDecimal acres;

    @Column(name="THE_GEOM", nullable=true, insertable=false, updatable=false)
    public String geom;

    @OneToMany(mappedBy="coverageTract", cascade = CascadeType.ALL)
    public Collection<CoverageTractSetEntity> coverageSet;
}
