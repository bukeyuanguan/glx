using DependencyInjectionSample.Version2;
using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionSample.Version1
{
    public class Student
    {
        public void Learn()
        {
            var teacher = new EnglishTeacher();
            var knowledge = teacher.KnowledgeTransfer();
            Understand(knowledge);
        }

        private void Understand(string knowledge) => Console.WriteLine(knowledge);
    }

    public class EnglishTeacher
    {
        public string KnowledgeTransfer() => "English";
    }

    public class HistoryTeacher
    {
        public string KnowledgeTransfer() => "History";
    }
}
