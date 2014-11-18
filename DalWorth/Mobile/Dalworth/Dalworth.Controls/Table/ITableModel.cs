using System;
using System.Collections.Generic;
using System.Text;
/**
 *  The <code>TableModel</code> interface specifies the methods the
 *  <code>JTable</code> will use to interrogate a tabular data model. <p>
 *
 *  The <code>JTable</code> can be set up to display any data
 *  model which implements the 
 *  <code>TableModel</code> interface with a couple of lines of code:  <p>
 *  <pre>
 *  	TableModel myData = new MyTableModel(); 
 *  	JTable table = new JTable(myData);
 *  </pre><p>
 *
 * For further documentation, see <a href="http://java.sun.com/docs/books/tutorial/uiswing/components/table.html#data">Creating a Table Model</a>
 * in <em>The Java Tutorial</em>.
 * <p>
 * @version 1.24 01/23/03
 * @author Philip Milne
 * @see JTable
 */

namespace Dalworth.Controls
{
    public delegate void TableModelChangeHandler();

    public interface ITableModel
    {
        /**
         * Returns the number of rows in the model. A
         * <code>JTable</code> uses this method to determine how many rows it
         * should display.  This method should be quick, as it
         * is called frequently during rendering.
         *
         * @return the number of rows in the model
         * @see #getColumnCount
         */
        int GetRowCount();

        /**
         * Returns the number of columns in the model. A
         * <code>JTable</code> uses this method to determine how many columns it
         * should create and display by default.
         *
         * @return the number of columns in the model
         * @see #getRowCount
         */
        int GetColumnCount();

        /**
         * Returns the name of the column at <code>columnIndex</code>.  This is used
         * to initialize the table's column header name.  Note: this name does
         * not need to be unique; two columns in a table can have the same name.
         *
         * @param	columnIndex	the index of the column
         * @return  the name of the column
         */
        String GetColumnName(int columnIndex);

        /**
         * Returns the most specific superclass for all the cell values 
         * in the column.  This is used by the <code>JTable</code> to set up a 
         * default renderer and editor for the column.
         *
         * @param columnIndex  the index of the column
         * @return the common ancestor class of the object values in the model.
         */
        Type GetColumnClass(int columnIndex);

        /**
         * Returns true if the cell at <code>rowIndex</code> and
         * <code>columnIndex</code>
         * is editable.  Otherwise, <code>setValueAt</code> on the cell will not
         * change the value of that cell.
         *
         * @param	rowIndex	the row whose value to be queried
         * @param	columnIndex	the column whose value to be queried
         * @return	true if the cell is editable
         * @see #setValueAt
         */
        bool IsCellEditable(int rowIndex, int columnIndex);

        /**
         * Returns the value for the cell at <code>columnIndex</code> and
         * <code>rowIndex</code>.
         *
         * @param	rowIndex	the row whose value is to be queried
         * @param	columnIndex 	the column whose value is to be queried
         * @return	the value Object at the specified cell
     */
        Object GetValueAt(int rowIndex, int columnIndex);

        /**
         * Sets the value in the cell at <code>columnIndex</code> and
         * <code>rowIndex</code> to <code>aValue</code>.
         *
         * @param	aValue		 the new value
         * @param	rowIndex	 the row whose value is to be changed
         * @param	columnIndex 	 the column whose value is to be changed
         * @see #getValueAt
         * @see #isCellEditable
         */
        void SetValueAt(Object aValue, int rowIndex, int columnIndex);

        Object GetObjectAt(int rowIndex, int columnIndex);

        /**
         * Adds a listener to the list that is notified each time a change
         * to the data model occurs.
         *
         * @param	l		the TableModelListener
		 
         void addTableModelListener(TableModelListener l);
        */
        /**
         * Removes a listener from the list that is notified each time a
         * change to the data model occurs.
         *
         * @param	l		the TableModelListener
		
         void removeTableModelListener(TableModelListener l);
         *  
         */

        event TableModelChangeHandler Change;
    }
}
