using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;

namespace Mvc.Controllers;

public class HomeController : Controller
{
   public readonly ILogger<HomeController> _logger;
    [Route("NashTech/Rookies/Index")]
    public List<Person> GetList(){
        return new List<Person>
        {
            new Person{
                FirstName = "Do",
                LastName = "Tien Thanh",
                Gender = "Male",
                DateOfBirth = 2000,
                PhoneNumber = "5333314422",
                BirthPlace = "Ha noi",
                Age = 22,
                IsGraduated = false
            },
            new Person{
                FirstName = "Pham",
                LastName = "Dinh Quan",
                Gender = "Male",
                DateOfBirth = 2000,
                PhoneNumber = "566611723",
                BirthPlace = "Thai Binh",
                Age = 22,
                IsGraduated = false
            },
            new Person{
                FirstName = "Tran",
                LastName = "Thanh Thao",
                Gender = "Female",
                DateOfBirth = 1999,
                PhoneNumber = "01298332132",
                BirthPlace = "Thanh hoa",
                Age = 23,
                IsGraduated = true
            },
            new Person{
                FirstName = "Pham",
                LastName = "Thanh Tung",
                Gender = "Male",
                DateOfBirth = 1997,
                PhoneNumber = "01298332132",
                BirthPlace = "Thanh hoa",
                Age = 25,
                IsGraduated = true
            }
        };
    }    
    public List<Person> GetMaleMembers(List<Person> listMember)
    {
        var maleMembers = from member in listMember where member.Gender == "Male" select member;
        return maleMembers.ToList();
    }
   public Person GetOldestMember(List<Person> listMember)
    {
        var oldestMember = from member in listMember orderby member.DateOfBirth ascending select member;
        return oldestMember.FirstOrDefault();
    }
   public List<string> GetFullNameList(List<Person> listMember){
        var fullname = from member in listMember select string.Join(" ", member.FirstName, member.LastName);
        return fullname.ToList();
    }

    public List<List<Person>> List3(List<Person> listMember){
        var under2000 = from member in listMember where (member.DateOfBirth < 2000) select member;
        var is2000 = from member in listMember where (member.DateOfBirth == 2000) select member;
        var over2000 = from member in listMember where (member.DateOfBirth > 2000) select member;

        List<List<Person>> list3 = new List<List<Person>>{under2000.ToList(), is2000.ToList(), over2000.ToList()};
        return list3;
    }
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

     public List<Person> MalePersons()
    {
        return GetMaleMembers(GetList());
    }

     public Person OldestPerson()
    {
        return GetOldestMember(GetList());
    }

      public List<string> FullnamePersons()
    {
        return GetFullNameList(GetList());
    }
      public List<List<Person>> Get3Lists()
    {
        return List3(GetList());
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public FileResult DownloadFile(){
        return File("Assets/Person.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Person.xlsx");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
