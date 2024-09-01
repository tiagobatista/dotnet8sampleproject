public class AnimalRepo(AppDbContext context) : IAnimalRepo
{
    public async Task<List<Animal>> GetAllAnimalsAsync()
    {
        await Task.Delay(500); //refer await and async
        /*
        In C#, async and await are key components of asynchronous programming. They are used to work with tasks and make code more efficient by allowing the program to perform multiple operations simultaneously without blocking the main thread.
        1. async keyword
        The async keyword is used to declare a method that contains asynchronous operations. When a method is marked with async, it can use the await keyword inside its body. An async method typically returns one of the following types:

        Task (for methods that don't return a value).
        Task<T> (for methods that return a value of type T).
        void (only for event handlers).
        2. await keyword
        The await keyword is used inside an async method to indicate that the program should asynchronously wait for the completion of a task. When the await keyword is used, the control is returned to the caller method until the awaited task is complete, without blocking the execution.
                
        Benefits of async and await:
        Non-Blocking: The main thread (typically the UI thread in desktop applications) is not blocked while awaiting the completion of a task, making the application more responsive.
        Improved Scalability: Especially useful in I/O-bound operations (like network calls, file I/O), where the program can perform other tasks while waiting for the operation to complete.
        Simplified Code: The use of async and await makes asynchronous programming easier to understand and maintain compared to traditional callback-based approaches.
        */
        
        /*
        LINQ (Language Integrated Query) is a powerful querying tool in C# that allows you to query and manipulate data from various data sources like collections, databases, XML documents, and more. LINQ provides a consistent model for working with data across different kinds of data sources and formats.

        Key Concepts of LINQ
        Query Expressions: LINQ queries can be written using query syntax (which resembles SQL) or method syntax (using extension methods). These queries can be executed against any IEnumerable<T> or IQueryable<T> data sources.
        Deferred Execution: LINQ queries are not executed when they are defined but when they are iterated over (e.g., in a foreach loop) or when certain terminal methods like ToList() or Count() are called.
        Standard Query Operators: LINQ provides a set of standard query operators, such as Where, Select, OrderBy, GroupBy, Join, etc., that allow you to perform common data operations.
        */
        var animals = context
            .Animals
            .OrderBy(a => a.Name)
            .ToList();

        return animals;
    }

    public List<Animal> GetAnimalsByMovementType(MovementType movementType)
    {
        var animals = context
            .Animals            //Making use of the encapsulation here
            .Where(a => a.MovementType == movementType) //Remember to refer LINQ documentation and .NET 8 performance improvement
            .ToList();

        return animals;
    }

    public void AddAnimalToZoo(Animal animal)
    {
            context.Animals.Add(animal);
            context.SaveChanges();
    }

    public bool AnimalExists(string name)
    {
        return context.Animals.Any(a => string.Equals(a.Name, name));
    }
}