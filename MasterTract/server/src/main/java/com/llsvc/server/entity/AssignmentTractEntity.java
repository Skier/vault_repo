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
@Table(name = "DOC_ASSIGNMENT_TRACT")
@SequenceGenerator(
        name="DocumentAssignmentTractSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_ASSIGNMENT_TRACT_SQC")
        
public class AssignmentTractEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentAssignmentTractSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="TRACT_ID", nullable=false)
    public LeaseTractEntity tract;
    
    @ManyToOne
    @JoinColumn(name="ASSIGNMENT_ID", nullable=false)
    public AssignmentEntity assignment;
    
}
