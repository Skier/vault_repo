package com.llsvc.server.entity;

import java.io.Serializable;
import java.util.Collection;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name = "SYS_MODULE_TYPE")
@SequenceGenerator(
        name="SystemModuleTypeSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="SYS_MODULE_TYPE_SQC")
        
public class ModuleTypeEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="SystemModuleTypeSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @Column(name="NAME", nullable=false, length=50, unique=true)
    public String name;
    
    @OneToMany(mappedBy="moduleType")
    public Collection<ModuleEntity> modules;
}
