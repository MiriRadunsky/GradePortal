﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeDO
{
    public class Student
    {
        public string ID {  get; set; }
        public string Name { get; set; }
        public string  Password { get; set; }

        public List<Grade> ExeList { get; set; } = new List<Grade>();
        public Grade TestGrade { get; set; }

        public double GradeAverage()
        {
            return ExeList.Average(grade => grade.GradeNumber);
        }

        public override string ToString()
        {
            string itemsAsString = ExeList != null ? string.Join("; ", ExeList) : "No Items";
            return $"Name: {Name}, ID: {ID}, TestGrade: {TestGrade}, ExeList: {itemsAsString}, TestGrade: {TestGrade.ToString}";
        }

    }
}
