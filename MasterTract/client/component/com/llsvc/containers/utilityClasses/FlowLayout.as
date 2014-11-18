package com.llsvc.containers.utilityClasses
{

import mx.containers.utilityClasses.BoxLayout;
import mx.core.mx_internal;
import mx.core.EdgeMetrics;
import mx.core.IFlexDisplayObject;

use namespace mx_internal;
	
/**
 * A FlowLayout implementation.  When the width of the children exceeds 
 * the width of the container, the next child is placed on a new row.
 */	
public class FlowLayout extends BoxLayout
{
	/**
	 * Constructor
	 */
	public function FlowLayout()
	{
		super();
	}
	
	/**
	 * Measure the contents of the container and report back to the
	 * layout manager.
	 */
	override public function measure():void
	{
		super.measure();
		
		// TODO: This is tricky.  Because the FlowLayout can accomodate
		// multiple width and heights, it's hard to determine what the
		// measured values are.  For the time being, we'll just the 
		// measurement of the BoxLyaout in a regular HBox fashion.  This
		// produces, essentially, a FlowLayout measurement with just 1
		// row defined.
	}
	
	/**
	 * Layout the contents of the target using a flow layout
	 */
	override public function updateDisplayList( unscaledWidth:Number, unscaledHeight:Number ):void
	{
		super.updateDisplayList( unscaledWidth, unscaledHeight );
		
		var vm:EdgeMetrics = target.viewMetricsAndPadding;
		
		var hAlign:Number = getHorizontalAlignValue();
		var vAlign:Number = getVerticalAlignValue();
		var hGap:Number = target.getStyle( "horizontalGap" );
		var vGap:Number = target.getStyle( "verticalGap" );
		var len:Number = target.numChildren;
		
		var currentRowChildren:Array = new Array;
		var currentRowHeight:Number = 0;
		var currentRowY:Number = vm.top;
		var currentRowLastX:Number = vm.left;
		
		var child:IFlexDisplayObject;
		var tmpChild:IFlexDisplayObject;
		var rowExcessSpace:Number;
		var top:Number;
		
		for ( var i:int = 0; i < len; i++ )
		{
			child = IFlexDisplayObject( target.getChildAt( i ) );
			
			// If the child can't be placed in the current row....
			if ( currentRowLastX + child.width > unscaledWidth - vm.right )
			{
				rowExcessSpace = unscaledWidth - vm.right - currentRowLastX;
				rowExcessSpace *= hAlign;
				currentRowLastX = rowExcessSpace == 0 ? vm.left : rowExcessSpace;
								
				// Go back through the row and adjust the children for
				// their vertical and horizontal align values
				for ( var j:int = 0; j < currentRowChildren.length; j++ )
				{
					tmpChild = currentRowChildren[ j ];
					top = ( currentRowHeight - tmpChild.height ) * vAlign;
					tmpChild.move( Math.floor( currentRowLastX ), currentRowY + Math.floor( top ) );
					currentRowLastX += hGap + tmpChild.width;
				}
				
				// Start a new row
				currentRowY += currentRowHeight + vGap;
				currentRowLastX = vm.left;
				currentRowHeight = 0;
				currentRowChildren = [];	
				
			}
			
			// Don't actually move the child yet because that'd done when we
			// "finish" a row
			//child.move( currentRowLastX, currentRowY );
			
			// Move on to the next x location in the row
			currentRowLastX += hGap + child.width;
			
			// Add the child to the current row so we can adjust the
			// coordinates based on vAlign and hAlign
			currentRowChildren.push( child );
			
			// The largest child height in the row is the height for the
			// entire row
			currentRowHeight = Math.max( child.height, currentRowHeight );
		}
		
		// Done laying out the children, finish up the children that
		// are in the last row -- adjust the children for
		// their vertical and horizontal align values
		rowExcessSpace = unscaledWidth - vm.right - currentRowLastX;
		rowExcessSpace *= hAlign;
		currentRowLastX = rowExcessSpace ? vm.left : rowExcessSpace;
				
		for ( j = 0; j < currentRowChildren.length; j++ )
		{
			tmpChild = currentRowChildren[ j ];
			top = ( currentRowHeight - tmpChild.height ) * vAlign;
			tmpChild.move( Math.floor( currentRowLastX ), currentRowY + Math.floor( top ) );
			currentRowLastX += hGap + tmpChild.width;
		}
		
	}

} // end class
} // end package