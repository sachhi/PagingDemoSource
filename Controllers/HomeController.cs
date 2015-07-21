using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using MVCPagingDemo.Models;

namespace MVCPagingDemo.Controllers
{
    public class HomeController : Controller
    {
        private const int defaultPageSize = 10;
        private IList<Employee> allEmployee = new List<Employee>();
        private string[] name = new string[4] { "Will", "Johnny", "Zia", "Bhaumik" };
        private string[] phone = new string[4] { "1-274-748-2630", "1-762-805-1019", "1-920-437-3485", "1-562-653-8258" };
        private string[] email = new string[4] { "donec@congueelitsed.org", "neque.non@Praesent.co.uk", "et.magna@Pellentesque.ca", "enim.commodo@orci.net" };
        private string[] city = new string[4] { "Wigtown", "Malderen", "Las Vegas", "Talence" };

        public HomeController()
        {
            InitializeEmployee();
        }

        private void InitializeEmployee()
        {
            // Create a list of 200 employee.
            int index = 0;

            for (int i = 0; i < 200; i++)
            {
                var employee = new Employee();
                //int categoryIndex = i % new Random().Next(1, 5);
                //if (categoryIndex > 3)
                //    categoryIndex = 3;
                index = index > 3 ? 0 : index;
                employee.ID = i + 1;
                employee.Name = name[index];
                employee.Phone = phone[index];
                employee.Email = email[index];
                employee.City = city[index];
                allEmployee.Add(employee);
                index++;
            }
        }


        public ActionResult Index(string employee_name, int? page)
        {
            ViewData["employee_name"] = employee_name;
            int currentPageIndex = page.HasValue ? page.Value : 1;
            IList<Employee> employees = this.allEmployee;

            if (string.IsNullOrWhiteSpace(employee_name))
            {
                employees = employees.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                employees = employees.Where(p => p.Name.ToLower() == employee_name.ToLower()).ToPagedList(currentPageIndex, defaultPageSize);
            }



            //var list = 
            if (Request.IsAjaxRequest())
                return PartialView("_AjaxEmployeeList", employees);
            else
                return View(employees);
        }
    }
}
