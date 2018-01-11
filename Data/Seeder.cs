using Bogus;
using KG.Weather.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KG.Weather.Data
{
    public class Seeder
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public Seeder(
            ApplicationDbContext db, 
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedDb()
        {
            db.Database.EnsureCreated();

            await Cities();
            await Workers();
            await Users();
        }

        private async Task Cities()
        {
            if (await db.Cities.AnyAsync())
            {
                return;
            }

            db.Cities.AddOrUpdate(
                new List<City>
                {
                    new City { Id = 1, Name = "Montego Bay", Country = Country.Jamaica.Clone() },
                    new City { Id = 2, Name = "Kingston", Country = Country.Jamaica.Clone() },
                    new City { Id = 3, Name = "Ocho Rios", Country = Country.Jamaica.Clone() },
                    new City { Id = 4, Name = "Falmouth", Country = Country.Jamaica.Clone() }
                });

            await db.SaveChangesAsync();
        }

        private async Task Workers()
        {
            if(await db.Workers.AnyAsync())
            {
                return;
            }

            var faker = new Faker();

            db.Workers.AddOrUpdate(
                Enumerable.Range(1, 100)
                .ToList()
                .Select(id => new Person().ToWorker(id, 1)));

            db.Workers.AddOrUpdate(
                Enumerable.Range(101, 200)
                .ToList()
                .Select(id => new Person().ToWorker(id, 2)));

            db.Workers.AddOrUpdate(
                Enumerable.Range(301, 150)
                .ToList()
                .Select(id => new Person().ToWorker(id, 3)));

            db.Workers.AddOrUpdate(
                Enumerable.Range(451, 50)
                .ToList()
                .Select(id => new Person().ToWorker(id, 4)));

            await db.SaveChangesAsync();
        }

        private async Task Users()
        {
            var adminRole = "admin";
            var employerRole = "employer";

            if (!(await roleManager.Roles.AnyAsync(r => new string[] { adminRole, employerRole }.Contains(r.Name))))
            {

                await roleManager.CreateAsync(new IdentityRole("admin"));
                await roleManager.CreateAsync(new IdentityRole("employer"));
            }

            if ((await userManager.FindByNameAsync("admin")) == null)
            {
                var admin = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@kg.co"
                };

                var result1 = await userManager.CreateAsync(admin);

                if (result1.Succeeded)
                {
                    await userManager.AddPasswordAsync(admin, "password");
                    await userManager.AddToRoleAsync(admin, adminRole);
                }
            }

            if ((await userManager.FindByNameAsync("boss")) == null)
            {
                var boss = new IdentityUser
                {
                    UserName = "boss",
                    Email = "boss@kg.co"
                };

                var result2 = await userManager.CreateAsync(boss);

                if (result2.Succeeded)
                {
                    await userManager.AddPasswordAsync(boss, "password");
                    await userManager.AddToRoleAsync(boss, employerRole);
                }
            }
        }
    }



    public static class Extensions
    {
        public static void AddOrUpdate<T>(this DbSet<T> dbSet, IEnumerable<T> records)
            where T : BaseEntity
        {
            foreach (var data in records)
            {
                var exists = dbSet.AsNoTracking().Any(x => x.Id == data.Id);
                if (exists)
                {
                    dbSet.Update(data);
                }
                else
                {
                    dbSet.Add(data);
                }
            }
        }

        public static Worker ToWorker(this Person person, int workerId, int cityId)
        {
            return new Worker
            {
                Id = workerId,
                CityId = cityId,
                Email = person.Email,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Phone = person.Phone,
                Type = workerTypes[random.Next(workerTypes.Length)].Clone()
            };
        }

        private static Random random = new Random();

        private static WorkerType[] workerTypes = WorkerType.GetAll();
    }
}
