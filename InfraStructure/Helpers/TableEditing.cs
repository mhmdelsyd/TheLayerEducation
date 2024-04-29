using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLayer.Core.Models.Identities;

namespace TheLayer.InfraStructure.Helpers
{
    public static class TableEditing
    {
        public static void EditTable(this ModelBuilder builder)
        {
            builder.Entity<Admin>().ToTable(nameof(Admin));
            builder.Entity<Teacher>().ToTable(nameof(Teacher));
            builder.Entity<Student>().ToTable(nameof(Student));
        }

    }
}