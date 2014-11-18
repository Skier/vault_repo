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
@Table(name = "DOC_STATUS")
@SequenceGenerator(
        name="DocumentStatusSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_STATUS_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=DocumentStatusEntity.FIND_ALL,
        query="select o from DocumentStatusEntity as o")
})

public class DocumentStatusEntity 
    implements Serializable 
{
    public static final String FIND_ALL = "findAllDocumentStatuses";

    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentStatusSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @Column(name="NAME", nullable=false, length=50, unique=true)
    public String name;
    
}
