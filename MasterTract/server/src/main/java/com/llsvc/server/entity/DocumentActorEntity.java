package com.llsvc.server.entity;

import java.io.Serializable;
import java.util.Date;
import java.util.Collection;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToOne;
import javax.persistence.OneToMany;
import javax.persistence.ManyToOne;
import javax.persistence.JoinColumn;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name = "DOC_ACTOR")
@SequenceGenerator(
        name="DocumentActorSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_ACTOR_SQC")
        
public class DocumentActorEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentActorSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="DOC_ID", nullable=false)
    public DocumentEntity document;
    
    @OneToOne(cascade = CascadeType.REMOVE)
    @JoinColumn(name="ADDRESS_ID", nullable=false)
    public AddressEntity address;
    
    @Column(name="NAME", nullable=false )
    public String name;

    @Column(name="IS_GIVER", nullable=false )
    public Boolean isGiver;

    @Column(name="IS_COMPANY", nullable=false )
    public Boolean isCompany;

    @Column(name="TAXID", nullable=true )
    public String taxId;

    @OneToMany(mappedBy="actor", cascade = CascadeType.REMOVE)
    public Collection<DocumentActorPhoneEntity> phones;

}
