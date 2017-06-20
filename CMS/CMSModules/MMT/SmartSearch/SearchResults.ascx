<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MMT_SmartSearch_SearchResults"
     CodeFile="SearchResults.ascx.cs" %>
<%@ Register src="~/CMSModules/SmartSearch/Controls/SearchResults.ascx" tagname="SearchResults" tagprefix="cms" %>

<asp:Panel runat="server" ID="pnlSearchResults">
    <asp:PlaceHolder runat="server" ID="plcBasicRepeater"></asp:PlaceHolder>
    <cms:UniPager runat="server" ID="pgrSearch" PageControl="repSearchResults" />
    <cms:LocalizedLabel runat="server" ID="lblNoResults" CssClass="ContentLabel" Visible="false"
        EnableViewState="false" />
    <cms:MessagesPlaceHolder ID="plcMess" runat="server" />
</asp:Panel>
