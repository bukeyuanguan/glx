using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionSample.Version3
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

    public class HistoryTeacher : ITeacher
    {
        private IBook _book;

        public HistoryTeacher(IBook book)
        {
            _book = book;
        }
        public string KnowledgeTransfer() => $"History teacher read {_book.Text}";
    }

    public interface IBook
    {
        string Text { get; set; }
    }

    public class EnglishBook:IBook
    {
        public string Text { get; set; } = "EnglishBook";
    }


}
