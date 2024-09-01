public class ZooService(IAnimalRepo animalRepo) : IZooService
{
    private IAnimalRepo _animalRepo = animalRepo;

    public async Task<List<Animal>> GetAllAnimalsFromZooAsync()
    {
        return await _animalRepo.GetAllAnimalsAsync();
    }

    public List<Animal> GetAnimalsByMovementType(MovementType movementType)
    {
        return _animalRepo.GetAnimalsByMovementType(movementType);
    }

    public bool AddAnimalToZoo(Animal animal) //explain functions/methods
    {
        var isAnimalPresent = _animalRepo.AnimalExists(animal.Name);

        if(!isAnimalPresent)
        {
            _animalRepo.AddAnimalToZoo(animal);
            return true;
        } // refer to conditional checks(while, else if, switch) and why we omit else here and mention for while e foreach

        return false;
    }
}