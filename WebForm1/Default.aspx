<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Label ID="lblbody" runat="server"></asp:Label>

            <div>
                <asp:Label runat="server" ID="lblID" Visible="false"></asp:Label>
                <asp:Label runat="server" Text="OrderID"></asp:Label>
                <asp:TextBox runat="server" ID="txtOrderID"></asp:TextBox>
                <asp:Label runat="server" Text="Description"></asp:Label>
                <asp:TextBox runat="server" ID="txtDescription"></asp:TextBox>
            </div>

            <asp:Button runat="server" ID="btnsubmit" Text="Submit" OnClick="btnsubmit_Click" />

            <div>
                <br />
            </div>

            <div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="Orderid" HeaderText="Orderid" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Action
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button ID="btneditdata" runat="server" UseSubmitBehavior="false" CommandName="EditData" Text="Edit" />
                                <asp:Button ID="btndelete" runat="server" UseSubmitBehavior="false" CommandName="DeleteData" Text="Delete" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" Width="100" />
                        </asp:TemplateField> 
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </form>
</body>
</html>
