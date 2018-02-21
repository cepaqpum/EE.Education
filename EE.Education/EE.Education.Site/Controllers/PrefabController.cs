﻿using EE.Education.Site.EF;
using EE.Education.Site.EF.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EE.Education.Site.Controllers
{
    public class PrefabController : Controller
    {
        private readonly EducationContext _context;

        public PrefabController(EducationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SetProtocol()
        {
            //Добавяем пользователя
            var teacher = new UserEntity
            {
                FirstName = "Евгений",
                LastName = "Елькин",
                MiddleName = "Сергеевич",
                Email = "evgeny.elkin@urfu.ru",
                IsTeacher = true
            };
            _context.Users.Add(teacher);

            //Добавляем курс
            var course = new CourseEntity
            {
                Name = "Протоколы интернет",
                Description = "Курс по изучению протоколов прикладного уровня ОСИ, ГРУППЫ КН-203",
                IsActive = true,
                Teachers = new[]
                {
                    new TeacherLink
                    {
                        Teacher = teacher
                    }
                }
            };
            _context.Courses.Add(course);

            //Добавляем группы задач
            _context.TaskGroups.AddRange(
                new TaskGroupEntity
                {
                    Course = course,
                    Name = "Низкий уровень",
                    Tasks = new[]
                    {
                        new TaskEntity {Name = "Трассировка автономных систем", Cost = 8, IsActive = false},
                        new TaskEntity {Name = "Сервер \"точного\" времени", Cost = 8, IsActive = false},
                        new TaskEntity {Name = "Сканер TCP и UDP портов", Cost = 5, IsActive = false}
                    }
                },
                new TaskGroupEntity
                {
                    Course = course,
                    Name = "Система доменных имён",
                    Tasks = new[]
                    {
                        new TaskEntity {Name = "Кэширующий DNS сервер", Cost = 20, IsActive = false}
                    }
                },
                new TaskGroupEntity
                {
                    Course = course,
                    Name = "Прикладные протоколы и API",
                    Tasks = new[]
                    {
                        new TaskEntity {Name = "SMTP клиент", Cost = 10, IsActive = false},
                        new TaskEntity {Name = "POP3 клиент", Cost = 10, IsActive = false},
                        new TaskEntity {Name = "HTTP proxy", Cost = 10, IsActive = false},
                        new TaskEntity {Name = "Использование HTTP API", Cost = 10, IsActive = false}
                    }
                });

            _context.SaveChanges();

            return Json(new {IsSuccess = true});
        }
    }
}
