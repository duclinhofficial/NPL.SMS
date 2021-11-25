using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities
{
    class Employee
    {
        private int employeeId;
        private string employeeName;
        private double salary;
        private int spvrId;

        //Default constructor
        public Employee()
        {

        }

        //Constructor 4 parameters
        public Employee(int employeeId, string employeeName, double salary, int spvrId)
        {
            this.employeeId = employeeId;
            this.employeeName = employeeName;
            this.salary = salary;
            this.spvrId = spvrId;
        }

        //EmployeeId
        public int EmployeeId
        {
            get => employeeId;
            set
            {
                if (employeeId > 0)
                    employeeId = value;
                else
                {
                    do
                    {
                        Console.Write("Nhap lai ID cua Nhan vien: ");
                        employeeId = int.Parse(Console.ReadLine());
                    }
                    while (employeeId < 0);
                }
            }
        }

        //EmployeName
        public string EmployeeName
        {
            get => employeeName;
            set => employeeName = value;
        }

        //Salary
        public double Salary
        {
            get => salary;
            set
            {
                if (salary >= 0)
                    salary = value;
                else
                    salary = 0;
            }
        }

        //SupervisorID
        public int SpvrId
        {
            get => spvrId;
            set
            {
                if (spvrId > 0)
                    spvrId = value;
                else
                {
                    do
                    {

                        Console.Write("Nhap lai ID cua Supervisor: ");
                        spvrId = int.Parse(Console.ReadLine());
                    }
                    while (spvrId < 0);
                }
            }
        }
    }
}
