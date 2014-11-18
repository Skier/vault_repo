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
@Table(name = "DOC_SURFACE_TRACT")
        
public class SurfaceTractEntity 
    implements Serializable 
{
    @Id
    @Column(name="ID", nullable=false)    
    public Integer id;

    @OneToOne
    @PrimaryKeyJoinColumn
    public DocumentEntity document;
    
    @Column(name="GLINK", nullable=false )
    public String glink;

    @Column(name="NO", nullable=false )
    public String no;

    @Column(name="AC", nullable=false )
    public BigDecimal acres;
    
    @Column(name="STATUS", nullable=false )
    public String status;

    @OneToMany(mappedBy="tract", cascade = CascadeType.REMOVE)
    public Collection<SurfaceOwnerEntity> owners;

    @OneToMany(mappedBy="tract", cascade = CascadeType.REMOVE)
    public Collection<SurfaceTractHistoryEntity> history;

}
