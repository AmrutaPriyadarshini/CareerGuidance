<%@ Page Title="" Language="C#" MasterPageFile="~/Main1.master" AutoEventWireup="true" CodeFile="Career.aspx.cs" Inherits="Career" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
<style>
    /* Full-width, transparent section */
    .career-section {
        width: 100%;
        padding: 3rem 2rem;
        color: #f3eaff;
        text-shadow: 0 0 6px rgba(180, 0, 255, 0.3);
    }

    .career-title {
        font-weight: bold;
        font-style: italic;
        text-align: center;
        color: #f0e6ff;
        margin-bottom: 2rem;
        font-size: 2rem;
    }

    .career-desc {
        width: 80%;
        color: #f8eaff;
        line-height: 1.7;
        font-size: 1.05rem;
    }

    .career-img {
        max-width: 100%;
        border-radius: 10px;
        box-shadow: 0 0 15px rgba(180, 0, 255, 0.4);
        border: 1px solid rgba(255, 255, 255, 0.2);
    }
    /* Error label */
    .error-label {
        display: block;
        margin-top: 1rem;
        font-weight: bold;
        color: #ff8080;
        text-shadow: 0 0 6px rgba(255, 0, 0, 0.3);
        text-align: center;
    }

    /* Better spacing and alignment on smaller devices */
    @media (max-width: 768px) {
        .career-section {
            padding: 2rem 1rem;
            text-align: center;
        }
        .career-title {
            font-size: 1.6rem;
        }
    }
</style>

<div class="career-section">
    <asp:Label ID="lblName" runat="server" CssClass="career-title"></asp:Label>

    <div class="container-fluid">
        <div class="row align-items-center">
            <!-- Left Side: Description -->
            <div class="col-md-8">
                <asp:Label ID="lblDes" runat="server" CssClass="career-desc"></asp:Label>
            </div>

            <!-- Right Side: Image -->
            <div class="col-md-4 text-center mt-4 mt-md-0">
                <asp:Image ID="Image1" runat="server" CssClass="career-img"
                    ImageUrl="Images/Sample.jpg" AlternateText="Career Image" />
            </div>
        </div>
              <asp:Label ID="lblerr" runat="server" CssClass="error-label"></asp:Label>
    </div>
</div>
</asp:Content>

