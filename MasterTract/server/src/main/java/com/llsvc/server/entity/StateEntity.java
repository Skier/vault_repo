package com.llsvc.server.entity;

import java.io.Serializable;

import java.util.List;
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
import javax.persistence.OneToOne;
import javax.persistence.OneToMany;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;
import javax.persistence.Transient;

@Entity
@Table(name="GEO_STATE")
@SequenceGenerator(
        name = "GeoStateSequence",
        initialValue = 1,
        allocationSize = 1,
        sequenceName = "GEO_STATE_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=StateEntity.FIND_ALL,
        query="select o from StateEntity as o")
})

public class StateEntity 
    implements Serializable 
{
    public static final String FIND_ALL = "findAllStates";

    @Id
    @GeneratedValue(
            strategy=GenerationType.SEQUENCE, 
            generator="GeoStateSequence")
            
    @Column(name="ID", nullable=false)
    public Integer id;
    
    @Column(name="NAME", nullable=false, length=50)
    public String name; 

    @Column(name="FIPS", nullable=false, length=2)
    public String fips; 

    @Column(name="ABBR", nullable=false, length=2)
    public String abbr; 

    @OneToMany(mappedBy="state")
    public Collection<CountyEntity> counties;

}
