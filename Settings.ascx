<%@ Control Language="vb" Inherits="DNNStuff.InjectAnything.Settings" CodeBehind="Settings.ascx.vb" AutoEventWireup="false" Explicit="True" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="URL" Src="~/controls/URLControl.ascx" %>
<div class="dnnForm dnnClear">
    <div id="editsettings" class="tabslayout">
        <ul id="editsettings-nav" class="tabslayout">
            <li><a href="#tab1"><span>
                <%=Localization.GetString("TabCaption_Tab1", LocalResourceFile)%></span></a></li>
            <li><a href="#help"><span>
                <%=Localization.GetString("TabCaption_Help", LocalResourceFile)%></span></a></li>
        </ul>
        <div class="tabs-container">
            <div class="tab" id="tab1">
				<div class="dnnFormItem">
                    <dnn:Label ID="lblInsertionPoint" runat="server" ControlName="ddlInsertionPoint" Suffix=":" />
					<asp:DropDownList ID="ddlInsertionPoint" runat="server" AutoPostBack="false">
						<asp:ListItem Value="Module" Text="Module" />
						<asp:ListItem Value="Head" Text="Head" />
						<asp:ListItem Value="AfterBody" Text="After Opening Body" />
						<asp:ListItem Value="AfterForm" Text="After Opening Form" />
						<asp:ListItem Value="BeforeForm" Text="Before Closing Form" />
						<asp:ListItem Value="BeforeBody" Text="Before Closing Body" />
					</asp:DropDownList>
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="lblSource" runat="server" ControlName="ddlSource" Suffix=":" />
					<asp:DropDownList ID="ddlSource" runat="server" AutoPostBack="true">
						<asp:ListItem Value="Text" Text="Freeform Text" />
						<asp:ListItem Value="File" Text="File Explorer" />
						<asp:ListItem Value="Standard" Text="Standard Script" />
					</asp:DropDownList>
                </div>
                <!-- Standard driven input -->
				<div id="divStandard" runat="server" class="dnnFormItem">
					<dnn:Label ID="lblStandard" runat="server" ControlName="ddlStandard" Suffix=":" Text="Standard" />
					<asp:DropDownList ID="ddlStandard" runat="server" AutoPostBack="true" />
				</div>
                <!-- File driven input -->
				<div id="divFile" runat="server">
					<div class="dnnFormItem">
						<dnn:Label ID="lblFile" runat="server" ControlName="ctlFile" Suffix=":" Text="File" />
						<div class="dnnLeft"><dnn:URL ID="ctlFile" runat="server" Width="300" ShowTabs="False" ShowUrls="False"
							ShowFiles="True" UrlType="F" ShowLog="False" ShowNewWindow="False" ShowTrack="False" Required="False" /></div>
					</div>
					<div class="dnnFormItem">
						<dnn:Label ID="lblLinkFile" runat="server" ControlName="chkLinkFile" Suffix=":" Text="Link this file as is" />
						<asp:CheckBox ID="chkLinkFile" runat="server" />
					</div>
				</div>
                <!-- Text driven input -->
				<div class="dnnFormItem" id="divText" runat="server">
					<dnn:Label ID="lblText" runat="server" ControlName="txtText" Suffix=":" />
					<asp:TextBox ID="txtText" runat="server" Width="60%" Rows="20" TextMode="MultiLine" />
				</div>
                <!-- Token definition -->
                <div class="dnnFormItem">
                    <dnn:Label ID="lblToken" runat="server" ControlName="pnlToken" Suffix=":" />
                    <asp:LinkButton ID="cmdRefresh" Text="Update" resourcekey="cmdRefresh" CausesValidation="True" runat="server" CssClass="CommandButton" BorderStyle="none" />
					<div id="token-panel" class="dnnClear">
					<asp:Panel id="pnlToken" runat="server" />
					</div>
                </div>
                <!-- Miscellaneous options -->
                <div class="dnnFormItem">
                    <dnn:Label ID="lblEnable" runat="server" ControlName="chkEnable" Suffix=":" />
					<asp:CheckBox runat="server" ID="chkEnable" />
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="lblDebug" runat="server" ControlName="chkDebug" Suffix=":" />
					<asp:CheckBox runat="server" ID="chkDebug" />
                </div>
            </div>
            <div class="tab" id="help">
                <div><%=Localization.GetString("DocumentationHelp.Text", LocalResourceFile)%></div>
            </div>
        </div>
    </div>
	<ul class="dnnActions dnnClear">
		<li><asp:LinkButton ID="cmdUpdate" Text="Update" resourcekey="cmdUpdate" CausesValidation="True" runat="server" CssClass="dnnPrimaryAction"  /></li>
		<li><asp:LinkButton ID="cmdCancel" Text="Cancel" resourcekey="cmdCancel" CausesValidation="False" runat="server" CssClass="dnnSecondaryAction" /></li>
	</ul>
	
</div>

<script type="text/javascript">
    var tabber1 = new Yetii({
        id: 'editsettings',
        persist: true
    });
</script>

