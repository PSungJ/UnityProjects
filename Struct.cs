using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*class, 구조체 struct의 공통점
관련된 데이터를 모아 놓았다.
class는 참조형식 heap, 구조체struct는 값형식 stack*/
namespace _05_12_Csharp
{
    struct Point3D
    {
        public int x;
        public int y; 
        public int z;
        public Point3D(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public override string ToString()
        {
            return string.Format($"{x} {y} {z}");
        }
    }
    internal class Struct
    {
        static void Main(string[] args)
        {
            Point3D p3d1;
            p3d1.x = 10;
            p3d1.y = 20;
            p3d1.z = 30;
            Console.WriteLine(p3d1.ToString());
            Point3D p3d2 = new Point3D(10, 20, 30);
            Point3D p3d3 = p3d2;
            p3d3.z = 400;
            Console.WriteLine(p3d2.ToString());
            Console.WriteLine(p3d3.ToString());
        }
    }
}
