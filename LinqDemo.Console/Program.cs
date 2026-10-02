using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LinqDemo.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            _1_TreeStages_WriteEvenNumbersToConsole();
            _2_From_SimpleWhere_WriteAdultsToConsole();
            _3_ComplexWhere_WriteLargeEvenNumbersToConsole();
            _4_ProjectionToMember_ConvertPeopleToAges();
            _5_ProjectionToAnonymousType_ConvertPeopleToStatistics();
            _6_OrderBy_SortPeopleByAgeDescendingThenByNameAscending();
            _7_Join_JoinPeopleOnAge();
            _8_ExecutionMethods_DoStuffWithWords();

            System.Console.WriteLine("Press any key to end the program...");
            System.Console.ReadKey(true);
        }

        static void _1_TreeStages_WriteEvenNumbersToConsole()
        {
            // 1. Data source
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // 2. Define a LINQ query
            IEnumerable<int> evenNumbersQuery = from n in numbers
                                                where n % 2 == 0
                                                select n;

            // 3. Execute the query
            System.Console.WriteLine("_1_ Even numbers:");
            foreach (int number in evenNumbersQuery)
            {
                System.Console.Write($"{number} ");
            }
            System.Console.WriteLine();
            System.Console.WriteLine();

            // Equivalent method syntax:
            var evenNumbersQuery2 = numbers.Where(n => n % 2 == 0);
        }

        static void _2_From_SimpleWhere_WriteAdultsToConsole()
        {
            // 1. Get data source
            Person[] people = new Person[]
            {
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Bob", Age = 15 },
                new Person { Name = "Charlie", Age = 25 },
                new Person { Name = "David", Age = 10 }
            };

            // 2. Create a LINQ query
            IEnumerable<Person> adultsQuery = from person in people
                                              where person.Age >= 18
                                              select person;

            // 3. Execute the query
            System.Console.WriteLine("_2_ Adults:");
            foreach (Person adult in adultsQuery)
            {
                System.Console.WriteLine(adult);
            }
            System.Console.WriteLine();

            // Equivalent method syntax:
            var adultsQuery2 = people.Where(p => p.Age >= 18);
        }

        static void _3_ComplexWhere_WriteLargeEvenNumbersToConsole()
        {
            // 1. Data source
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // 2. Define a LINQ query
            IEnumerable<int> query = from n in numbers
                                     where IsEven(n) && n > 4
                                     select n;

            // 3. Execute the query
            System.Console.WriteLine("_3_ Large even numbers:");
            foreach (int number in query)
            {
                System.Console.Write($"{number} ");
            }
            System.Console.WriteLine();
            System.Console.WriteLine();

            // Equivalent method syntax:
            var query2 = numbers.Where(n => IsEven(n) && n > 4);
        }

        private static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        static void _4_ProjectionToMember_ConvertPeopleToAges()
        {
            // 1. Get data source
            Person[] people = new Person[]
            {
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Bob", Age = 15 },
                new Person { Name = "Charlie", Age = 25 },
                new Person { Name = "David", Age = 10 }
            };

            // 2. Create a LINQ query
            IEnumerable<int> query = from person in people
                                     select person.Age;

            // 3. Execute the query
            System.Console.WriteLine("_4_ Ages of people:");
            foreach (int age in query)
            {
                System.Console.Write($"{age} ");
            }
            System.Console.WriteLine();
            System.Console.WriteLine();

            // Equivalent method syntax:
            var query2 = people.Select(p => p.Age);
        }

        static void _5_ProjectionToAnonymousType_ConvertPeopleToStatistics()
        {
            // 1. Get data source
            Person[] people = new Person[]
            {
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Bob", Age = 15 },
                new Person { Name = "Charlie", Age = 25 },
                new Person { Name = "David", Age = 10 }
            };

            // 2. Create a LINQ query
            var query = from person in people
                        select new
                        {
                            NameLength = person.Name.Length,
                            IsAdult = person.Age >= 18
                        };

            // 3. Execute the query
            System.Console.WriteLine("_5_ People statistics:");
            foreach (var stats in query)
            {
                System.Console.WriteLine($"Name Length: {stats.NameLength}, Is Adult: {stats.IsAdult}");
            }
            System.Console.WriteLine();

            // Equivalent method syntax:
            var query2 = people.Select(p => new
            {
                NameLength = p.Name.Length,
                IsAdult = p.Age >= 18
            });
        }

        static void _6_OrderBy_SortPeopleByAgeDescendingThenByNameAscending()
        {
            // 1. Get data source
            Person[] people = new Person[]
            {
                new Person { Name = "Eve", Age = 30 },
                new Person { Name = "Charlie", Age = 25 },
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Frank", Age = 15 },
                new Person { Name = "David", Age = 10 },
                new Person { Name = "Bob", Age = 15 }
            };

            // 2. Create a LINQ query
            var query = from person in people
                        orderby person.Age descending, person.Name ascending // ascending keyword is optional
                        select person;

            // 3. Execute the query
            System.Console.WriteLine("_6_ Sorted people:");
            foreach (Person person in query)
            {
                System.Console.WriteLine(person);
            }
            System.Console.WriteLine();

            // Equivalent method syntax:
            var query2 = people
                .OrderByDescending(p => p.Age)
                .ThenBy(p => p.Name);
        }

        static void _7_Join_JoinPeopleOnAge()
        {
            // 1. Get data sources
            List<Person> group1 = new List<Person>
            {
                new Person { Name = "Eve", Age = 10 },
                new Person { Name = "Charlie", Age = 25 },
                new Person { Name = "Alice", Age = 25 },
            };

            Person[] group2 = new Person[]
            {
                new Person { Name = "Frank", Age = 25 },
                new Person { Name = "David", Age = 10 },
                new Person { Name = "Bob", Age = 30 }
            };

            // 2. Create a LINQ query
            var query = from person1 in group1
                        join person2 in group2 on person1.Age equals person2.Age
                        select new
                        {
                            Person1 = person1,
                            Person2 = person2
                        };

            // 3. Execute the query
            System.Console.WriteLine("_7_ Joined people:");
            foreach (var pair in query)
            {
                System.Console.WriteLine($"Person 1: {pair.Person1}, Person 2: {pair.Person2}");
            }
            System.Console.WriteLine();

            // Equivalent method syntax:
            var query2 = group1.Join(
                group2,
                p1 => p1.Age,
                p2 => p2.Age,
                (p1, p2) => new { Person1 = p1, Person2 = p2 });
        }

        static void _8_ExecutionMethods_DoStuffWithWords()
        {
            // 1. Get data sources
            string[] words = { "apple", "banana", "cherry", "date", "and", "tomato", "randomness" };

            // 2. Create a LINQ query
            var query = from word in words
                        where word.Length > 4
                        orderby word.Length descending
                        select word.ToUpper();

            // 3. Execute the query
            System.Console.WriteLine("_8_ Execution methods:");

            // ToList
            IList<string> longWordsList = query.ToList(); // Immediate execution, materializes the results into a List<string>
            System.Console.WriteLine($"Long words list: {string.Join(", ", longWordsList)}");

            // ToArray
            string[] longWordsArray = query.ToArray(); // Immediate execution, materializes the results into a string[]
            System.Console.WriteLine($"Long words array: {string.Join(", ", longWordsArray)}");

            // ToDictionary
            Dictionary<string, int> longWordsDictionary = query.ToDictionary(word => word, word => word.Length); // Immediate execution, materializes the results into a Dictionary<string, int>
            System.Console.WriteLine(
                $"Long words dictionary: {string.Join(", ", longWordsDictionary.Select(kvp => $"{kvp.Key} ({kvp.Value})"))}");

            // FirstOrDefault
            string? firstLongWord = query.FirstOrDefault(); // Immediate execution, retrieves the first element or default (null for reference types)
            System.Console.WriteLine($"First long word: {firstLongWord}");

            // Count
            int longWordCount = query.Count(); // Immediate execution, counts the number of elements
            System.Console.WriteLine($"Long word count: {longWordCount}");

            // Any
            bool hasLongWords = query.Any(); // Immediate execution, checks if there are any elements
            System.Console.WriteLine($"Are there any long words? {hasLongWords}");

            // All
            bool allAreLong = query.All(word => word.Length > 4); // Immediate execution, checks if all elements satisfy the condition
            System.Console.WriteLine($"Are all words long? {allAreLong}");

            System.Console.WriteLine();

            // Equivalent method syntax:
            var query2 = words
                .Where(word => word.Length > 4)
                .OrderByDescending(word => word.Length)
                .Select(word => word.ToUpper());
        }
    }
}
