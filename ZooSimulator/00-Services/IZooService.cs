/*
An interface in C# is a fundamental concept in object-oriented programming that defines a contract or a set of methods and properties that implementing classes must provide. Interfaces are used to specify what methods and properties a class should implement without dictating how these members should be implemented. They promote a flexible and loosely-coupled design by allowing different classes to implement the same interface in various ways.

Key Characteristics of Interfaces:
Method Signatures: An interface defines method signatures (i.e., method names, return types, and parameters) but does not provide the method implementations. Classes that implement the interface must provide the actual method bodies.
No Fields: Interfaces cannot contain fields or data members. They can only contain methods, properties, events, or indexers.
Multiple Interfaces: A class or struct can implement multiple interfaces, allowing it to be more versatile and adhere to multiple contracts.
Default Implementations: In C# 8.0 and later, interfaces can have default implementations for methods. However, this feature is used sparingly to maintain the interface's primary role as a contract.
*/

public interface IZooService
{
    Task<List<Animal>> GetAllAnimalsFromZooAsync();

    List<Animal> GetAnimalsByMovementType(MovementType movementType);

    bool AddAnimalToZoo(Animal animal);

}