using DormitoryProject.DomainObjects;
using DormitoryProject.PGServer;
using DormitoryProject.ServicesBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DormitoryProject.Presenters
{
    public class RoomFormPresenter
    {
        private readonly RoomForm form;
        private DataTable table;
        private ServiceFactory serviceFactory;
        private RoomService service;

        private string colHeaderNumber = "Номер комнаты";
        private string colHeaderState = "Состояние";
        private string colHeaderAmount = "Кол-во жильцов";
        public RoomFormPresenter(RoomForm form)
        {
            this.form = form;
            serviceFactory = new ServiceFactory(new PGRepositoryFactory());
            service = serviceFactory.getRoomService();
            sendToGrid();
        }

        private void buildRoomTable()
        {
            table = new DataTable();
            DataColumn
                roomNumCol = new DataColumn(colHeaderNumber, typeof(Int32)),
                stateCol = new DataColumn(colHeaderState, typeof(string)),
                amountCol=new DataColumn(colHeaderAmount,typeof(Int32));
            table.Columns.Add(roomNumCol);
            table.Columns.Add(stateCol);
            table.Columns.Add(amountCol);
        }
        
        public void sendToGrid()
        {
            buildRoomTable();
            fillRoomTable();
            form.loadTable(table);
        }

        private void fillRoomTable()
        {
            DataRow row;
            foreach (RoomBLL r in service.getAllRooms())
            {
                row = table.NewRow();
                row[colHeaderNumber] = r.number;
                row[colHeaderState] = r.state;
                row[colHeaderAmount] = r.amountOfStudents;
                table.Rows.Add(row);
            }
            table.AcceptChanges();
        }

        public void updateState()
        {
            DataGridViewRow row = form.getSelected();
            //валидатор проверяет
            service.updateState(new RoomBLL { number = Convert.ToInt32(row.Cells[0].Value),state=form.getState() });
            sendToGrid();
        }
    }
}
