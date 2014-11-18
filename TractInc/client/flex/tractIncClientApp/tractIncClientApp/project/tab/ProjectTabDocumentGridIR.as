package tractIncClientApp.project.tab
{
import mx.controls.dataGridClasses.DataGridItemRenderer;

import truetract.domain.ProjectTabDocument;

public class ProjectTabDocumentGridIR extends DataGridItemRenderer
{
    override public function set data(value:Object):void
    {
        super.data = value;

        if (data && data is ProjectTabDocument && data.IsActive == true)
        {
            this.setStyle("fontWeight", "bold");
        } 
        else 
        {
            this.setStyle("fontWeight", "normal");
        }

        if (data && data is ProjectTabDocument 
        		 && ProjectTabDocument(data).DocumentRef 
        		 && ProjectTabDocument(data).DocumentRef.IsActive == false)
        {
            this.setStyle("color", "#000099");
        } 
        else 
        {
            this.setStyle("color", "#000000");
        }
    }
}
}