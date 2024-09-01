public class Eagle(MovementType movementType) : Animal(movementType, nameof(Eagle)) //this is how you call base call from primary ctor
{
    public override void MakeSound() => Console.WriteLine("Screeeeeeech!!!");
}