namespace DormitoryProject
{
    partial class JournalForm
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
            this.DGV = new System.Windows.Forms.DataGridView();
            this.gbJournals = new System.Windows.Forms.GroupBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbAuthor = new System.Windows.Forms.Label();
            this.cbAuthor = new System.Windows.Forms.ComboBox();
            this.lbRoom = new System.Windows.Forms.Label();
            this.cbRoom = new System.Windows.Forms.ComboBox();
            this.lbDescription = new System.Windows.Forms.Label();
            this.rbDelete = new System.Windows.Forms.RadioButton();
            this.rbAdd = new System.Windows.Forms.RadioButton();
            this.rbSearch = new System.Windows.Forms.RadioButton();
            this.lbDay = new System.Windows.Forms.Label();
            this.lbMonth = new System.Windows.Forms.Label();
            this.lbYear = new System.Windows.Forms.Label();
            this.cbDay = new System.Windows.Forms.ComboBox();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.действияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокЗаявленийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокВыполненныхРаботToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.gbJournals.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGV
            // 
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Location = new System.Drawing.Point(31, 46);
            this.DGV.Name = "DGV";
            this.DGV.Size = new System.Drawing.Size(530, 195);
            this.DGV.TabIndex = 0;
            this.DGV.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGV_RowHeaderMouseClick);
            // 
            // gbJournals
            // 
            this.gbJournals.Controls.Add(this.btnReload);
            this.gbJournals.Controls.Add(this.btnClear);
            this.gbJournals.Controls.Add(this.lbAuthor);
            this.gbJournals.Controls.Add(this.cbAuthor);
            this.gbJournals.Controls.Add(this.lbRoom);
            this.gbJournals.Controls.Add(this.cbRoom);
            this.gbJournals.Controls.Add(this.lbDescription);
            this.gbJournals.Controls.Add(this.rbDelete);
            this.gbJournals.Controls.Add(this.rbAdd);
            this.gbJournals.Controls.Add(this.rbSearch);
            this.gbJournals.Controls.Add(this.lbDay);
            this.gbJournals.Controls.Add(this.lbMonth);
            this.gbJournals.Controls.Add(this.lbYear);
            this.gbJournals.Controls.Add(this.cbDay);
            this.gbJournals.Controls.Add(this.cbMonth);
            this.gbJournals.Controls.Add(this.cbYear);
            this.gbJournals.Controls.Add(this.btnApply);
            this.gbJournals.Controls.Add(this.tbDescription);
            this.gbJournals.Location = new System.Drawing.Point(12, 27);
            this.gbJournals.Name = "gbJournals";
            this.gbJournals.Size = new System.Drawing.Size(895, 225);
            this.gbJournals.TabIndex = 2;
            this.gbJournals.TabStop = false;
            this.gbJournals.Text = "Список заявлений на ремонт";
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(729, 171);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 41);
            this.btnReload.TabIndex = 18;
            this.btnReload.Text = "Весь список";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(648, 171);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 41);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Очистить поля";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbAuthor
            // 
            this.lbAuthor.AutoSize = true;
            this.lbAuthor.Location = new System.Drawing.Point(751, 38);
            this.lbAuthor.Name = "lbAuthor";
            this.lbAuthor.Size = new System.Drawing.Size(94, 13);
            this.lbAuthor.TabIndex = 16;
            this.lbAuthor.Text = "Автор заявления";
            // 
            // cbAuthor
            // 
            this.cbAuthor.FormattingEnabled = true;
            this.cbAuthor.Location = new System.Drawing.Point(732, 56);
            this.cbAuthor.Name = "cbAuthor";
            this.cbAuthor.Size = new System.Drawing.Size(121, 21);
            this.cbAuthor.TabIndex = 15;
            this.cbAuthor.DropDownClosed += new System.EventHandler(this.cbAuthor_DropDownClosed);
            // 
            // lbRoom
            // 
            this.lbRoom.AutoSize = true;
            this.lbRoom.Location = new System.Drawing.Point(729, 80);
            this.lbRoom.Name = "lbRoom";
            this.lbRoom.Size = new System.Drawing.Size(89, 13);
            this.lbRoom.TabIndex = 14;
            this.lbRoom.Text = "Номер комнаты";
            // 
            // cbRoom
            // 
            this.cbRoom.FormattingEnabled = true;
            this.cbRoom.Location = new System.Drawing.Point(732, 99);
            this.cbRoom.Name = "cbRoom";
            this.cbRoom.Size = new System.Drawing.Size(86, 21);
            this.cbRoom.TabIndex = 13;
            this.cbRoom.DropDownClosed += new System.EventHandler(this.cbRoom_DropDownClosed);
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(567, 80);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(69, 13);
            this.lbDescription.TabIndex = 12;
            this.lbDescription.Text = "Коментарий";
            // 
            // rbDelete
            // 
            this.rbDelete.AutoSize = true;
            this.rbDelete.Location = new System.Drawing.Point(715, 18);
            this.rbDelete.Name = "rbDelete";
            this.rbDelete.Size = new System.Drawing.Size(68, 17);
            this.rbDelete.TabIndex = 10;
            this.rbDelete.TabStop = true;
            this.rbDelete.Text = "Удалить";
            this.rbDelete.UseVisualStyleBackColor = true;
            this.rbDelete.Click += new System.EventHandler(this.checkedChanged);
            // 
            // rbAdd
            // 
            this.rbAdd.AutoSize = true;
            this.rbAdd.Location = new System.Drawing.Point(634, 18);
            this.rbAdd.Name = "rbAdd";
            this.rbAdd.Size = new System.Drawing.Size(75, 17);
            this.rbAdd.TabIndex = 9;
            this.rbAdd.TabStop = true;
            this.rbAdd.Text = "Добавить";
            this.rbAdd.UseVisualStyleBackColor = true;
            this.rbAdd.Click += new System.EventHandler(this.checkedChanged);
            // 
            // rbSearch
            // 
            this.rbSearch.AutoSize = true;
            this.rbSearch.Location = new System.Drawing.Point(567, 18);
            this.rbSearch.Name = "rbSearch";
            this.rbSearch.Size = new System.Drawing.Size(57, 17);
            this.rbSearch.TabIndex = 8;
            this.rbSearch.TabStop = true;
            this.rbSearch.Text = "Поиск";
            this.rbSearch.UseVisualStyleBackColor = true;
            this.rbSearch.Click += new System.EventHandler(this.checkedChanged);
            // 
            // lbDay
            // 
            this.lbDay.AutoSize = true;
            this.lbDay.Location = new System.Drawing.Point(689, 38);
            this.lbDay.Name = "lbDay";
            this.lbDay.Size = new System.Drawing.Size(34, 13);
            this.lbDay.TabIndex = 7;
            this.lbDay.Text = "День";
            // 
            // lbMonth
            // 
            this.lbMonth.AutoSize = true;
            this.lbMonth.Location = new System.Drawing.Point(631, 38);
            this.lbMonth.Name = "lbMonth";
            this.lbMonth.Size = new System.Drawing.Size(40, 13);
            this.lbMonth.TabIndex = 6;
            this.lbMonth.Text = "Месяц";
            // 
            // lbYear
            // 
            this.lbYear.AutoSize = true;
            this.lbYear.Location = new System.Drawing.Point(584, 38);
            this.lbYear.Name = "lbYear";
            this.lbYear.Size = new System.Drawing.Size(25, 13);
            this.lbYear.TabIndex = 5;
            this.lbYear.Text = "Год";
            // 
            // cbDay
            // 
            this.cbDay.FormattingEnabled = true;
            this.cbDay.Location = new System.Drawing.Point(680, 56);
            this.cbDay.Name = "cbDay";
            this.cbDay.Size = new System.Drawing.Size(43, 21);
            this.cbDay.TabIndex = 4;
            // 
            // cbMonth
            // 
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Location = new System.Drawing.Point(634, 56);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(37, 21);
            this.cbMonth.TabIndex = 3;
            // 
            // cbYear
            // 
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(570, 56);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(54, 21);
            this.cbYear.TabIndex = 2;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(567, 171);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 41);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Выполнить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(566, 99);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(157, 66);
            this.tbDescription.TabIndex = 11;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.действияToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(919, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // действияToolStripMenuItem
            // 
            this.действияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокЗаявленийToolStripMenuItem,
            this.списокВыполненныхРаботToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.действияToolStripMenuItem.Name = "действияToolStripMenuItem";
            this.действияToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.действияToolStripMenuItem.Text = "Действия";
            // 
            // списокЗаявленийToolStripMenuItem
            // 
            this.списокЗаявленийToolStripMenuItem.Name = "списокЗаявленийToolStripMenuItem";
            this.списокЗаявленийToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.списокЗаявленийToolStripMenuItem.Text = "Список заявлений";
            this.списокЗаявленийToolStripMenuItem.Click += new System.EventHandler(this.списокЗаявленийToolStripMenuItem_Click);
            // 
            // списокВыполненныхРаботToolStripMenuItem
            // 
            this.списокВыполненныхРаботToolStripMenuItem.Name = "списокВыполненныхРаботToolStripMenuItem";
            this.списокВыполненныхРаботToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.списокВыполненныхРаботToolStripMenuItem.Text = "Список отчетов";
            this.списокВыполненныхРаботToolStripMenuItem.Click += new System.EventHandler(this.списокВыполненныхРаботToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // JournalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 258);
            this.Controls.Add(this.DGV);
            this.Controls.Add(this.gbJournals);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "JournalForm";
            this.Text = "JournalsForm";
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.gbJournals.ResumeLayout(false);
            this.gbJournals.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.GroupBox gbJournals;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lbDay;
        private System.Windows.Forms.Label lbMonth;
        private System.Windows.Forms.Label lbYear;
        private System.Windows.Forms.ComboBox cbDay;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.RadioButton rbDelete;
        private System.Windows.Forms.RadioButton rbAdd;
        private System.Windows.Forms.RadioButton rbSearch;
        private System.Windows.Forms.Label lbRoom;
        private System.Windows.Forms.ComboBox cbRoom;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem действияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокЗаявленийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокВыполненныхРаботToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.Label lbAuthor;
        private System.Windows.Forms.ComboBox cbAuthor;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnClear;
    }
}