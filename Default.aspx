<%@ Page Title="" Language="C#" MasterPageFile="~/Main1.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
<style>
    /* Medium screens (tablets) */
@media (max-width: 992px) {
    .skin-section {
        padding: 30px 20px;
    }

    .skin-heading {
        font-size: 1.8rem;
    }

    .skin-btn {
        height: 90px;
        font-size: 1rem;
    }

    .container-fluid {
        padding: 0 25px;
    }
}

/* Small screens (large phones) */
@media (max-width: 768px) {
    .skin-section {
        padding: 25px 18px;
    }

    .skin-heading {
        font-size: 1.6rem;
    }

    .skin-btn {
        height: 80px;
        font-size: 0.95rem;
    }

    .col-6 {
        width: 50%;
    }

    .container-fluid {
        padding: 0 20px;
    }
}

/* Extra small screens (phones) */
@media (max-width: 576px) {
    .skin-section {
        padding: 20px 15px;
    }

    .skin-heading {
        font-size: 1.4rem;
        margin-bottom: 18px;
    }

    .skin-btn {
        height: 70px;
        font-size: 0.9rem;
    }

    .col-6 {
        width: 100%; /* stack buttons vertically */
    }

    .container-fluid {
        padding: 0 15px;
    }
}
    /* Transparent, glassy section */
    .skin-section {
        background: rgba(255, 255, 255, 0.05);
        backdrop-filter: blur(6px);
        padding: 40px 30px;
        border-radius: 15px;
        margin-top: 40px;
        border: 1px solid rgba(255, 255, 255, 0.1);
        box-shadow: 0 4px 15px rgba(0, 0, 0, 0.3);
        position: relative;
    }

    /* Heading */
    .skin-heading {
        text-align: center;
        font-weight: 700;
        font-size: 2rem;
        color: #BB86FC;
        margin-bottom: 25px;
        text-shadow: 0 0 8px rgba(102, 51, 153, 0.6);
        letter-spacing: 1px;
    }

    /* Button styling — all same color */
    .skin-btn {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100px;
        width: 100%;
        border-radius: 12px;
        border: none;
        cursor: pointer;
        font-weight: 600;
        font-size: 1.1rem;
        text-align: center;
        color: #fff;
        background: linear-gradient(135deg, #7B1FA2, #9C27B0);
        background-size: 200% auto;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.25);
        transition: all 0.3s ease;
    }

    .skin-btn:hover {
        transform: translateY(-4px);
        box-shadow: 0 8px 20px rgba(0, 0, 0, 0.3);
        filter: brightness(1.1);
    }

    /* Container padding */
    .container-fluid {
        padding: 0 40px;
    }
    /* Label */
    .lblerr {
        display: block;
        text-align: center;
        margin-top: 15px;
        color: #FFB74D;
        font-weight: 600;
    }
</style>

<div class="container-fluid my-4">
    <div class="skin-section">
        <h2 class="skin-heading">Choose Your Interest</h2>

        <div class="row g-3">
            <%
                foreach (int pid in dctInterest.Keys)
                {
            %>
                <div class="col-6 col-md-3 text-center">
                    <input type="button"
                           value="<%= dctInterest[pid] %>"
                           class="skin-btn"
                           onclick="location.href='Skill.aspx?InterestID=<%= pid %>';" />
                </div>
            <%
                }
            %>
        </div>

        <asp:Label ID="lblerr" runat="server" CssClass="lblerr"></asp:Label>
    </div>
</div>
</asp:Content>
