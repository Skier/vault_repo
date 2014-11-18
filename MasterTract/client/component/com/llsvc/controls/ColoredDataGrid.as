package com.llsvc.controls
{
	import mx.controls.DataGrid;
	import flash.display.Sprite;
	import mx.collections.ArrayCollection;

	public class ColoredDataGrid extends DataGrid
	{
        private var _rowColorFunction:Function;
        
        public function ColoredDataGrid()
        {
            super();
        }

        public function set rowColorFunction(f:Function):void
        {
            this._rowColorFunction = f;
        }
        
        public function get rowColorFunction():Function
        {
            return this._rowColorFunction;
        }
        
        private var displayWidth:Number; 

        override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
        {
            super.updateDisplayList(unscaledWidth, unscaledHeight);         
            if (displayWidth != unscaledWidth - viewMetrics.right - viewMetrics.left) {
                displayWidth = unscaledWidth - viewMetrics.right - viewMetrics.left;
            }
        }

        override protected function drawRowBackground(s:Sprite, rowIndex:int,
                                                y:Number, height:Number, color:uint, dataIndex:int):void
        {
            if( this.rowColorFunction != null && this.dataProvider != null) {
                if( dataIndex < (this.dataProvider as ArrayCollection).length ) {
                    var item:Object = (this.dataProvider as ArrayCollection).getItemAt(dataIndex);
                    color = this.rowColorFunction.call(this, item, color);
                }
            }
            
            super.drawRowBackground(s, rowIndex, y, height, color, dataIndex);
        }
        
        
	}
}