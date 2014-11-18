package com.llsvc.server.entity;

import java.io.Serializable;
import java.util.Date;
import java.util.Collection;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.ManyToOne;
import javax.persistence.JoinColumn;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name = "DOC_ACTOR_PHONE")
@SequenceGenerator(
        name="DocumentActorPhoneSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_ACTOR_PHONE_SQC")
        
public class DocumentActorPhoneEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentActorPhoneSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="ACTOR_ID", nullable=false)
    public DocumentActorEntity actor;
    
    @Column(name="PHONE", nullable=false )
    public String phone;

    @Column(name="IS_PRIMARY", nullable=false )
    public Boolean isPrimary;
}
