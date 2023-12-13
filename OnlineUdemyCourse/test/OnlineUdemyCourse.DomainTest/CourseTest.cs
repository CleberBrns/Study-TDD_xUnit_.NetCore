using ExpectedObjects;

namespace OnlineUdemyCourse.DomainTest
{
    public class CourseTest
    {
        [Fact]
        public void ShouldCreateCourse()
        {
            // Arrange
            var expectedCourse = new
            {
                Name = "Basic Computing",
                Workload = (double)80,
                TargetAudience = "Students",
                Price = (double)950
            };

            // Act
            var course = new Course(expectedCourse.Name, expectedCourse.Workload, expectedCourse.TargetAudience, expectedCourse.Price);

            // Assert
            expectedCourse.ToExpectedObject().ShouldMatch(course);
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