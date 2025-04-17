namespace Lecture_4._LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // LINQ-запросы
            FirstExample();
            SecondExample();
            ThirdExample();
            FourthExample();

            //// Проекция данных
            FifthExample();
            SixthExample();
            SeventhExample();
            EighthExample();

            //// Выборка из нескольких источников
            NinthExample();
            TenthExample();
            EleventhExample();
            TwelthExample();
            ThirteenthExample();
        }

        static private void FirstExample()
        {
            string[] people = { "tom", "Bob", "Sam", "Tim", "Tomas", "Bill" };

            var selectedPeople = new List<string>();
            foreach (string person in people)
            {
                if (person.ToUpper().StartsWith('T'))
                    selectedPeople.Add(person);
            }

            selectedPeople.Sort();

            foreach (string person in selectedPeople)
                Console.WriteLine(person);
        }

        static private void SecondExample()
        {
            string[] people = { "Tom", "Bob", "Sam", "Tim", "Tomas", "Bill" };

            // LINQ-запрос имеет следующий вид:
            // from переменная in набор_объектов
            // select переменная;
            var selectedPeople = from p in people
                                 where p.ToUpper().StartsWith('T')
                                 orderby p
                                 select p;

            foreach (string person in selectedPeople)
                Console.WriteLine(person);
        }

        static private void ThirdExample()
        {
            string[] people = { "Tom", "Bob", "Sam", "Tim", "Tomas", "Bill" };

            var selectedPeople = people.Where(p => p.ToUpper().StartsWith("T"))
                .OrderBy(p => p);

            foreach (string person in selectedPeople)
                Console.WriteLine(person);
        }

        static private void FourthExample()
        {
            string[] people = { "Tom", "Bob", "Sam", "Tim", "Tomas", "Bill" };

            var number = (from p in people
                          where p.ToUpper().StartsWith("T")
                          select p).Count();

            Console.WriteLine(number);
        }

        record class Person(string Name, int Age);
        static private void FifthExample()
        {
            var people = new List<Person>
            {
                new Person ("Tom", 23),
                new Person ("Bob", 27),
                new Person ("Sam", 29),
                new Person ("Alice", 24)
            };

            // Выделяем из каждого объекта поле "Name"
            var names = from p in people
                        select p.Name;

            foreach (string n in names)
                Console.WriteLine(n);
        }

        static private void SixthExample()
        {
            var people = new List<Person>
            {
                new Person ("Tom", 23),
                new Person ("Bob", 27),
                new Person ("Sam", 29),
                new Person ("Alice", 24)
            };

            // Проекция данных
            var names = people.Select(u => u.Age);

            foreach (var n in names)
                Console.WriteLine(n);
        }

        static private void SeventhExample()
        {
            var people = new List<Person>
            {
                new Person ("Tom", 23),
                new Person ("Bob", 27),
                new Person ("Sam", 29),
                new Person ("Alice", 24)
            };

            var personel = from p in people
                           select new
                           {
                               FirstName = p.Name,
                               Year = DateTime.Now.Year - p.Age
                           };

            foreach (var p in personel)
                Console.WriteLine($"{p.FirstName} - {p.Year}");
        }

        static private void EighthExample()
        {
            var people = new List<Person>
            {
                new Person ("Tom", 23),
                new Person ("Bob", 27),
                new Person ("Sam", 29),
                new Person ("Alice", 24)
            };

            var personnel = from p in people
                            let name = $"Mr. {p.Name}"
                            let year = DateTime.Now.Year - p.Age
                            select new
                            {
                                Name = name,
                                Year = year
                            };

            foreach (var p in personnel)
                Console.WriteLine($"{p.Name} - {p.Year}");
        }

        record class Course(string Title);  // учебный курс
        record class Student(string Name);  // студент

        static private void NinthExample()
        {
            var courses = new List<Course> { new Course("C#"), new Course("Python") };
            var students = new List<Student> { new Student("Tom"), new Student("Bob") };

            var enrollments = from student in students       //  выбираем по одному студенту
                              from course in courses    //  выбираем по одному курсу
                              select new    // соединяем каждого студента с каждым курсом
                              {
                                  Student = student.Name,
                                  Course = course.Title
                              };

            foreach (var enrollment in enrollments)
                Console.WriteLine($"{enrollment.Student} - {enrollment.Course}");
        }

        record class Employee(string Name);
        record class Company(string Name, List<Employee> Staff);
        static private void TenthExample()
        {
            var companies = new List<Company>
            {
                new Company("Microsoft", new List<Employee> {new Employee("Tom"), new Employee("Bob")}),
                new Company("Google", new List<Employee> {new Employee("Sam"), new Employee("Mike")}),
            };

            var employees = companies.SelectMany(c => c.Staff);

            foreach (var emp in employees)
                Console.WriteLine($"{emp.Name}");
        }

        static private void EleventhExample()
        {
            var companies = new List<Company>
            {
                new Company("Microsoft", new List<Employee> {new Employee("Tom"), new Employee("Bob")}),
                new Company("Google", new List<Employee> {new Employee("Sam"), new Employee("Mike")}),
            };

            var employees = from c in companies
                            from emp in c.Staff
                            select emp;

            foreach (var emp in employees)
                Console.WriteLine($"{emp.Name}");
        }

        static private void TwelthExample()
        {
            var companies = new List<Company>
            {
                new Company("Microsoft", new List<Employee> {new Employee("Tom"), new Employee("Bob")}),
                new Company("Google", new List<Employee> {new Employee("Sam"), new Employee("Mike")}),
            };

            var employees = companies.SelectMany(c => c.Staff,
                                                (c, emp) => new
                                                {
                                                    Name = emp.Name,
                                                    Company = c.Name
                                                });

            foreach (var emp in employees)
                Console.WriteLine($"{emp.Name} - {emp.Company}");
        }

        static private void ThirteenthExample()
        {
            var companies = new List<Company>
            {
                new Company("Microsoft", new List<Employee> {new Employee("Tom"), new Employee("Bob")}),
                new Company("Google", new List<Employee> {new Employee("Sam"), new Employee("Mike")}),
            };

            var employees = from c in companies
                            from emp in c.Staff
                            select new {
                                Name = emp.Name,
                                Company = c.Name
                            };

            foreach (var emp in employees)
                Console.WriteLine($"{emp.Name} - {emp.Company}");
        }
    }
}
