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
@Table(name = "DOC_PROJECT")
@SequenceGenerator(
        name="DocumentProjectSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_PROJECT_SQC")

public class DocumentProjectEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentProjectSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="DOC_ID", nullable=false)
    public DocumentEntity document;
    
    @ManyToOne
    @JoinColumn(name="PROJECT_ID", nullable=false)
    public ProjectEntity project;
    
}
