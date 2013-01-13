﻿namespace pwiz.Skyline.EditUI
{
    partial class ChromChartPropertyDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChromChartPropertyDlg));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textLineWidth = new System.Windows.Forms.TextBox();
            this.textFontSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textTimeRange = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTimeUnits = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbRelative = new System.Windows.Forms.CheckBox();
            this.textMaxIntensity = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textLineWidth
            // 
            resources.ApplyResources(this.textLineWidth, "textLineWidth");
            this.textLineWidth.Name = "textLineWidth";
            // 
            // textFontSize
            // 
            resources.ApplyResources(this.textFontSize, "textFontSize");
            this.textFontSize.Name = "textFontSize";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textTimeRange
            // 
            resources.ApplyResources(this.textTimeRange, "textTimeRange");
            this.textTimeRange.Name = "textTimeRange";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelTimeUnits
            // 
            resources.ApplyResources(this.labelTimeUnits, "labelTimeUnits");
            this.labelTimeUnits.Name = "labelTimeUnits";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.cbRelative);
            this.groupBox1.Controls.Add(this.textMaxIntensity);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textTimeRange);
            this.groupBox1.Controls.Add(this.labelTimeUnits);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // cbRelative
            // 
            resources.ApplyResources(this.cbRelative, "cbRelative");
            this.cbRelative.Name = "cbRelative";
            this.cbRelative.UseVisualStyleBackColor = true;
            this.cbRelative.CheckedChanged += new System.EventHandler(this.cbRelative_CheckedChanged);
            // 
            // textMaxIntensity
            // 
            resources.ApplyResources(this.textMaxIntensity, "textMaxIntensity");
            this.textMaxIntensity.Name = "textMaxIntensity";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // ChromChartPropertyDlg
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textFontSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textLineWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChromChartPropertyDlg";
            this.ShowInTaskbar = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textLineWidth;
        private System.Windows.Forms.TextBox textFontSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textTimeRange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTimeUnits;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textMaxIntensity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbRelative;
    }
}