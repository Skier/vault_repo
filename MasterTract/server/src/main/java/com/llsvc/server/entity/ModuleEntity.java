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
@Table(name="SYS_MODULE")
@SequenceGenerator(
        name = "SystemModuleSequence",
        initialValue = 1,
        allocationSize = 1,
        sequenceName = "SYS_MODULE_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=ModuleEntity.FIND_ALL,
        query="select o from ModuleEntity as o"),
    @NamedQuery(
        name=ModuleEntity.FIND_BY_USER_ID,
        query="select o from ModuleEntity as o")
})

public class ModuleEntity 
    implements Serializable 
{
    public static final String FIND_ALL = "findAllModules";
    public static final String FIND_BY_USER_ID = "findModulesByUserId";

    @Id
    @GeneratedValue(
            strategy=GenerationType.SEQUENCE, 
            generator="SystemModuleSequence")
            
    @Column(name="ID", nullable=false)
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="TYPE_ID", nullable=false)
    public ModuleTypeEntity moduleType;
    
    @Column(name="NAME", nullable=false, length=20, unique=true)
    public String name; 

    @Column(name="DESCRIPTION", nullable=false)
    public String description;

    @Column(name="URL", nullable=false)
    public String url;

    public Integer getModuleTypeId() {
        return moduleType.id;
    }

    public void setModuleTypeId(Integer moduleTypeId) {
    }
}
