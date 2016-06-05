﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DormitoryProject.Presenters;
using DormitoryProject.DataAccessClasses;
using DormitoryProject.DomainObjects;
using DormitoryProject.ServicesBLL;
using DormitoryProject.PGServer;

namespace DormitoryProject
{
    public partial class UserForm : Form
    {
        UsersFormPresenter presenter;
        public List<TextBox> tbList = new List<TextBox>();
        public UserForm()
        {
            InitializeComponent();
            DGV.ReadOnly = true;
            presenter = new UsersFormPresenter(this);
            btnUpdate.Enabled = false;
            DGV.MultiSelect = false;
            cbRoom.Sorted = true;
            cbRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            hideWorkerControls();
            списокСтудентовToolStripMenuItem.Checked = true;
            showStudentControls();
            addStudentTextBoxes();
            initDaysCombobox();
            prepareTextBoxes();
        }
        
        private void prepareTextBoxes()
        {
            tbLastName.MaxLength = 20;
            tbName.MaxLength = 20;
            tbPatr.MaxLength = 20;
            tbSerial.MaxLength = 2;
            tbNumber.MaxLength = 6;
            prepareCommonTextBoxes();
            tbGroup.MaxLength = 1;
            tbSpec.MaxLength = 10;
            tbSerial.CharacterCasing = CharacterCasing.Upper;
        }

        private void prepareCommonTextBoxes()
        {
            if(списокРаботниковToolStripMenuItem.Checked)
            {
                tbKurs.MaxLength = 9;
                tbFacult.MaxLength = 16;
            }
            else if(списокСтудентовToolStripMenuItem.Checked)
            {
                tbKurs.MaxLength = 1;
                tbFacult.MaxLength = 15;
            }
        }
        public void hideWorkerControls()
        {
            listBoxWD.Visible = false;
            cbDays.Visible = false;
        }
        public void showWorkerControls()
        {
            listBoxWD.Visible = true;
        }

        private void unCheckRadioButton()
        {
            foreach(Control c in Controls)
            {
                if(c is GroupBox)
                {
                    foreach(Control g in c.Controls)
                    {
                        if (g is RadioButton)
                        {
                            (g as RadioButton).Checked = false;
                        }
                    }
                }
                if(c is RadioButton)
                {
                    (c as RadioButton).Checked = false;
                }
            }
        }
        private void enableTextBoxes()
        {
            foreach (TextBox t in tbList)
            {
                t.Enabled = true;
            }
            cbRoom.Enabled = true;
        }

        private void disableTextBoxes()
        {
            foreach(TextBox t in tbList)
            {
                t.Enabled = false;
            }
            cbRoom.Enabled = false;
        }

        public void hideStudentControls()
        {
            tbGroup.Visible = false;
            rbResettle.Visible = false;
        }
        public void showStudentControls()
        {
            tbGroup.Visible = true;
            rbResettle.Visible = true;
        }

        public ComboBox getRoomsComboBox()
        {
            return cbRoom;
        }
        public string getDayValue()
        {
            return cbDays.Text;
        }
        public void initDaysCombobox()
        {
            cbDays.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDays.Items.Add("не указано");
            cbDays.Items.Add("Понедельник");
            cbDays.Items.Add("Вторник");
            cbDays.Items.Add("Среда");
            cbDays.Items.Add("Четверг");
            cbDays.Items.Add("Пятница");
            cbDays.Items.Add("Суббота");
            cbDays.Items.Add("Воскресенье");
            cbDays.Text = cbDays.Items[0].ToString();
        }


        public void loadTable(DataTable table)
        {   
            DGV.DataSource = table;
            lbValues.Text= "Значений: "+(DGV.RowCount - 1).ToString();
        }

        private void addStudentTextBoxes()
        {
            tbList = new List<TextBox>();
            tbList.Add(tbRoom);
            tbList.Add(tbLastName);
            tbList.Add(tbName);
            tbList.Add(tbPatr);
            tbList.Add(tbKurs);
            tbList.Add(tbFacult);
            tbList.Add(tbGroup);
            tbList.Add(tbSpec);
            tbList.Add(tbSerial);
            tbList.Add(tbNumber);
        }
        private void addWorkerTextBoxes()
        {
            tbList = new List<TextBox>();
            tbList.Add(tbLastName);
            tbList.Add(tbName);
            tbList.Add(tbPatr);
            tbList.Add(tbKurs);
            tbList.Add(tbFacult);
            tbList.Add(tbSerial);
            tbList.Add(tbNumber);
        }

        private void списокСтудентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            списокСтудентовToolStripMenuItem.Checked = true;
            списокРаботниковToolStripMenuItem.Checked = false;
            hideWorkerControls();
            showStudentControls();
            unCheckRadioButton();
            enableTextBoxes();
            prepareCommonTextBoxes();
            clear();
            #region Пару перемещений
            lbKurs.Text = "Курс";
            lbKurs.Location = new Point(222, 53); ;
            lbFacult.Text = "Факультет";
            lbFacult.Location = new Point(190, 83);
            rbResettle.Visible = true;
            tbGroup.Visible = true;
            lbGroup.Text = "Группа";
            lbGroup.Location = new Point(211, 111);
            #endregion
            addStudentTextBoxes();
            presenter.getServices();
            presenter.getListOfStudents();
            presenter.reloadStudGrid();
        }

        private void списокРаботниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            списокРаботниковToolStripMenuItem.Checked = true;
            списокСтудентовToolStripMenuItem.Checked = false;
            hideStudentControls();
            showWorkerControls();
            unCheckRadioButton();
            prepareCommonTextBoxes();
            enableTextBoxes();
            clear();
            #region Пару перемещений
            lbKurs.Text = "Специализация";
            lbKurs.Location = new Point(173, 53);
            lbFacult.Text = "Телефон";
            lbFacult.Location = new Point(205, 83);
            lbGroup.Text = "Рабочие дни:";
            lbGroup.Location = new Point(173, 111);
            #endregion 
            addWorkerTextBoxes();
            presenter.getServices();
            presenter.getListOfWorkers();
            presenter.buildWorkTable();
            presenter.reloadWorkerGrid();
        }

        public List<TextBox> getTextBoxValues()
        {
            return tbList;
        }
        
        private void btnApply_Click(object sender, EventArgs e)
        {
            if(rbSearch.Checked)
            {
                if(списокСтудентовToolStripMenuItem.Checked)
                {
                    presenter.searchStudent();
                }
                else if(списокРаботниковToolStripMenuItem.Checked)
                {
                    presenter.searchWorker();
                }
            }
            else if(rbAdd.Checked)
            {
                if (списокСтудентовToolStripMenuItem.Checked)
                {
                    presenter.addStudent();
                }
                else if (списокРаботниковToolStripMenuItem.Checked)
                {
                    presenter.addWorker();
                }
            }
            else if(rbDelete.Checked)
            {
                if (списокСтудентовToolStripMenuItem.Checked)
                {
                    presenter.deleteStudent();
                }
                else if (списокРаботниковToolStripMenuItem.Checked)
                {
                    presenter.deleteWorker();
                }
            }
            else if(rbResettle.Checked)
            {
                if (списокСтудентовToolStripMenuItem.Checked)
                {
                    presenter.resettleStudent();
                    presenter.loadAvailableRooms();
                    presenter.reloadStudGrid();
                } 
            }
            else
            {
                MessageBox.Show("Выберите операцию", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void clear()
        {
            btnUpdate.Enabled = false;
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(GroupBox))
                {
                    foreach (Control d in c.Controls)
                    {
                        if (d.GetType() == typeof(TextBox))
                        {
                            d.Text = string.Empty;
                        }
                        else if (d.GetType() == typeof(ListBox))
                        {
                            (d as ListBox).Items.Clear();
                        }
                    }
                }
                if (c.GetType() == typeof(TextBox))
                {
                    c.Text = string.Empty;
                }
                else if (c.GetType() == typeof(ListBox))
                {
                    (c as ListBox).Items.Clear();
                }
                cbDays.Text = cbDays.Items[0].ToString();
            }
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            cbDays.Visible = false;
            clear();
            btnUpdate.Enabled = false;
            RadioButton rb = (sender as RadioButton);
            gbMenu.Text = rb.Text;
            if(rb.Text=="Поиск")
            {
                if (списокСтудентовToolStripMenuItem.Checked)
                {
                    presenter.loadRoomsForSearch();
                }
            }
            if(rb.Text=="Удалить")
            {
                disableTextBoxes();
            }
            else if(rb.Text=="Переселить")
            {
                enableTextBoxes();
                for(int i=1;i<tbList.Count;i++)
                {
                    tbList[i].Enabled = false;
                }
                if (списокСтудентовToolStripMenuItem.Checked)
                {
                    presenter.loadAvailableRooms();
                }
                
            }
            else if (rb.Text.Equals("Добавить"))
            {
                enableTextBoxes();
                if (списокСтудентовToolStripMenuItem.Checked)
                {
                    presenter.loadAvailableRooms();
                }
            }
            else
            {
                enableTextBoxes();
                if (списокРаботниковToolStripMenuItem.Checked && rbSearch.Checked)
                {
                    cbDays.Visible = true;
                }
            }
            
        }

        private void DGV_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(DGV.CurrentRow.Cells[0].Value.ToString().Equals(""))
            {
                btnUpdate.Enabled = false;
                MessageBox.Show("Данная запись пуста, выберите другую");
            }
            else
            {
                btnUpdate.Enabled = true;
                for (int i = 0; i < DGV.ColumnCount; i++)
                {
                    tbList[i].Text = DGV.CurrentRow.Cells[i].Value.ToString();
                }
                if (списокСтудентовToolStripMenuItem.Checked)
                {
                    if (rbSearch.Checked || rbDelete.Checked)
                    {
                        presenter.loadRoomsForSearch();
                    }
                    else if (rbAdd.Checked || rbResettle.Checked)
                    {
                        presenter.loadAvailableRooms();
                    }
                    cbRoom.Text = tbRoom.Text;
                }

                if (списокРаботниковToolStripMenuItem.Checked)
                {
                    listBoxWD.Items.Clear();
                    WorkerTicketBLL worker = presenter.findWorkerInUserListBySerial(tbSerial.Text, tbNumber.Text);
                    setWorkDaysText(worker.workDays);
                }
            }
        }

        public void setWorkDaysText(List<WorkDayDAL> workdays)
        {
            if(workdays!=null && workdays.Count!=0)
            {
                foreach (WorkDayDAL wd in workdays)
                {
                    if (wd.restStart != null)
                    {
                        listBoxWD.Items.Add(wd.day + ":");
                        listBoxWD.Items.Add("\tв/р (" + wd.startTime + "-" + wd.endTime + ")");
                        listBoxWD.Items.Add("\tобед (" + wd.restStart + "-" + wd.restEnd + ")");
                    }
                    else
                    {
                        listBoxWD.Items.Add(wd.day + ":");
                        listBoxWD.Items.Add("\tв/р (" + wd.startTime + "-" + wd.endTime + ")");
                        listBoxWD.Items.Add("\tбез обеда");
                    }
                }
            }
            else if(workdays==null || workdays.Count==0)
            {
                listBoxWD.Items.Add("Рабочие дни не указаны");
                listBoxWD.Items.Add("это можно сделать в ");
                listBoxWD.Items.Add("меню изменения данных");
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (списокСтудентовToolStripMenuItem.Checked)
            {
                presenter.reloadStudGrid();
            }
            else if (списокРаботниковToolStripMenuItem.Checked)
            {
                presenter.reloadWorkerGrid();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(списокСтудентовToolStripMenuItem.Checked)
            {
                presenter.openUpdateStudentForm();
            }
            else if(списокРаботниковToolStripMenuItem.Checked)
            {
                presenter.openUpdateWorkerForm();
            }
        }

        private void cbRoom_DropDownClosed(object sender, EventArgs e)
        {
            if (!cbRoom.Text.Equals("--"))
            {
                tbRoom.Text = cbRoom.Text;
            }
            else
            {
                tbRoom.Text = "";
            }
        }

        private void digitEnterControl(TextBox tb,KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar==8) return;
            else
                e.Handled = true;
        }

        private void tbKurs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(списокСтудентовToolStripMenuItem.Checked)
            {
                digitEnterControl(sender as TextBox, e);
            }
        }

        private void tbGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            digitEnterControl(sender as TextBox, e);
        }

        private void tbNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            digitEnterControl(sender as TextBox, e);
        }
    }
}
