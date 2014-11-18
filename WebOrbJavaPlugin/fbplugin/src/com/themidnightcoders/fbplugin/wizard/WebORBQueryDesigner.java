package com.themidnightcoders.fbplugin.wizard;

import java.util.List;
import java.util.ArrayList;
import org.eclipse.swt.*;
import org.eclipse.swt.dnd.*;
import org.eclipse.swt.widgets.*;
import org.eclipse.swt.graphics.*;
import org.eclipse.swt.layout.*;
import org.eclipse.swt.custom.*;
import org.eclipse.swt.events.*;
import org.eclipse.jface.wizard.*;
import com.themidnightcoders.*;
import com.themidnightcoders.fbplugin.service.*;

public class WebORBQueryDesigner 
{

    private WizardPage page = null;
    private WebORBModel model = null;
    private Composite parent = null;
    private SashForm sashForm = null;
    private SashForm sashFormMain = null;
    private ToolBar toolBar = null;
    private Combo databaseCombo = null;
    private Tree tree = null;
    private Composite diagramPane = null;
    private Table gridPane = null;
    private Text sqlPane = null;
    private Table resultPane = null;

    public WebORBQueryDesigner(WizardPage page, WebORBModel model) {
        this.page = page;
        this.model = model;
    }

    public void createControl(Composite parent) {
        this.parent = parent;
        GridLayout layout = new GridLayout();
        layout.numColumns = 1;
        parent.setLayout(layout);
    
        toolBar = createToolbar(parent);

        sashFormMain = new SashForm(parent, SWT.HORIZONTAL);
        GridData gridData = new GridData();
        gridData.grabExcessHorizontalSpace = true;
        gridData.grabExcessVerticalSpace = true;
        gridData.horizontalAlignment = GridData.FILL;
        gridData.verticalAlignment = GridData.FILL;
        sashFormMain.setLayoutData(gridData);

        tree = createTree(sashFormMain);

        final ScrolledComposite scrollComposite = new ScrolledComposite(sashFormMain, SWT.BORDER | SWT.H_SCROLL | SWT.V_SCROLL);

        sashFormMain.setWeights(new int[] {1, 4});

        sashForm = new SashForm(scrollComposite, SWT.VERTICAL);

        scrollComposite.setContent(sashForm);
        scrollComposite.setExpandVertical(true);
        scrollComposite.setExpandHorizontal(true);
        scrollComposite.setMinWidth(sashForm.getSize().x);
        scrollComposite.setMinHeight(sashForm.getSize().y);
        scrollComposite.addControlListener(new ControlAdapter() {
            public void controlResized(ControlEvent e) {
                Rectangle r = scrollComposite.getClientArea();
                scrollComposite.setMinSize(sashForm.computeSize(SWT.DEFAULT, SWT.DEFAULT));
            }
        });

        diagramPane = createDiagramPane(sashForm);
        gridPane = createGridPane(sashForm);
        sqlPane = createSqlPane(sashForm);
        resultPane = createResultPane(sashForm);

        sashForm.setWeights(new int[] {2, 2, 1, 2});

        setupTreeDragAndDrop();
        setupGridPaneMenu();
    }

    public ToolBar createToolbar(Composite parent) {
        ToolBar toolBar = new ToolBar (parent, SWT.WRAP);
        
        ToolItem addInstanceItem = new ToolItem (toolBar, SWT.PUSH);
        addInstanceItem.setText ("Connect");
        addInstanceItem.addListener(SWT.Selection, new Listener() {
            public void handleEvent (Event event) {
                addInstance();
            }
        });

        ToolItem generateSqlItem = new ToolItem (toolBar, SWT.PUSH);
        generateSqlItem.setText ("Show SQL");
        generateSqlItem.addListener(SWT.Selection, new Listener() {
            public void handleEvent (Event event) {
                generateSql();
            }
        });

        ToolItem executeSqlItem = new ToolItem (toolBar, SWT.PUSH);
        executeSqlItem.setText ("Execute SQL");
        executeSqlItem.addListener(SWT.Selection, new Listener() {
            public void handleEvent (Event event) {
                executeSql();
            }
        });

        ToolItem databaseItem = new ToolItem (toolBar, SWT.SEPARATOR);
        databaseCombo = new Combo(toolBar, SWT.READ_ONLY);
        databaseCombo.pack();
/*
        databaseCombo.addListener(SWT.Selection, new Listener() {
            public void handleEvent (Event event) {
                handleDatabaseSelected(event.widget);
            }
        });
*/
        databaseItem.setWidth(databaseCombo.getSize ().x);
        databaseItem.setControl(databaseCombo);

        return toolBar;
    }

    public Tree createTree(Composite parent) {
        final Tree tree = new Tree(parent, SWT.VIRTUAL | SWT.BORDER);
        ServiceFacade.getInstance(getModel()).fillTree(tree);     
        tree.addListener(SWT.Selection, new Listener() {
            public void handleEvent (Event event) {
                TreeItem[] selection = tree.getSelection();
                if (selection.length > 0 ) {
                    TreeItem selected = selection[0];
                    if ( selected.getData() instanceof DatabaseInfo ) {
                        DatabaseInfo info = (DatabaseInfo) selected.getData();
                        ServiceFacade.getInstance(getModel()).filldatabasesCombo(databaseCombo, info.id);
                    }
                }
            }
        });
        return tree;
    }

    public Composite createDiagramPane(Composite parent) {
        final Composite result = new Composite(parent, SWT.BORDER);
        RowLayout row = new RowLayout(SWT.HORIZONTAL);
        row.spacing = 10;
        row.marginLeft = 10;
        row.marginRight = 10;
        row.marginTop = 10;
        row.marginBottom = 10;
        result.setLayout(row);
        return result;
    }

    public Table createGridPane(Composite parent) {
        Table table = new Table (parent, SWT.MULTI | SWT.BORDER | SWT.FULL_SELECTION);
        table.setLinesVisible (true);
        table.setHeaderVisible (true);
        String[] titles = {"Column", "Alias", "Table", "Output", "Sort Type", "Sort Order", "Filter", "Or"};
        for (int i=0; i<titles.length; i++) {
            TableColumn column = new TableColumn (table, SWT.NONE);
            column.setText (titles [i]);
        }   
        for (int i=0; i<titles.length; i++) {
            if ( 0 == i || 2 == i || 6 == i || 7 == i) {
                table.getColumn(i).setWidth(100);
            } else if ( 4 == i ) {
                table.getColumn(i).setWidth(80);
            } else {
                table.getColumn (i).pack ();
            }
        }   
        table.setSize (table.computeSize (SWT.DEFAULT, 200));

        return table;
    }

    public Text createSqlPane(Composite parent) {
        Text text = new Text(parent, SWT.MULTI | SWT.WRAP);
        return text;
    }

    public Table createResultPane(Composite parent) {
        Table table = new Table (parent, SWT.MULTI | SWT.BORDER | SWT.FULL_SELECTION);
        table.setLinesVisible (true);
        table.setHeaderVisible (true);
        table.setSize (table.computeSize (SWT.DEFAULT, 200));
        return table;
    }

/*
    public void handleDatabaseSelected(Widget widget) {
        System.out.println("handleDatabaseSelected: widget=" + widget);
        clear();
        Combo databaseCombo = (Combo) widget;
        String database = databaseCombo.getText();
        if ( !"".equals(database) ) {
//            String[] tables = getService().GetTables(getCurrentDatabase().id, database);
            String[] tables = new String[0];
            for (int i=0; i<tables.length; i++) {
                String tableName = tables[i];
                Table table = new Table (diagramPane, SWT.CHECK | SWT.MULTI | SWT.BORDER);
                table.setLinesVisible (true);
                table.setHeaderVisible (true);
                TableColumn column = new TableColumn (table, SWT.NONE);
                column.setText(tableName);
//                column.pack ();
                column.setWidth(110);
                TableItem item = new TableItem (table, SWT.NONE);
                item.setText (0, "*");

//                ColumnInfo[] columns = getService().GetColumns(getCurrentDatabase().id, database, tableName);
                ColumnInfo[] columns = new ColumnInfo[0];
                for (int j=0; j<columns.length; j++) {
                    ColumnInfo columnData = columns[j];
                    item = new TableItem (table, SWT.NONE);
                    item.setText (0, columnData.name);
                }

                table.setSize (table.computeSize (SWT.DEFAULT, SWT.DEFAULT));
                table.addListener (SWT.Selection, new Listener () {
                    public void handleEvent (Event event) {
                        if ( event.detail == SWT.CHECK ) {
                            handleColumnSelected(event.item);
                        }
                    }
                });
            }
        }
        diagramPane.layout();
    }
*/

    public void handleTableSelected(TableLocation tableloc) {
        System.out.println("handleTableSelected: tableloc=" + tableloc);
        Table table = new Table (diagramPane, SWT.SINGLE | SWT.BORDER);
        table.setLinesVisible (true);
        table.setHeaderVisible (true);

        ServiceFacade.getInstance(getModel()).fillColumnsTable(table, tableloc);

        table.setSize (table.computeSize (SWT.DEFAULT, SWT.DEFAULT));
        table.addListener (SWT.Selection, new Listener () {
            public void handleEvent (Event event) {
                handleColumnSelected(event.item);
            }
        });

        TableColumn closeColumn = new TableColumn (table, SWT.NONE);
        closeColumn.setText("X");
        closeColumn.pack ();
        closeColumn.addListener (SWT.Selection, new Listener () {
            public void handleEvent (Event event) {
                System.out.println("closeColumn: widget=" + event.widget);
                TableColumn col = (TableColumn) event.widget;
                col.getParent().dispose();
                diagramPane.layout();
            }
        });

        ServiceFacade.getInstance(getModel()).filldatabasesCombo(databaseCombo, tableloc.instanceId);
        databaseCombo.select(databaseCombo.indexOf(tableloc.database));

        diagramPane.layout();
    }

    public void handleColumnSelected(Widget widget) {
        System.out.println("handleColumnWidget: widget=" + widget);
        TableItem selectedItem = (TableItem) widget;
        final TableItem item = new TableItem(gridPane, SWT.NONE);
        item.setData("columnInfo", selectedItem.getData());
        
        final List itemControls = new ArrayList();
        TableEditor editor = new TableEditor (gridPane);
        Text text = new Text (gridPane, SWT.NONE);
        itemControls.add(text);
        editor.grabHorizontal = true;
        editor.setEditor(text, item, 0);
        text.setText(selectedItem.getText(0));
        item.setText(0, selectedItem.getText(0)); 
        text.addListener (SWT.Modify, new Listener () {
            public void handleEvent (Event e) {
                Text text = (Text) e.widget;
                item.setText(0, text.getText());
//                System.out.println("handleColumnSelect: modify event widget=" + e.widget
//                    + " text=" + text.getText());
            }
        });

        editor = new TableEditor (gridPane);
        text = new Text (gridPane, SWT.NONE);
        itemControls.add(text);
        editor.grabHorizontal = true;
        editor.setEditor(text, item, 1);
        text.addListener (SWT.Modify, new Listener () {
            public void handleEvent (Event e) {
                Text text = (Text) e.widget;
                item.setText(1, text.getText());
            }
        });

        editor = new TableEditor (gridPane);
        text = new Text (gridPane, SWT.NONE);
        itemControls.add(text);
        editor.grabHorizontal = true;
        editor.setEditor(text, item, 2);
        text.setText(selectedItem.getParent().getColumn(0).getText());
        item.setText(2, selectedItem.getParent().getColumn(0).getText()); 
        text.addListener (SWT.Modify, new Listener () {
            public void handleEvent (Event e) {
                Text text = (Text) e.widget;
                item.setText(2, text.getText());
            }
        });

        editor = new TableEditor(gridPane);
        Button button = new Button (gridPane, SWT.CHECK);
        itemControls.add(button);
        button.pack ();
        editor.minimumWidth = button.getSize ().x;
        editor.horizontalAlignment = SWT.LEFT;
        editor.setEditor (button, item, 3);
        button.setSelection(true);
        item.setText(3, "1");
        button.addListener (SWT.Selection, new Listener () {
            public void handleEvent (Event e) {
                Button button = (Button) e.widget;
                item.setText(3, button.getSelection() ? "1" : "0");
            }
        });

        editor = new TableEditor(gridPane);
        Combo sortCombo = new Combo(gridPane, SWT.READ_ONLY);
        itemControls.add(sortCombo);
        sortCombo.add("Ascending");
        sortCombo.add("Descending");
        sortCombo.pack ();
        editor.minimumWidth = sortCombo.getSize ().x;
        editor.minimumHeight = sortCombo.getSize ().y;
        editor.horizontalAlignment = SWT.LEFT;
        editor.setEditor (sortCombo, item, 4);
        sortCombo.addListener (SWT.Modify, new Listener () {
            public void handleEvent (Event e) {
                Combo combo = (Combo) e.widget;
                item.setText(4, "" + combo.getText());
            }
        });

        editor = new TableEditor (gridPane);
        text = new Text (gridPane, SWT.NONE);
        itemControls.add(text);
        editor.grabHorizontal = true;
        editor.setEditor(text, item, 5);
        text.addListener (SWT.Modify, new Listener () {
            public void handleEvent (Event e) {
                Text text = (Text) e.widget;
                item.setText(5, text.getText());
            }
        });

        editor = new TableEditor (gridPane);
        text = new Text (gridPane, SWT.NONE);
        itemControls.add(text);
        editor.grabHorizontal = true;
        editor.setEditor(text, item, 6);
        text.addListener (SWT.Modify, new Listener () {
            public void handleEvent (Event e) {
                Text text = (Text) e.widget;
                item.setText(6, text.getText());
            }
        });

        editor = new TableEditor (gridPane);
        text = new Text (gridPane, SWT.NONE);
        itemControls.add(text);
        editor.grabHorizontal = true;
        editor.setEditor(text, item, 7);
        text.addListener (SWT.Modify, new Listener () {
            public void handleEvent (Event e) {
                Text text = (Text) e.widget;
                item.setText(7, text.getText());
            }
        });

        item.setData(itemControls);
    }

    private void handleRemoveGridPaneItem(TableItem item) {
        List itemControls = (List) item.getData();
        for (int i=0; i<itemControls.size(); i++) {
            Control ctrl = (Control) itemControls.get(i);
            ctrl.dispose();
        }
        gridPane.remove(gridPane.indexOf(item));
        gridPane.setSize(gridPane.getSize().x, 
            gridPane.getSize().y + (0 == gridPane.getItemCount() % 2 ? 1 : -1));
        gridPane.layout();
    }

    private void setupGridPaneMenu() {
        Menu menu = new Menu (gridPane.getShell(), SWT.POP_UP);
        gridPane.setMenu (menu);
        MenuItem item = new MenuItem (menu, SWT.PUSH);
        item.setText("Delete");
        item.addListener (SWT.Selection, new Listener () {
            public void handleEvent (Event event) {
                int itemIndex = gridPane.getSelectionIndex();
                if ( -1 != itemIndex ) {
                    handleRemoveGridPaneItem(gridPane.getItem(itemIndex));
                }
            }
        });
    }

    private void setupTreeDragAndDrop() {
        Transfer[] types = new Transfer[] {TextTransfer.getInstance()};
        int operations = DND.DROP_MOVE | DND.DROP_COPY;
        
        final DragSource source = new DragSource (tree, operations);
        source.setTransfer(types);
        final TreeItem[] dragSourceItem = new TreeItem[1];
        source.addDragListener (new DragSourceListener () {

            public void dragStart(DragSourceEvent event) {
                System.out.println("Tree.dragStart: called.");
                TreeItem[] selection = tree.getSelection();
                if (selection.length > 0 ) {
                    TreeItem selected = selection[0];
                    if ( selected.getData() instanceof TableLocation ) {
                        event.doit = true;
                        dragSourceItem[0] = selected;
                    } else {
                        event.doit = false;
                    }
                } else {
                    event.doit = false;
                }
            }

            public void dragSetData (DragSourceEvent event) {
                event.data = dragSourceItem[0].getData().toString();
            }

            public void dragFinished(DragSourceEvent event) {
                dragSourceItem[0] = null;
            }
        });

        DropTarget target = new DropTarget(diagramPane, operations);
        target.setTransfer(types);
        target.addDropListener (new DropTargetAdapter() {
            public void dragOver(DropTargetEvent event) {
                event.feedback = DND.FEEDBACK_EXPAND | DND.FEEDBACK_SCROLL;
            }

            public void drop(DropTargetEvent event) {
                System.out.println("drop");
                if (event.data == null) {
                    event.detail = DND.DROP_NONE;
                    return;
                } else {
                    String stringifiedTableLocation = (String)event.data;
                    TableLocation tableloc = new TableLocation(stringifiedTableLocation);
                    System.out.println("drop: tableloc=" + tableloc);
                    handleTableSelected(tableloc);
                }
            }
        });
    }

    private QueryDescription getQueryDescription() {
        QueryDescription result = new QueryDescription();
        TableItem[] items = gridPane.getItems();
        for (int i=0; i<items.length; i++) {
            TableItem item = items[i];
            String columnname = item.getText(0);
            String alias = item.getText(1);
            String tablename = item.getText(2);
            String output = item.getText(3);
            String sortType = item.getText(4);
            String sortOrder = item.getText(5);
            String filter = item.getText(6);
            String or = item.getText(7);
            ColumnInfo columnInfo = null;
            if ( item.getData("columnInfo") instanceof ColumnInfo ) {
                columnInfo = (ColumnInfo) item.getData("columnInfo");
            } else {
                System.out.println("--->!: " + item.getData("columnInfo"));
            }

            System.out.println("output=" + output);
            System.out.println("sortType=" + sortType);
            System.out.println("sortOrder=" + sortOrder);
            System.out.println("filter=" + filter);
            System.out.println("or=" + or);
            System.out.println("columnInfo=" + columnInfo);

            if ( null != columnname && !"".equals(columnname)
                    && null != columnname && !"".equals(columnname) )
            {
                ColumnDescription col = new ColumnDescription();
                col.tableName = tablename;
                col.columnName = columnname;
                col.alias = ("".equals(alias) ? null : alias);
                col.output = "1".equals(output);
                col.sortType = ("".equals(sortType) ? null 
                    : ("Ascending".equals(sortType) ? "asc" : "desc"));
                col.sortOrder = ("".equals(sortOrder) ? null : sortOrder);
                col.filter = ("".equals(filter) ? null : filter);
                col.or = ("".equals(or) ? null : or);
                col.columnInfo = columnInfo;
                result.addColumn(col);
            }
        }
        return result;
    }

    private void addInstance() {
        ((WebORBPage3) getPage()).addInstance();
    }
    
    private void generateSql() {
        QueryDescription qdesc = getQueryDescription();        
        sqlPane.setText(qdesc.toQuery());
    }

    private void executeSql() {
        String query = sqlPane.getText();
        String instanceId = (String) databaseCombo.getData();
        String database = databaseCombo.getText();

        resultPane.removeAll();
        TableColumn[] columns = resultPane.getColumns();
        for (int i=0; i<columns.length; i++) {
            columns[i].dispose();
        }

        clearComposite(resultPane);
        ServiceFacade.getInstance(getModel()).runQuery(resultPane, instanceId, query, database);
        getModel().query = getQueryDescription();
    }

    private void clear() {
        gridPane.removeAll();
        clearComposite(diagramPane);
        clearComposite(gridPane);
    }

    private void clearComposite(Composite composite) {
        Control[] controls = composite.getChildren();
        for (int i=0; i<controls.length; i++) {
            controls[i].dispose();
        }
    }

    private WizardPage getPage() {
        return page;
    }
    
    private WebORBModel getModel() {
        return model;
    }
       
}
