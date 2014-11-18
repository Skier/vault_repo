package truetract.web.dashboard.projectPanel.tabPanel.contactPanel
{
import mx.controls.dataGridClasses.DataGridItemRenderer;

import truetract.domain.ProjectTabContact;

public class ProjectTabContactGridIR extends DataGridItemRenderer
{
    override public function set data(value:Object):void
    {
        super.data = value;

        if (data && data is ProjectTabContact && data.IsActive == true)
        {
            this.setStyle("fontWeight", "bold");
        } 
        else 
        {
            this.setStyle("fontWeight", "normal");
        }

    }
}
}