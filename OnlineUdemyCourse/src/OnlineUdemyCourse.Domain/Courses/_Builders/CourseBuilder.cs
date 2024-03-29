﻿using OnlineUdemyCourse.Domain.Courses.Enums;

namespace OnlineUdemyCourse.Domain.Courses._Builders
{
    public class CourseBuilder
    {
        private string _name = "Basic Computing";
        private double _workload = 80;
        private TargetAudience _targetAudience = TargetAudience.Student;
        private double _price = 950;
        private string _description = "Course Description";

        public static CourseBuilder New()
        {
            return new CourseBuilder();
        }

        public CourseBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CourseBuilder WithWorkload(double workload)
        {
            _workload = workload;
            return this;
        }

        public CourseBuilder WithTargetAudience(TargetAudience targetAudience)
        {
            _targetAudience = targetAudience;
            return this;
        }

        public CourseBuilder WithPrice(double price)
        {
            _price = price;
            return this;
        }

        public Course Build()
        {
            return new Course(_name, _description, _workload, _targetAudience, _price);
        }

    }
}
