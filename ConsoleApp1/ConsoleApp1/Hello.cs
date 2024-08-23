using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Hello
{
    class Constructor
    {
        public int no;
        public string name;
        //constructor
        public Constructor(int employno, string employname)
        { 
            no = employno;
            name = employname;
        }
    }
    class Encapsulation 
    {
        private string name = "";
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
    }
    class Parent
    {
        public string name = "Shyam";
        public void welcome()
        {
            Console.WriteLine("Welcome One and All \n");
        }
    }
    class Child : Parent
    {
        public int age = 22;
    }
    class Child2: Child
    {
        public void notif()
        {
            Console.WriteLine("Notif from Child 2 \n");
        }
    }

    abstract class Employee 
    {
        abstract public void name();   
    }

    class Emp1 : Employee
    {
        public override void name()
        {
            Console.WriteLine("Shyam");
        }
    }
    class Emp2 : Employee
    {
        public override void name()
        {
            Console.WriteLine("Ganesh");
        }
    }
    interface IEmployeeN
    {
        public void name();
    }
    interface IEmployeeA
    {
        public void age();
    }
    class Emp3 : IEmployeeN, IEmployeeA
    {
        public void name()
        {
            Console.WriteLine("My Name is Shyam");
        }
        public void age()
        {
            Console.WriteLine("My Age is "+22);
        }
    }
    class Program
    {
        //Constructor
        //static void Main(string[] args)
        //{
        //    Employee Emp = new Employee(1, "Shyam");
        //    Console.WriteLine(Emp.no + "\n" + Emp.name);
        //}


        //Encapsulation
        //public static void Main(string[] args)
        //{
        //    Employee e = new Employee();
        //    Console.WriteLine("Enter the Employee Name:");
        //    //string Ename=Console.ReadLine();
        //    e.Name = Console.ReadLine();
        //    Console.WriteLine("\n" + e.Name);
        //}


        //Inheritance
        //static void Main(string[] args)
        //{
        //    Child2 c = new Child2();
        //    c.welcome();
        //    Console.Write(c.name);
        //    c.notif();
        //}

        //Polymorphism & Abstraction
        //static void Main(string[] args)
        //{
        //    Emp1 emp = new Emp1();
        //    emp.name();
        //    Emp2 emp2 = new Emp2();
        //    emp2.name();   
        //}

        //Interface
        //static void Main(string[] args)
        //{
        //    Emp3 ee = new Emp3();
        //    ee.name();
        //    ee.age();
        //}

        ////Array
        //static void Main(string[] args)
        //{
        //    int n = 10;
        //    string[] name=new string[n];
        //    for (int i = 0; i < n; i++)
        //    {
        //        name[i] = Console.ReadLine();
        //    }
        //    for (int i = 0; i < n; i++)
        //    {
        //        Console.WriteLine(name[i] + '\n');
        //    }
        //}

        //try_catch
        //static void Main(string[] args)
        //{
        //    try
        //    {
        //        int[] num = {1,2,3};
        //        Console.WriteLine(num[2]);
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    finally
        //    {
        //        Console.WriteLine("The Exception has Ended");
        //    }
        //}


        //Dynamic Array(List)
        static void Main(string[] args)
        {
            List <int> Numbers = new List<int>();
            {
                Numbers.Add(1);
            }
            foreach (int value in Numbers)
            {
                Console.WriteLine(value);
            }
        }

    }
}
