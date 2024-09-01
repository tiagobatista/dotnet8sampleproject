//refer access modifiers, class and objects, inheritance and polymorphism
/*
1. Access Modifiers
Access modifiers control who can see and use your code (like variables and methods). Think of them as levels of privacy:

public: Anyone can access it. It's like having a public phone number anyone can call.
private: Only the code inside the same class can access it. It's like having a personal journal no one else can read.
protected: Only the class and its child classes (derived classes) can access it. It's like a family secret shared only with relatives.
internal: Accessible only within the same project or assembly. It’s like a neighborhood watch where only people in your neighborhood know about it.
protected internal: A combination of protected and internal—accessible by classes in the same assembly or derived classes.
private protected: Accessible only within the same class or in derived classes that are in the same assembly.
2. Classes and Objects
Class: A class is like a blueprint for creating objects. It defines properties (attributes) and methods (actions) that the objects created from the class can have.
Example: If Car is a class, it defines what a car should be like—its color, make, model, etc., and what it can do—drive, stop, honk, etc.
Object: An object is an instance of a class. It’s like creating a specific car from the Car blueprint. Each car (object) can have different properties (like color) 
but can do the same actions.
Example: myCar might be an object of the Car class, and it might be a red Toyota.
*/
public abstract class Animal(MovementType movementType, string name) //primary constructor
{
    // refer data types - int, string, bool - value types and reference types
    public int Id { get; private set; }

    private string _name = name;

    public string Name
    {
        get => _name;
        private set
        {
            //Encapsulation. We don't want to access the setter directly because we always want the business logic to run
            //add business logic
            _name = value;
        }
    }

    private MovementType _movementType = movementType;

    public MovementType MovementType
    {
        get => _movementType;
        private set
        {
            _movementType = value;
        }
    }

    public virtual void MakeSound() => Console.WriteLine("*Silence*"); //Lambda expression
}