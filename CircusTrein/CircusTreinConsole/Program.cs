using CircusTreinModels;

namespace CircusTreinConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            CircusTrain circusTrain = new CircusTrain();
            List<Animal> animals = circusTrain.GenerateAnimals(10);
            circusTrain.AddWagon(new Wagon());

            foreach (var animal in animals)
            {
                circusTrain.CheckAnimal(animal);
            }
            circusTrain.PrintTrain();
        }
    }
}
