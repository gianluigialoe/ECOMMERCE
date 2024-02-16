<%@ Page Title="" Language="C#" MasterPageFile="~/COMPONENTS/ecommerce.Master" AutoEventWireup="true" CodeBehind="details.aspx.cs" Inherits="ECOMMERCE.COMPONENTS.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h2 id="TtlName" runat="server"></h2>
    <h3 id="LblCategory" runat="server"></h3>
    <img id="ImgProduct" runat="server" />
    <p id="ParContent" runat="server"></p>
      <asp:Button ID="Button1" runat="server" Text="Aggiungi al Carrello" CssClass="btn btn-success" OnClick="Button1_Click" />
      <asp:Button ID="ButtonHome" runat="server" Text="Torna alla Home" CssClass="btn btn-primary" OnClick="ButtonHome_Click" />
</asp:Content>
