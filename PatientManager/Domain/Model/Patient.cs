namespace PatientManager.Domain.Model
{
    public class Patient
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double BMI => Weight / (Height * Height);

    }
}