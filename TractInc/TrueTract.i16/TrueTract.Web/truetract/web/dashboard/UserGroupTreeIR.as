package truetract.web.dashboard
{
import flash.events.MouseEvent;

import mx.controls.Button;
import mx.controls.treeClasses.TreeItemRenderer;
import mx.effects.Fade;

public class UserGroupTreeIR extends TreeItemRenderer
{
    [Embed(source="/assets/delete_mini.png")] 
    private var deleteGroupIcon:Class;

    private var deleteGroupButton:Button;

    private var fadeEffect:Fade;
    
    override protected function createChildren():void
    {
        super.createChildren();
        
        if (!deleteGroupButton)
        {
            deleteGroupButton = new Button();
            deleteGroupButton.width = deleteGroupButton.height = 16;
            deleteGroupButton.toolTip = "Delete group";
            deleteGroupButton.setStyle("icon", deleteGroupIcon);
            deleteGroupButton.setStyle("paddingLeft", -2);
            deleteGroupButton.addEventListener(MouseEvent.CLICK, deleteGroupButton_clickHandler);
            deleteGroupButton.alpha = 0.0;

            addChild(deleteGroupButton);

            fadeEffect = new Fade(deleteGroupButton);

            addEventListener(MouseEvent.ROLL_OVER, 
                function ():void {
                    if (data.systemGroup) return;

                    fadeEffect.end();
                    fadeEffect.alphaFrom = 0.0;
                    fadeEffect.alphaTo = 1;
                    fadeEffect.play();
                });

            addEventListener(MouseEvent.ROLL_OUT, 
                function ():void {
                    if (data.systemGroup) return;

                    fadeEffect.end();
                    fadeEffect.alphaFrom = 1;
                    fadeEffect.alphaTo = 0.0;
                    fadeEffect.play();
                });
        }
    }    

    override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
    {
        super.updateDisplayList(unscaledWidth, unscaledHeight);
        
        var paddingRight:int = getStyle("paddingRight");
        deleteGroupButton.y = label.y;
        deleteGroupButton.x = label.x + label.width - deleteGroupButton.width - paddingRight;
    }
    
    private function deleteGroupButton_clickHandler(event:MouseEvent):void
    {
        if (data.systemGroup) return;

        event.preventDefault();
        event.stopPropagation();

        parentDocument.controller.deleteGroupRequest(data);
    }
}
}