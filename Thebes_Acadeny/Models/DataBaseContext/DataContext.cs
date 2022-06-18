using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models.Entity;

namespace Thebes_Acadeny.Models.DataBaseContext
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
                
        }

        public DbSet<Level> Levels { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Books> BooksAdmins { get; set; }
        public DbSet<Categores> Categores { get; set; }
        public DbSet<Exam> ExamAdmins { get; set; }
        public DbSet<MessagesUser> MessagesUsers { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Posts> PostsAdmins { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Specialties> Specialties { get; set; }
        public DbSet<Video> VideoAdmins { get; set; }
        public DbSet<Tearm> Tearms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Level>().HasData
            (

                new Level
                {
                    LevelName = "اختر الفرقه",
                    LevelId = -1,
                },
                new Level
                {
                    LevelName = "الفرقه الاولي",
                    LevelId = 1,
                },
                new Level
                {
                    LevelName = "الفرقه الثانيه",
                    LevelId = 2
                },
                new Level
                {
                    LevelName = "الفرقه الثالثه",
                    LevelId = 3
                },
                new Level
                {
                    LevelName = "الفرقه الرابعه",
                    LevelId = 4
                }
            );

            modelBuilder.Entity<Plant>().HasData(new Plant { PlantName = "اختر ماده", PlantId = -1, LevelIdFk = 1, SpecialtiesIdFk = 1 });

            modelBuilder.Entity<Specialties>().HasData
                (
                    new Specialties
                    {
                        SpecialtiesName = "اختر التخصص",
                        SpecialtiesId = -1,
                    },
                    new Specialties
                    {
                        SpecialtiesName = "نظم المعلومات الاداريه",
                        SpecialtiesId = 1,
                    }
                );


            modelBuilder.Entity<Exam>().HasData
                (
                    new Exam
                    {
                        ExamId = -1,
                        Title = "اختر امتحان",
                        AdminIdFk = -92539587,
                        Assent = true,

                    }
                );


            modelBuilder.Entity<Admin>().HasData
                (
                    new Admin
                    {
                        FullName = "Mohamed Ali",
                        ImageUrl = "InShot_20211111_204615744.jpg",
                        Password = "nd112004",
                        UserName = "mo112004",
                        WhatsApp = "Https://api.whatsapp.com/send?phone=0201159252091",
                        AdminId = -92539587,
                        GategoryIdFk = 1,
                        LevelIdFk = 2,
                        SpecialtiesId = 1,
                        PhoneNumper = "01159252091"
                    }
                );

            modelBuilder.Entity<Categores>().HasData
                (
                   new Categores
                   {
                       CategoryName = "اختر الفئه",
                       GategoryId = -1,
                   },

                   new Categores
                   {
                       CategoryName = "Owner",
                       GategoryId = 1,
                   },

                   new Categores
                   {
                       CategoryName = "AdminShimaa",
                       GategoryId = 2,
                   },

                    new Categores
                    {
                        CategoryName = "Admin",
                        GategoryId = 3,
                    }
                );
            modelBuilder.Entity<Tearm>().HasData
                (
                    new Tearm
                    {
                        Id = 1,
                        Name = "الترم الاول"
                    },
                    new Tearm
                    {
                        Id = 2,
                        Name = "الترم الثاني"
                    }
                );
            modelBuilder.Entity<UrlWebSite>().HasData(new UrlWebSite { UrlId = 1, UrlText = "https://localhost:44347/" });
            modelBuilder.Entity<Question>().Property<string>("img");
            modelBuilder.Entity<Question_True_or_false>().Property<string>("img");
            base.OnModelCreating(modelBuilder);
        }
    }
}
