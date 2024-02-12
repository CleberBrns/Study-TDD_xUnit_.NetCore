using OnlineUdemyCourse.Domain.Courses.Enums;

namespace OnlineUdemyCourse.Domain.Courses
{
    public class CourseStore
    {
        private readonly ICourseRepository _courseRepository;

        public CourseStore(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Store(CourseDto courseDto)
        {
            var savedCourse = _courseRepository.GetByName(courseDto.Name);

            if (savedCourse != null)
                throw new ArgumentException("Course Name is already used.");

            if (!Enum.TryParse<TargetAudience>(courseDto.TargetAudience, out var targetAudience))
                throw new ArgumentException("Target Audience Invalid.");

            Course course = new (
                courseDto.Name,
                courseDto.Description,
                courseDto.Workload,
                targetAudience,
                courseDto.Price);

            _courseRepository.Add(course);
        }
    }
}
