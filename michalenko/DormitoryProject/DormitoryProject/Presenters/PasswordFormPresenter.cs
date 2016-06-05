using DormitoryProject.DomainObjects;
using DormitoryProject.PGServer;
using DormitoryProject.ServicesBLL;
using DormitoryProject.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DormitoryProject.Presenters
{
    public class PasswordFormPresenter
    {
        private readonly PasswordForm form;
        private TicketBLL user;
        private ServiceFactory serviceFactory;
        private UserService service;
        private FormValidator validator; 
        public PasswordFormPresenter(PasswordForm form,TicketBLL ticket)
        {
            this.form = form;
            user = ticket;
            validator = new FormValidator();
            serviceFactory = new ServiceFactory(new PGRepositoryFactory());
            service = serviceFactory.getUserService();
        }

        public void changePassword()
        {
            validator.validateOldPassword(form.getOldPwdTextBox().Text);   
            validator.validateNewPassword(form.getNewPwdTextBox().Text);
            if(validator.isValid())
            {
                service.ChangePassword(null, user.serial, user.number, form.getOldPwdTextBox().Text, form.getNewPwdTextBox().Text);
                form.Close();
            }
            else
            {
                MessageBox.Show(validator.getErrorString(), "Ошибки ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            validator.resetValues();
        }


    }
}
