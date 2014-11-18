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
@Table(name = "GEO_LAYER")
@SequenceGenerator(
        name="LayerSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="GEO_LAYER_SQC")
public class LayerEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="LayerSequence")
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @Column(name="NAME", nullable=false)
    public String name;

    @Column(name="DESCRIPTION", nullable=true)
    public String description;

    @Column(name="IS_ACTIVE", nullable=false)
    public Boolean isActive;

    @Column(name="IS_PUBLIC", nullable=false)
    public Boolean isPublic;

}
