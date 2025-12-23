<%@ Page Title="" Language="C#" MasterPageFile="~/Main1.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

        <!-- ✅ Page Container -->
    <div class="container py-4">

        <!-- 🏷️ Page Header -->
        <h1 class="text-center fw-bold page-title mb-4"> Admin Panel</h1>

<div class="d-flex justify-content-center gap-4 mb-4">
    <!-- Interest ID -->
    <div class="col-md-3">
        <label for="txtIntID" class="fw-semibold text-accent mb-1">Interest ID:</label>
        <asp:TextBox ID="txtIntID" runat="server" CssClass="form-control input-field"
            placeholder="Enter Interest ID" Enabled="False"></asp:TextBox>
    </div>
    <div class="col-md-3">
        <label for="txtInt" class="fw-semibold text-accent mb-1">Interest:</label>
        <asp:TextBox ID="txtInt" runat="server" CssClass="form-control input-field"
            placeholder="Enter Interest" Enabled="False"></asp:TextBox>
    </div>

</div>
        <!-- 🛠️ Action Buttons -->
        <div class="card border-0 shadow-lg mb-5 rounded-4 glass-card card-animate">
            <div class="card-header gradient-purple text-white fw-bold fs-5 rounded-top-4">
                🧩 Actions
            </div>
            <div class="card-body text-center">
                <div class="d-flex flex-wrap gap-2 justify-content-center">
                    <asp:Button ID="btnFirst" runat="server" Text="⏮ First" CssClass="btn btn-outline-light fw-bold rounded-pill action-btn" OnClick="btnFirst_Click" />
                    <asp:Button ID="btnPrev" runat="server" Text="⬅ Prev" CssClass="btn btn-outline-light fw-bold rounded-pill action-btn" OnClick="btnPrev_Click" />
                    <asp:Button ID="btnNew" runat="server" Text="➕ New" CssClass="btn btn-success fw-bold rounded-pill action-btn" OnClick="btnNew_Click" />
                    <asp:Button ID="btnEdit" runat="server" Text="✏ Edit" CssClass="btn btn-warning fw-bold text-dark rounded-pill action-btn" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="💾 Save" CssClass="btn btn-primary fw-bold rounded-pill action-btn" Enabled="False" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="❌ Cancel" CssClass="btn btn-secondary fw-bold rounded-pill action-btn" Enabled="False" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnDel" runat="server" Text="🗑 Delete" CssClass="btn btn-danger fw-bold rounded-pill action-btn"
                        OnClientClick="return confirm('Are You Sure To Delete?')" OnClick="btnDel_Click" />
                    <asp:Button ID="btnNext" runat="server" Text="Next ➡" CssClass="btn btn-outline-light fw-bold rounded-pill action-btn" OnClick="btnNext_Click" />
                    <asp:Button ID="btnLast" runat="server" Text="Last ⏭" CssClass="btn btn-outline-light fw-bold rounded-pill action-btn" OnClick="btnLast_Click" />
                </div>
            </div>
        </div>

        <!-- 📋 Table Section -->
        <div class="table-responsive glass-card shadow-lg rounded-4 mb-5">
            <table class="table table-bordered table-hover text-center align-middle mb-0">
                <thead class="gradient-purple text-white text-uppercase fw-bold">
                    <tr>
                        <th>Interest ID</th>
                        <th>Interest Name</th>
                    </tr>
                </thead>
                <tbody>
                     <%= sbTableData.ToString() %> 
                </tbody>
            </table>
        </div>

        <!-- ⚠️ Error Label -->
        <asp:Label ID="lblErr" runat="server"
            CssClass="alert alert-danger d-block fw-bold text-center fs-5 rounded-3 shadow-sm fade-in"
            ViewStateMode="Disabled"></asp:Label>

<div class="text-end">
    <a href="AdminEntry.aspx" class="btn btn-warning btn-lg">
        Add Skills 👉
    </a>
</div>
    </div>

    <!-- 🎨 Styling -->
    <style>
        body {
            color: #f3e8ff;
            font-family: 'Segoe UI', sans-serif;
        }

        .page-title {
            font-size: 2.5rem;
            color: #e9d8fd;
            text-shadow: 0 0 12px rgba(187, 134, 252, 0.9);
        }

        .text-accent {
            color: #c792ea;
            font-weight: 600;
        }
        input[disabled] {
    background-color: lavender; 
    color: purple; 
}

        .input-field {
            width: 250px;
            background: rgba(255, 255, 255, 0.1);
            border: 1px solid rgba(255, 255, 255, 0.2);
            color: #fff;
            border-radius: 8px;
            padding: 8px 12px;
            transition: all 0.3s ease;
        }

        .input-field:focus {
            outline: none;
            box-shadow: 0 0 10px rgba(187, 134, 252, 0.5);
            border-color: #bb86fc;
            transform: scale(1.02);
        }

        .gradient-purple {
            background: linear-gradient(90deg, #6a00f4, #9d4edd);
        }

        .glass-card {
            background: rgba(255, 255, 255, 0.06);
            border: 1px solid rgba(255, 255, 255, 0.1);
            backdrop-filter: blur(8px);
        }

/* 🎨 Unified Button Colors (Purple Theme) */
.action-btn {
    transition: all 0.3s ease;
    padding: 8px 16px;
    font-size: 0.95rem;
    border-radius: 50px !important;
}

/* 🟣 Primary Buttons */
.btn-primary {
    background-color: #7b2cbf;
    border-color: #9d4edd;
}
.btn-primary:hover {
    background-color: #9d4edd;
    border-color: #c77dff;
    box-shadow: 0 0 10px rgba(157, 78, 221, 0.6);
}

/* 🟢 Success (New) */
.btn-success {
    background-color: #5a189a;
    border-color: #9d4edd;
    color: #fff;
}
.btn-success:hover {
    background-color: #9d4edd;
    border-color: #c77dff;
    box-shadow: 0 0 10px rgba(157, 78, 221, 0.6);
}

/* 🟡 Warning */
.btn-warning {
    background-color: #e0aaff;
    border-color: #c77dff;
    color: #2e0854 !important;
}
.btn-warning:hover {
    background-color: #c77dff;
    border-color: #9d4edd;
}

/* 🔴 Danger */
.btn-danger {
    background-color: #b5179e;
    border-color: #ff4dff;
}
.btn-danger:hover {
    background-color: #ff4dff;
    border-color: #ffb3ff;
}

/* ⚪ Secondary + Outline Buttons */
.btn-outline-light {
    color: #d0aaff;
    border-color: #b97cff;
}
.btn-outline-light:hover {
    background-color: #9d4edd;
    color: white;
    box-shadow: 0 0 8px rgba(157, 78, 221, 0.5);
}

.btn-secondary {
    background-color: #3c096c;
    border-color: #7b2cbf;
}
.btn-secondary:hover {
    background-color: #5a189a;
    border-color: #9d4edd;
}
        .card-animate:hover {
            transform: translateY(-4px);
            box-shadow: 0 8px 20px rgba(255, 255, 255, 0.2);
        }

        .table {
            color: #eaddff;
        }

        .table-hover tbody tr:hover {
            background-color: rgba(157, 78, 221, 0.2);
        }

        .alert-danger {
            background: rgba(255, 0, 60, 0.1);
            border: 1px solid #ff80bf;
            color: #ff99cc;
        }

        .fade-in {
            animation: fadeIn 0.6s ease-in-out;
        }

        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(-10px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        @media (max-width: 768px) {
            .input-field {
                width: 100%;
                margin-top: 10px;
            }
        }
    </style>
</asp:Content>

