package com.themidnightcoders.fbplugin.action;

import org.eclipse.ui.IWorkbenchWindow;
import org.eclipse.ui.IWorkbenchWindowActionDelegate;
import org.eclipse.jface.action.IAction;
import org.eclipse.jface.viewers.ISelection;
import org.eclipse.swt.widgets.*;
import org.eclipse.jface.viewers.IStructuredSelection;
import org.eclipse.jface.wizard.WizardDialog;

import com.themidnightcoders.fbplugin.wizard.WebORBWizard;

public class WebORBAction implements IWorkbenchWindowActionDelegate
{
    private IWorkbenchWindow window;
    ISelection selection;

    int i;

    /**
     * The constructor.
     */

    public WebORBAction()
    {
    }

    public void run( IAction action )
    {
        // Wizard starts here
        Shell shell = window.getShell();
        // WebWizard wizard = new WebWizard();
        WebORBWizard wizard = new WebORBWizard();
        // if ((selection instanceof IStructuredSelection) || (selection ==
        // null))
        wizard.init( window.getWorkbench(), (IStructuredSelection) selection );
        // Instantiates the wizard container with the wizard and opens it
        WizardDialog dialog = new WizardDialog( shell, wizard );
        dialog.setPageSize(800, 600);
        dialog.create();
        dialog.open();
    }

    /**
     * Selection in the workbench has been changed. We can change the state of
     * the 'real' action here if we want, but this can only happen after the
     * delegate has been created.
     * 
     * @see IWorkbenchWindowActionDelegate#selectionChanged
     */
    public void selectionChanged( IAction action, ISelection selection )
    {
        this.selection = selection;
    }

    /**
     * We can use this method to dispose of any system resources we previously
     * allocated.
     * 
     * @see IWorkbenchWindowActionDelegate#dispose
     */
    public void dispose()
    {
    }

    public void init( IWorkbenchWindow window )
    {
        this.window = window;
    }
}
