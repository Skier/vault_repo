package com.themidnightcoders.fbplugin.service;

import java.util.List;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.HashMap;
import com.themidnightcoders.ColumnKeyType;

public class QueryDescription 
{
    private final List columnDescriptors = new ArrayList();

    public void addColumn(ColumnDescription col) {
        columnDescriptors.add(col);
    }

    public ColumnDescription[] getColumns() {
    	return (ColumnDescription[]) columnDescriptors.toArray(
   			new ColumnDescription[columnDescriptors.size()]);
    }
    
    public ColumnDescription getPrimaryKeyDescription() {
        for (int i=0; i<columnDescriptors.size(); i++) {
            ColumnDescription col = (ColumnDescription) columnDescriptors.get(i);
            if ( null != col.columnInfo 
            		&& ColumnKeyType.DISC_PRIMARY == col.columnInfo.keyType.getDiscriminator() ) {
            	return col;
            }
        }
        return null;
    }
    
    public String[] getTables() {
        HashMap tablemap = new HashMap();
        for (int i=0; i<columnDescriptors.size(); i++) {
            ColumnDescription col = (ColumnDescription) columnDescriptors.get(i);
            String tablename = col.tableName;
            if ( null == tablemap.get(tablename) ) {
                tablemap.put(tablename, tablename);
            }
        }
        return (String[]) tablemap.values().toArray(new String[tablemap.size()]);
    }
    
    public String toQuery() {
        String query = "select ";
        
        // select clause
        boolean first = true;
        for (int i=0; i<columnDescriptors.size(); i++) {
            ColumnDescription col = (ColumnDescription) columnDescriptors.get(i);
            if ( col.output ) {
                if ( !first ) {
                    query += ", ";
                } else {
                    first = false;
                }
                query += col.tableName + "." + col.columnName;
                if ( null != col.alias ) {
                    query += " as " + col.alias;
                }
            }
        }

        // from clause
        query += " from ";
        first = true;
        HashMap tablemap = new HashMap();
        for (int i=0; i<columnDescriptors.size(); i++) {
            ColumnDescription col = (ColumnDescription) columnDescriptors.get(i);
            String tablename = col.tableName;
            if ( null == tablemap.get(tablename) ) {
                tablemap.put(tablename, tablename);
                if ( !first ) {
                    query += ", ";
                } else {
                    first = false;
                }
                query += tablename;
            }
        }
        
        // to do: join part

        // where clause
        first = true;
        for (int i=0; i<columnDescriptors.size(); i++) {
            ColumnDescription col = (ColumnDescription) columnDescriptors.get(i);
            if ( null != col.filter ) {
                if ( !first ) {
                    query += " and ";
                } else {
                    first = false;
                    query += " where ";
                }

                if ( null != col.or ) { 
                    query += "(";
                }
                query += col.tableName + "." + col.columnName + " " + col.filter;
                if ( null != col.or ) { 
                    query += " or " + col.tableName + "." + col.columnName + " " + col.or + ")";
                }
            }
        }

        // order by clause 
        ColumnDescription[] cols = (ColumnDescription[]) columnDescriptors.toArray(
            new ColumnDescription[columnDescriptors.size()]);
        Arrays.sort(cols, new Comparator() {
            public int compare(Object o1, Object o2) {
                ColumnDescription col1 = (ColumnDescription) o1;
                ColumnDescription col2 = (ColumnDescription) o2;
                if ( null == col1.sortOrder ) {
                    return 1;
                }
                if ( null == col2.sortOrder ) {
                    return -1;
                }
                Integer sort1 = new Integer(col1.sortOrder);
                Integer sort2 = new Integer(col2.sortOrder);
                if ( sort1.intValue() > sort2.intValue() ) {
                    return 1;
                } else if ( sort1.intValue() < sort2.intValue() ) {
                    return -1;
                } else {    
                    return 0;
                }
            }
            
            public boolean equals(Object o) {   
                return (null != o);
            }
        });
        first = true;       
        for (int i=0; i<cols.length; i++) {
            ColumnDescription col = cols[i];
            if ( null != col.sortOrder ) {
                if ( !first ) {
                    query += ", ";
                } else {
                    first = false;
                    query += " order by ";
                }
                query += col.columnName;
                if ( null != col.sortType ) { 
                    query += " " + col.sortType;
                }
            }
        }

        return query;          
    }
}

