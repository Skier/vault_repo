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
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;
import javax.persistence.OneToOne;
import javax.persistence.JoinColumn;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name = "DOC_ATTACHMENT")
@SequenceGenerator(
        name="DocumentAttachmentSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_ATTACHMENT_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=DocumentAttachmentEntity.FIND_BY_TYPE,
        query="select o from DocumentAttachmentEntity as o where o.type = :type")
})

public class DocumentAttachmentEntity 
    implements Serializable 
{
    public static final String FIND_BY_TYPE = "findDocumentAttachmentByType";

    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentAttachmentSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="DOC_ID", nullable=false)
    public DocumentEntity document;
    
    @OneToOne
    @JoinColumn(name="FILE_ID", nullable=true)
    public FileEntity file;
    
    @OneToOne
    @JoinColumn(name="RECORD_ID", nullable=true)
    public DocumentRecordEntity record;

    @Column(name="TYPE", nullable=false )
    public String type;

    @Column(name="NAME", nullable=false )
    public String name;

    @Column(name="MEMO", nullable=false )
    public String memo;

    @Column(name="MEMO_DATE", nullable=false )
    public Date memoDate;

    @Column(name="COR_DATE", nullable=true )
    public Date correspondenceDate;
    
    @Column(name="COR_FROM", nullable=true )
    public String from;
    
    @Column(name="COR_TO", nullable=true )
    public String to;
    
}
