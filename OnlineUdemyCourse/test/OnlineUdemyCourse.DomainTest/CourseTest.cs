using System.Data;
using System.Reflection.PortableExecutable;

namespace OnlineUdemyCourse.DomainTest
{
    public class CourseTest
    {
        [Fact]
        public void ShouldCreateCourse()
        {
            // Arrange
            const string name = "Basic computing";
            const double workload = 80;
            const string targetAudience = "Students";
            const double price = 950;

            // Act
            var course = new Course(name, workload, targetAudience, price);

            // Assert
            Assert.Equal(name, course.Name);
            Assert.Equal(workload, course.Workload);
            Assert.Equal(targetAudience, course.TargetAudience);
            Assert.Equal(price, course.Price);
        }
    }

    internal class Course
    {
        public string Name { get; private set; }
        public double Workload { get; private set; }
        public string TargetAudience { get; private set; }
        public double Price { get; private set; }

        public Course(string name, double workload, string targetAudience, double price)
        {
            this.Name = name;
            this.Workload = workload;
            this.TargetAudience = targetAudience;
            this.Price = price;
        }
    }
}