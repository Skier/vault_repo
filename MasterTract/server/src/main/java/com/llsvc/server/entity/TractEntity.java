package com.llsvc.server.entity;

import java.io.Serializable;

import java.util.Collection;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;
import javax.persistence.Transient;

@Entity
@Table(name="GEO_TRACT")
@SequenceGenerator(
        name = "GeoTractSequence",
        initialValue = 1,
        allocationSize = 1,
        sequenceName = "GEO_TRACT_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=TractEntity.FIND_ALL,
        query="select o from TractEntity as o"),
    @NamedQuery(
        name=TractEntity.FIND_BY_MASK,
        query="select t from TractEntity as t where t.township=:township and t.range=:range and t.section=:section and (:qq='!' or t.qq=:qq)")
})

public class TractEntity 
    implements Serializable 
{
    public static final String FIND_ALL = "findAllTracts";
    public static final String FIND_BY_MASK = "findTractsByMask";

    @Id
    @GeneratedValue(
            strategy=GenerationType.SEQUENCE, 
            generator="GeoTractSequence")
            
    @Column(name="ID", nullable=false)
    public Integer id;

/*    
    @ManyToOne
    @JoinColumn(name="STATE_ID", nullable=false)
    public StateEntity state;
*/    
    @Column(name="TOWNSHIP", nullable=false)
    public String township; 

    @Column(name="TWP_DIR", nullable=false)
    public String townshipDir; 

    @Column(name="RANGE", nullable=false)
    public String range;

    @Column(name="RNG_DIR", nullable=false)
    public String rangeDir;

    @Column(name="MERIDIAN", nullable=false)
    public String meridian;

    @Column(name="SECTION", nullable=false)
    public String section;

    @Column(name="QQ", nullable=false)
    public String qq;

    @Column(name="LOT", nullable=true)
    public String lot;

    @Column(name="QQ_ID", nullable=true)
    public Integer externalId;
}
