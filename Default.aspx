<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Poker._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
.repeater-container {
    margin-bottom: 20px; /* Adds spacing between repeaters */
}

.image-item {
    display: inline-block; /* Arranges images side-by-side */
    margin-right: 10px; /* Adds spacing between images */
    vertical-align: top; /* Aligns images at the top if they have different heights */
}

.side-by-side-image {
    width: 100px; /* Example width, adjust as needed */
    height: 100px; /* Maintain aspect ratio */
}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <p>Player Number One</p>
        <asp:Repeater ID="PokerHand1" runat="server">
            <ItemTemplate>
                <div class="image-item">
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' CssClass="side-by-side-image" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <br />
		<br />
		<asp:Label ID="Hand1Result" runat="server" ForeColor="#990000" Font-Size="Medium"></asp:Label>
		<br />
        <p>Player Number Two</p>
        <asp:Repeater ID="PokerHand2" runat="server">
            <ItemTemplate>
                <div class="image-item">
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' CssClass="side-by-side-image" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <br />
		<br />
		<asp:Label ID="Hand2Result" runat="server" ForeColor="#990000" Font-Size="Medium" ></asp:Label>
        <br />
		<br />
        <div>
		    <asp:Button Text="Deal New Hand" runat="server" ID="BtnDeal" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Label ID="HandWinner" runat="server" ForeColor="#006600"></asp:Label>
        </div>
    </form>
</body>
</html>
