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
import javax.persistence.OneToMany;
import javax.persistence.ManyToOne;
import javax.persistence.JoinColumn;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

import org.hibernate.search.annotations.Indexed;
import org.hibernate.search.annotations.DocumentId;
import org.hibernate.search.annotations.Field;

@Entity
@Table(name = "DOC_DOCUMENT")
@SequenceGenerator(
        name="DocumentSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_DOCUMENT_SQC")
public class DocumentEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentSequence")
            
    @Column(name="ID", nullable=false)    
    @DocumentId
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="TYPE_ID", nullable=false)
    public DocumentTypeEntity documentType;
    
    @ManyToOne
    @JoinColumn(name="STATUS_ID", nullable=false)
    public DocumentStatusEntity documentStatus;
    
    @ManyToOne
    @JoinColumn(name="USER_ID", nullable=false)
    public UserEntity user;
    
    @Column(name="NOTE", nullable=true )
    public String note;

    @OneToMany(mappedBy="document", cascade = CascadeType.REMOVE)
    public Collection<DocumentActorEntity> actors;

    @OneToMany(mappedBy="document", cascade = CascadeType.REMOVE)
    public Collection<DocumentAttachmentEntity> attachments;

    @OneToMany(mappedBy="document", cascade = CascadeType.REMOVE)
    public Collection<DocumentRecordEntity> records;

    @OneToMany(mappedBy="document", cascade = CascadeType.REMOVE)
    public Collection<DocumentProjectEntity> projects;

    @OneToMany(mappedBy="referrer", cascade = CascadeType.REMOVE)
    public Collection<DocumentReferenceEntity> references;
}
