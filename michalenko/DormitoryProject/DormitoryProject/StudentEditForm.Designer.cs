namespace DormitoryProject
{
    partial class StudentEditForm
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
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbPatr = new System.Windows.Forms.TextBox();
            this.tbKurs = new System.Windows.Forms.TextBox();
            this.tbFacult = new System.Windows.Forms.TextBox();
            this.lbLastName = new System.Windows.Forms.Label();
            this.tbSpec = new System.Windows.Forms.TextBox();
            this.tbGroup = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.lbPatr = new System.Windows.Forms.Label();
            this.lbKurs = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbSpec = new System.Windows.Forms.Label();
            this.lbGroup = new System.Windows.Forms.Label();
            this.tbSerial = new System.Windows.Forms.TextBox();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.lbSerial = new System.Windows.Forms.Label();
            this.lbNumber = new System.Windows.Forms.Label();
            this.gbStudTicket = new System.Windows.Forms.GroupBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnPwd = new System.Windows.Forms.Button();
            this.gbStudTicket.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLastName
            // 
            this.tbLastName.Location = new System.Drawing.Point(62, 58);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(110, 20);
            this.tbLastName.TabIndex = 0;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(62, 85);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(110, 20);
            this.tbName.TabIndex = 1;
            // 
            // tbPatr
            // 
            this.tbPatr.Location = new System.Drawing.Point(62, 112);
            this.tbPatr.Name = "tbPatr";
            this.tbPatr.Size = new System.Drawing.Size(110, 20);
            this.tbPatr.TabIndex = 2;
            // 
            // tbKurs
            // 
            this.tbKurs.Location = new System.Drawing.Point(245, 112);
            this.tbKurs.Name = "tbKurs";
            this.tbKurs.Size = new System.Drawing.Size(100, 20);
            this.tbKurs.TabIndex = 3;
            this.tbKurs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.digitEnterControl);
            // 
            // tbFacult
            // 
            this.tbFacult.Location = new System.Drawing.Point(245, 58);
            this.tbFacult.Name = "tbFacult";
            this.tbFacult.Size = new System.Drawing.Size(100, 20);
            this.tbFacult.TabIndex = 4;
            // 
            // lbLastName
            // 
            this.lbLastName.AutoSize = true;
            this.lbLastName.Location = new System.Drawing.Point(0, 61);
            this.lbLastName.Name = "lbLastName";
            this.lbLastName.Size = new System.Drawing.Size(56, 13);
            this.lbLastName.TabIndex = 5;
            this.lbLastName.Text = "Фамилия";
            // 
            // tbSpec
            // 
            this.tbSpec.Location = new System.Drawing.Point(160, 148);
            this.tbSpec.Name = "tbSpec";
            this.tbSpec.Size = new System.Drawing.Size(100, 20);
            this.tbSpec.TabIndex = 6;
            // 
            // tbGroup
            // 
            this.tbGroup.Location = new System.Drawing.Point(245, 84);
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.Size = new System.Drawing.Size(100, 20);
            this.tbGroup.TabIndex = 7;
            this.tbGroup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.digitEnterControl);
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(27, 88);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(29, 13);
            this.lbName.TabIndex = 8;
            this.lbName.Text = "Имя";
            // 
            // lbPatr
            // 
            this.lbPatr.AutoSize = true;
            this.lbPatr.Location = new System.Drawing.Point(2, 115);
            this.lbPatr.Name = "lbPatr";
            this.lbPatr.Size = new System.Drawing.Size(54, 13);
            this.lbPatr.TabIndex = 9;
            this.lbPatr.Text = "Отчество";
            // 
            // lbKurs
            // 
            this.lbKurs.AutoSize = true;
            this.lbKurs.Location = new System.Drawing.Point(208, 115);
            this.lbKurs.Name = "lbKurs";
            this.lbKurs.Size = new System.Drawing.Size(31, 13);
            this.lbKurs.TabIndex = 10;
            this.lbKurs.Text = "Курс";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Факультет";
            // 
            // lbSpec
            // 
            this.lbSpec.AutoSize = true;
            this.lbSpec.Location = new System.Drawing.Point(69, 151);
            this.lbSpec.Name = "lbSpec";
            this.lbSpec.Size = new System.Drawing.Size(85, 13);
            this.lbSpec.TabIndex = 12;
            this.lbSpec.Text = "Специальность";
            // 
            // lbGroup
            // 
            this.lbGroup.AutoSize = true;
            this.lbGroup.Location = new System.Drawing.Point(197, 87);
            this.lbGroup.Name = "lbGroup";
            this.lbGroup.Size = new System.Drawing.Size(42, 13);
            this.lbGroup.TabIndex = 13;
            this.lbGroup.Text = "Группа";
            // 
            // tbSerial
            // 
            this.tbSerial.Enabled = false;
            this.tbSerial.Location = new System.Drawing.Point(60, 19);
            this.tbSerial.Name = "tbSerial";
            this.tbSerial.Size = new System.Drawing.Size(100, 20);
            this.tbSerial.TabIndex = 14;
            // 
            // tbNumber
            // 
            this.tbNumber.Enabled = false;
            this.tbNumber.Location = new System.Drawing.Point(233, 19);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(100, 20);
            this.tbNumber.TabIndex = 15;
            // 
            // lbSerial
            // 
            this.lbSerial.AutoSize = true;
            this.lbSerial.Location = new System.Drawing.Point(16, 22);
            this.lbSerial.Name = "lbSerial";
            this.lbSerial.Size = new System.Drawing.Size(38, 13);
            this.lbSerial.TabIndex = 16;
            this.lbSerial.Text = "Серия";
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.Location = new System.Drawing.Point(185, 22);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(41, 13);
            this.lbNumber.TabIndex = 17;
            this.lbNumber.Text = "Номер";
            // 
            // gbStudTicket
            // 
            this.gbStudTicket.Controls.Add(this.tbNumber);
            this.gbStudTicket.Controls.Add(this.lbNumber);
            this.gbStudTicket.Controls.Add(this.tbSerial);
            this.gbStudTicket.Controls.Add(this.lbSerial);
            this.gbStudTicket.Location = new System.Drawing.Point(12, 9);
            this.gbStudTicket.Name = "gbStudTicket";
            this.gbStudTicket.Size = new System.Drawing.Size(352, 45);
            this.gbStudTicket.TabIndex = 18;
            this.gbStudTicket.TabStop = false;
            this.gbStudTicket.Text = "Студенческий билет";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(15, 174);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(81, 39);
            this.btnConfirm.TabIndex = 19;
            this.btnConfirm.Text = "Подтвердить";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(103, 174);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 39);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(185, 175);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 38);
            this.btnReset.TabIndex = 21;
            this.btnReset.Text = "Начальные данные";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnPwd
            // 
            this.btnPwd.Location = new System.Drawing.Point(267, 175);
            this.btnPwd.Name = "btnPwd";
            this.btnPwd.Size = new System.Drawing.Size(75, 38);
            this.btnPwd.TabIndex = 22;
            this.btnPwd.Text = "Сброс пароля";
            this.btnPwd.UseVisualStyleBackColor = true;
            this.btnPwd.Click += new System.EventHandler(this.btnPwd_Click);
            // 
            // StudentEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 225);
            this.Controls.Add(this.btnPwd);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lbGroup);
            this.Controls.Add(this.lbSpec);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbKurs);
            this.Controls.Add(this.lbPatr);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.tbGroup);
            this.Controls.Add(this.tbSpec);
            this.Controls.Add(this.lbLastName);
            this.Controls.Add(this.tbFacult);
            this.Controls.Add(this.tbKurs);
            this.Controls.Add(this.tbPatr);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.tbLastName);
            this.Controls.Add(this.gbStudTicket);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "StudentEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StudentEditForm";
            this.gbStudTicket.ResumeLayout(false);
            this.gbStudTicket.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbPatr;
        private System.Windows.Forms.TextBox tbKurs;
        private System.Windows.Forms.TextBox tbFacult;
        private System.Windows.Forms.Label lbLastName;
        private System.Windows.Forms.TextBox tbSpec;
        private System.Windows.Forms.TextBox tbGroup;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbPatr;
        private System.Windows.Forms.Label lbKurs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbSpec;
        private System.Windows.Forms.Label lbGroup;
        private System.Windows.Forms.TextBox tbSerial;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label lbSerial;
        private System.Windows.Forms.Label lbNumber;
        private System.Windows.Forms.GroupBox gbStudTicket;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnPwd;
    }
}