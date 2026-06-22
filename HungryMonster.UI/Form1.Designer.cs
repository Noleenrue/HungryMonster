namespace HungryMonster.UI;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    // Controls
    private NumericUpDown nudYear;
    private ComboBox cmbClient;
    private NumericUpDown nudServings;
    private Button btnEnter;
    private Button btnAnalyze;
    private Button btnUpdate;
    private Button btnDelete;
    private DataGridView dgvRecords;
    private Label lblPeakYear;
    private Label lblStatus;

    // Label helpers
    private Label lblYear;
    private Label lblClient;
    private Label lblServings;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();

        // ── Labels ──────────────────────────────────────────────────────────
        lblYear     = new Label { Text = "Year:",             Location = new Point(16, 18),  AutoSize = true };
        lblClient   = new Label { Text = "Client:",           Location = new Point(16, 58),  AutoSize = true };
        lblServings = new Label { Text = "Number of Servings:", Location = new Point(16, 98), AutoSize = true };

        // ── NumericUpDown: Year ──────────────────────────────────────────────
        nudYear = new NumericUpDown
        {
            Location = new Point(160, 14),
            Size     = new Size(120, 26),
            Minimum  = 2000,
            Maximum  = 2100,
            Value    = DateTime.Now.Year
        };

        // ── ComboBox: Client ─────────────────────────────────────────────────
        cmbClient = new ComboBox
        {
            Location      = new Point(160, 54),
            Size          = new Size(220, 26),
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        // ── NumericUpDown: Servings ──────────────────────────────────────────
        nudServings = new NumericUpDown
        {
            Location = new Point(160, 94),
            Size     = new Size(120, 26),
            Minimum  = 1,
            Maximum  = 100000,
            Value    = 1
        };

        // ── Buttons ──────────────────────────────────────────────────────────
        btnEnter = new Button
        {
            Text     = "Enter Data",
            Location = new Point(16, 140),
            Size     = new Size(110, 34),
            BackColor = Color.SteelBlue,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnEnter.Click += btnEnter_Click;

        btnAnalyze = new Button
        {
            Text      = "Analyze Data",
            Location  = new Point(136, 140),
            Size      = new Size(110, 34),
            BackColor = Color.SeaGreen,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnAnalyze.Click += btnAnalyze_Click;

        btnUpdate = new Button
        {
            Text      = "Update",
            Location  = new Point(256, 140),
            Size      = new Size(110, 34),
            BackColor = Color.DarkOrange,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnUpdate.Click += btnUpdate_Click;

        btnDelete = new Button
        {
            Text      = "Delete",
            Location  = new Point(376, 140),
            Size      = new Size(110, 34),
            BackColor = Color.Firebrick,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnDelete.Click += btnDelete_Click;

        // ── Peak Year Label ──────────────────────────────────────────────────
        lblPeakYear = new Label
        {
            Text      = "Peak Year: —",
            Location  = new Point(16, 192),
            Size      = new Size(760, 24),
            Font      = new Font("Segoe UI", 10f, FontStyle.Bold),
            ForeColor = Color.DarkSlateBlue
        };

        // ── Status Label ─────────────────────────────────────────────────────
        lblStatus = new Label
        {
            Text     = "No record selected.",
            Location = new Point(16, 220),
            Size     = new Size(760, 20),
            ForeColor = Color.Gray
        };

        // ── DataGridView ─────────────────────────────────────────────────────
        dgvRecords = new DataGridView
        {
            Location          = new Point(16, 250),
            Size              = new Size(956, 360),
            ReadOnly          = true,
            AllowUserToAddRows = false,
            SelectionMode     = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor   = Color.White,
            BorderStyle       = BorderStyle.Fixed3D
        };
        dgvRecords.SelectionChanged += dgvRecords_SelectionChanged;

        // ── Form ─────────────────────────────────────────────────────────────
        this.Text            = "HungryMonster — Catering Management";
        this.ClientSize      = new Size(992, 630);
        this.MinimumSize     = new Size(992, 630);
        this.StartPosition   = FormStartPosition.CenterScreen;
        this.BackColor       = Color.WhiteSmoke;
        this.Font            = new Font("Segoe UI", 9f);

        this.Controls.AddRange(new Control[]
        {
            lblYear, nudYear,
            lblClient, cmbClient,
            lblServings, nudServings,
            btnEnter, btnAnalyze, btnUpdate, btnDelete,
            lblPeakYear, lblStatus,
            dgvRecords
        });
    }
}

