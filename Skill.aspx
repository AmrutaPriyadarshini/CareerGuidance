<%@ Page Title="" Language="C#" MasterPageFile="~/Main1.master" AutoEventWireup="true" CodeFile="Skill.aspx.cs" Inherits="Skill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">

<style>

    .skill-section {
        width: 100%;
        padding: 4rem 2rem;
        color: #f3eaff;
    }

    /* Centered container with equal spacing */
    .skill-container {
        background: rgba(25, 0, 40, 0.55);
        border: 1px solid rgba(255, 255, 255, 0.1);
        border-radius: 20px;
        padding: 3rem;
        box-shadow: 0 0 30px rgba(140, 0, 255, 0.3);
        backdrop-filter: blur(10px);
        width: 100%;
        max-width: 1100px;
        margin: 0 auto;
    }

    .skill-title {
        font-size: 2rem;
        font-weight: bold;
        color: #e5ccff;
        margin-bottom: 2rem;
        text-shadow: 0 0 8px rgba(180, 0, 255, 0.6);
        text-align: center;
    }

    /* Skill list styling */
    .skill-list input[type="checkbox"] {
        accent-color: #a855f7;
        transform: scale(1.2);
        margin-right: 8px;
    }

    .skill-list label {
        color: #f5e9ff;
        font-size: 1.1rem;
        background: rgba(255, 255, 255, 0.05);
        padding: 8px 14px;
        border-radius: 10px;
        display: inline-block;
        margin: 8px;
        border: 1px solid rgba(255, 255, 255, 0.1);
        transition: 0.3s;
        cursor: pointer;
    }

    .skill-list label:hover {
        background: rgba(180, 0, 255, 0.15);
        box-shadow: 0 0 10px rgba(180, 0, 255, 0.4);
    }

    /* Button */
    .btn-next {
        background: linear-gradient(45deg, #a855f7, #7e22ce);
        border: none;
        border-radius: 30px;
        color: #f8eaff;
        font-weight: bold;
        padding: 12px 40px;
        margin-top: 20px;
        letter-spacing: 1px;
        box-shadow: 0 0 20px rgba(150, 0, 255, 0.4);
        transition: 0.3s ease;
    }

    .btn-next:hover {
        background: linear-gradient(45deg, #c77dff, #9333ea);
        box-shadow: 0 0 30px rgba(180, 0, 255, 0.6);
        transform: translateY(-3px);
    }

    /* Image styling */
    .skill-img {
        width: 100%;
        max-width: 400px;
        border-radius: 15px;
        box-shadow: 0 0 25px rgba(150, 0, 255, 0.3);
        transition: 0.3s ease;
    }

    .skill-img:hover {
        transform: scale(1.03);
        box-shadow: 0 0 35px rgba(180, 0, 255, 0.5);
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

    @media (max-width: 768px) {
        .skill-container {
            padding: 2rem;
        }
        .skill-img {
            margin-top: 2rem;
        }
    }
</style>

    <div class="skill-section">
        <div class="skill-container">
            <h2 class="skill-title">💡 Select Your Skills</h2>

            <div class="row align-items-center">
                <!-- Left side: Skill list -->
                <div class="col-md-7">
                    <div class="skill-list">
                        <asp:CheckBoxList
                            ID="CheListSkill"
                            runat="server"
                            RepeatDirection="Horizontal"
                            RepeatColumns="2"
                            CssClass="form-check">
                        </asp:CheckBoxList>

                    </div>

                    <div class="text-center">
                    <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-next" OnClick="btnNext_Click" />
                    <asp:Label ID="lblerr" runat="server" CssClass="error-label"></asp:Label>
                </div>
            </div>

            <!-- Right side: Image -->
            <div class="col-md-5 text-center mt-4 mt-md-0">
                <asp:Image ID="ImageSkill" runat="server" CssClass="skill-img"
                    ImageUrl="Images/Skill.jpg" AlternateText="Skill Illustration" />
            </div>
        </div>
    </div>
</div></asp:Content>

