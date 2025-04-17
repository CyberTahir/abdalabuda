namespace Lecture_4._LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            SecondExample();

            FourthExample();


        }



        static private void SecondExample()
        {
            int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
            var result = from n in numbers
                         where n > 10 && n % 2 == 0
                         select n;

            foreach (var number in result)
            {
                Console.WriteLine(number);
            }
        }

    


        static private void FourthExample()
        {
            string[] people = { "Tom", "Alice", "Bob", "Sam", "Tim", "Tomas", "Bill" };
            var result = from p in people
                         where p.Length == 3
                         select p;

            foreach (var name in result)
            {
                Console.WriteLine(name);
            }
        }

       
    }
}
        