package com.llsvc.server.entity;

import java.io.Serializable;
import java.util.Date;
import java.util.Collection;

import java.math.BigDecimal;

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
@Table(name = "DOC_SURFACE_OWNER")
@SequenceGenerator(
        name="SurfaceOwnerSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_SURFACE_OWNER_SQC")

public class SurfaceOwnerEntity 
    implements Serializable 
{

    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="SurfaceOwnerSequence")
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="TRACT_ID", nullable=false)
    public SurfaceTractEntity tract;

    @OneToOne
    @JoinColumn(name="ACTOR_ID", nullable=false)
    public DocumentActorEntity actor;

    @Column(name="INTR", nullable=false)    
    public BigDecimal interest;

    @Column(name="IS_OWNER", nullable=false )
    public Boolean isOwner;

    @Column(name="NOTE", nullable=true )
    public String note;

}
