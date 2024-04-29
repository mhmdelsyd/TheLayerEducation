using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLayer.Core.Models.Entities;

namespace TheLayer.InfraStructure.Helpers.Seed
{
    public static class CoursesSeeding
    {
        public static void SeedCourses(this ModelBuilder model)
        {
            model.Entity<Course>().HasData(
            new Course
            {
                Id = Guid.Parse("afdfe8f1-19ae-4249-8ec3-09b6f8cd54e1"),
                Name = "Arabic"
            },
            new Course
            {
                Id = Guid.Parse("312a839b-f953-4910-9cb2-d5eeb8cecdef"),
                Name = "English"
            },
            new Course
            {
                Id = Guid.Parse("ef41b6c7-9e80-4b25-ae55-4c42aa5100b0"),
                Name = "Math"
            },
            new Course
            {
                Id = Guid.Parse("e73d6110-79d7-4883-b718-35981e9eaa00"),
                Name = "Physics"

            },
            new Course
            {
                Id = Guid.Parse("80f1619b-4226-496b-9b16-7059536fbc40"),
                Name = "Anatomy"
            }
            );
        }
    }
}
