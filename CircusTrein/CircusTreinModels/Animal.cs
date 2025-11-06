namespace CircusTreinModels
{
    public class Animal
    {
        public string Name { get; set; }
        public EConsumptionType ConsumptionType { get; set; }
        public EAnimalSize Size { get; set; }
        public Animal(string name, EConsumptionType consumptionType, EAnimalSize size)
        {
            Name = name;
            ConsumptionType = consumptionType;
            Size = size;
        }
    }
}
