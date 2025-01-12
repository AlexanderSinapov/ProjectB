using Microsoft.AspNetCore.Mvc;
using ProjectB.Models;

namespace ProjectB.Controllers
{
    public class TestController : Controller
    {
        private readonly List<TestQuestion> _questions = new List<TestQuestion>
        {
            new TestQuestion
            {
                Id = 1,
                WorkType = "onegin",
                Question = "Кой е автор на 'Евгений Онегин'?",
                Options = new List<string>
                {
                    "Лев Толстой",
                    "Александър Пушкин",
                    "Фьодор Достоевски",
                    "Николай Гогол"
                },
                CorrectAnswer = 1
            },
            new TestQuestion
            {
                Id = 2,
                WorkType = "onegin",
                Question = "Кой жанр най-добре описва 'Евгений Онегин'?",
                Options = new List<string>
                {
                    "Роман в стихове",
                    "Повест",
                    "Трагедия",
                    "Новела"
                },
                CorrectAnswer = 0
            },
            new TestQuestion
            {
                Id = 3,
                WorkType = "onegin",
                Question = "Коя е основната причина за дуела между Онегин и Ленски?",
                Options = new List<string>
                {
                    "Политически спор",
                    "Онегин танцува с Олга на бал",
                    "Завист към успехите на Ленски",
                    "Финансов конфликт"
                },
                CorrectAnswer = 1
            },
            new TestQuestion
            {
                Id = 4,
                WorkType = "onegin",
                Question = "Как Татяна изразява своите чувства към Онегин?",
                Options = new List<string>
                {
                    "Пише му писмо",
                    "Изповядва се пред приятелка",
                    "Изразява любовта си на публичен бал",
                    "Избягва срещи с него"
                },
                CorrectAnswer = 0
            },
            new TestQuestion
            {
                Id = 5,
                WorkType = "onegin",
                Question = "Какво намира Татяна в библиотеката на Онегин?",
                Options = new List<string>
                {
                    "Писмо до Ленски",
                    "Колекция от романтични стихотворения",
                    "Книги, отразяващи неговата философия",
                    "Скрито любовно писмо"
                },
                CorrectAnswer = 2
            },
            new TestQuestion
            {
                Id = 6,
                WorkType = "onegin",
                Question = "Какво символизира дуелът в романа?",
                Options = new List<string>
                {
                    "Конфликт между любов и дълг",
                    "Безсмислието на мъжките честолюбия",
                    "Борба за власт",
                    "Жертвата в името на приятелството"
                },
                CorrectAnswer = 1
            },
            new TestQuestion
            {
                Id = 7,
                WorkType = "onegin",
                Question = "Каква е основната тема на романа?",
                Options = new List<string>
                {
                    "Природата и нейната красота",
                    "Любовта и нейната трагедия",
                    "Изгубените възможности",
                    "Социалната несправедливост"
                },
                CorrectAnswer = 2
            },
            new TestQuestion
            {
                Id = 8,
                WorkType = "goriot",
                Question = "Кой е автор на 'Дядо Горио'?",
                Options = new List<string>
                {
                    "Оноре дьо Балзак",
                    "Емил Зола",
                    "Виктор Юго",
                    "Гюстав Флобер"
                },
                CorrectAnswer = 0
            },
            new TestQuestion
            {
                Id = 9,
                WorkType = "goriot",
                Question = "Какъв жанр е 'Дядо Горио'?",
                Options = new List<string>
                {
                    "Исторически роман",
                    "Социален роман",
                    "Философски роман",
                    "Поема"
                },
                CorrectAnswer = 1
            },
            new TestQuestion
            {
                Id = 10,
                WorkType = "goriot",
                Question = "Кой е Южен дьо Растиняк?",
                Options = new List<string>
                {
                    "Студент по право",
                    "Доктор",
                    "Приятел на дъщерите на Горио",
                    "Изследовател"
                },
                CorrectAnswer = 0
            },
            new TestQuestion
            {
                Id = 11,
                WorkType = "bovary",
                Question = "Кой е авторът на 'Мадам Бовари'?",
                Options = new List<string>
                {
                    "Емил Зола",
                    "Оноре дьо Балзак",
                    "Гюстав Флобер",
                    "Виктор Юго"
                },
                CorrectAnswer = 2
            },
            new TestQuestion
            {
                Id = 12,
                WorkType = "bovary",
                Question = "Как Ема Бовари се опитва да намери щастие извън брака?",
                Options = new List<string>
                {
                    "Чрез любовни афери",
                    "Като се занимава с благотворителност",
                    "Като се мести в Париж",
                    "Чрез четене на книги"
                },
                CorrectAnswer = 0
            },
            new TestQuestion
            {
                Id = 13,
                WorkType = "bovary",
                Question = "Как завършва животът на Ема Бовари?",
                Options = new List<string>
                {
                    "Заминава за Париж",
                    "Става монахиня",
                    "Умира от отравяне",
                    "Живее в самота"
                },
                CorrectAnswer = 2
            },

        };

        [HttpGet]
        public IActionResult GetTest(string work)
        {
            var questions = _questions
                .Where(q => q.WorkType.Equals(work, StringComparison.OrdinalIgnoreCase))
                .Select(q => new
                {
                    id = q.Id,
                    question = q.Question,
                    options = q.Options
                })
                .ToList();

            return Json(questions);
        }

        [HttpPost]
        public IActionResult SubmitTest([FromBody] Dictionary<int, int> answers)
        {
            if (answers == null || !answers.Any())
            {
                return BadRequest("No answers provided");
            }

            int correctAnswers = answers.Count(answer =>
                _questions.FirstOrDefault(q => q.Id == answer.Key)?.CorrectAnswer == answer.Value
            );

            double score = (double)correctAnswers / answers.Count * 100;

            return Json(new { score = Math.Round(score, 1) });
        }
    }
}