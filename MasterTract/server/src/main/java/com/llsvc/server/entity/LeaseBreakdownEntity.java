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
import javax.persistence.OneToMany;
import javax.persistence.ManyToOne;
import javax.persistence.JoinColumn;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name = "DOC_LEASE_BREAK")
@SequenceGenerator(
        name="DocumentLeaseBreakdownSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="DOC_LEASE_BREAK_SQC")

/*        
@NamedQueries({
    @NamedQuery(
        name=LeaseBreakdownEntity.FIND_BY_TRACT_MASK,
        query="select o from LeaseBreakdownEntity as o join o.qqs as qq join qq.tract as t where t.township=:township and t.range=:range and t.section=:section and (:qq='!' or t.qq=:qq)")
})
*/

public class LeaseBreakdownEntity 
    implements Serializable 
{
    public static final String FIND_BY_TRACT_MASK = "findLeaseBreakdownByTractMask";

    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="DocumentLeaseBreakdownSequence")
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="TRACT_ID", nullable=false)
    public LeaseTractEntity tract;

    @ManyToOne
    @JoinColumn(name="ACTOR_ID", nullable=false)
    public DocumentActorEntity actor;

    @Column(name="INTR", nullable=false )
    public BigDecimal interest;

    @Column(name="FROM_DEPTH", nullable=true )
    public String fromDepth;

    @Column(name="TO_DEPTH", nullable=true )
    public String toDepth;

    @Column(name="PRODUCT", nullable=true )
    public String product;

    @Column(name="FORMATION", nullable=true )
    public String formation;

}
