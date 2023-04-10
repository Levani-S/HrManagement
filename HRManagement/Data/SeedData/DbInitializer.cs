using HRManagement.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HRManagement.Data.SeedData
{
    public class DbInitializer
    {
        public static void Initialize(HrManagementDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Genders.Count() > 0)
            {
                return;
            }
            var genders = new GenderModel[]
             {
            new GenderModel{GenderId= Guid.Parse("E4361419-3C2D-4DC8-AD92-18E423C9E0A6"), GenderName="Female"},
            new GenderModel{GenderId= Guid.Parse("0C5C21F2-EAB4-41D7-9EA4-87CB99BD0325"), GenderName="Male"},
             };
            foreach (GenderModel g in genders)
            {
                context.Genders.Add(g);
            }
            context.SaveChanges();
        }
    }
}
