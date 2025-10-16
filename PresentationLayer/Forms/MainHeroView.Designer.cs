namespace Superhero_Mangement_System.PresentationLayer.Forms
{
    partial class MainHeroView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvHeroes = new System.Windows.Forms.DataGridView();
            this.btnAddHero = new System.Windows.Forms.Button();
            this.btnUpdateHero = new System.Windows.Forms.Button();
            this.btnDeleteHero = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSuperpower = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numExamScore = new System.Windows.Forms.NumericUpDown();
            this.btnSummaryReport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblChange = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numAge = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHeroes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExamScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAge)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHeroes
            // 
            this.dgvHeroes.AllowUserToAddRows = false;
            this.dgvHeroes.AllowUserToDeleteRows = false;
            this.dgvHeroes.AllowUserToResizeRows = false;
            this.dgvHeroes.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvHeroes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHeroes.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHeroes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvHeroes.Location = new System.Drawing.Point(12, 12);
            this.dgvHeroes.MultiSelect = false;
            this.dgvHeroes.Name = "dgvHeroes";
            this.dgvHeroes.RowHeadersVisible = false;
            this.dgvHeroes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHeroes.Size = new System.Drawing.Size(981, 308);
            this.dgvHeroes.TabIndex = 100;
            this.dgvHeroes.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvHeroes_ColumnHeaderMouseClick);
            this.dgvHeroes.SelectionChanged += new System.EventHandler(this.dgvHeroes_SelectionChanged);
            // 
            // btnAddHero
            // 
            this.btnAddHero.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAddHero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddHero.Location = new System.Drawing.Point(6, 455);
            this.btnAddHero.Name = "btnAddHero";
            this.btnAddHero.Size = new System.Drawing.Size(175, 54);
            this.btnAddHero.TabIndex = 4;
            this.btnAddHero.Text = "Add New Hero";
            this.btnAddHero.UseVisualStyleBackColor = false;
            this.btnAddHero.Click += new System.EventHandler(this.btnAddHero_Click);
            // 
            // btnUpdateHero
            // 
            this.btnUpdateHero.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnUpdateHero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateHero.Location = new System.Drawing.Point(220, 455);
            this.btnUpdateHero.Name = "btnUpdateHero";
            this.btnUpdateHero.Size = new System.Drawing.Size(175, 54);
            this.btnUpdateHero.TabIndex = 5;
            this.btnUpdateHero.Text = "Update Hero";
            this.btnUpdateHero.UseVisualStyleBackColor = false;
            this.btnUpdateHero.Click += new System.EventHandler(this.btnUpdateHero_Click);
            // 
            // btnDeleteHero
            // 
            this.btnDeleteHero.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnDeleteHero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteHero.Location = new System.Drawing.Point(422, 455);
            this.btnDeleteHero.Name = "btnDeleteHero";
            this.btnDeleteHero.Size = new System.Drawing.Size(175, 54);
            this.btnDeleteHero.TabIndex = 6;
            this.btnDeleteHero.Text = "Delete Hero";
            this.btnDeleteHero.UseVisualStyleBackColor = false;
            this.btnDeleteHero.Click += new System.EventHandler(this.btnDeleteHero_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(821, 455);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(175, 54);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit Program";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(21, 360);
            this.txtName.MaxLength = 40;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(409, 29);
            this.txtName.TabIndex = 0;
            this.txtName.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 393);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Superpower:";
            // 
            // txtSuperpower
            // 
            this.txtSuperpower.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSuperpower.Location = new System.Drawing.Point(21, 420);
            this.txtSuperpower.MaxLength = 30;
            this.txtSuperpower.Name = "txtSuperpower";
            this.txtSuperpower.Size = new System.Drawing.Size(409, 29);
            this.txtSuperpower.TabIndex = 1;
            this.txtSuperpower.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(438, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 24);
            this.label4.TabIndex = 15;
            this.label4.Text = "Exam Score:";
            // 
            // numExamScore
            // 
            this.numExamScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numExamScore.Location = new System.Drawing.Point(451, 420);
            this.numExamScore.Name = "numExamScore";
            this.numExamScore.Size = new System.Drawing.Size(117, 29);
            this.numExamScore.TabIndex = 3;
            // 
            // btnSummaryReport
            // 
            this.btnSummaryReport.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSummaryReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSummaryReport.Location = new System.Drawing.Point(617, 455);
            this.btnSummaryReport.Name = "btnSummaryReport";
            this.btnSummaryReport.Size = new System.Drawing.Size(175, 54);
            this.btnSummaryReport.TabIndex = 101;
            this.btnSummaryReport.Text = "Summary Report";
            this.btnSummaryReport.UseVisualStyleBackColor = false;
            this.btnSummaryReport.Click += new System.EventHandler(this.btnSummaryReport_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(617, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(175, 49);
            this.btnSave.TabIndex = 102;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(821, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(175, 49);
            this.btnCancel.TabIndex = 103;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.Location = new System.Drawing.Point(614, 360);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(0, 18);
            this.lblChange.TabIndex = 104;
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(438, 333);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 24);
            this.label5.TabIndex = 13;
            this.label5.Text = "Age:";
            // 
            // numAge
            // 
            this.numAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numAge.Location = new System.Drawing.Point(451, 360);
            this.numAge.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numAge.Minimum = new decimal(new int[] {
            17,
            0,
            0,
            0});
            this.numAge.Name = "numAge";
            this.numAge.Size = new System.Drawing.Size(117, 29);
            this.numAge.TabIndex = 2;
            this.numAge.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
            // 
            // MainHeroView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1005, 521);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSummaryReport);
            this.Controls.Add(this.numExamScore);
            this.Controls.Add(this.numAge);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSuperpower);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDeleteHero);
            this.Controls.Add(this.btnUpdateHero);
            this.Controls.Add(this.btnAddHero);
            this.Controls.Add(this.dgvHeroes);
            this.Name = "MainHeroView";
            this.Text = "One Kick Heroes Academy";
            this.Load += new System.EventHandler(this.MainHeroView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHeroes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExamScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHeroes;
        private System.Windows.Forms.Button btnAddHero;
        private System.Windows.Forms.Button btnUpdateHero;
        private System.Windows.Forms.Button btnDeleteHero;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSuperpower;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numExamScore;
        private System.Windows.Forms.Button btnSummaryReport;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numAge;
    }
}