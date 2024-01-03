using ExpectedObjects;
using OnlineUdemyCourse.DomainTest._Util;

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
                TargetAudience = TargetAudience.Student,
                Price = (double)950
            };

            // Act
            var course = new Course(expectedCourse.Name, expectedCourse.Workload, expectedCourse.TargetAudience, expectedCourse.Price);

            // Assert
            expectedCourse.ToExpectedObject().ShouldMatch(course);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldNotCreateCourseWithInvalidName(string invalidName)
        {
            // Arrange
            var expectedCourse = new
            {
                Name = invalidName,
                Workload = (double)80,
                TargetAudience = TargetAudience.Student,
                Price = (double)950
            };

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() =>
                new Course(expectedCourse.Name, expectedCourse.Workload, expectedCourse.TargetAudience,
                    expectedCourse.Price)).WithMessage("Name cannot be empty!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ShouldNotCreateCourseWithWorkloadLessThan1(double invalidWorkload)
        {
            // Arrange
            var expectedCourse = new
            {
                Name = "Basic Computing",
                Workload = invalidWorkload,
                TargetAudience = TargetAudience.Student,
                Price = (double)950
            };

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() =>
                new Course(expectedCourse.Name, expectedCourse.Workload, expectedCourse.TargetAudience,
            expectedCourse.Price)).WithMessage("Workload cannot be less than '1'!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ShouldNotCreateCourseWithPriceLessThan1(double invalidPrice)
        {
            // Arrange
            var expectedCourse = new
            {
                Name = "Basic Computing",
                Workload = (double)80,
                TargetAudience = TargetAudience.Student,
                Price = invalidPrice
            };

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() =>
                new Course(expectedCourse.Name, expectedCourse.Workload, expectedCourse.TargetAudience,
                    expectedCourse.Price)).WithMessage("Price cannot be less than '1'!");
        }
    }

    public enum TargetAudience
    {
        Student,
        CollegeStudent,
        Employee,
        Entrepreneur
    }

    internal class Course
    {
        public string Name { get; private set; }
        public double Workload { get; private set; }
        public TargetAudience TargetAudience { get; private set; }
        public double Price { get; private set; }

        public Course(string name, double workload, TargetAudience targetAudience, double price)
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
            this.Workload = workload;
            this.TargetAudience = targetAudience;
            this.Price = price;
        }
    }
}