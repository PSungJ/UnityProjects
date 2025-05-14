//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _05_12_Csharp
//{
//    //분할 클래스 : 클래스의 구현이 길어질 경우 여러파일로 나누어 구현할 수 있기 위해
//    partial class MyClass
//    {
//        public void Method_A()
//        {
//            Console.WriteLine("Method_A");
//        }
//        public void Method_B()
//        {
//            Console.WriteLine("Method_B");
//        }
//    }
//    partial class MyClass
//    {
//        public void Method_C()
//        {
//            Console.WriteLine("Method_C");
//        }
//        public void Method_D()
//        {
//            Console.WriteLine("Method_D");
//        }
//    }
//    internal class Partial
//    {
//        static void Main(string[] args)
//        {
//            MyClass myClass = new MyClass();
//            myClass.Method_A();
//            myClass.Method_B();
//            myClass.Method_C();
//            myClass.Method_D();
//            Console.ReadLine();
//        }
//    }
//}
