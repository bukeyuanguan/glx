using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionSample.Version2
{
    public class Student
    {
        private ITeacher _teacher;
        public Student(ITeacher teacher)
        {
            _teacher = teacher;
        }

        public void Learn()
        {
            var knowledge = _teacher.KnowledgeTransfer();
            Understand(knowledge);
        }

        private void Understand(string knowledge) => Console.WriteLine(knowledge);
    }

    public interface ITeacher
    {
        string KnowledgeTransfer();
    }

    public class EnglishTeacher: ITeacher
    {
        public string KnowledgeTransfer() => "English";
    }

    public class HistoryTeacher: ITeacher
    {
        public string KnowledgeTransfer() => "History";
    }


}
