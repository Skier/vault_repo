<%@ Page Language="C#" MasterPageFile="~/RugCleaningMasterPage.master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="Dalworth.Server.Web.RugCleaning.Feedback" %>
<%@ MasterType VirtualPath="~/RugCleaningMasterPage.master" %>
<%@ Import Namespace="Dalworth.Server.Web.RugCleaning" %>

<asp:Content ID="title" ContentPlaceHolderID="titlePlaceHolder" Runat="Server"><title>Contact Information, Dalworth Area Rug Cleaning, Dallas Texas</title>
</asp:Content>

<asp:Content ID="description" ContentPlaceHolderID="descriptionPlaceHolder" Runat="Server">
    <meta name="description" content="Contact Dalworth for your Oriental Rug Cleaning and get Free Quote Today!. Call: <%Response.Write(Master.PhoneNumber);%>"/>
</asp:Content>

<asp:Content ID="keywords" ContentPlaceHolderID="keywordsPlaceHolder" Runat="Server">
    <meta name="keywords" content="dalworth rug cleaning, driving directions, phone number"/>
</asp:Content>

<asp:Content ID="article" ContentPlaceHolderID="articlePlaceholder" Runat="Server">
 <div class="content_col">
    <div class="form_s">
        <form id="m_formFeedback" runat="server" class="full">
             <h3>Thank you for helping us improve our service.<br />
                  Your feedback is critical to us</h3>
              <h4 style="padding:10px;">Your information:</h4>
              <div class="center">
                <table class="result paddings" width="75%"  style="margin: 0 0 0 22%;">
                <tbody>
                        <tr>
                            <td width="45%"> First Name:</td>
                            <td><span><%Response.Write(FirstName);%></span></td>
                        </tr>
                        <tr>
                            <td> Last Name:</td>
                            <td><span><%Response.Write(LastName);%></span></td>
                        </tr>
                        <tr>
                            <td> Date of service:</td>
                            <td><span><%Response.Write(DateCompleted); %></span></td>
                        </tr>
                        <tr>
                            <td> Number of rugs cleaned:</td>
                            <td><span><%Response.Write(RugCount);%></span></td>
                        </tr>
                    </tbody>
                </table>
                <h4 class="separator">Please rate our services:</h4>
                <div class="row">
                    <label>&nbsp;</label>
                    <select id="m_selServiceRate" runat="server" name="serviceRate">
                        <option value="0" class="center" disabled="disabled" selected="selected"> Please select -</option>
                        <option value="1">Excellent</option>
                        <option value="2">Good</option>
                        <option value="3">Acceptable</option>
                        <option value="4">Needs improvement</option>
                        <option value="5">Not satisfied</option>
                    </select>
                </div>
                <h4 class="separator">When can we remind you about your next cleaning?</h4>
                <div class="row">
                    <label>&nbsp;</label>
                    <select id="m_selRemindPeriod" runat="server" name="remindPeriod" >
                        <option value="0" class="center" disabled="disabled" selected="selected"> -Please select -</option>
                        <option value="1">6 month</option>
                        <option value="2">1 year</option>
                        <option value="3">1.5 years</option>
                        <option value="4">2 years</option>
                        <option value="5">Please do not remind me</option>
                    </select>
                </div>
                <h4 class="separator"> Comment: </h4>
                <div class="row center">
                    <textarea id ="m_txtComment"  runat="server" name="comments" rows="5" cols="30"   style="width:310px;margin-right:20px;"></textarea>
                </div>
                <div class="row center padbefore">
                    <input type="submit" id ="btnSubmit" runat="server" name="btnSubmit" value="Submit"/>
                </div>
            </div>
        </form>
    </div>
 </div>
</asp:Content>
