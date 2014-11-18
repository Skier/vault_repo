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
@Table(name = "GEO_ADDRESS")
@SequenceGenerator(
        name="GeoAddressSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="GEO_ADDRESS_SQC")
        
public class AddressEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="GeoAddressSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="STATE_ID", nullable=false)
    public StateEntity state;
    
    @Column(name="ADDR1", nullable=false )
    public String address1;

    @Column(name="ADDR2", nullable=true )
    public String address2;

    @Column(name="CITY", nullable=false )
    public String city;

    @Column(name="ZIP", nullable=false )
    public String zip;

}
