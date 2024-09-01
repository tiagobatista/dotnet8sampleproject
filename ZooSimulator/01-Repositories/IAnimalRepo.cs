public interface IAnimalRepo
{

    Task<List<Animal>> GetAllAnimalsAsync();

    List<Animal> GetAnimalsByMovementType(MovementType movementType);

    void AddAnimalToZoo(Animal animal);

    bool AnimalExists(string name);
}