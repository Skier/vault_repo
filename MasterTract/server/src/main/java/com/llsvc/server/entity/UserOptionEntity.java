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

import org.hibernate.search.annotations.Store;
import org.hibernate.search.annotations.Index;
import org.hibernate.search.annotations.Indexed;
import org.hibernate.search.annotations.DocumentId;
import org.hibernate.search.annotations.Field;

@Entity
@Table(name="SYS_USER_OPTION")
@SequenceGenerator(
        name = "SystemUserOptionSequence",
        initialValue = 1,
        allocationSize = 1,
        sequenceName = "SYS_USER_OPTION_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=UserOptionEntity.FIND_BY_USER,
        query="select o from UserOptionEntity as o where o.userId = :userId")
})

public class UserOptionEntity 
    implements Serializable 
{
    public static final String FIND_BY_USER = "findUserOptionByUser";

    @Id
    @GeneratedValue(
            strategy=GenerationType.SEQUENCE, 
            generator="SystemUserOptionSequence")
            
    @Column(name="ID", nullable=false)
    public Integer id;
    
    @Column(name="USER_ID", nullable=false)
    public Integer userId; 
    
    @Column(name="OPT_KEY", nullable=false)
    public String optionKey; 

    @Column(name="OPT_VAL", nullable=false)
    public String optionValue; 

}
