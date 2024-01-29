using Bogus;
using Moq;
using NuGet.Frameworks;
using OnlineUdemyCourse.Domain.Courses;
using OnlineUdemyCourse.Domain.Courses.Enums;
using OnlineUdemyCourse.DomainTest._Util;

namespace OnlineUdemyCourse.DomainTest.Courses
{
    public class CourseStoreTest
    {
        private readonly CourseDto _courseDto;

        private readonly CourseStore _courseStore;
        private readonly Mock<ICourseRepository> _courseRepositoryMock;

        public CourseStoreTest()
        {
            var faker = new Faker();

            _courseDto = new CourseDto
            {
                Name = faker.Company.CompanyName(),
                Workload = faker.Random.Double(80, 500),
                TargetAudience = "Student",
                Price = faker.Random.Double(380, 1800),
                Description = faker.Lorem.Paragraph(),
            };

            _courseRepositoryMock = new Mock<ICourseRepository>();
            _courseStore = new CourseStore(_courseRepositoryMock.Object);
        }

        [Fact]
        public void ShouldAddCourse()
        {
            _courseStore.Store(_courseDto);

            _courseRepositoryMock.Verify(x => x.Add(
                    It.Is<Course>(c => 
                        c.Name == _courseDto.Name &&
                        c.Description == _courseDto.Description
                    )
                )
            );
        }

        [Fact]
        public void SholdNotInformInvalidTargetAudience()
        {
            var invalidTargetAudience = "Doctor";
            _courseDto.TargetAudience = invalidTargetAudience;

            Assert.ThrowsAny<ArgumentException>(() => _courseStore.Store(_courseDto)).WithMessage("Target Audience Invalid.");
        }
    }

    public interface ICourseRepository
    {
        void Add(Course course);
    }

    public class CourseStore
    {
        private readonly ICourseRepository _courseRepository;

        public CourseStore(ICourseRepository courseRepository)
        {
            this._courseRepository = courseRepository;
        }

        public void Store(CourseDto courseDto)
        {
            Enum.TryParse(typeof(TargetAudience), courseDto.TargetAudience, out var targetAudience);

            if (targetAudience == null)
            {
                throw new ArgumentException("Target Audience Invalid.");
            }

            Course course = new Course(
                courseDto.Name, 
                courseDto.Description, 
                courseDto.Workload,
                (TargetAudience)targetAudience, 
                courseDto.Price);

            _courseRepository.Add(course);
        }
    }

    public class CourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Workload { get; set; }
        public string TargetAudience { get; set; }
        public double Price { get; set; }
    }
}
