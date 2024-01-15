using OnlineUdemyCourse.Domain.Courses.Enums;

namespace OnlineUdemyCourse.Domain.Courses
{
    public class Course
    {
        public string Name { get; private set; }
        public string Description { get; set; }
        public double Workload { get; private set; }
        public TargetAudience TargetAudience { get; private set; }
        public double Price { get; private set; }

        public Course(string name, string description, double workload, TargetAudience targetAudience, double price)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be empty!");
            }

            if (workload <= 1)
            {
                throw new ArgumentException("Workload cannot be less than '1'!");
            }

            if (price <= 1)
            {
                throw new ArgumentException("Price cannot be less than '1'!");
            }

            this.Name = name;
            this.Description = description;
            this.Workload = workload;
            this.TargetAudience = targetAudience;
            this.Price = price;
        }
    }
}
