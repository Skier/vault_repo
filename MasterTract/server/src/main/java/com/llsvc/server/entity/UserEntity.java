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
import javax.persistence.CascadeType;

import org.hibernate.search.annotations.Store;
import org.hibernate.search.annotations.Index;
import org.hibernate.search.annotations.Indexed;
import org.hibernate.search.annotations.DocumentId;
import org.hibernate.search.annotations.Field;

@Entity
@Table(name="SYS_USER")
@SequenceGenerator(
        name = "SystemUserSequence",
        initialValue = 1,
        allocationSize = 1,
        sequenceName = "SYS_USER_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=UserEntity.FIND_ALL,
        query="select o from UserEntity as o"),
    @NamedQuery(
        name=UserEntity.FIND_BY_LOGIN,
        query="select o from UserEntity as o where o.login = :login")
})

//@Indexed        
public class UserEntity 
    implements Serializable 
{
    public static final String FIND_ALL = "findAllUsers";
    public static final String FIND_BY_LOGIN = "findUserByLogin";

    @Id
    @GeneratedValue(
            strategy=GenerationType.SEQUENCE, 
            generator="SystemUserSequence")
            
    @Column(name="ID", nullable=false)
    @DocumentId
    public Integer id;
    
    @OneToOne
    @OneToMany(mappedBy="lease", cascade = CascadeType.MERGE)
    @JoinColumn(name="PERSON_ID", nullable=false)
    public PersonEntity personal;
    
    @Column(name="LOGIN", nullable=false, length=50)
    @Field(index=Index.TOKENIZED, store=Store.YES)
    public String login; 

    @Column(name="PASSWORD", nullable=false, length=50)
    @Field(index=Index.TOKENIZED, store=Store.YES)
    public String password; 

    @Column(name="IS_ACTIVE", nullable=true)
    @Field
    public Boolean isActive; 

    @Column(name="HACK_ATTEMPTS", nullable=false)
    @Field
    public Integer hackAttempts; 

    @Column(name="IS_ADMIN", nullable=false)
    @Field
    public Boolean isAdmin; 

    @Column(name="IS_PM", nullable=false)
    @Field
    public Boolean isProjectManager; 

    @ManyToOne
    @JoinColumn(name="CLIENT_ID", nullable=true)
    public ClientEntity client;
    
    @Transient
    public List roleList;

}
