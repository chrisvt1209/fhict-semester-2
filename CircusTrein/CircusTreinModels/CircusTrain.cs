namespace CircusTreinModels
{
    public class CircusTrain
    {
        private readonly List<Wagon> wagons;

        public CircusTrain()
        {
            wagons = new List<Wagon>();
        }
        public List<Animal> GenerateAnimals(int count)
        {
            List<Animal> animals = new List<Animal>();
            Random random = new Random();

            int herbivoreCount = count / 2;
            int carnivoreCount = count - herbivoreCount;

            for (int i = 0; i < herbivoreCount; i++)
            {
                string name = "Herbivore" + (i + 1);
                EAnimalSize size = GetRandomSize(random);
                Animal animal = new Animal(name, EConsumptionType.Herbivore, size);
                animals.Add(animal);
            }

            for (int i = 0; i < carnivoreCount; i++)
            {
                string name = "Carnivore" + (i + 1);
                EAnimalSize size = GetRandomSize(random);
                Animal animal = new Animal(name, EConsumptionType.Carnivore, size);
                animals.Add(animal);
            }
            return animals;
        }
        private EAnimalSize GetRandomSize(Random random)
        {
            EAnimalSize[] sizes = (EAnimalSize[])Enum.GetValues(typeof(EAnimalSize));
            return sizes[random.Next(sizes.Length)];
        }
        public void CheckAnimal(Animal animal)
        {
            foreach (Wagon wagon in wagons)
            {
                if (wagon.AddAnimal(animal))
                    return;
            }

            Wagon newWagon = new Wagon();
            newWagon.AddAnimal(animal);
            wagons.Add(newWagon);
        }
        public void AddWagon(Wagon wagon)
        {
            wagons.Add(wagon);
        }

        public void PrintTrain()
        {
            Console.WriteLine("Circus Train Details:");
            foreach (Wagon wagon in wagons)
            {
                Console.WriteLine($"Wagon: {wagon}");
                wagon.PrintWagon();
                Console.WriteLine();
            }
        }
    }
}
