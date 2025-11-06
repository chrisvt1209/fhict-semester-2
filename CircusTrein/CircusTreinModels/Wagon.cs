namespace CircusTreinModels
{
    public class Wagon
    {
        private readonly List<Animal> animals;
        private const int capacity = 10;

        public Wagon()
        {
            animals = new List<Animal>();
        }
        public bool AddAnimal(Animal animal)
        {
            if (CanAnimalFit(animal) && IsAnimalSafe(animal))
            {
                animals.Add(animal);
                return true;
            }
            return false;
        }

        private bool CanAnimalFit(Animal animal)
        {
            int animalSizeValue = (int)animal.Size;
            return animals.Count < capacity && animalSizeValue <= GetRemainingSpace();
        }
        private int GetRemainingSpace()
        {
            int usedSpace = 0;
            foreach (Animal animal in animals)
            {
                usedSpace += (int)animal.Size;
            }
            return capacity - usedSpace;
        }

        private bool IsAnimalSafe(Animal animal)
        {
            bool hasCarnivore = false;
            int i = 0;
            while (i < animals.Count && !hasCarnivore)
            {
                if (animals[i].ConsumptionType == EConsumptionType.Carnivore)
                {
                    hasCarnivore = true;
                }
                i++;
            }

            if (animal.ConsumptionType == EConsumptionType.Carnivore)
            {
                i = 0;
                while (i < animals.Count)
                {
                    if (animals[i].Size <= animal.Size)
                    {
                        return false;
                    }
                    i++;
                }

                if (animals.Count > 0)
                {
                    return false;
                }
            }

            if (hasCarnivore && animal.ConsumptionType != EConsumptionType.Carnivore)
            {
                return false;
            }

            return true;
        }
        private int GetLoad()
        {
            int load = 0;
            foreach (Animal animal in animals)
            {
                load += (int)animal.Size;
            }
            return load;
        }
        public void PrintWagon()
        {
            int currentLoad = GetLoad();
            Console.WriteLine($"Current load: {currentLoad} / Maximum capacity: {capacity}");
            foreach (Animal animal in animals)
            {
                Console.WriteLine($"Name: {animal.Name}, Size: {animal.Size}, Consumption Type: {animal.ConsumptionType}");
            }
        }
    }
}
