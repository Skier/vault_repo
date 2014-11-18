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
@Table(name = "DOC_RECORD")
@SequenceGenerator(
        name="DocumentRecordSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_RECORD_SQC")
        
public class DocumentRecordEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentRecordSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="DOC_ID", nullable=false)
    public DocumentEntity document;
    
    @ManyToOne
    @JoinColumn(name="STATE_ID", nullable=false)
    public StateEntity state;
    
    @ManyToOne
    @JoinColumn(name="COUNTY_ID", nullable=false)
    public CountyEntity county;
    
    @Column(name="DOC_DATE", nullable=false )
    public Date docDate;
    
    @Column(name="DOC_NO", nullable=true )
    public String docNo;
    
    @Column(name="VOLUME", nullable=true )
    public String volume;
    
    @Column(name="PAGE", nullable=true )
    public String page;

    @Column(name="IS_PUBLIC", nullable=false )
    public Boolean isPublic;

    @OneToOne(mappedBy="record")
    public DocumentAttachmentEntity attachment;
}
