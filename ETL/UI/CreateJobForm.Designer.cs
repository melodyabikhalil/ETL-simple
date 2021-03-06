﻿namespace ETL.UI
{
    partial class CreateJobForm
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
            this.nameGroupbox = new System.Windows.Forms.GroupBox();
            this.ETLJobNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.etlsLabel = new System.Windows.Forms.Label();
            this.etlJobDataGridView = new System.Windows.Forms.DataGridView();
            this.datagridviewGroupBox = new System.Windows.Forms.GroupBox();
            this.nameGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etlJobDataGridView)).BeginInit();
            this.datagridviewGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameGroupbox
            // 
            this.nameGroupbox.Controls.Add(this.ETLJobNameTextBox);
            this.nameGroupbox.Controls.Add(this.label1);
            this.nameGroupbox.Location = new System.Drawing.Point(15, 17);
            this.nameGroupbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nameGroupbox.Name = "nameGroupbox";
            this.nameGroupbox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nameGroupbox.Size = new System.Drawing.Size(1228, 118);
            this.nameGroupbox.TabIndex = 0;
            this.nameGroupbox.TabStop = false;
            this.nameGroupbox.Text = "Name";
            // 
            // ETLJobNameTextBox
            // 
            this.ETLJobNameTextBox.Location = new System.Drawing.Point(417, 55);
            this.ETLJobNameTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ETLJobNameTextBox.Name = "ETLJobNameTextBox";
            this.ETLJobNameTextBox.Size = new System.Drawing.Size(419, 22);
            this.ETLJobNameTextBox.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Choose a name for the ETL Job";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1143, 664);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 28);
            this.saveButton.TabIndex = 19;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // etlsLabel
            // 
            this.etlsLabel.AutoSize = true;
            this.etlsLabel.Location = new System.Drawing.Point(44, 57);
            this.etlsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.etlsLabel.Name = "etlsLabel";
            this.etlsLabel.Size = new System.Drawing.Size(344, 51);
            this.etlsLabel.TabIndex = 18;
            this.etlsLabel.Text = "Choose the ETLs you which to add in this Job.\r\n\r\nThe ETLs will run in the order t" +
    "hat you select them in.";
            // 
            // etlJobDataGridView
            // 
            this.etlJobDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.etlJobDataGridView.Location = new System.Drawing.Point(417, 90);
            this.etlJobDataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.etlJobDataGridView.Name = "etlJobDataGridView";
            this.etlJobDataGridView.RowHeadersWidth = 51;
            this.etlJobDataGridView.Size = new System.Drawing.Size(420, 356);
            this.etlJobDataGridView.TabIndex = 17;
            this.etlJobDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.EtlJobDataGridView_RowsAdded);
            // 
            // datagridviewGroupBox
            // 
            this.datagridviewGroupBox.Controls.Add(this.etlJobDataGridView);
            this.datagridviewGroupBox.Controls.Add(this.etlsLabel);
            this.datagridviewGroupBox.Location = new System.Drawing.Point(15, 165);
            this.datagridviewGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.datagridviewGroupBox.Name = "datagridviewGroupBox";
            this.datagridviewGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.datagridviewGroupBox.Size = new System.Drawing.Size(1228, 475);
            this.datagridviewGroupBox.TabIndex = 21;
            this.datagridviewGroupBox.TabStop = false;
            this.datagridviewGroupBox.Text = "ETLs order";
            // 
            // CreateJobForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 727);
            this.Controls.Add(this.datagridviewGroupBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.nameGroupbox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CreateJobForm";
            this.Text = "Create ETL job";
            this.nameGroupbox.ResumeLayout(false);
            this.nameGroupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etlJobDataGridView)).EndInit();
            this.datagridviewGroupBox.ResumeLayout(false);
            this.datagridviewGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox nameGroupbox;
        private System.Windows.Forms.TextBox ETLJobNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label etlsLabel;
        private System.Windows.Forms.DataGridView etlJobDataGridView;
        private System.Windows.Forms.GroupBox datagridviewGroupBox;
    }
}