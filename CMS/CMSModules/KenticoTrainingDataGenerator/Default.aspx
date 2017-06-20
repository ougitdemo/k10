<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CMSApp_CMSModules_KenticoTrainingDataGenerator_Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    
     <cms:CMSUpdatePanel ID="pnlU" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <cms:AlertLabel runat="server" ID="lblInfo" />
            <cms:MessagesPlaceHolder runat="server" ID="plcMess" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>

    <h2>Kentico training data generator</h2>

    <h3>Initialization</h3>
    <p>
        <cms:CMSButton ID="btnInit" Text="Initialize contacts" OnClick="btnInit_Click" runat="server" />
    </p>

    <h3>Emails #1</h3>
    <p>
        <cms:CMSDropDownList ID="ddEmailIssues" runat="server" /><br />
        <cms:CMSButton ID="btnEmail" Text="Generate email #1 data" OnClick="btnEmail_Click" runat="server" />
    </p>

    <h3>Emails #2</h3>
    <p>
        <cms:CMSDropDownList ID="ddEmail2Issues" runat="server" /><br />
        <cms:CMSButton ID="btnEmail2" Text="Generate email #2 data" OnClick="btnEmail2_Click" runat="server" />
    </p>

    <h3>A/B test #1</h3>
    <p>
        <cms:CMSDropDownList ID="ddABSelector" runat="server" /><br />
        <cms:CMSDropDownList ID="ddConversionSelector" runat="server" /><br />
        <cms:CMSButton ID="btnABtest" Text="Generate A/B #1 data" OnClick="btnABtest_Click" runat="server" />
    </p>
    <h3>E-mail A/B Test</h3>
    <p>
        <cms:CMSDropDownList ID="ddEmailIssuesABTest" runat="server" /><br />
        <cms:CMSButton ID="btnEmailAbTest" Text="Generate issue A/B data" OnClick="btnEmailAbTest_OnClick" runat="server" />
    </p>
</asp:Content>
