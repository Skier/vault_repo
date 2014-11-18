/*
 *  $Id: PaymentGatewayTranslator.asmx.cs 10495 2006-01-16 18:46:21Z vitaly $
 *
 *  Copyright(c) 2005 Exceleron Software
 */

using System;
using System.Configuration;
using System.ComponentModel;
using System.Web.Services;
//using log4net;

namespace ZoomOnline
{
public class ZoomWebService : WebService
{
    public ZoomWebService() {
        InitializeComponent();
    }

    #region Component Designer generated code

    //Required by the Web Services Designer 
    private IContainer components = null;
        
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing ) {
        if(disposing && components != null) {
            components.Dispose();
        }
        base.Dispose(disposing);        
    }

    #endregion    

    [WebMethod]
    public string[] GetLocalNumbers(string areaCode) {
        string phoneList = System.Configuration.ConfigurationSettings.AppSettings[areaCode];
        if ( null != phoneList ) {
            return phoneList.Split(new char[] {','});
        } else {
            return new string[0];
        }
    }

}

}
