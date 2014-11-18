package com.llsvc.server.entity;

import java.io.Serializable;
import java.util.Date;
import java.util.Collection;
import java.util.Iterator;

import java.math.BigDecimal;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.OneToOne;
import javax.persistence.ManyToOne;
import javax.persistence.JoinColumn;
import javax.persistence.PrimaryKeyJoinColumn;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name = "DOC_SURFACE_RUNSHEET")
        
public class SurfaceRunsheetEntity 
    implements Serializable 
{
    @Id
    @Column(name="ID", nullable=false)    
    public Integer id;

    @OneToOne
    @PrimaryKeyJoinColumn
    public DocumentEntity document;
    
    @Column(name="INSTRUMENT", nullable=false )
    public String instrument;

    @Column(name="NO", nullable=false )
    public String no;

    @Column(name="DOD", nullable=false )
    public Date dod;
    
    @Column(name="dor", nullable=false )
    public Date dor;
    
    @Column(name="DESCR", nullable=true )
    public String description;

    @Column(name="REMARKS", nullable=true )
    public String remarks;

}
