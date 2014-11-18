package com.llsvc.server.entity;

import java.io.Serializable;

import java.util.Date;
import java.util.Collection;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name="SYS_PERSON")
@SequenceGenerator(
        name = "SystemPersonSequence",
        initialValue = 1,
        allocationSize = 1,
        sequenceName = "SYS_PERSON_SQC")
        
public class PersonEntity 
    implements Serializable 
{
    @Id
    @GeneratedValue(
            strategy=GenerationType.SEQUENCE, 
            generator="SystemPersonSequence")
            
    @Column(name="ID", nullable=false)
    public Integer id;
    
    @Column(name="FIRST_NAME", nullable=false, length=50)
    public String firstName; 

    @Column(name="LAST_NAME", nullable=false, length=50)
    public String lastName; 

    @Column(name="MIDDLE_NAME", nullable=true, length=50)
    public String middleName; 

    @Column(name="EMAIL", nullable=false, length=50)
    public String email; 

    @Column(name="SSN", nullable=true, length=50)
    public String ssn; 

    @Column(name="P_PHONE", nullable=false, length=50)
    public String primaryPhoneNumber; 

    @Column(name="S_PHONE", nullable=true, length=50)
    public String secondaryPhoneNumber; 

    @Column(name="BIRTH_DAY", nullable=false)
    public Date birthDay; 

}
