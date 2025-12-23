<%@ Page Title="" Language="C#" MasterPageFile="~/Main1.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
<style>
    /* Background area */
    .login-bg {
        min-height: 60vh;
        background: linear-gradient(160deg, #000000 0%, #2E0854 40%, #8E24AA 100%);
        display: flex;
        justify-content: center;
        align-items: center;
    }

    /* Glassy dark card */
    .login-card {
        width: 400px;
        border-radius: 20px;
        border: 1px solid rgba(255, 255, 255, 0.15);
        background: rgba(30, 0, 50, 0.6);
        box-shadow: 0 0 25px rgba(140, 0, 255, 0.3);
        backdrop-filter: blur(10px);
        color: #f3eaff;
    }

    .login-title {
        color: #e6ccff;
        font-weight: bold;
        text-shadow: 0 0 6px rgba(180, 0, 255, 0.5);
    }

    .form-label {
        color: #d1b3ff;
        font-weight: 600;
    }

    .form-control {
        background: rgba(255, 255, 255, 0.1);
        border: 1px solid rgba(255, 255, 255, 0.2);
        color: #f8eaff;
    }

    .form-control::placeholder {
        color: #c9a9ff;
    }

    .form-control:focus {
        background: rgba(255, 255, 255, 0.15);
        border-color: #9c27b0;
        box-shadow: 0 0 10px rgba(156, 39, 176, 0.5);
        color: #fff;
    }

    .btn-login {
        background: linear-gradient(45deg, #9c27b0, #8e24aa, #4a148c);
        border: none;
        font-weight: bold;
        color: #f3eaff;
        border-radius: 30px;
        padding: 10px 0;
        transition: 0.3s;
    }

    .btn-login:hover {
        background: linear-gradient(45deg, #ba68c8, #9c27b0);
        box-shadow: 0 0 20px rgba(180, 0, 255, 0.4);
    }

    .error-label {
        color: #ff8080;
        text-shadow: 0 0 6px rgba(255, 0, 0, 0.3);
    }
</style>

<div class="container-fluid login-bg">
    <div class="card login-card shadow-lg p-4">

        <!-- Title -->
        <h1 class="text-center mb-4 login-title">
            🔮 Login
        </h1>

        <!-- ID -->
        <div class="mb-3">
            <label for="txtId" class="form-label">👤 ID</label>
            <asp:TextBox ID="txtId" runat="server" CssClass="form-control"
                         placeholder="Enter your ID"></asp:TextBox>
        </div>

        <!-- Password -->
        <div class="mb-3">
            <label for="txtPwd" class="form-label">🔒 Password</label>
            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" CssClass="form-control"
                         placeholder="Enter your password"></asp:TextBox>
        </div>

        <!-- Login Button -->
        <div class="d-grid mb-3">
            <asp:Button ID="btnLogin" runat="server" Text="Login"
                        CssClass="btn btn-login"
                        OnClick="btnLogin_Click" />
        </div>

        <!-- Error Label -->
        <asp:Label ID="lblerror" runat="server" Text=""
                   CssClass="d-block text-center fw-bold mt-2 error-label"></asp:Label>
    </div>
</div>
</asp:Content>

