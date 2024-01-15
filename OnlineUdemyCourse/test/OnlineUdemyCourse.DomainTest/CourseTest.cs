using Bogus;
using ExpectedObjects;
using OnlineUdemyCourse.Domain.Courses;
using OnlineUdemyCourse.Domain.Courses._Builders;
using OnlineUdemyCourse.Domain.Courses.Enums;
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
        private readonly string _description;

        public CourseTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            ShowMethodNameBeenExecute(null, "Constructor");

            var faker = new Faker();

            _name = faker.Company.CompanyName();
            _workload = faker.Random.Double(80, 500);
            _targetAudience = TargetAudience.Student;
            _price = faker.Random.Double(380, 1800);
            _description = faker.Lorem.Paragraph();
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
                Price = _price,
                Description = _description
            };

            // Act
            var course = new Course(expectedCourse.Name, expectedCourse.Description, expectedCourse.Workload, expectedCourse.TargetAudience, expectedCourse.Price);

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
}