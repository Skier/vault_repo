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
@Table(name="SYS_CLIENT")
@SequenceGenerator(
        name = "OrgClientSequence",
        initialValue = 1,
        allocationSize = 1,
        sequenceName = "SYS_CLIENT_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=ClientEntity.FIND_ALL,
        query="select o from ClientEntity as o")
})

public class ClientEntity 
    implements Serializable 
{
    public static final String FIND_ALL = "findAllClients";

    @Id
    @GeneratedValue(
            strategy=GenerationType.SEQUENCE, 
            generator="OrgClientSequence")
            
    @Column(name="ID", nullable=false)
    public Integer id;
    
    @Column(name="ABBR", nullable=false)
    public String abbreviation; 

    @Column(name="NAME", nullable=false)
    public String name; 

    @Column(name="IS_ACTIVE", nullable=false)
    public Boolean isActive; 

}
