public class Tiger(MovementType movementType) : Animal(movementType, nameof(Tiger))
{
    public override void MakeSound() => Console.WriteLine("Roooaaaar!!!");
}