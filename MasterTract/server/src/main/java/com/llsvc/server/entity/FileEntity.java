package com.llsvc.server.entity;

import java.io.Serializable;
import java.util.Date;
import java.util.Collection;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;
import javax.persistence.JoinColumn;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name = "SYS_FILE")
@SequenceGenerator(
        name="SystemFileSequence",
        initialValue=1, 
        allocationSize=1, 
        sequenceName="SYS_FILE_SQC")
        
@NamedQueries({
    @NamedQuery(
        name=FileEntity.FIND_BY_NOTE,
        query="select o from FileEntity as o where o.note = :note")
})

public class FileEntity 
    implements Serializable 
{
    public static final String FIND_BY_NOTE = "findFileByNote";

    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, 
            generator="SystemFileSequence")
            
    @Column(name="ID", nullable=false)    
    public Integer id;
    
    @ManyToOne
    @JoinColumn(name="USER_ID", nullable=false)
    public UserEntity user;
    
    @Column(name="ORIG_FILENAME", nullable=false )
    public String origFilename;

    @Column(name="MIME_TYPE", nullable=false )
    public String mimeType;

    @Column(name="STORAGE_KEY", nullable=false )
    public String storageKey;

    @Column(name="CHANGED", nullable=false )
    public Date changed;

    @Column(name="NOTE", nullable=true )
    public String note;

}
