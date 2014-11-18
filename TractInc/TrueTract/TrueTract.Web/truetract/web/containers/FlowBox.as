package truetract.web.containers
{

import mx.containers.Box;
import mx.core.mx_internal;
import mx.containers.BoxDirection;
import truetract.web.containers.utilityClasses.FlowLayout;

use namespace mx_internal;
    
/**
 * The FlowBox is an extension of Box that implements a
 * FlowLayout algorithm for laying out children.  FlowBox
 * will lay out children in a horizontal fashion.  When
 * the width of the children exceeds the width of the container,
 * the child is placed on a new row.
 */ 
public class FlowBox extends Box
{
    /**
     * Constructor
     */
    public function FlowBox()
    {
        super();
        
        // Force horizontal direction
        direction = BoxDirection.HORIZONTAL;
        
        // Use a FlowLayout to lay out the children
        layoutObject = new FlowLayout();
        layoutObject.target = this; 
    }
    
    /**
     * A FlowBox container can only be horizontal, so override the
     * direction and don't allow the user to change it.
     */
    override public function set direction( value:String ):void
    {
        // Do nothing -- direction cannot be changed and we force
        // a horizontal layout.
    }

} // end class
} // end package