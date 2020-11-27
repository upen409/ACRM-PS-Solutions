using ACUS.Logging;
using ACUS.Server;
using ACUS.Shared;
using System;
using System.IO;
using System.Windows.Forms;

namespace ACUS.Client
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            pgbProcess.Value = 0;
            pgbProcess.Visible = false;
            ACUSConstants.SelectedDatabaseType = DatabaseType.SQLServer;
            HideAllPanels();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCalculateMeasurements_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                {
                    MessageBox.Show("Please enter a data in textbox", "Error");
                    return;
                }
                UseWaitCursor = true;
                btnCalculateMeasurements.Enabled = false;
                pgbProcess.Visible = true;
                var processor = new InitProcess();
                var inputFile = AppDomain.CurrentDomain.BaseDirectory;
                inputFile += "/Measurements/Measurements.xml";

                foreach (var percentage in processor.ProcessData(inputFile))
                {
                    pgbProcess.Value = percentage;
                }

                Logger.LogCSV(ACUSConstants.LstOutputData);
                btnCalculateMeasurements.Enabled = true;
                UseWaitCursor = false;
                btnSaveResults.Enabled = true;
            }
            catch (Exception)
            {
                UseWaitCursor = false;
                btnSaveResults.Enabled = false;
                btnCalculateMeasurements.Enabled = true;
                throw;
            }
        }

        private bool ValidateForm()
        {
            return string.IsNullOrEmpty(ACUSConstants.CRMDataDBInstance) ||
                    string.IsNullOrEmpty(ACUSConstants.CRMDataDBName) ||
                    string.IsNullOrEmpty(ACUSConstants.CRMDataDBUser) ||
                    string.IsNullOrEmpty(ACUSConstants.CRMDataDBPassword) ||
                    string.IsNullOrEmpty(ACUSConstants.CRMDesignerDBInstance) ||
                    string.IsNullOrEmpty(ACUSConstants.CRMDesignerDBName) ||
                    string.IsNullOrEmpty(ACUSConstants.CRMDesignerDBUser) ||
                    string.IsNullOrEmpty(ACUSConstants.CRMDesignerDBPassword) ||
                    string.IsNullOrEmpty(ACUSConstants.TablePrefix) ? false : true;
        }

        private void rbd_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.Checked == true)
            {
                switch (radioButton.Text)
                {
                    case "SQL Server":
                        ACUSConstants.SelectedDatabaseType = DatabaseType.SQLServer;
                        break;
                    case "Oracle":
                        ACUSConstants.SelectedDatabaseType = DatabaseType.Oracle;
                        break;
                }
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;

            switch (txtBox.Name)
            {
                case "txtCRMDBInstance":
                    ACUSConstants.CRMDataDBInstance = txtBox.Text;
                    break;
                case "txtCRMDBName":
                    ACUSConstants.CRMDataDBName = txtBox.Text;
                    break;
                case "txtCRMDBUser":
                    ACUSConstants.CRMDataDBUser = txtBox.Text;
                    break;
                case "txtCRMDBPassword":
                    ACUSConstants.CRMDataDBPassword = txtBox.Text;
                    break;
                case "txtCRMDesDBInstance":
                    ACUSConstants.CRMDesignerDBInstance = txtBox.Text;
                    break;
                case "txtCRMDesDBName":
                    ACUSConstants.CRMDesignerDBName = txtBox.Text;
                    break;
                case "txtCRMDesDBUser":
                    ACUSConstants.CRMDesignerDBUser = txtBox.Text;
                    break;
                case "txtCRMDesDBPassword":
                    ACUSConstants.CRMDesignerDBPassword = txtBox.Text;
                    break;
                case "txtCRMDBTablePrefix":
                    ACUSConstants.TablePrefix = txtBox.Text;
                    break;
            }
        }

        private void btnSaveResults_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdResults = new SaveFileDialog();
            string filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            sfdResults.Filter = filter;
            sfdResults.Title = "Save an CSV File";
            var res = sfdResults.ShowDialog();
            if (res == DialogResult.OK)
            {
                File.Copy(ACUSConstants.ResultFilePath, sfdResults.FileName, true);
                MessageBox.Show("File saved.", "Information");
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnDBInstanceHelpClose_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var pnl = btn.Parent;
            pnl.Visible = false;
            //pnlDBInstanceHelp.Visible = false;
        }

        private void btnDBInstanceHelp_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            HideAllPanels();
            switch (btn.Name)
            {
                case "btnDBInstanceHelp":
                case "btnDesDBInstanceHelp":
                    {
                        pnlDBInstanceHelp.Visible = true;
                        SetPanelPosition(pnlDBInstanceHelp);
                    }
                    break;
                case "btnDBNameHelp":
                    {
                        pnlCRMDBNameHelp.Visible = true;
                        SetPanelPosition(pnlCRMDBNameHelp);
                    }
                    break;
                case "btnDBUserHelp":
                case "btnDesDBUserHelp":
                    {
                        pnlCRMDBUserHelp.Visible = true;
                        SetPanelPosition(pnlCRMDBUserHelp);
                    }
                    break;
                case "btnDBPassHelp":
                case "btnDesDBPassHelp":
                    {
                        pnlCRMDBPassHelp.Visible = true;
                        SetPanelPosition(pnlCRMDBPassHelp);
                    }
                    break;
                case "btnDesDBNameHelp":
                    {
                        pnlCRMDesDBNameHelp.Visible = true;
                        SetPanelPosition(pnlCRMDesDBNameHelp);
                    }
                    break;
                case "btnTablePrefixHelp":
                    {
                        pnlTablePrefixHelp.Visible = true;
                        SetPanelPosition(pnlTablePrefixHelp);
                    }
                    break;
                default:
                    break;
            }
        }

        private void HideAllPanels()
        {
            pnlDBInstanceHelp.Visible = false;
            pnlCRMDBNameHelp.Visible = false;
            pnlCRMDBUserHelp.Visible = false;
            pnlCRMDBPassHelp.Visible = false;
            pnlCRMDesDBNameHelp.Visible = false;
            pnlTablePrefixHelp.Visible = false;
        }

        private void SetPanelPosition(Panel displayPanel)
        {
            displayPanel.Location = new System.Drawing.Point(ClientSize.Width / 2 - displayPanel.Size.Width / 2, ClientSize.Height / 2 - displayPanel.Size.Height / 2);
            displayPanel.Anchor = AnchorStyles.None;
            displayPanel.BringToFront();
        }
    }
}
