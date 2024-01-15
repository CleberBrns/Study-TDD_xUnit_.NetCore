using ExpectedObjects;
using OnlineUdemyCourse.DomainTest._Builders;
using OnlineUdemyCourse.DomainTest._Util;
using System.Reflection;
using Xunit.Abstractions;

namespace OnlineUdemyCourse.DomainTest
{
    public class CourseTest : IDisposable
    {
        private readonly ITestOutputHelper _outputHelper;

        private readonly string _name;
        private readonly double _workload;
        private readonly TargetAudience _targetAudience;
        private readonly double _price;

        public CourseTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            ShowMethodNameBeenExecute(null, "Constructor");

            _name = "Basic Computing";
            _workload = 80;
            _targetAudience = TargetAudience.Student;
            _price = 950;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            ShowMethodNameBeenExecute(MethodBase.GetCurrentMethod());
        }

        private void ShowMethodNameBeenExecute(MethodBase? methodBase, string? methodName = null)
        {
            if (methodBase != null)
            {
                _outputHelper.WriteLine($"{methodBase.Name} been executed.");
            }
            else if (!string.IsNullOrEmpty(methodName))
            {
                _outputHelper.WriteLine($"{methodName} been executed.");
            }
        }

        [Fact]
        public void ShouldCreateCourse()
        {
            ShowMethodNameBeenExecute(MethodBase.GetCurrentMethod());

            // Arrange
            var expectedCourse = new
            {
                Name = _name,
                Workload = _workload,
                TargetAudience = _targetAudience,
                Price = _price
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
            ShowMethodNameBeenExecute(MethodBase.GetCurrentMethod());

            // Arrange

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() =>
            CourseBuilder.New().WithName(invalidName).Build())
                .WithMessage("Name cannot be empty!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ShouldNotCreateCourseWithWorkloadLessThan1(double invalidWorkload)
        {
            ShowMethodNameBeenExecute(MethodBase.GetCurrentMethod());

            // Arrange

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() =>
            CourseBuilder.New().WithWorkload(invalidWorkload).Build())
                .WithMessage("Workload cannot be less than '1'!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void ShouldNotCreateCourseWithPriceLessThan1(double invalidPrice)
        {
            ShowMethodNameBeenExecute(MethodBase.GetCurrentMethod());

            // Arrange

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() =>
            CourseBuilder.New().WithPrice(invalidPrice).Build())
                .WithMessage("Price cannot be less than '1'!");
        }
    }

    public enum TargetAudience
    {
        Student,
        CollegeStudent,
        Employee,
        Entrepreneur
    }

    public class Course
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