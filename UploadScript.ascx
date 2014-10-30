<%@ Control Language="vb" Inherits="DNNStuff.InjectAnything.UploadScript" CodeBehind="UploadScript.ascx.vb"
    AutoEventWireup="false" Explicit="True" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<div class="dnnForm dnnClear">
    <div class="dnnFormItem">
        <dnn:Label ID="lblBrowse" runat="server" ControlName="cmdBrowse" Suffix=":" Text="Browse Files" />
        <input id="cmdBrowse" type="file" size="50" name="cmdBrowse" runat="server" />&nbsp;&nbsp;
        <dnn:CommandButton ID="cmdAdd" runat="server" CssClass="CommandButton" Text="Upload New Script"
            ResourceKey="cmdAdd" ImageUrl="~/images/save.gif" />
    </div>
    <div class="dnnFormItem">
        <asp:Label ID="lblMessage" runat="server" CssClass="Normal" Width="500px" EnableViewState="False"></asp:Label>
    </div>
    <ul class="dnnActions dnnClear">
        <asp:LinkButton ID="cmdReturn" runat="server" Text="Return" ResourceKey="cmdReturn" CssClass="dnnPrimaryAction" CausesValidation="False" />
    </ul>
</div>
<table id="tblLogs" cellspacing="0" cellpadding="0" summary="Resource Upload Logs Table"  runat="server" visible="False">
    <tr>
        <td>
            <asp:Label ID="lblLogTitle" runat="server" resourcekey="LogTitle">Resource Upload Logs</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:PlaceHolder ID="phPaLogs" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <dnn:CommandButton ID="cmdReturn2" runat="server" CssClass="CommandButton" ImageUrl="~/images/lt.gif"
                ResourceKey="cmdReturn" />
        </td>
    </tr>
</table>
