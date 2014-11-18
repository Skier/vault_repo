package com.affilia.components
{
    import mx.controls.dataGridClasses.DataGridColumn;
    import mx.collections.ArrayCollection;

    public class MultiLinesHeaderColumn extends DataGridColumn
    {

        public function MultiLinesHeaderColumn()
        {
            super();
        }

        [Bindable] public var lines:ArrayCollection = new ArrayCollection();
    }

}