using DormitoryProject.DomainObjects;
using DormitoryProject.Presenters;
using DormitoryProject.ServicesBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DormitoryProject
{
    public partial class JournalForm : Form
    {
        
        private readonly JournalFormPresenter presenter;
        public JournalForm(TicketBLL ticket)
        {
            InitializeComponent();
            presenter = new JournalFormPresenter(this,ticket);
            prepareView();
            DGV.MultiSelect = false;
            DGV.ReadOnly = true;
            fillDateComboBoxes();
            presenter.setCurrentDate();
            списокЗаявленийToolStripMenuItem.Checked = true;
            tbDescription.MaxLength = 50;
        }

        public void loadIntoGrid(DataTable table)
        {
            DGV.DataSource = table;
            //lbValues.Text = "Значений: " + (DGV.RowCount - 1).ToString();
        }

        #region Геттеры
        public string getDate()
        {
            StringBuilder sb = new StringBuilder();
            if (cbYear.Text.Equals("----"))
            {
                sb.Append("----.");
            }
            else
            {
                sb.Append(cbYear.Text + ".");
            }
            if (cbMonth.Text.Equals("----"))
            {
                sb.Append("--.");
            }
            else
            {
                sb.Append(cbMonth.Text + ".");
            }
            if (cbDay.Text.Equals("----"))
            {
                sb.Append("--");
            }
            else
            {
                sb.Append(cbDay.Text);
            }
            return sb.ToString();
        }
        public int? getRoomNum()
        {
            int? num = null;
            if(!cbRoom.SelectedItem.Equals("----"))
            {
                StringBuilder sb = new StringBuilder();
                int i = 0;
                while (char.IsDigit(cbRoom.Text[i]))
                {
                    sb.Append(cbRoom.Text[i]);
                    if (i+1< cbRoom.Text.Length)
                    {
                        i++;
                    }
                    else
                    {
                        break;
                    }
                    
                }
                
                num = Convert.ToInt32(sb.ToString());
            }
            return num;
        }
        public string getDescription()
        {
            return tbDescription.Text;
        }
        public ComboBox getAuthorComboBox()
        {
            return cbAuthor;
        }
        public ComboBox getRoomComboBox()
        {
            return cbRoom;
        }
        public string getSelectedAuthor()
        {
            return cbAuthor.SelectedItem.ToString();
        }
        #endregion
        #region Сеттеры
        public void setYear(int year)
        {
            cbYear.Text = year.ToString();
        }
        public void setMonth(int month)
        {
            if(month<10)
            {
                cbMonth.Text = "0"+month.ToString();
            }
            else
            {
                cbMonth.Text = month.ToString();
            }
        }
        public void setDay(int day)
        {
            if(day<10)
            {
                cbDay.Text = "0"+day.ToString();
            }
            else
            {
                cbDay.Text = day.ToString();
            }
            
        }
        #endregion
        private void fillDateComboBoxes()
        {
            DateTime currentDate = DateTime.Now;
            cbYear.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDay.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAuthor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            cbYear.Items.Add("----");
            cbMonth.Items.Add("----");
            cbDay.Items.Add("----");
            cbAuthor.Items.Add("--------");
            cbRoom.Sorted = true;
            cbAuthor.Sorted = true;
            for (int i=2015;i<=currentDate.Year;i++)
            {
                cbYear.Items.Add(i);
            }


            for (int i = 1; i < 13; i++)
            {
                if (i < 10)
                {
                    cbMonth.Items.Add("0"+i);
                }
                else
                {
                    cbMonth.Items.Add(i);
                }
                
            }
            for(int i=1;i<32;i++)
            {
                if(i<10)
                {
                    cbDay.Items.Add("0"+i);
                }
                else
                {
                    cbDay.Items.Add(i);
                }
                
            }
        }

        private void prepareView()
        {
            if(LoginInfo.isWorker())
            {
                rbAdd.Enabled = false;
                rbDelete.Enabled = false;
                presenter.loadAuthorsForSearch();
                presenter.loadRoomsForSearch();
            }
        }

        private void списокЗаявленийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableAll();
            resetChecked();
            clearControls();
            списокЗаявленийToolStripMenuItem.Checked = true;
            списокВыполненныхРаботToolStripMenuItem.Checked = false;
            gbJournals.Text = "Список заявлений на ремонт";
            lbRoom.Text = "Номер комнаты";
            presenter.reloadRequestsGrid();
            cbAuthor.Width = 121;
            cbRoom.Width = 100;
            cbRoom.Items.Clear();
            cbAuthor.Items.Clear();
            if (LoginInfo.isStudent())
            {
                rbDelete.Enabled = true;
                rbAdd.Enabled = true;
                tbDescription.Enabled = true;
                cbRoom.Enabled = true;
            }
            else if (LoginInfo.isWorker())
            {
                rbDelete.Enabled = false;
                rbAdd.Enabled = false;
                tbDescription.Enabled = false;
                cbRoom.Enabled = false;
            }
        }

        private void списокВыполненныхРаботToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetChecked();
            clearControls();
            списокЗаявленийToolStripMenuItem.Checked = false;
            списокВыполненныхРаботToolStripMenuItem.Checked = true;
            gbJournals.Text = "Список отчетов о ремонте";
            presenter.reloadReportsGrid();
            lbDescription.Text = "Коментарий:";
            lbRoom.Text = "Номер заявки";
            cbAuthor.Width = 150;
            cbRoom.Width = 150;
            
            if(LoginInfo.isStudent())
            {
                rbDelete.Enabled = false;
                rbAdd.Enabled = false;
                tbDescription.Enabled = false;
                cbRoom.Enabled = false;
            }
            else if(LoginInfo.isWorker())
            {
                rbDelete.Enabled = true;
                rbAdd.Enabled = true;
                tbDescription.Enabled = true;
                cbRoom.Enabled = false;
            }
        }

        private void resetChecked()
        {
            foreach(Control c in Controls)
            {
                if(c is GroupBox)
                {
                    foreach(Control g in c.Controls)
                    {
                        if(g is RadioButton)
                        {
                            (g as RadioButton).Checked = false;
                        }
                    }
                }
                if (c is RadioButton)
                {
                    (c as RadioButton).Checked = false;
                }
            }
        }
        private void checkedChanged(object sender, EventArgs e)
        {
            clearControls();
            enableAll();
            RadioButton rb = (sender as RadioButton);
            #region KOMENDANT
            if (LoginInfo.isKomendant())
            {
                if (rb.Text.Equals("Поиск"))
                {
                    if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        presenter.loadAuthorsForSearch();
                        presenter.loadRoomsForSearch();
                    }
                    else if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        presenter.loadAuthorsForSearch();
                        cbRoom.Items.Clear();
                        cbRoom.Items.Add("----");
                        cbRoom.Text = cbRoom.Items[0].ToString();
                        cbRoom.Enabled = false;
                    }

                }
                else if (rb.Text.Equals("Добавить"))
                {
                    presenter.setCurrentDate();
                    if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        presenter.loadRoomsForAdding();
                    }
                    else if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        presenter.loadNotComplitedRequests();
                    }
                    disableDate();
                    disableApplyButton();
                    disableClearButton();
                    cbAuthor.Enabled = false;
                    tbDescription.Text = "";
                }
                else if (rb.Text.Equals("Удалить"))
                {
                    disableAll();
                }
            }
            #endregion
            #region STUDENT
            if (LoginInfo.isStudent())
            {
                if (rb.Text.Equals("Поиск"))
                {
                    if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        presenter.loadAuthorsForSearch();
                        presenter.loadRoomsForSearch();
                    }
                    else if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        presenter.loadAuthorsForSearch();
                        cbRoom.Enabled = false;
                    }

                }
                else if (rb.Text.Equals("Добавить"))
                {
                    presenter.setCurrentDate();
                    if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        presenter.loadCurrentStudentInfo();
                        disableDate();
                        disableClearButton();
                        cbAuthor.Enabled = false;
                        cbRoom.Enabled = false;
                        tbDescription.Text = "";
                    }
                }
                else if(rb.Text.Equals("Удалить"))
                {
                    if(списокЗаявленийToolStripMenuItem.Checked)
                    {
                        disableAll();
                        presenter.loadCurrentStudentInfo();
                        presenter.searchRequest();
                        btnReload.Enabled = false;
                    }
                    
                }
            }
            #endregion
            #region Worker
            if (LoginInfo.isWorker())
            {
                if (rb.Text.Equals("Поиск"))
                {
                    if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        presenter.loadAuthorsForSearch();
                        presenter.loadRoomsForSearch();
                    }
                    else if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        presenter.loadAuthorsForSearch();
                        cbRoom.Items.Clear();
                        cbRoom.Items.Add("----");
                        cbRoom.Text = cbRoom.Items[0].ToString();
                        cbRoom.Enabled = false;
                    }
                }
                else if (rb.Text.Equals("Добавить"))
                {
                    presenter.setCurrentDate();
                    if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        presenter.loadCurrentWorkerInfo();
                        presenter.loadNotComplitedRequests();
                        disableDate();
                        disableClearButton();
                        cbAuthor.Enabled = false;
                        tbDescription.Text = "";
                    }
                }
                else if(rb.Text.Equals("Удалить"))
                {
                    if(списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        disableAll();
                        cbRoom.Items.Add("----");
                        cbRoom.Text="----";
                        presenter.loadCurrentWorkerInfo();
                        presenter.searchReport();
                        btnReload.Enabled = false;
                    }
                }
            }
            #endregion

        }

        private void disableDate()
        {
            cbYear.Enabled = false;
            cbMonth.Enabled = false;
            cbDay.Enabled = false;
        }
        
        public void enableDate()
        {
            cbYear.Enabled = true;
            cbMonth.Enabled = true;
            cbDay.Enabled = true;
        }

        public void disableAll()
        {
            disableApplyButton();
            disableClearButton();
            disableDate();
            tbDescription.Enabled = false;
            cbAuthor.Enabled = false;
            cbRoom.Enabled = false;
        }
        private void enableAll()
        {
            enableApplyButton();
            enableClearButton();
            btnReload.Enabled = true;
            enableDate();
            tbDescription.Enabled = true;
            cbAuthor.Enabled = true;
            cbRoom.Enabled = true;
        }


        public void disableApplyButton()
        {
            btnApply.Enabled = false;
        }
        public void enableApplyButton()
        {
            btnApply.Enabled = true;
        }
        public void disableClearButton()
        {
            btnClear.Enabled = false;
        }
        public void enableClearButton()
        {
            btnClear.Enabled = true;
        }


        private void clearControls()
        {
            foreach (Control c in this.Controls)
            {
                if (c is GroupBox)
                {
                    foreach (Control g in c.Controls)
                    {
                        if (g is ComboBox)
                        {
                            if((g as ComboBox).Items.Contains("----") || (g as ComboBox).Items.Contains("--------"))
                            {
                                (g as ComboBox).SelectedItem = (g as ComboBox).Items[0];
                            }
                            else
                            {
                                (g as ComboBox).Items.Clear();
                            }
                        }
                        if (g is TextBox)
                        {
                            g.Text = "";
                        }
                    }
                }
                if (c is ComboBox)
                {
                    (c as ComboBox).SelectedItem = (c as ComboBox).Items[0];
                }
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearControls();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if(списокЗаявленийToolStripMenuItem.Checked)
            {
                presenter.reloadRequestsGrid();
            }
            else if(списокВыполненныхРаботToolStripMenuItem.Checked)
            {
                presenter.reloadReportsGrid();
            }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            #region KOMENDANT
            if (LoginInfo.isKomendant())
            {
                if (rbSearch.Checked)
                {
                    if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        presenter.searchReport();
                    }
                    else if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        presenter.searchRequest();
                    }
                }
                else if (rbAdd.Checked)
                {
                    if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        if (!cbRoom.Text.Equals("Все выполнено"))
                        {
                            presenter.addReportNote();
                        }
                        else
                        {
                            MessageBox.Show("На данный момент нету заявок на ремонт", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (списокЗаявленийToolStripMenuItem.Checked)
                    {

                        presenter.addRequestNote();
                    }
                    disableDate();
                    btnApply.Enabled = false;
                    cbAuthor.Enabled = false;
                    cbAuthor.Items.Clear();
                }
                else if (rbDelete.Checked)
                {
                    if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        presenter.deleteFixRequestByID(Convert.ToInt32(DGV.CurrentRow.Cells[0].Value));
                    }
                    else if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        presenter.deleteFixJournalByID(Convert.ToInt32(DGV.CurrentRow.Cells[0].Value));
                    }
                }
                else
                {
                    MessageBox.Show("Выберите операцию для выполнения", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion
            #region STUDENT
            else if (LoginInfo.isStudent())
            {
                if (rbSearch.Checked)
                {
                    if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        presenter.searchReport();
                    }
                    else if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        presenter.searchRequest();
                    }
                }
                else if (rbAdd.Checked)
                {
                    if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        presenter.addRequestNote();
                    }
                }
                else if(rbDelete.Checked)
                {
                    if(списокЗаявленийToolStripMenuItem.Checked)
                    {
                        presenter.deleteFixRequestByID(Convert.ToInt32(DGV.CurrentRow.Cells[0].Value));
                    }
                }
                else
                {
                    MessageBox.Show("Выберите операцию для выполнения", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion
            #region Worker
            else if (LoginInfo.isStudent())
            {
                if (rbSearch.Checked)
                {
                    if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        presenter.searchReport();
                    }
                    else if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        presenter.searchRequest();
                    }
                }
                else if (rbAdd.Checked)
                {
                    if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        if(!cbRoom.Text.Equals("Все выполнено"))
                        {
                            presenter.addReportNote();
                        }
                        else
                        {
                            MessageBox.Show("На данный момент нету заявок на ремонт", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else if (rbDelete.Checked)
                {
                    if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        presenter.deleteFixJournalByID(Convert.ToInt32(DGV.CurrentRow.Cells[0].Value));
                    }
                }
                else if(rbDelete.Checked)
                {
                    if(списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        presenter.deleteFixJournalByID(Convert.ToInt32(DGV.CurrentRow.Cells[0].Value));
                    }
                }
                else
                {
                    MessageBox.Show("Выберите операцию для выполнения", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion
        }

        private void cbRoom_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox box = (sender as ComboBox);
            if (rbAdd.Checked)
            {
                #region KOMENDANT
                if (LoginInfo.isKomendant())
                {
                    if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        if (!box.Text.Equals("----"))
                        {
                            presenter.loadStudentsFromRoom(Convert.ToInt32(cbRoom.Text.Substring(0, 2)));
                            cbAuthor.Enabled = true;
                        }
                        else
                        {
                            cbAuthor.Enabled = false;
                            cbAuthor.Items.Clear();
                            disableApplyButton();
                        }
                    }
                    else if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        if (!box.Text.Equals("----"))
                        {
                            presenter.loadAllWorkersForAdding();
                            cbAuthor.Enabled = true;
                        }
                        else
                        {
                            cbAuthor.Enabled = false;
                            cbAuthor.Items.Clear();
                            disableApplyButton();
                        }
                    }
                }
                #endregion
                #region STUDENT
                if (LoginInfo.isStudent())
                {
                    if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        if (!box.Text.Equals("----"))
                        {
                            presenter.loadStudentsFromRoom(Convert.ToInt32(cbRoom.Text.Substring(0, 2)));
                            cbAuthor.Enabled = true;
                        }
                        else
                        {
                            cbAuthor.Enabled = false;
                            cbAuthor.Items.Clear();
                            disableApplyButton();
                        }
                    }
                    else if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        if (!box.Text.Equals("----"))
                        {
                            presenter.loadAllWorkersForAdding();
                            cbAuthor.Enabled = true;
                        }
                        else
                        {
                            cbAuthor.Enabled = false;
                            cbAuthor.Items.Clear();
                            disableApplyButton();
                        }
                    }
                }
                #endregion
            }
            else if(rbSearch.Checked)
            {
                if (списокЗаявленийToolStripMenuItem.Checked)
                    {
                        if (!box.Text.Equals("----"))
                        {
                            presenter.loadStudentsFromRoom(Convert.ToInt32(cbRoom.Text.Substring(0, 2)));
                        }
                        else
                        {
                            cbAuthor.Items.Clear();
                            presenter.loadAuthorsForSearch();
                        }
                    }
                    else if (списокВыполненныхРаботToolStripMenuItem.Checked)
                    {
                        presenter.loadAuthorsForSearch();
                    }
            }
        }

        private void cbAuthor_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox box = (sender as ComboBox);
            if(rbAdd.Checked)
            {
                if(!box.Text.Equals(""))
                {
                    enableApplyButton();
                }
            }
        }

        private void DGV_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(rbDelete.Checked)
            {
                if(DGV.CurrentRow.Cells[0].Value.ToString().Equals(""))
                {
                    MessageBox.Show("Выбранная запись не содержит значений, выберите другую", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    disableApplyButton();
                }
                else
                {
                    enableApplyButton();
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
