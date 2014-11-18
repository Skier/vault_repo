package com.llsvc.containers.utilityClasses
{
import flash.display.DisplayObject;
import mx.containers.utilityClasses.Layout;
import mx.controls.VRule;
import mx.core.Container;
import mx.core.EdgeMetrics;
import mx.core.IUIComponent;
import mx.core.mx_internal;

import mx.core.UIComponent;

use namespace mx_internal;

[ExcludeClass]

/**
 *  @private
 *  The FlowLayout class is for internal use only.
 */
public class AdobeFlowLayout extends Layout
{
	//--------------------------------------------------------------------------
	//
	//  Constructor
	//
	//--------------------------------------------------------------------------

	/**
	 *  Constructor.
	 */
	public function AdobeFlowLayout()
	{
		super();
	}

	//--------------------------------------------------------------------------
	//
	//  Variables
	//
	//--------------------------------------------------------------------------

	private var lastWidth:int;

	mx_internal var explicitHeightSet:Boolean = false;

	mx_internal var explicitMeasuredHeight:int = 0;

	mx_internal var modifyTargetHeight:Boolean = false;

	mx_internal var rowCount:int = -1;

	/**
	 *  @private
	 *  For future use
	 */
	//private var direction:String = FlowDirection.HORIZONTAL;
	
	//--------------------------------------------------------------------------
	//
	//  Overridden methods
	//
	//--------------------------------------------------------------------------
	
	/**
	 *  @private
	 *  Measure container as per Flow layout rules.
	 */
	override public function measure():void
	{
		var target:Container = Container(super.target);

		var minWidth:Number = 0;
		var minHeight:Number = 0;

		var preferredWidth:Number = 0;
		var preferredHeight:Number = 0;

		var n:int = target.numChildren;
		var numGaps:int = -1;
		for (var i:int = 0; i < n; i++)
		{
			var child:IUIComponent = IUIComponent(target.getChildAt(i));
			if (!child.includeInLayout)
				continue;

			numGaps++;
			var wPref:Number = child.getExplicitOrMeasuredWidth();
			var hPref:Number = child.getExplicitOrMeasuredHeight();

			minWidth = Math.max(minWidth, child.minWidth);
			minHeight = Math.max(minHeight, hPref);
			preferredWidth += wPref;
		}

		var vm:EdgeMetrics = target.viewMetricsAndPadding;
		var wPadding:Number = vm.left + vm.right + numGaps*target.getStyle("horizontalGap");
		var hPadding:Number = vm.top + vm.bottom;

		target.measuredMinWidth = minWidth + wPadding;
		target.measuredMinHeight = minHeight + hPadding;

		if (rowCount > 1)
			target.measuredWidth = preferredWidth / rowCount + wPadding + minWidth;
		else
			target.measuredWidth = preferredWidth + wPadding;
		
		target.measuredHeight = minHeight + hPadding;

		if (explicitMeasuredHeight)
			target.measuredMinHeight = target.measuredHeight = explicitMeasuredHeight;
	}

	/**
	 *  @private
	 *  Lay out children as per Flow layout rules.
	 */
	override public function updateDisplayList(unscaledWidth:Number,
											   unscaledHeight:Number):void
	{
		var target:Container = Container(super.target);

		var vm:EdgeMetrics = target.viewMetricsAndPadding;
		
		var horizontalGap:Number = target.getStyle("horizontalGap");
		var verticalGap:Number = target.getStyle("verticalGap");
       
		var xPos:Number = vm.left;
		var yPos:Number = vm.top;
		var maxYPos:Number = 0;
		
		var n:int = target.numChildren;
		var child:IUIComponent;
		var lastChild:IUIComponent;
		var childWidth:int;
		var childHeight:int;
		var popupIndex:int = 0;
		var xEnd:Number = unscaledWidth - vm.right;
		var full:Boolean = false;
		var remainingRows:int = 0xFFFF;
		
		var xWrapWidth:int = 0xFFFF;
		
		if (rowCount > 0 && target.parent)
		{
			var numLines:int = target.parent.height / target.measuredHeight;
			xWrapWidth = target.measuredWidth / rowCount;
			remainingRows = rowCount - 1;
		}
		for (var i:int = 0; i < target.numChildren; i++)
		{
			child = IUIComponent(target.getChildAt(i));

			if (!child.includeInLayout)
				continue;

			childWidth = child.getExplicitOrMeasuredWidth();
			childHeight = child.getExplicitOrMeasuredHeight();
			
			if(!full)
			{
				// Start a new row?
				if (((xPos + childWidth > xEnd && xPos != vm.left) ||
					xPos > xWrapWidth) && remainingRows)
				{
					yPos = maxYPos + verticalGap;
					remainingRows--;
					xPos = vm.left;

					if (child is VRule)
					{
						child.setActualSize(0, 0);
						child.move(xPos, yPos);
						continue;
					}
					else if (lastChild is VRule)
						lastChild.setActualSize(0, 0);
				}
			}
			
			child.setActualSize(childWidth, childHeight)
			child.move(xPos, yPos);
			lastChild = child;
			maxYPos = Math.max(maxYPos, yPos + childHeight);
			xPos += (childWidth + horizontalGap);
			
		}
			
		maxYPos += vm.bottom;

		if (target.height != maxYPos && isNaN(target.percentHeight) && !explicitHeightSet)
		{
			if (modifyTargetHeight)
			{
				target.height = maxYPos;
			}
			else
			{
				explicitMeasuredHeight = target.measuredHeight = maxYPos;
				target.height = NaN;
			}
			explicitHeightSet = false;
			
			if (lastWidth != unscaledWidth)
			{
				target.callLater(target.invalidateDisplayList);
				lastWidth = unscaledWidth;
			}
		}
	}

}

}
