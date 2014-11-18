package com.themidnightcoders.fbplugin.service;

import org.eclipse.swt.SWT;
import org.eclipse.swt.widgets.*;

import com.themidnightcoders.*;
import com.themidnightcoders.fbplugin.wizard.WebORBModel;

public final class ServiceFacade
{
    private static ServiceFacade instance = null;

    public static synchronized ServiceFacade getInstance( WebORBModel model )
    {
        if( instance == null )
            return instance = new ServiceFacade( model );

        return instance;
    }

    ServiceSoap svcSoap;
    DatabaseInfo[] dbInfoArray;
    WebORBModel model;

    private ServiceFacade( WebORBModel model ) {
        this.model = model;
        this.svcSoap = model.svcSoap;
    }

    public void dispose() {
        instance = null;
    }

    public String checkHost()
    {
        return svcSoap.CheckDatabaseHost( model.type, model.host, model.port, model.username, model.password );
    }

    public DatabaseInfo addHost()
    {
        DatabaseInfo result = svcSoap.AddDatabaseHost( model.type, model.host, model.port, model.username, model.password );
        dbInfoArray = null;
        return result;
    }

    public void getHosts()
    {
        if( dbInfoArray == null ) {
            dbInfoArray = svcSoap.GetDatabaseHosts();
        }
    }

    public boolean hasHosts()
    {
        getHosts();
        return dbInfoArray.length != 0;
    }

    public void filldatabasesCombo( Combo databasesCombo, String instanceId )
    {
        databasesCombo.removeAll();
        databasesCombo.setData(instanceId);
        String databases[] = svcSoap.GetDatabases( instanceId );
        for( int i = 0; i < databases.length; i++ ) {
            databasesCombo.add( databases[ i ] );
        }
    }

    public void fillTree( Tree tree )
    {
        getHosts();
        for( int p = 0; p < dbInfoArray.length; p++ )
        {
            DatabaseInfo dbInfo = dbInfoArray[ p ];
            TreeItem item0 = new TreeItem( tree, 0 );
            item0.setData(dbInfo);
            item0.setText(dbInfo.hostname + "(" + dbInfo.type.getValue() + ")");
            String[] databasesArray = svcSoap.GetDatabases( dbInfo.id );
            for( int i = 0; i < databasesArray.length; i++ )
            {
                String database = databasesArray[i];
                String[] tables = svcSoap.GetTables(dbInfo.id, database);
                TreeItem item1 = new TreeItem( item0, 0 );
                item1.setData(null);
                item1.setText(database);
                for( int j = 0; j < tables.length; j++ )
                {
                    String tablename = tables[j];
                    TreeItem item2 = new TreeItem( item1, 0 );
                    TableLocation tl = new TableLocation(dbInfo.id, database, tablename);
                    item2.setData(tl);
                    item2.setText(tablename);
/*
                        ColumnInfo[] columns = svcSoap.GetColumns( dbInfo.id, databasesArray[ i ], tables[ j ] );
                        for( int k = 0; k < columns.length; k++ )
                        {
                            TreeItem item3 = new TreeItem( item2, 0 );
                            item3.setData( columns[k] );
                            item3.setText( columns[ k ].name );
                        }
*/
                }
            }
        }
        tree.getItems()[ 0 ].setExpanded( true );
    }

    public void fillColumnsTable(Table table, TableLocation tableloc)
    {
        TableColumn column = new TableColumn (table, SWT.NONE);
        column.setText(tableloc.tablename);
        column.setWidth(110);
        TableItem item = new TableItem (table, SWT.NONE);
        item.setText (0, "*");

        ColumnInfo[] columns = svcSoap.GetColumns(tableloc.instanceId, tableloc.database, tableloc.tablename);
        for (int j=0; j<columns.length; j++) {
            ColumnInfo columnData = columns[j];
            item = new TableItem (table, SWT.NONE);
            item.setText (0, columnData.name);
            item.setData(columnData);
        }
    }

    public void fillCombo( Combo dbCombo )
    {
        DatabaseInfoType[] dbInfoTypeArray = svcSoap.GetSupportedDatabaseTypes();
        for( int i = 0; i < dbInfoTypeArray.length; i++ )
        {
            dbCombo.add( dbInfoTypeArray[ i ].getValue() );
            dbCombo.setData( dbInfoTypeArray[ i ].getValue(), dbInfoTypeArray[ i ] );
        }
    }

    public void runQuery( Table table, String id, String query, String database )
    {
        try
        {
            QueryResult queryresult = svcSoap.TestQuery( id, database, query );
            Object[][] arrayresult = queryresult.data;
            String[] columntitles = queryresult.columnTitles;
            
            this.model.instanceId = id;
            this.model.columns = columntitles;
            this.model.database = database;

            table.setHeaderVisible( true );

            TableColumn tc;
            TableItem item;
            int columns = columntitles.length;
            String[] row = new String[ columns ];

            for( int i = 0; i < columntitles.length; i++ )
            {
                tc = new TableColumn( table, SWT.NONE );
                tc.setText( columntitles[ i ] );
                tc.setWidth( 80 );
            }

            for( int i = 0; i < arrayresult.length; i++ )
            {
                for( int t = 0; t < columns; t++ )
                    row[ t ] = "";

                item = new TableItem( table, SWT.NONE );
                for( int j = 0; j < columns; j++ )
                {
                    row[ j ] = arrayresult[ i ][ j ] == null ? "" : arrayresult[ i ][ j ].toString();
                }
                item.setText( row );
            }
        }
        catch( Throwable t )
        {
            t.printStackTrace();
            org.eclipse.jface.dialogs.MessageDialog.openInformation(
                org.eclipse.ui.PlatformUI.getWorkbench().getActiveWorkbenchWindow().getShell(), "Error",
                    "Please select a valid database from the tree first!" );
        }
    }

    public void generateServerSideCode(String id, String database, String table, String query )
    {
        try
        {
            svcSoap.GenerateSourceCode( id, database, table, query );
        }
        catch( Throwable t )
        {
            t.printStackTrace();
            org.eclipse.jface.dialogs.MessageDialog.openInformation(
                org.eclipse.ui.PlatformUI.getWorkbench().getActiveWorkbenchWindow().getShell(), "Error",
                    "Server side code generation failed!" );
        }
    }
}