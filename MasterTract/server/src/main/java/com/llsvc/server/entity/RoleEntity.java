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
@Table(name = "SYS_ROLE")
@SequenceGenerator(
        name="SystemRoleSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="SYS_ROLE_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=RoleEntity.FIND_ALL,
        query="select o from RoleEntity as o"),
    @NamedQuery(
        name=RoleEntity.FIND_BY_USER_ID,
        query="select o from RoleEntity as o")
})

public class RoleEntity 
    implements Serializable 
{
    public static final String FIND_ALL = "findAllRoles";
    public static final String FIND_BY_USER_ID = "findRolesByUserId";

    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="SystemRoleSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @Column(name="NAME", nullable=false, length=50, unique=true)
    public String name;
   
}
