public class Lion(MovementType movementType) : Animal(movementType, nameof(Lion))
{
    public override void MakeSound() => Console.WriteLine("Graaaaawww!!!");
}