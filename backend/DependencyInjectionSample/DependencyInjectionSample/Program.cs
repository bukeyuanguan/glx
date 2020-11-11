using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DependencyInjectionSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            {
                var student = new Version1.Student();
                student.Learn();
            }


            //{
            //    var teacher = new Version2.EnglishTeacher();
            //    var student = new Version2.Student(teacher);
            //    student.Learn();
            //}


            //{
            //var builder = new HostBuilder().ConfigureServices((hostContext, services) =>
            //    {
            //        services.AddTransient<Version2.ITeacher, Version2.HistoryTeacher>();
            //        services.AddTransient<Version2.Student>();
            //    });
            //var provider = builder.Build();


            //    var student = provider.Services.GetService<Version2.Student>();
            //    student.Learn();
            //}

            //{
            //    var builder = new HostBuilder().ConfigureServices((hostContext, services) =>
            //    {
            //        services.AddTransient<Version3.ITeacher, Version3.HistoryTeacher>();
            //        services.AddTransient<Version3.IBook, Version3.EnglishBook>();
            //        services.AddTransient<Version3.Student>();

            //    });
            //    var provider = builder.Build();

            //    var student = provider.Services.GetService<Version3.Student>();
            //    student.Learn();
            //}
            //}
        }
    }
}
