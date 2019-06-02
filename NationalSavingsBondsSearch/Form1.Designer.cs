namespace NationalSavingsBondsSearch
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdResults = new System.Windows.Forms.DataGridView();
            this.colDenomination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBondNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDrawDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDrawNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrizeAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lnkMyBonds = new System.Windows.Forms.LinkLabel();
            this.datCheckDrawsAfter = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkApplyDate = new System.Windows.Forms.LinkLabel();
            this.lnkViewResult = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grdResults)).BeginInit();
            this.SuspendLayout();
            // 
            // grdResults
            // 
            this.grdResults.AllowUserToAddRows = false;
            this.grdResults.AllowUserToDeleteRows = false;
            this.grdResults.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.grdResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDenomination,
            this.colBondNumber,
            this.colDrawDate,
            this.colDrawNo,
            this.colPrizeAmount});
            this.grdResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdResults.Location = new System.Drawing.Point(11, 41);
            this.grdResults.MultiSelect = false;
            this.grdResults.Name = "grdResults";
            this.grdResults.ReadOnly = true;
            this.grdResults.RowHeadersVisible = false;
            this.grdResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdResults.ShowEditingIcon = false;
            this.grdResults.ShowRowErrors = false;
            this.grdResults.Size = new System.Drawing.Size(526, 273);
            this.grdResults.TabIndex = 2;
            this.grdResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdResults_CellContentClick);
            this.grdResults.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdResults_CellContentDoubleClick);
            // 
            // colDenomination
            // 
            this.colDenomination.HeaderText = "Denomination";
            this.colDenomination.Name = "colDenomination";
            this.colDenomination.ReadOnly = true;
            // 
            // colBondNumber
            // 
            this.colBondNumber.HeaderText = "Bond No.";
            this.colBondNumber.Name = "colBondNumber";
            this.colBondNumber.ReadOnly = true;
            // 
            // colDrawDate
            // 
            dataGridViewCellStyle6.Format = "d";
            dataGridViewCellStyle6.NullValue = "dd-MMM-YYYY";
            this.colDrawDate.DefaultCellStyle = dataGridViewCellStyle6;
            this.colDrawDate.HeaderText = "Draw Date";
            this.colDrawDate.Name = "colDrawDate";
            this.colDrawDate.ReadOnly = true;
            // 
            // colDrawNo
            // 
            this.colDrawNo.HeaderText = "Draw No.";
            this.colDrawNo.Name = "colDrawNo";
            this.colDrawNo.ReadOnly = true;
            // 
            // colPrizeAmount
            // 
            this.colPrizeAmount.HeaderText = "Prize Amount";
            this.colPrizeAmount.Name = "colPrizeAmount";
            this.colPrizeAmount.ReadOnly = true;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(12, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(433, 17);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Starting...";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lnkMyBonds
            // 
            this.lnkMyBonds.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkMyBonds.Location = new System.Drawing.Point(8, 317);
            this.lnkMyBonds.Name = "lnkMyBonds";
            this.lnkMyBonds.Size = new System.Drawing.Size(116, 17);
            this.lnkMyBonds.TabIndex = 6;
            this.lnkMyBonds.TabStop = true;
            this.lnkMyBonds.Text = "My Bonds";
            this.lnkMyBonds.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkMyBonds.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMyBonds_LinkClicked);
            // 
            // datCheckDrawsAfter
            // 
            this.datCheckDrawsAfter.CustomFormat = "dd-MMM-yyyy";
            this.datCheckDrawsAfter.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datCheckDrawsAfter.Location = new System.Drawing.Point(375, 317);
            this.datCheckDrawsAfter.Name = "datCheckDrawsAfter";
            this.datCheckDrawsAfter.Size = new System.Drawing.Size(110, 20);
            this.datCheckDrawsAfter.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(273, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Check draws after:";
            // 
            // lnkApplyDate
            // 
            this.lnkApplyDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkApplyDate.Location = new System.Drawing.Point(491, 317);
            this.lnkApplyDate.Name = "lnkApplyDate";
            this.lnkApplyDate.Size = new System.Drawing.Size(47, 17);
            this.lnkApplyDate.TabIndex = 9;
            this.lnkApplyDate.TabStop = true;
            this.lnkApplyDate.Text = "Apply";
            this.lnkApplyDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkApplyDate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkApplyDate_LinkClicked);
            // 
            // lnkViewResult
            // 
            this.lnkViewResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkViewResult.Location = new System.Drawing.Point(453, 13);
            this.lnkViewResult.Name = "lnkViewResult";
            this.lnkViewResult.Size = new System.Drawing.Size(85, 17);
            this.lnkViewResult.TabIndex = 10;
            this.lnkViewResult.TabStop = true;
            this.lnkViewResult.Text = "View Result";
            this.lnkViewResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkViewResult.Visible = false;
            this.lnkViewResult.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewResult_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 350);
            this.Controls.Add(this.lnkViewResult);
            this.Controls.Add(this.lnkApplyDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.datCheckDrawsAfter);
            this.Controls.Add(this.lnkMyBonds);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.grdResults);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "National Savings Bond Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView grdResults;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.LinkLabel lnkMyBonds;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDenomination;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBondNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDrawDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDrawNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrizeAmount;
        private System.Windows.Forms.DateTimePicker datCheckDrawsAfter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnkApplyDate;
        private System.Windows.Forms.LinkLabel lnkViewResult;
    }
}

