using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Others;
using Application.ViewModels.AnswerViewModel;
using Application.ViewModels.CorrectTestViewModel;
using Application.ViewModels.OrderViewModel;
using Application.ViewModels.QuestionViewModel;
using Application.ViewModels.TeacherViewModel;
using Application.ViewModels.TestViewModel;
using Application.ViewModels.WorkBookViewModel;
using Domin.Interfaces;
using Domin.Models;

namespace Application.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IWorkBookRepository _workBookRepository;
        private readonly IOrderRepository _orderRepository;


        public TestService(ITestRepository testRepository,IAnswerRepository answerRepository,IStudentRepository studentRepository
            ,IQuestionRepository questionRepository,ILessonRepository lessonRepository,IWorkBookRepository workBookRepository,IOrderRepository orderRepository)
        {
            _testRepository = testRepository;
            _answerRepository = answerRepository;
            _studentRepository = studentRepository;
            _questionRepository = questionRepository;
            _lessonRepository = lessonRepository;
            _workBookRepository = workBookRepository;
            _orderRepository = orderRepository;
        }

        public Tuple<List<TestsViewModel>, int, int> GetTestList(int page = 1)
        {
            List<TestsViewModel> test = new List<TestsViewModel>();
            var list = _testRepository.GetTestList().Result.ToList();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count, 10);
            int skip = (page - 1) * 10;
            var resultList = list.Skip(skip).Take(10).ToList();
            foreach (var item in resultList)
            {
                var questionCounts = _testRepository.GetAllQuestionCountsById(item.TestId);
                test.Add(new TestsViewModel()
                {
                    TestTitle = item.TestTitle,
                    TestDayTime = item.TestDayTime.ToString("yyyy/MM/dd"),
                    TestId = item.TestId,
                    StartTime = item.StartTest.ToString("HH:mm"),
                    EndTime = item.EndTest.ToString("HH:mm"),
                    TestFile = item.TestFile,
                    TestCode = item.TestCode,
                    TestDuration = item.TestDuration.ToString(),
                    GradeId = item.GradeId,
                    QuestionCounts = questionCounts,
                    LessonId = item.LessonId,
                    TestPrice = item.TestPrice.ToString("#,0")
                });
            }
            return Tuple.Create(test, pageCount, pageNumber);
        }
        public Tuple<List<TestsViewModel>, int, int> GetTestList(string? testTitle, string? testCode, string? fromDate, string? toDate, string IsFinish, int page = 1)
        {
            var startDate = Convert.ToDateTime(fromDate);
            var endDate = Convert.ToDateTime(toDate);
            List<TestsViewModel> test = new List<TestsViewModel>();
            var list = _testRepository.GetTestFilterList(testTitle, testCode, startDate, endDate, IsFinish).Result.ToList();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count, 6);
            int skip = (page - 1) * 6;
            var resultList = list.Skip(skip).Take(6).ToList();
            foreach (var item in resultList)
            {
                var questionCounts = _testRepository.GetAllQuestionCountsById(item.TestId);
                test.Add(new TestsViewModel()
                {
                    TestTitle = item.TestTitle,
                    TestDayTime = item.TestDayTime.ToString("yyyy/MM/dd"),
                    TestId = item.TestId,
                    StartTime = item.StartTest.ToString("HH:mm"),
                    EndTime = item.EndTest.ToString("HH:mm"),
                    TestDuration = item.TestDuration.ToString(),
                    TestFile = item.TestFile,
                    TestCode = item.TestCode,
                    QuestionCounts = questionCounts,
                    LessonId = item.LessonId,
                    TestPrice = item.TestPrice.ToString("#,0")
                });
            }
            return Tuple.Create(test, pageCount, pageNumber);
        }
        public Tuple<List<TestsViewModel>, int, int> GetDeletedTestList(int page = 1)
        {
            List<TestsViewModel> test = new List<TestsViewModel>();
            var list = _testRepository.GetDeletedTestList().Result.ToList(); 
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count, 6);
            int skip = (page - 1) * 6;
            var resultList = list.Skip(skip).Take(6).ToList();
            foreach (var item in resultList)
            {
                var questionCounts = _testRepository.GetAllQuestionCountsById(item.TestId);
                test.Add(new TestsViewModel()
                {
                    TestTitle = item.TestTitle,
                    TestDayTime = item.TestDayTime.ToString("yyyy/MM/dd"),
                    TestId = item.TestId,
                    StartTime = item.StartTest.ToString("HH:mm"),
                    EndTime = item.EndTest.ToString("HH:mm"),
                    TestDuration = item.TestDuration.ToString(),
                    TestFile = item.TestFile,
                    TestCode = item.TestCode,
                    QuestionCounts = questionCounts,
                    TestPrice = item.TestPrice.ToString("#,0")
                });
            }
            return Tuple.Create(test, pageCount, pageNumber);
        }
        public Tuple<List<TestsViewModel>, int, int> GetDeletedTestList(string? testTitle, string? testCode, string? fromDate, string? toDate, string IsFinish, int page = 1)
        {
            var startDate = Convert.ToDateTime(fromDate);
            var endDate = Convert.ToDateTime(toDate);
            List<TestsViewModel> test = new List<TestsViewModel>();
            var list = _testRepository.GetDeletedTestFilterList(testTitle, testCode, startDate, endDate, IsFinish).Result.ToList();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count, 6);
            int skip = (page - 1) * 6;
            var resultList = list.Skip(skip).Take(6).ToList();
            foreach (var item in resultList)
            {
                var questionCounts = _testRepository.GetAllQuestionCountsById(item.TestId);
                test.Add(new TestsViewModel()
                {
                    TestTitle = item.TestTitle,
                    TestDayTime = item.TestDayTime.ToString("yyyy/MM/dd"),
                    TestId = item.TestId,
                    StartTime = item.StartTest.ToString("HH:mm"),
                    EndTime = item.EndTest.ToString("HH:mm"),
                    TestDuration = item.TestDuration.ToString(),
                    TestFile = item.TestFile,
                    TestCode = item.TestCode,
                    QuestionCounts = questionCounts,
                    TestPrice = item.TestPrice.ToString("#,0")
                });
            }
            return Tuple.Create(test, pageCount, pageNumber);
        }
        public async Task<EditTestViewModel> GetTestById(int testId)
        {
            var allTestCount = _testRepository.GetAllQuestionCountsById(testId);
            var testCount = _testRepository.GetQuestionCountsById(testId);
            var descriptiveTestCount = _testRepository.GetDescriptiveQuestionCountsById(testId);
            var test = await _testRepository.GetTestById(testId);
            EditTestViewModel model = new EditTestViewModel();
            model.TestDescription = test.TestDescription;
            model.TestTitle = test.TestTitle;
            model.TestDayTime = test.TestDayTime.ToString("yyyy/MM/dd");
            model.FilePath = test.TestFile;
            model.TeacherId = test.TeacherId;
            model.GradeId = test.GradeId;
            model.LessonId = test.LessonId;
            model.StartTest = test.StartTest.ToString("HH:mm");
            model.EndTest = test.EndTest.ToString("HH:mm");
            model.TestDuration = test.TestDuration.ToString();
            model.TestId = test.TestId;
            model.TestCode = test.TestCode;
            model.QuestionCounts = testCount;
            model.DescriptiveQuestionCounts = descriptiveTestCount;
            model.AllQuestionCounts = allTestCount;
            model.NegativePoint = test.NegativePoint;
            model.IsComprehensiveTest = test.IsComprehensiveTest;
            model.TestPrice = test.TestPrice;
            return model;
        }
        public List<ItemGradeViewModel> GetGradeList()
        {
            var list = _testRepository.GetGradeItems().Result;
            List<ItemGradeViewModel> items = new List<ItemGradeViewModel>();
            foreach (var item in list)
            {
                items.Add(new ItemGradeViewModel()
                {
                    ItemGradeId = item.ItemGradeId,
                    ItemGradeName = item.ItemGradeName
                });
            }

            return items;
        }

        public List<ItemLessonViewModel> GetLessonList(int gradeId)
        {
            var list = _testRepository.GetLessonItems(gradeId).Result;
            List<ItemLessonViewModel> items = new List<ItemLessonViewModel>();
            foreach (var item in list)
            {
                items.Add(new ItemLessonViewModel()
                {
                    LessonId = item.LessonId,
                    LessonName = item.LessonName
                });
            }

            return items;
        }
        public List<ItemLessonViewModel> GetLessonList(int gradeId, int lessonId)
        {
            var list = _testRepository.GetLessonItems(gradeId, lessonId).Result;
            List<ItemLessonViewModel> items = new List<ItemLessonViewModel>();
            foreach (var item in list)
            {
                items.Add(new ItemLessonViewModel()
                {
                    LessonId = item.LessonId,
                    LessonName = item.LessonName
                });
            }

            return items;
        }
        public List<ItemTeacherViewModel> GetTeacherList(int lessonId)
        {
            var list = _testRepository.GetTeacherList(lessonId).Result;
            List<ItemTeacherViewModel> items = new List<ItemTeacherViewModel>();
            foreach (var item in list)
            {
                items.Add(new ItemTeacherViewModel()
                {
                    TeacherId = item.TeacherId,
                    TeacherName = item.TeacherName
                });
            }
            return items;
        }

        public void CreateTest(AddTestViewModel test)
        {
            Random random = new Random();
            TestModel model = new TestModel();
            model.TestDescription = test.TestDescription;
            model.TestTitle = test.TestTitle;
            model.TestDayTime = test.TestDayTime;
            if (test.TestFile != null)
            {
                model.TestFile = FileConvertor.SaveFile(test.TestFile);
            }
            model.TeacherId = test.TeacherId;
            model.GradeId = test.GradeId;
            model.TestCode = Guid.NewGuid().ToString();
            model.LessonId = test.LessonId;
            model.StartTest = Convert.ToDateTime(test.StartTest);
            model.EndTest = Convert.ToDateTime(test.EndTest);
            model.TestDuration = TimeSpan.Parse(test.TestDuration);
            model.TestCode = random.Next(100000, 999999).ToString();
            model.NegativePoint = test.NegativePoint;
            model.IsComprehensiveTest = test.IsComprehensiveTest;
            model.TestPrice = test.TestPrice;
            _testRepository.CreateTest(model);
        }

        public void EditTest(EditTestViewModel test)
        {
            var model = _testRepository.GetTestById(test.TestId).Result;
            model.TestDescription = test.TestDescription;
            model.TestTitle = test.TestTitle;
            model.TestDayTime = Convert.ToDateTime(test.TestDayTime);
            model.TeacherId = test.TeacherId;
            model.GradeId = test.GradeId;
            model.LessonId = test.LessonId;
            model.StartTest = Convert.ToDateTime(test.StartTest);
            model.EndTest = Convert.ToDateTime(test.EndTest);
            model.TestDuration = TimeSpan.Parse(test.TestDuration);
            model.TestId = test.TestId;
            model.TestCode = test.TestCode;
            model.NegativePoint = test.NegativePoint;
            model.IsComprehensiveTest = test.IsComprehensiveTest;
            model.Finish = false;
            model.TestPrice = test.TestPrice;
            if (test.TestFile != null)
            {
                FileConvertor.RemoveFile(model.TestFile);
                model.TestFile = FileConvertor.SaveFile(test.TestFile);
            }
            _testRepository.EditTest(model);
        }
        public void AddToTrash(int testId)
        {
            var model = _testRepository.GetTestById(testId).Result;
            model.IsDelete = true;
            _testRepository.EditTest(model);
        }

        public void BackToList(int testId)
        {
            var model = _testRepository.GetDeletedTestById(testId).Result;
            model.IsDelete = false;
            _testRepository.EditTest(model);
        }
        public void DeleteTest(int testId)
        {
            var model = _testRepository.GetDeletedTestById(testId).Result;
            if (model.TestFile != null)
            {
                FileConvertor.RemoveFile(model.TestFile);
            }
            _testRepository.RemoveTest(model);
        }
        public async Task<IEnumerable<TestStudentsViewModel>> GetTestStudents(int gradeId)
        {
            var testStudents = await _testRepository.GetTestStudents(gradeId);
            List<TestStudentsViewModel> model = new List<TestStudentsViewModel>();
            foreach (var student in testStudents)
            {
                model.Add(new TestStudentsViewModel()
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName
                });
            }
            return model;
        }

        public async Task<SelectStudentViewModel> GetTestStudentsById(int testId)
        {
            var testStudents = await _testRepository.GetTestStudentById(testId);
            SelectStudentViewModel model = new SelectStudentViewModel();
            List<int> items = new List<int>();
            foreach (var student in testStudents)
            {
                items.Add(student.StudentId);
                model.TestId = student.TestId;
                model.items = items;
            }
            return model;
        }
        public async Task<bool> UpdateEnterCount(int testId, int studentId)
        {
            var testStudent = await _testRepository.GetTestStudentByIds(studentId,testId);
            testStudent.EnterCount++;
            _testRepository.UpdateTestStudents(testStudent);
            if (testStudent.EnterCount <= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void UpdateTestStudents(SelectStudentViewModel model)
        {
            var students = _testRepository.GetTestStudentById(model.TestId).Result;
            foreach (var student in students)
            {
               _testRepository.RemoveTestStudent(student); 
            }
            if (model.items != null)
            {
                foreach (var studentId in model.items)
                {
                    TestStudentsModel testStudent = new TestStudentsModel();
                    testStudent.StudentId = studentId;
                    testStudent.TestId = model.TestId;
                    _testRepository.AddTestStudents(testStudent);
                }
            }
        }

        public Tuple<List<TestsViewModel>, int, int> GetStudentTests(int studentId, int lessonId,string search,bool IsFinished,int page=1)
        {
            List<TestsViewModel> test = new List<TestsViewModel>();
            var list = _testRepository.GetFilterTestsByLesson(studentId,lessonId,search,IsFinished).Result.ToList();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count, 10);
            int skip = (page - 1) * 10;
            var resultList = list.Skip(skip).Take(10).ToList();
            foreach (var item in resultList)
            {
                string sDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string dtNow = DateTime.Now.ToShamsi();
                DateTime date = DateTime.ParseExact(sDate, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime nowDt = DateTime.ParseExact(dtNow, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                var testCounts = _testRepository.GetQuestionCountsById(item.TestId);
                bool IsSubmitAnswer = _answerRepository.IsSubmitAnswer(studentId,item.TestId).Result;
                bool IsStartedExam = _testRepository.IsStartedExam(item.TestId,date, nowDt).Result;
                bool IsFinish = _testRepository.IsFinishedExam(item.TestId,date, nowDt).Result;
                bool IsBuyTest = _orderRepository.CheckByTest(studentId,item.TestId).Result;
                if (IsFinish)
                {
                    item.Finish = true;
                    _testRepository.EditTest(item);
                }
                test.Add(new TestsViewModel()
                {
                    TestTitle = item.TestTitle,
                    TestDayTime = item.TestDayTime.ToString("yyyy/MM/dd"),
                    TestId = item.TestId,
                    StartTime = item.StartTest.ToString("HH:mm"),
                    EndTime = item.EndTest.ToString("HH:mm"),
                    TestFile = item.TestFile,
                    GradeId = item.GradeId,
                    TestDuration = item.TestDuration.ToString(),
                    QuestionCounts = testCounts,
                    IsSubmitAnswer = IsSubmitAnswer,
                    IsStartedExam = IsStartedExam,
                    IsFinish = IsFinish,
                    TestPrice = item.TestPrice.ToString("#,0"),
                    IsBuyTest = IsBuyTest
                });
            }
            return Tuple.Create(test, pageCount, pageNumber);
        }
        public Tuple<List<TestsViewModel>, int, int> GetBoughtStudentTests(int studentId, string search, bool IsFinished, int page = 1)
        {
            List<TestsViewModel> test = new List<TestsViewModel>();
            var list = _testRepository.GetFilterBoughtTests(studentId, search, IsFinished).Result.ToList();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(list.Count, 10);
            int skip = (page - 1) * 10;
            var resultList = list.Skip(skip).Take(10).ToList();
            foreach (var item in resultList)
            {
                string sDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string dtNow = DateTime.Now.ToShamsi();
                //DateTime date = DateTime.ParseExact(sDate, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime date = DateConvert.ToMiladiDateTime(sDate);
                //DateTime nowDt = DateTime.ParseExact(dtNow, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                DateTime nowDt = DateConvert.ToMiladiDateTime(dtNow);
                var testCounts = _testRepository.GetQuestionCountsById(item.TestId);
                bool IsSubmitAnswer = _answerRepository.IsSubmitAnswer(studentId, item.TestId).Result;
                bool IsStartedExam = _testRepository.IsStartedExam(item.TestId, date, nowDt).Result;
                bool IsFinish = _testRepository.IsFinishedExam(item.TestId, date, nowDt).Result;
                if (IsFinish)
                {
                    item.Finish = true;
                    _testRepository.EditTest(item);
                }
                test.Add(new TestsViewModel()
                {
                    TestTitle = item.TestTitle,
                    TestDayTime = item.TestDayTime.ToString("yyyy/MM/dd"),
                    TestId = item.TestId,
                    StartTime = item.StartTest.ToString("HH:mm"),
                    EndTime = item.EndTest.ToString("HH:mm"),
                    TestFile = item.TestFile,
                    GradeId = item.GradeId,
                    TestDuration = item.TestDuration.ToString(),
                    QuestionCounts = testCounts,
                    IsSubmitAnswer = IsSubmitAnswer,
                    IsStartedExam = IsStartedExam,
                    IsFinish = IsFinish,
                    TestPrice = item.TestPrice.ToString("#,0"),
                    LessonId = item.LessonId
                });
            }
            return Tuple.Create(test, pageCount, pageNumber);
        }
        public async Task<List<KeyTestViewModel>> GetKeyTestsByTestId(int testId)
        {
            var questions = await _testRepository.GetQuestionsByTestId(testId);
            List<KeyTestViewModel> models = new List<KeyTestViewModel>();
            foreach (var question in questions)
            {
                models.Add(new KeyTestViewModel()
                {
                    QuestionNumber = question.QuestionNumber,
                    KeyAnswer = question.TestKeyAnswer
                });
            }

            return models;
        }

        #region Participants

        public Tuple<List<ParticipantsViewModel>, int, int> GetParticipants(int testId, string search = "", int page = 1)
        {
            var students =  _testRepository.GetParticipants(testId).Result.ToList();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(students.Count, 6);
            int skip = (page - 1) * 6;
            var studentList = students.Skip(skip).Take(6).ToList();
            List<ParticipantsViewModel> models = new List<ParticipantsViewModel>();
            foreach (var student in studentList)
            {
                var studentModel = _studentRepository.GetStudentById(student.StudentId,search).Result;
                var IsSubmitAnswer = _answerRepository.IsSubmitAnswer(student.StudentId, testId).Result;
                bool IsSubmitWorkBook = _workBookRepository.IsSubmitWorkBook(student.StudentId,testId).Result;
                if (studentModel != null)
                {
                    models.Add(new ParticipantsViewModel()
                    {
                        Grade = studentModel.Grade.GradeName,
                        IsSubmitAnswer = IsSubmitAnswer,
                        NationalCode = studentModel.StudentNationalCode,
                        StudentId = studentModel.StudentId,
                        StudentImage = studentModel.StudentAvatar,
                        StudentName = studentModel.StudentName,
                        IsSubmitWorkBook = IsSubmitWorkBook
                    });
                }
                
            }
            return Tuple.Create(models,pageCount,pageNumber);
        }

        public Tuple<List<AnswerViewModel>,List<AnswerViewModel>,List<QuestionViewModel>,List<QuestionViewModel>,string, List<CorrectTestViewModel>> CorrectTest(int testId, int studentId, List<string>? descriptiveScore)
        {
            //Get Answers And Questions
            var answersList = _answerRepository.GetAnswersForStudent(studentId, testId).Result;
            var descriptiveAnswerList = _answerRepository.GetDescriptiveAnswersForStudent(studentId, testId).Result;
            var questionList = _questionRepository.GetTestQuestions(testId).Result;
            var descriptiveQuestionList = _questionRepository.GetAllDescriptiveQuestion(testId).Result;
            var fileAddress = _answerRepository.GetAnswerFile(studentId, testId);
            List<AnswerViewModel> answers = new List<AnswerViewModel>();
            List<AnswerViewModel> descriptiveAnswers = new List<AnswerViewModel>();
            List<QuestionViewModel> questions = new List<QuestionViewModel>();
            List<QuestionViewModel> descriptiveQuestions = new List<QuestionViewModel>();
            foreach (var answer in answersList)
            {
                answers.Add(new AnswerViewModel()
                {
                    StudentId = answer.StudentId,
                    TestId = answer.TestId,
                    AnswerId = answer.AnswerId,
                    AnswerChecked = answer.AnswerChecked,
                    AnswerNumber = answer.AnswerNumber
                });
            }

            foreach (var answer in descriptiveAnswerList)
            {
                descriptiveAnswers.Add(new AnswerViewModel()
                {
                    StudentId = answer.StudentId,
                    TestId = answer.TestId,
                    AnswerId = answer.AnswerId,
                    AnswerContext = answer.AnswerContext,
                    AnswerNumber = answer.AnswerNumber,
                    AnswerFile = answer.AnswerFile,
                });
            }

            foreach (var question in questionList)
            {
                questions.Add(new QuestionViewModel()
                {
                    Id = question.Id,
                    QuestionNumber = question.QuestionNumber.ToString(),
                    KeyAnswer = question.TestKeyAnswer
                });
            }

            foreach (var question in descriptiveQuestionList)
            {
                descriptiveQuestions.Add(new QuestionViewModel()
                {
                    Id = question.Id,
                    QuestionNumber = question.QuestionNumber.ToString(),
                    KeyAnswer = question.TestKeyAnswer,
                    QuestionContext = question.Question,
                    Score = question.Score
                });
            }
            int allTestCounts = _testRepository.GetAllQuestionCountsById(testId);
            int testCounts = _testRepository.GetQuestionCountsById(testId);
            List<string> descriptiveScores = descriptiveScore;
            List<CorrectTestViewModel> correctTest = new List<CorrectTestViewModel>();
            List<int> lessonIds = new List<int>();
            List<int> answerNumbers = new List<int>();
            int lessonId = 0;
            //Get LessonIds
            foreach (var question in questionList)
            {
                if (question.LessonId != lessonId)
                {
                    lessonIds.Add(question.LessonId);
                }
                lessonId = question.LessonId;
            }

            foreach (var LessonId in lessonIds)
            {
                // Declare Variables For CorrectAnswers
                int correctAnswers = 0;
                int wrongAnswers = 0;
                int noCheckedAnswers = 0;
                double correctWithScore = 0;
                double WrongWithScore = 0;
                double noCheckedWithScore = 0;
                double percent = 0;
                double allScores = 0;
                var questionCounts = _questionRepository.GetQuestionCountsByLessonId(LessonId,testId);
                var lesson = _lessonRepository.GetLessonName(LessonId).Result;
                // Correct Test Answers
                CorrectTestAnswer(answersList,ref correctAnswers,ref correctWithScore,ref wrongAnswers,ref WrongWithScore, LessonId);
                // Add NoChecked Answers
                AddNocheckedAnswers(testId,LessonId,ref noCheckedAnswers,ref noCheckedWithScore);
                allScores = correctWithScore + WrongWithScore + noCheckedWithScore;
                // Correct Descriptive Answers
                CorrectDescriptiveAnswers(descriptiveScore, descriptiveQuestionList,ref correctAnswers,ref correctWithScore,ref noCheckedAnswers,ref noCheckedWithScore,ref allScores);
                
                if (allScores != 0)
                {
                    percent = CalculatePercent(correctWithScore, WrongWithScore, allScores, testId);
                }
                correctTest.Add(new CorrectTestViewModel()
                {
                    TestCounts = questionCounts,
                    Percent = percent,
                    CorrectAnswers = correctAnswers,
                    NoCheckedAnswers = noCheckedAnswers,
                    WrongAnswers = wrongAnswers,
                    LessonName = lesson.LessonName
                });
            }
            return Tuple.Create(answers, descriptiveAnswers, questions, descriptiveQuestions, fileAddress, correctTest);
        }

        private double CalculatePercent(double correctWithScore,double WrongWithScore,double allScores, int testId)
        {
            var test = _testRepository.GetTestById(testId).Result;
            double percent = 0;
            if (allScores != 0)
            {
                if (test.NegativePoint)
                {
                    percent = (((3 * correctWithScore) - WrongWithScore) / (allScores * 3)) * 100;
                }
                else
                {
                    percent = (correctWithScore / allScores) * 100;
                }
            }
            return percent;
        }
        private void CorrectTestAnswer(IEnumerable<AnswerModel> answersList,ref int correctAnswers,ref double correctWithScore,ref int wrongAnswers,ref double WrongWithScore,int lessonId)
        {
            foreach (var answer in answersList)
            {
                var question = _questionRepository.GetQuestionByNumber(answer.AnswerNumber, answer.TestId, lessonId).Result;
                if (question is not null)
                {
                    if (answer.AnswerChecked == question.TestKeyAnswer && answer.AnswerChecked != null)
                    {
                        correctAnswers++;
                        correctWithScore += 1 * question.Score;
                    }
                    else if (answer.AnswerChecked != null)
                    {
                        wrongAnswers++;
                        WrongWithScore += 1 * question.Score;
                    }
                }
            }
        }
        private void AddNocheckedAnswers(int testId,int lessonId,ref int noCheckedAnswers,ref double noCheckedWithScore)
        {
            var questionsByLessonId = _questionRepository.GetTestQuestionsByLessonId(testId, lessonId).Result;
            foreach (var question in questionsByLessonId)
            {
                bool IsExistAnswer = _answerRepository.IsExistAnswer(question.QuestionNumber).Result;
                if (!IsExistAnswer)
                {
                    noCheckedAnswers++;
                    noCheckedWithScore += 1 * question.Score;
                }
            }
        }
        private void CorrectDescriptiveAnswers(List<string> descriptiveScores,IEnumerable<QuestionModel> descriptiveQuestionList,ref int correctAnswers,ref double correctWithScore,ref int noCheckedAnswers,ref double noCheckedWithScore,ref double allScores)
        {
            if (descriptiveScores.Count != 0)
            {
                int index = 0;
                foreach (var question in descriptiveQuestionList)
                {
                    if (descriptiveScores[index] != "0")
                    {
                        correctAnswers++;
                        correctWithScore += 1 * int.Parse(descriptiveScores[index]);
                        allScores += 1 * (question.Score - int.Parse(descriptiveScores[index]));
                    }
                    else
                    {
                        noCheckedAnswers++;
                        noCheckedWithScore += 1 * int.Parse(descriptiveScores[index]);
                        allScores += 1 * (question.Score);
                    }
                    index++;
                }
            }
        }
        #endregion

        public async Task<bool> IsComprehensiveTest(int testId)
        {
            return await _testRepository.IsComprehensiveTest(testId);
        }

        #region TestStudents

        public void EnableWorkBook(int studentId, int testId)
        {
            var testStudent = _testRepository.GetTestStudentByIds(studentId,testId).Result;
            testStudent.IsShowingWorkBook = true;
            _testRepository.UpdateTestStudents(testStudent);
        }
        public void EnableAllWorkBooks(int testId)
        {
            var testStudents = _testRepository.GetTestStudentById(testId).Result;
            foreach (var model in testStudents)
            {
                bool IsSubmitWorkBook = _workBookRepository.IsSubmitWorkBook(model.StudentId,model.TestId).Result;
                if (IsSubmitWorkBook)
                {
                    model.IsShowingWorkBook = true;
                    _testRepository.UpdateTestStudents(model);
                }
            }
        }
        public void EnableAllWorkBooks()
        {
            var testStudents = _testRepository.GetTestStudentById().Result;
            foreach (var model in testStudents)
            {
                bool IsSubmitWorkBook = _workBookRepository.IsSubmitWorkBook(model.StudentId, model.TestId).Result;
                if (IsSubmitWorkBook)
                {
                    model.IsShowingWorkBook = true;
                    _testRepository.UpdateTestStudents(model);
                }
            }
        }
        public void EnableAllMasterWorkBooks(int teacherId)
        {
            var testStudents = _testRepository.GetTestStudentById().Result;
            foreach (var model in testStudents)
            {
                bool IsSubmitWorkBook = _workBookRepository.IsSubmitWorkBook(model.StudentId, model.TestId).Result;
                var test = _testRepository.GetTestByIds(model.TestId,teacherId).Result;
                if (IsSubmitWorkBook && test != null)
                {
                    model.IsShowingWorkBook = true;
                    _testRepository.UpdateTestStudents(model);
                }
            }
        }
        #endregion

        #region WorkBooksList

        public Tuple<List<ShowWorkBooksViewModel>, int, int> ShowWorkBooksList(string search, int page = 1)
        {
            var participants = _testRepository.GetParticipants(search).Result;
            List<ShowWorkBooksViewModel> models = new List<ShowWorkBooksViewModel>();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(participants.Count, 6);
            int skip = (page - 1) * 6;
            var participantsList = participants.Skip(skip).Take(6).ToList();
            foreach (var participant in participantsList)
            {
                bool IsSubmitWorkBook = _workBookRepository.IsSubmitWorkBook(participant.StudentId,participant.TestId).Result;
                var student = _studentRepository.GetStudentById(participant.StudentId).Result;
                var test = _testRepository.GetTestById(participant.TestId).Result;
                int correctAnswers = _workBookRepository.GetCorrectsCountById(participant.TestId,participant.StudentId);
                int wrongAnswers = _workBookRepository.GetWrongsCountById(participant.TestId,participant.StudentId);
                int whiteAnswers = _workBookRepository.GetNoCheckedById(participant.TestId,participant.StudentId);
                var levelPercent = _workBookRepository.GetLevelPercentByIds(participant.TestId,participant.StudentId).Result;
                int questionCounts = _testRepository.GetQuestionCountsById(participant.TestId);
                if (IsSubmitWorkBook)
                {
                    models.Add(new ShowWorkBooksViewModel() {
                        TestTitle = test.TestTitle,
                        StudentId = student.StudentId,
                        StudentName = student.StudentName,
                        TestId = test.TestId,
                        TestFile = test.TestFile,
                        TestDayTime = test.TestDayTime.ToString("yyyy/MM/dd"),
                        WrongAnswers = wrongAnswers,
                        TrueAnswers = correctAnswers,
                        NoCheckedAnswers = whiteAnswers,
                        Score = levelPercent.TestScore,
                        QuestionCounts = questionCounts
                    });
                }
            }
            return Tuple.Create(models, pageCount, pageNumber);
        }
        public Tuple<List<ShowWorkBooksViewModel>, int, int> ShowWorkBooksListForMaster(string search, int teacherId, int page = 1)
        {
            var participants = _testRepository.GetParticipants(search).Result;
            List<ShowWorkBooksViewModel> models = new List<ShowWorkBooksViewModel>();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(participants.Count, 6);
            int skip = (page - 1) * 6;
            var participantsList = participants.Skip(skip).Take(6).ToList();
            foreach (var participant in participantsList)
            {
                bool IsSubmitWorkBook = _workBookRepository.IsSubmitWorkBook(participant.StudentId, participant.TestId).Result;
                var student = _studentRepository.GetStudentById(participant.StudentId).Result;
                var test = _testRepository.GetTestByIds(participant.TestId,teacherId).Result;
                int correctAnswers = _workBookRepository.GetCorrectsCountById(participant.TestId, participant.StudentId);
                int wrongAnswers = _workBookRepository.GetWrongsCountById(participant.TestId, participant.StudentId);
                int whiteAnswers = _workBookRepository.GetNoCheckedById(participant.TestId, participant.StudentId);
                var levelPercent = _workBookRepository.GetLevelPercentByIds(participant.TestId, participant.StudentId).Result;
                int questionCounts = _testRepository.GetQuestionCountsById(participant.TestId);
                if (IsSubmitWorkBook)
                {
                    models.Add(new ShowWorkBooksViewModel()
                    {
                        TestTitle = test.TestTitle,
                        StudentId = student.StudentId,
                        StudentName = student.StudentName,
                        TestId = test.TestId,
                        TestFile = test.TestFile,
                        TestDayTime = test.TestDayTime.ToString("yyyy/MM/dd"),
                        WrongAnswers = wrongAnswers,
                        TrueAnswers = correctAnswers,
                        NoCheckedAnswers = whiteAnswers,
                        Score = levelPercent.TestScore,
                        QuestionCounts = questionCounts
                    });
                }
            }
            return Tuple.Create(models, pageCount, pageNumber);
        }
        public async Task<ReportTestViewModel> GetReportOfTest(int testId)
        {
            string sDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string dtNow = DateTime.Now.ToShamsi();
            DateTime date = DateTime.ParseExact(sDate, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime nowDt = DateTime.ParseExact(dtNow, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            double participated = await _testRepository.GetParticipatedSingleTest(date, nowDt,testId);
            double notParticipated = await _testRepository.GetNotParticipatedSingleTest(date, nowDt,testId);
            double finishedStudents = await _testRepository.GetSingleFinishedStudents(date, nowDt,testId);
            double unFinishedStudents = await _testRepository.GetSingleUnFinishedStudents(date, nowDt,testId);
            CalculateChartInfos.CalculatePercent(ref participated, ref notParticipated, ref finishedStudents, ref unFinishedStudents);
            ReportTestViewModel model = new ReportTestViewModel();
            model.Participated = participated;
            model.NotParticipated = notParticipated;
            model.FinishedStudents = finishedStudents;
            model.UnFinishedStudents = unFinishedStudents;
            return model;

        }
        #endregion

        #region StudentPanel WorkBook

        public Tuple<List<ShowWorkBooksViewModel>, int, int> ShowStudentWorkBooksList(string search, int studentId, int page = 1)
        {
            var participants = _testRepository.GetParticipantsByStudentId(search,studentId).Result;
            List<ShowWorkBooksViewModel> models = new List<ShowWorkBooksViewModel>();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(participants.Count, 6);
            int skip = (page - 1) * 6;
            var participantsList = participants.Skip(skip).Take(6).ToList();
            foreach (var participant in participantsList)
            {
                bool IsSubmitWorkBook = _workBookRepository.IsSubmitWorkBook(participant.StudentId, participant.TestId).Result;
                var student = _studentRepository.GetStudentById(participant.StudentId).Result;
                var test = _testRepository.GetTestById(participant.TestId).Result;
                int correctAnswers = _workBookRepository.GetCorrectsCountById(participant.TestId, participant.StudentId);
                int wrongAnswers = _workBookRepository.GetWrongsCountById(participant.TestId, participant.StudentId);
                int whiteAnswers = _workBookRepository.GetNoCheckedById(participant.TestId, participant.StudentId);
                var levelPercent = _workBookRepository.GetLevelPercentByIds(participant.TestId, participant.StudentId).Result;
                int questionCounts = _testRepository.GetQuestionCountsById(participant.TestId);
                if (IsSubmitWorkBook)
                {
                    models.Add(new ShowWorkBooksViewModel()
                    {
                        TestTitle = test.TestTitle,
                        StudentId = student.StudentId,
                        StudentName = student.StudentName,
                        TestId = test.TestId,
                        TestFile = test.TestFile,
                        TestDayTime = test.TestDayTime.ToString("yyyy/MM/dd"),
                        LessonName = test.Lesson.LessonName,
                        WrongAnswers = wrongAnswers,
                        TrueAnswers = correctAnswers,
                        NoCheckedAnswers = whiteAnswers,
                        Score = levelPercent.TestScore,
                        QuestionCounts = questionCounts
                    });
                }
            }
            return Tuple.Create(models, pageCount, pageNumber);
        }

        #endregion

        #region BasketOrders

        public Tuple<List<OrdersBasketViewModel>, int, int> ShowOrdersBasket(int studentId, string search, int page = 1)
        {
            var orders = _orderRepository.GetOrdersByStudentId(studentId,search).Result.ToList();
            int pageNumber = page;
            int pageCount = PageCounts.PageCount(orders.Count, 6);
            int skip = (page - 1) * 6;
            var resultList = orders.Skip(skip).Take(6).ToList();
            List<OrdersBasketViewModel> models = new List<OrdersBasketViewModel>();
            foreach (var order in resultList)
            {
                var student = _studentRepository.GetStudentById(order.StudentId).Result;
                var details = _orderRepository.GetDetailsByOrderId(order.OrderId).Result;
                List<DetailsBasketViewModel> detailsVW = new List<DetailsBasketViewModel>();
                foreach (var detail in details)
                {
                    detailsVW.Add(new DetailsBasketViewModel() 
                    {
                        TestTitle = detail.TestModel.TestTitle,
                        TestCode = detail.TestModel.TestCode,
                        TestDayTime = detail.TestModel.TestDayTime.ToString("yyyy/MM/dd"),
                        TestPrice = detail.Price.ToString("#,0"),
                        EndTest = detail.TestModel.EndTest.ToString("HH:mm"),
                        StartTest = detail.TestModel.StartTest.ToString("HH:mm")
                    });
                }
                models.Add(new OrdersBasketViewModel()
                { 
                    StudentName = student.StudentName,
                    CreateDate = order.CreateDate.ToString("yyyy/MM/dd"),
                    OrderCode = order.OrderCode,
                    IsFinaly = order.IsFinally,
                    OrderId = order.OrderId,
                    Details = detailsVW
                });
            }
            return Tuple.Create(models, pageCount,pageNumber);
        }

        #endregion

    }
}