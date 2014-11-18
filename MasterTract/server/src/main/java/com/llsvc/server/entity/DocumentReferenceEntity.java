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
@Table(name = "DOC_REFERENCE")
@SequenceGenerator(
        name="DocumentReferenceSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_REFERENCE_SQC")

/*        
@NamedQueries({
    @NamedQuery(
        name=DocumentRecordAttachmentEntity.FIND_BY_DESCRIPTION,
        query="select o from DocumentRecordAttachmentEntity as o where o.description = :description")
})
*/

public class DocumentReferenceEntity 
    implements Serializable 
{
//    public static final String FIND_BY_DESCRIPTION = "findDocumentRecordAttachmentByDescription";

    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentReferenceSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="REFERRER_ID", nullable=false)
    public DocumentEntity referrer;
    
    @Column(name="REFEREE_ID", nullable=false )
    public Integer refereeId;
}
