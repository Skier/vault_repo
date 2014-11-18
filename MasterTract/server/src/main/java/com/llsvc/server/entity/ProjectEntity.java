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
@Table(name="ORG_PROJECT")
@SequenceGenerator(
        name = "OrgProjectSequence",
        initialValue = 1,
        allocationSize = 1,
        sequenceName = "ORG_PROJECT_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=ProjectEntity.FIND_ALL,
        query="select o from ProjectEntity as o")
})

public class ProjectEntity 
    implements Serializable 
{
    public static final String FIND_ALL = "findAllProjects";

    @Id
    @GeneratedValue(
            strategy=GenerationType.SEQUENCE, 
            generator="OrgProjectSequence")
            
    @Column(name="ID", nullable=false)
    public Integer id;
    
    @Column(name="NAME", nullable=false)
    public String name; 

    @Column(name="IS_ACTIVE", nullable=false)
    public Boolean isActive; 

    @ManyToOne
    @JoinColumn(name="CLIENT_ID", nullable=false)
    public ClientEntity client;
    
/*
    @OneToMany(mappedBy="state")
    public Collection<CountyEntity> counties;
*/

}
