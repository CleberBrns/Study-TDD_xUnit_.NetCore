using Moq;
using OnlineUdemyCourse.Domain.Courses;

namespace OnlineUdemyCourse.DomainTest.Courses
{
    public class CourseStoreTest
    {
        [Fact]
        public void ShouldAddCourse()
        {
            var courseDto = new CourseDto
            {
                Name = "Course A",
                Description = "Description",
                Workload = 80,
                TargetAudience = 1,
                Price = 850.00
            };

            var courseRepositoryMock = new Mock<ICourseRepository>();

            var courseStore = new CourseStore(courseRepositoryMock.Object);

            courseStore.Store(courseDto);

            courseRepositoryMock.Verify(x => x.Add(It.IsAny<Course>()));
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
            Course course = new Course(
                courseDto.Name, 
                courseDto.Description, 
                courseDto.Workload, 
                Domain.Courses.Enums.TargetAudience.Student, 
                courseDto.Price);

            _courseRepository.Add(course);
        }
    }

    public class CourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Workload { get; set; }
        public int TargetAudience { get; set; }
        public double Price { get; set; }
    }
}
