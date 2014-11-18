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
@Table(name = "DOC_TYPE")
@SequenceGenerator(
        name="DocumentTypeSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_TYPE_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=DocumentTypeEntity.FIND_ALL,
        query="select o from DocumentTypeEntity as o")
})

public class DocumentTypeEntity 
    implements Serializable 
{
    public static final String FIND_ALL = "findAllDocumentTypes";

    public static final String KIND_LEASE = "LEASE";
    public static final String KIND_ASSIGNMENT = "ASSIGNMENT";
    public static final String KIND_SURFACE_TRACT = "SURFACE_TRACT";
    public static final String KIND_SURFACE_RUNSHEET = "SURFACE_RUNSHEET";

    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentTypeSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @Column(name="KIND", nullable=false)
    public String kind;
    
    @Column(name="NAME", nullable=false, length=50, unique=true)
    public String name;
    
    @Column(name="GIVER_ROLE", nullable=true)
    public String giverRole;
    
    @Column(name="RECV_ROLE", nullable=true)
    public String receiverRole;
    
}
