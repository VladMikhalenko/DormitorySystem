using DormitoryProject.DataAccessClasses;
using DormitoryProject.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DormitoryProject.Validation
{
    public class FormValidator
    {
        List<string> brokenRules = new List<string>();
        bool validFlag = true;

        public object Regexstudent { get; private set; }
        #region own methods
        public void resetValues()
        {
            brokenRules = new List<string>();
            validFlag = true;
        }
        public bool isValid()
        {
            return validFlag;
        }
        #endregion

        #region Adding validation
        public void validateStudentToAdd(StudentTicketBLL student)
        {
            resetValues();
            #region Проверка полей на null
            if (string.IsNullOrWhiteSpace(student.lastName))
            {
                brokenRules.Add("Поле фамилии не может быть пустым");
                validFlag = false;
            }
            if (string.IsNullOrWhiteSpace(student.name))
            {
                brokenRules.Add("Поле имени не может быть пустым");
                validFlag = false;
            }
            if (string.IsNullOrWhiteSpace(student.serial))
            {
                brokenRules.Add("Поле серии студ билета не может быть пустым");
                validFlag = false;
            }
            if (string.IsNullOrWhiteSpace(student.number))
            {
                brokenRules.Add("Поле номера студ билета не может быть пустым");
                validFlag = false;
            }
            if (string.IsNullOrWhiteSpace(student.speciality))
            {
                brokenRules.Add("Поле специальности не может быть пустым");
                validFlag = false;
            }
            if (string.IsNullOrWhiteSpace(student.facult))
            {
                brokenRules.Add("Поле факультета не может быть пустым");
                validFlag = false;
            }
            if (!student.roomNumber.HasValue)
            {
                brokenRules.Add("Поле номера комнаты не может быть пустым");
                validFlag = false;
            }
            if (!student.kurs.HasValue)
            {
                brokenRules.Add("Поле номера курса не может быть пустым");
                validFlag = false;
            }
            if (!student.group.HasValue)
            {
                brokenRules.Add("Поле номера группы не может быть пустым");
                validFlag = false;
            }
            #endregion
            #region Проверка на наличие запрещенных символов
            if (student.lastName.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле фамилии содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (student.name.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле имени содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (student.patronimic.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле отчества содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (student.serial.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле серии студ билета содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (student.speciality.Any(s => (char.IsDigit(s) || (s.Equals(',') || s.Equals('-')) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле специальности содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }

            if (student.facult.Any(s => (char.IsDigit(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле факультета содержит недопустимый(е) символ(ы) (цифры, */-+ и т.п.)");
                validFlag = false;
            }

            if (student.number.Any(s => (char.IsLetter(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле номера студ билета содержит недопустимый(е) символ(ы) (буквы, знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }

            #endregion
            #region Замена пробелов
            if (student.lastName.Any(c => char.IsSeparator(c)))
            {
                student.lastName.Replace(" ", string.Empty);
            }
            if (student.name.Any(c => char.IsSeparator(c)))
            {
                student.name.Replace(" ", string.Empty);
            }
            if (student.patronimic.Any(c => char.IsSeparator(c)))
            {
                student.patronimic.Replace(" ", string.Empty);
            }
            if (student.facult.Any(c => char.IsSeparator(c)))
            {
                student.facult.Replace(" ", string.Empty);
            }
            if (student.speciality.Any(c => char.IsSeparator(c)))
            {
                student.speciality.Replace(" ", string.Empty);
            }
            #endregion
            #region Курс
            if (student.kurs.HasValue)
            {
                if (student.kurs.Value > 5 || student.kurs < 1)
                {
                    brokenRules.Add("Значение поля курса не может быть больше 5 и меньше 1");
                    validFlag = false;
                }
            }
            #endregion
            #region Группа
            if (student.group.HasValue)
            {
                if (student.group.Value > 2 || student.group.Value < 1)
                {
                    brokenRules.Add("Значение поля группы не может быть больше 2 и меньше 1");
                    validFlag = false;
                }
            }
            #endregion
            #region Замена первой малой буквы на большую
            /*if (char.IsLower(student.lastName[0]))
            {
                student.lastName.ToUpper();
            }
            if (char.IsLower(student.name[0]))
            {
                student.name.ToUpper();
            }
            if (student.patronimic != null)
            {
                if (char.IsLower(student.patronimic[0]))
                {
                    student.patronimic.ToUpper();
                }
            }*/
            #endregion
            #region Длина серийного номера
            if (student.number.Length < 6 && !string.IsNullOrWhiteSpace(student.number))
            {
                brokenRules.Add("Длина номера студ билета не меньше 6 символов, сейчас введено " + student.number.Length);
                validFlag = false;
            }
            #endregion
        }
        public void validateWorkerToAdd(WorkerTicketBLL worker)
        {
            resetValues();
            #region Проверка полей на пустоту
            if (string.IsNullOrWhiteSpace(worker.lastName))
            {
                brokenRules.Add("Поле фамилии не может быть пустым");
                validFlag = false;
            }
            if (string.IsNullOrWhiteSpace(worker.name))
            {
                brokenRules.Add("Поле имени не может быть пустым");
                validFlag = false;
            }
            if (string.IsNullOrWhiteSpace(worker.serial))
            {
                brokenRules.Add("Поле серии рабочего удостоверения не может быть пустым");
                validFlag = false;
            }
            if (string.IsNullOrWhiteSpace(worker.number))
            {
                brokenRules.Add("Поле номера рабочего удостоверения не может быть пустым");
                validFlag = false;
            }
            if (string.IsNullOrWhiteSpace(worker.speciality))
            {
                brokenRules.Add("Поле специализации не может быть пустым");
                validFlag = false;
            }
            if (string.IsNullOrWhiteSpace(worker.phoneNumber))
            {
                brokenRules.Add("Поле номера телефона не может быть пустым");
                validFlag = false;
            }
            #endregion
            #region Проверка на запрещенные символы
            if (worker.lastName.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле фамилии содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (worker.name.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле имени содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (worker.patronimic.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле отчества содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (worker.serial.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле серии студ билета содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (!string.IsNullOrWhiteSpace(worker.speciality))
            {
                if (!(worker.speciality.Equals("сантехник")) && !(worker.speciality.Equals("электрик")) && !(worker.speciality.Equals("не опред.")))
                {
                    brokenRules.Add("Поле специальности может сдержать только значения - сантехник, электрик или не опред.");
                    validFlag = false;
                }
            }


            if (worker.number.Any(s => (char.IsLetter(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле номера студ билета содержит недопустимый(е) символ(ы) (буквы, знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            #endregion
            #region Телефон
            if (!string.IsNullOrWhiteSpace(worker.phoneNumber))
            {
                if (worker.phoneNumber[0] != '+')
                {
                    brokenRules.Add("Номер телефона может начинаться только с +");
                    validFlag = false;
                }
                if (!Regex.IsMatch(worker.phoneNumber, "^[+]{1}380[0-9]{2}-[0-9]{3}-[0-9]{2}-[0-9]{2}$"))
                {
                    brokenRules.Add("Номер телефона должен иметь вид: +380ХХ-ХХХ-ХХ-ХХ");
                    validFlag = false;
                }
            }
            #endregion

            if (worker.number.Length < 6 && !string.IsNullOrWhiteSpace(worker.number))
            {
                brokenRules.Add("Длина номера рабочего удостоверения не может быть меньше 6 символов, сейчас она равна" + worker.number.Length);
                validFlag = false;
            }
        }
        #endregion
        #region Search Validation
        public void validateStudentToSearch(StudentTicketBLL student)
        {
            resetValues();
            #region Проверка на наличие запрещенных символов
            if (student.lastName.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле фамилии содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (student.name.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле имени содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (student.patronimic.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле отчества содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (student.serial.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле серии студ билета содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (student.speciality.Any(s => (char.IsDigit(s) || (s.Equals(',') || s.Equals('-')) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле специальности содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }

            if (student.facult.Any(s => (char.IsDigit(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле факультета содержит недопустимый(е) символ(ы) (цифры, */-+ и т.п.)");
                validFlag = false;
            }

            if (student.number.Any(s => (char.IsLetter(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле номера студ билета содержит недопустимый(е) символ(ы) (буквы, знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }

            #endregion
        }
        public void validateWorkerToSearch(WorkerTicketBLL worker)
        {
            resetValues();
            #region Проверка на запрещенные символы
            if (worker.lastName.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле фамилии содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (worker.name.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле имени содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (worker.patronimic.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле отчества содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (worker.serial.Any(s => (char.IsDigit(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле серии студ билета содержит недопустимый(е) символ(ы) (цифры,знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            if (!string.IsNullOrWhiteSpace(worker.speciality))
            {
                if (!(worker.speciality.Equals("сантехник")) && !(worker.speciality.Equals("электрик")) && !(worker.speciality.Equals("не опред.")))
                {
                    brokenRules.Add("Поле специальности может сдержать только значения - сантехник, электрик или не опред.");
                    validFlag = false;
                }
            }
            if (worker.phoneNumber.Any(s => ((char.IsPunctuation(s) && !s.Equals('-')) || s.Equals('*') || s.Equals('/'))))
            {
                brokenRules.Add("Поле номера телефона содержит недопустимый(е) символ(ы) (знаки пунктуации, */ и т.п.)");
                validFlag = false;
            }
            if (worker.number.Any(s => (char.IsLetter(s) || char.IsPunctuation(s) || char.IsSymbol(s))))
            {
                brokenRules.Add("Поле номера студ билета содержит недопустимый(е) символ(ы) (буквы, знаки пунктуации, */-+ и т.п.)");
                validFlag = false;
            }
            #endregion
        }
        #endregion
        #region Журналы
        public void validateDateFromComboBoxes(string dateString)
        {
            DateTime date = new DateTime(2000, 1, 1);
            int? year;
            int? month;
            int? day;
            #region Проверки
            if (!dateString.Substring(0, 4).Equals("----"))
            {
                year = Convert.ToInt32(dateString.Substring(0, 4));
            }
            else
            {
                year = null;
            }
            if (!dateString.Substring(5, 2).Equals("--"))
            {
                month = Convert.ToInt32(dateString.Substring(5, 2));
            }
            else
            {
                month = null;
            }
            if (!dateString.Substring(8, 2).Equals("--"))
            {
                day = Convert.ToInt32(dateString.Substring(8, 2));
            }
            else
            {
                day = null;
            }
            #endregion
            if (year != null && month != null && day != null)
            {
                try
                {
                    date = new DateTime((int)year, (int)month, (int)day);
                }
                catch (ArgumentException ex)
                {
                    validFlag = false;
                    brokenRules.Add("Введенная дата не существует!");
                }
            }

        }
        public void validateDescriptionToAdd(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                brokenRules.Add("Поле описания не может быть пустым");
                validFlag = false;
            }
            if (!text.Any(s => char.IsLetter(s)))
            {
                brokenRules.Add("Поле описания не может не содержать слов");
                validFlag = false;
            }
            #region Проверка на наличие запрещенных символов
            if (text.Any(s => (char.IsSymbol(s) || s.Equals('*') || s.Equals('/') || s.Equals('\\'))))
            {
                brokenRules.Add("Поле описания содержит недопустимый(е) символ(ы) ( */-+ и т.п.)");
                validFlag = false;
            }
            #endregion
        }
        public void validateDescriptionToSearch(string text)
        {
            if (text.Any(s => (char.IsSymbol(s) || s.Equals('*') || s.Equals('/') || s.Equals('\\'))))
            {
                brokenRules.Add("Поле описания содержит недопустимый(е) символ(ы) ( */-+ и т.п.)");
                validFlag = false;
            }
        }
        #endregion
        public void validateWorkDay(WorkDayDAL day)
        {
            TimeSpan? workStartTime = null;
            TimeSpan? workEndTime = null;
            TimeSpan? restStartTime = null;
            TimeSpan? restEndTime = null;
            TimeSpan workCheck = new TimeSpan(4, 0, 0);
            TimeSpan restCheck = new TimeSpan(1, 0, 0);
            #region Начало и конец дня
            if (day.startTime != null)
            {
                workStartTime = new TimeSpan(Convert.ToInt32(day.startTime.Substring(0, 2)), Convert.ToInt32(day.startTime.Substring(3, 2)), 0);
            }
            else
            {
                brokenRules.Add("(" + day.day + ")" + "Время начала работы не указано");
                validFlag = false;
            }
            if (day.endTime != null)
            {
                workEndTime = new TimeSpan(Convert.ToInt32(day.endTime.Substring(0, 2)), Convert.ToInt32(day.endTime.Substring(3, 2)), 0);
            }
            else
            {
                brokenRules.Add("(" + day.day + ")" + "Время завершения работы не указано");
                validFlag = false;
            }
            #endregion
            #region Перерыв
            if (day.restStart != "---:---" && day.restStart != null)
            {
                restStartTime = new TimeSpan(Convert.ToInt32(day.restStart.Substring(0, 2)), Convert.ToInt32(day.restStart.Substring(3, 2)), 0);
            }
            else if (day.restStart == "---:---")
            {
                brokenRules.Add("(" + day.day + ")" + "Время начала перерыва не задано");
                validFlag = false;
            }

            if (day.restEnd != "---:---" && day.restEnd != null)
            {
                restEndTime = new TimeSpan(Convert.ToInt32(day.restEnd.Substring(0, 2)), Convert.ToInt32(day.restEnd.Substring(3, 2)), 0);
            }
            else if (day.restEnd == "---:---")
            {
                brokenRules.Add("(" + day.day + ")" + "Время окончания перерыва не задано");
                validFlag = false;
            }
            #endregion
            #region Сравнение введенных значений
            if (workStartTime.HasValue && workEndTime.HasValue)
            {
                if (workStartTime > workEndTime)
                {
                    brokenRules.Add("(" + day.day + ")" + "Время начала работы не может превышать время окончания работы");
                    validFlag = false;
                }
                if (workStartTime == workEndTime)
                {
                    brokenRules.Add("(" + day.day + ")" + "Рабочий день не может начинаться и заканчиваться в одно и то же время");
                    validFlag = false;
                }
                if (workEndTime - workStartTime < workCheck)
                {
                    brokenRules.Add("(" + day.day + ")" + "Рабочий день не может длиться менее 4 часов");
                    validFlag = false;
                }
                if (restStartTime != null && restEndTime != null)
                {
                    if (restStartTime < workStartTime)
                    {
                        brokenRules.Add("(" + day.day + ")" + "Перерыв не может начинаться перед началом рабочего дня");
                        validFlag = false;
                    }
                    if (restStartTime > workEndTime)
                    {
                        brokenRules.Add("(" + day.day + ")" + "Перерыв не может начинаться после окончания рабочего дня");
                        validFlag = false;
                    }
                    if (restStartTime > restEndTime)
                    {
                        brokenRules.Add("(" + day.day + ")" + "Перерыв не может начинаться после окончания перерыва");
                        validFlag = false;
                    }
                    if (restEndTime > workEndTime)
                    {
                        brokenRules.Add("(" + day.day + ")" + "Перерыв не может заканчиваться после окончания рабочего дня");
                        validFlag = false;
                    }
                    if (restEndTime < workStartTime)
                    {
                        brokenRules.Add("(" + day.day + ")" + "Перерыв не может начинаться перед началом рабочего дня");
                        validFlag = false;
                    }
                    if (restEndTime == restStartTime)
                    {
                        brokenRules.Add("(" + day.day + ")" + "Перерыв не может начинаться и заканчиваться в одно и то же время");
                        validFlag = false;
                    }
                    if (restStartTime == workStartTime)
                    {
                        brokenRules.Add("(" + day.day + ")" + "Перерыв не может начинатся одновременно с началом рабочего дня");
                        validFlag = false;
                    }
                    if (restStartTime == workEndTime)
                    {
                        brokenRules.Add("(" + day.day + ")" + "Перерыв не может начинатся одновременно с окончанием рабочего дня");
                        validFlag = false;
                    }
                    if (restEndTime == workStartTime)
                    {
                        brokenRules.Add("(" + day.day + ")" + "Перерыв не может заканчиваться одновременно с началом рабочего дня");
                        validFlag = false;
                    }
                    if (restEndTime == workEndTime)
                    {
                        brokenRules.Add("(" + day.day + ")" + "Перерыв не может заканчиваться одновременно окончанием рабочего дня");
                        validFlag = false;
                    }
                    if (restEndTime - restStartTime > restCheck)
                    {
                        brokenRules.Add("(" + day.day + ")" + "Перерыв не может длиться более 1 часа");
                        validFlag = false;
                    }
                }
            }
            #endregion
        }
        public void validateOldPassword(string pwd)
        {
            if (!LoginInfo.isKomendant())
            {
                if (string.IsNullOrWhiteSpace(pwd))
                {
                    validFlag = false;
                    brokenRules.Add("Поле старого пароля не может быть пустым!");
                }
                if (!Regex.IsMatch(pwd, "^([0-9]|[A-Z]|[a-z]|[А-Я]|[а-я]|[_])+$"))
                {
                    validFlag = false;
                    brokenRules.Add("Поле старого пароля содержит недопустимые символы. Пароль может содержать буквы, цифры, '_'");
                }
            }

        }
        public void validateNewPassword(string pwd)
        {
            if (string.IsNullOrWhiteSpace(pwd))
            {
                validFlag = false;
                brokenRules.Add("Поле пароля не может быть пустым!");
            }
            if (!Regex.IsMatch(pwd, "^([0-9]|[A-Z]|[a-z]|[А-Я]|[а-я]|[_])+$"))
            {
                validFlag = false;
                brokenRules.Add("Поле пароля содержит недопустимые символы. Пароль может содержать (буквы_цифры)");
            }
        }


        public void validateLogin(string enteredLogin)
        {
            string userType = null;
            string serial = null;
            string number = null;
            if (enteredLogin.Length<9)
            {
                validFlag = false;
                brokenRules.Add("Логин имеет фиксированную длину в 9 символов, введено меньше");
            }
            else
            {
                userType = enteredLogin.Substring(0, 1);
                serial = enteredLogin.Substring(1, 2);
                number = enteredLogin.Substring(3, enteredLogin.Length - 3);
            }
            if(userType!=null)
            {
                if (Regex.IsMatch(userType,"^[^РСК]$"))
                {
                    validFlag = false;
                    brokenRules.Add("Первый символ логина указывает на личность Р,С,К");
                }
            }
            if (serial != null)
            {
                if (!Regex.IsMatch(serial, "^([А-Я]{2})$"))
                {
                    validFlag = false;
                    brokenRules.Add("В серии могут быть только буквы");
                }
            }
            if (number != null)
            {
                if (!Regex.IsMatch(number, "^[0-9]{6}$"))
                {
                    //Валидация номера и серии осталась
                    validFlag = false;
                    brokenRules.Add("Среди символов номера присутствуют недопустимые символы. Номер состоит только из цифр");
                }
            }

        }

        public string getErrorString()
        {
            if(!isValid())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Были допущены ошибки:\n");
                int count = 1;
                foreach (string s in brokenRules)
                {
                    sb.Append(count+++"."+s + "\n");
                }
                return sb.ToString();
            }
            else
            {
                return null;
            }
        }

        public int getCountOfBrokenRules()
        {
            return brokenRules.Count;
        }
    }
}
