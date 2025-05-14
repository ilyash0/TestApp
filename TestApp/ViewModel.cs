using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp
{
    class ViewModel
    {
        public static List<Test> GetTestsForView()
        {
            using TestAppContext ctx = new();
            return ctx.Tests
                .Include(t => t.Subject)
                .Include(t => t.CreatedByNavigation)
                .ToList();
        }
    }
}
