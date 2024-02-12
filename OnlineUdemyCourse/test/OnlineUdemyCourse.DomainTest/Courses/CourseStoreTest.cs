using Bogus;
using Moq;
using OnlineUdemyCourse.Domain.Courses;
using OnlineUdemyCourse.Domain.Courses._Builders;
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
        public void ShouldNotCreateCourseWithCourseNameAlreadyExisted()
        {
            var savedCourse = CourseBuilder.New().WithName(_courseDto.Name).Build();
            _courseRepositoryMock.Setup(x => x.GetByName(_courseDto.Name)).Returns(savedCourse);

            Assert.ThrowsAny<ArgumentException>(() => _courseStore.Store(_courseDto)).WithMessage("Course Name is already used.");
        }

        [Fact]
        public void ShouldNotInformInvalidTargetAudience()
        {
            var invalidTargetAudience = "Doctor";
            _courseDto.TargetAudience = invalidTargetAudience;

            Assert.ThrowsAny<ArgumentException>(() => _courseStore.Store(_courseDto)).WithMessage("Target Audience Invalid.");
        }
    }
}
