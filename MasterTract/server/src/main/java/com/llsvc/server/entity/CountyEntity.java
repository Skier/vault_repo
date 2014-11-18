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
@Table(name="GEO_COUNTY")
@SequenceGenerator(
        name = "GeoCountySequence",
        initialValue = 1,
        allocationSize = 1,
        sequenceName = "GEO_COUNTY_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=CountyEntity.FIND_ALL,
        query="select o from CountyEntity as o"),
    @NamedQuery(
        name=CountyEntity.FIND_BY_STATE_ID,
        query="select o from CountyEntity as o where o.state.id = :stateId")
})

public class CountyEntity 
    implements Serializable 
{
    public static final String FIND_ALL = "findAllCounties";
    public static final String FIND_BY_STATE_ID = "findCountiesByStateId";

    @Id
    @GeneratedValue(
            strategy=GenerationType.SEQUENCE, 
            generator="GeoCountySequence")
            
    @Column(name="ID", nullable=false)
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="STATE_ID", nullable=false)
    public StateEntity state;
    
    @Column(name="NAME", nullable=false, length=50)
    public String name; 

    @Column(name="CFIPS", nullable=false)
    public String cfips;

    @Column(name="FIPS", nullable=false)
    public String fips;

}
