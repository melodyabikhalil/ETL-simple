﻿namespace ETL.UI
{
    partial class MapForm
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
            this.mapDataGridView = new System.Windows.Forms.DataGridView();
            this.finishButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mapDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mapDataGridView
            // 
            this.mapDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mapDataGridView.Location = new System.Drawing.Point(21, 44);
            this.mapDataGridView.Name = "mapDataGridView";
            this.mapDataGridView.Size = new System.Drawing.Size(477, 157);
            this.mapDataGridView.TabIndex = 0;
            // 
            // finishButton
            // 
            this.finishButton.Location = new System.Drawing.Point(695, 403);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(75, 23);
            this.finishButton.TabIndex = 1;
            this.finishButton.Text = "Finish";
            this.finishButton.UseVisualStyleBackColor = true;
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.finishButton);
            this.Controls.Add(this.mapDataGridView);
            this.Name = "MapForm";
            this.Text = "MapForm";
            ((System.ComponentModel.ISupportInitialize)(this.mapDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView mapDataGridView;
        private System.Windows.Forms.Button finishButton;
    }
}