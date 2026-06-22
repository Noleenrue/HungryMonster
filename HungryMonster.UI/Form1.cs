namespace HungryMonster.UI;

public partial class Form1 : Form
{
    private readonly ApiService _api;
    private int? _selectedRecordId;

    public Form1(ApiService api)
    {
        _api = api;
        InitializeComponent();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        nudYear.Value = DateTime.Now.Year;
        await RefreshClientsAsync();
        await RefreshGridAsync();
    }

    private async Task RefreshClientsAsync()
    {
        try
        {
            var clients = await _api.GetClientsAsync();
            cmbClient.DataSource = clients;
            cmbClient.DisplayMember = nameof(ClientResponse.Name);
            cmbClient.ValueMember = nameof(ClientResponse.Id);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load clients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task RefreshGridAsync()
    {
        try
        {
            var records = await _api.GetMealRecordsAsync();
            dgvRecords.DataSource = records;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load records: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearSelection()
    {
        _selectedRecordId = null;
        nudYear.Value = DateTime.Now.Year;
        nudServings.Value = 1;
        lblStatus.Text = "No record selected.";
    }

    private async void btnEnter_Click(object sender, EventArgs e)
    {
        if (cmbClient.SelectedValue is not int clientId) return;
        try
        {
            await _api.AddMealRecordAsync((int)nudYear.Value, (int)nudServings.Value, clientId);
            await RefreshGridAsync();
            ClearSelection();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to add record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnAnalyze_Click(object sender, EventArgs e)
    {
        try
        {
            var result = await _api.GetPeakYearAsync();
            lblPeakYear.Text = result is not null
                ? $"Peak Year: {result.Year}  |  Active Companies: {result.ActiveCompaniesCount}"
                : "No data available.";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Analysis failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selectedRecordId is null)
        {
            MessageBox.Show("Select a record in the grid first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        try
        {
            await _api.UpdateMealRecordAsync(_selectedRecordId.Value, (int)nudServings.Value);
            await RefreshGridAsync();
            ClearSelection();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to update record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnDelete_Click(object sender, EventArgs e)
    {
        if (_selectedRecordId is null)
        {
            MessageBox.Show("Select a record in the grid first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        var confirm = MessageBox.Show($"Delete record #{_selectedRecordId}?", "Confirm",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirm != DialogResult.Yes) return;
        try
        {
            await _api.DeleteMealRecordAsync(_selectedRecordId.Value);
            await RefreshGridAsync();
            ClearSelection();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to delete record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvRecords_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvRecords.CurrentRow?.DataBoundItem is MealRecordResponse record)
        {
            _selectedRecordId = record.Id;
            nudYear.Value = record.Year;
            nudServings.Value = record.NumberOfServings;

            // Sync combo to the client of the selected record
            for (int i = 0; i < cmbClient.Items.Count; i++)
            {
                if (cmbClient.Items[i] is ClientResponse c && c.Id == record.ClientId)
                {
                    cmbClient.SelectedIndex = i;
                    break;
                }
            }
            lblStatus.Text = $"Selected record ID: {record.Id}";
        }
    }
}

