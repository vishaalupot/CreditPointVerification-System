using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CPV_Mark3.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace CPV_Mark3.Controllers
{
    public class HomeController : Controller
    {
        private CPV_DB1Entities db = new CPV_DB1Entities();
        public List<UserRolesViewModel> UsersWithTargetRoles { get; set; }

        DateTime allocationDate;
        //private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public ActionResult CasesReports()
        {
            return View();
        }

        public ActionResult FETransfer()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            CaseTable caseTable = new CaseTable();

            var results = db.CaseTables
                    .Where(item => item.Final_Status == "Pending")
                    .ToList();
            return View(results);
        }



        [HttpPost]
        public ActionResult FETransfer(string query, string query2)
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            CaseTable caseTable = new CaseTable();
            // Filter items based on the search query
            //var results = caseTable.Where(item => item.ToLower().Contains(query.ToLower())).ToList();
            //return Json(results);

            var results = db.CaseTables
                    .Where(item => item.Application_no == query)
                    .ToList();


            return PartialView(results);


        }

        public ActionResult _SearchCases()
        {


            CPV_DB1Entities db = new CPV_DB1Entities();
            CaseTable caseTable = new CaseTable();

            var results = db.CaseTables
                    .Where(item => item.Final_Status == "Pending")
                    .ToList();
            return PartialView("_SearchCases", results);


        }


        [HttpPost]
        public ActionResult _SearchCases(string query, string query3)
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            CaseTable caseTable = new CaseTable();
            // Filter items based on the search query
            //var results = caseTable.Where(item => item.ToLower().Contains(query.ToLower())).ToList();
            //return Json(results);


            var results = db.CaseTables
                  .Where(item => item.Final_Status == "Pending")
                  .ToList();

            if (query != "" || query3 != "")
            {
                results = db.CaseTables
                   .Where(item => item.Application_no == query || item.FE_Name == query3)
                   .ToList();
            }









            return PartialView("_SearchCases", results);


        }


        public ActionResult _SearchAllCases()
        {

            CPV_DB1Entities db = new CPV_DB1Entities();
            List<CaseTable> cases = db.CaseTables.ToList();
            return PartialView(cases);
        }

        public ActionResult _SearchVerifyManager()
        {

            CPV_DB1Entities db = new CPV_DB1Entities();
            List<CaseTable> cases = db.CaseTables.ToList();
            return PartialView(cases);
        }

        [HttpPost]
        public ActionResult _SearchVerifyManager(string query, string query2, string query3, string query4)
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            CaseTable caseTable = new CaseTable();

            var results = db.CaseTables.ToList();





            if (query != "" || query2 != ""|| query3 != ""|| query4 != "")
            {
                results = db.CaseTables
                .Where(item => item.Application_no == query ||
                               item.FE_Name == query2 ||
                               item.Product == query3 ||
                               item.Final_Status == query4
                               )
                .ToList();

            }
            else
            {
                results = db.CaseTables.ToList();
            }

            return PartialView("_SearchVerifyManager", results);
        }






        [HttpPost]
        public ActionResult _SearchAllCases(string query)
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            CaseTable caseTable = new CaseTable();

            var results = db.CaseTables.ToList();

            if (query != "")
            {
                results = db.CaseTables
                .Where(item => item.Application_no == query ||
                               item.Application_name == query ||
                               item.Company_Name == query)
                .ToList();

            }
            else
            {
                results = db.CaseTables.ToList();
            }

            return PartialView("_SearchAllCases", results);


        }

        //[HttpPost]
        //public ActionResult DisplayVerifyManager(string query)
        //{
        //    CPV_DB1Entities db = new CPV_DB1Entities();
        //    CaseTable caseTable = new CaseTable();

        //    var results = db.CaseTables.ToList();

        //    if (query != "")
        //    {
        //        results = db.CaseTables
        //        .Where(item => item.Application_no == query ||
        //                       item.Application_name == query ||
        //                       item.Company_Name == query)
        //        .ToList();

        //    }
        //    else
        //    {
        //        results = db.CaseTables.ToList();
        //    }

        //    return View(results);

        //}


        public ActionResult UploadAllocation()
        {
            return View();
        }

        public ActionResult Index()
        {
            //return View();
            



            if (User.IsInRole("FE"))
            {
                return RedirectToAction("FEDash", "FE");
            }
            else
            {
                CPV_DB1Entities db = new CPV_DB1Entities();
                List<CaseTable> caseTable = db.CaseTables.ToList();

                List<int> Dashdata = new List<int>();
                //var data = new List<int>() { 90, 25, 30, 10 };

                List<CaseTable> totalcase = caseTable.Where(w => w.Allocation_Date < DateTime.Today.AddDays(10)).ToList();
                int donecase = totalcase.Where(w => w.Final_Status == "Final_Status").Count();                
                int newcase = totalcase.Where(w => w.Final_Status == "Pending").Count();
                int overdueCase = totalcase.Where(w => w.Final_Status == "").Count();
                //int overDue = 

                Dashdata.Add(totalcase.Count());
                Dashdata.Add(donecase);
                Dashdata.Add(newcase);
                Dashdata.Add(overdueCase);

                var json = JsonConvert.SerializeObject(Dashdata);

                ViewBag.DashData = json;

                return View(caseTable);
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult UserView()
        {

            CPV_DB1Entities db = new CPV_DB1Entities();
            List<AspNetUser> users = db.AspNetUsers.ToList();
            return View(users);

        }

        public ActionResult UserView1()
        {
            ViewBag.Message = "Your application description page.";
            return View();

        }

        public ActionResult PrintVerifyManager(int id)
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            CaseTable caseTable = db.CaseTables.Find(id);
            List<byte[]> imageList = GetImageFromDataBase(id).ToList();

            List<string> base64Images = new List<string>();

            if (imageList.Any())
            {
                foreach (var imageData in imageList)
                {
                    string base64Image = Convert.ToBase64String(imageData);
                    base64Images.Add(base64Image);
                }

                ViewBag.Images = base64Images;
                return View(caseTable);
                //string imageBase64 = Convert.ToBase64String(images);
                //ViewBag.ImageBase64 = imageBase64;

                //ViewBag.Images = images;
            }
            else
            {
                ViewBag.Images = new List<string>();
                ViewBag.ErrorMessage = "No images found for the specified ID.";
            }
            return View(caseTable);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserView1(FormCollection form)
        {

            CPV_DB1Entities db = new CPV_DB1Entities();
            AspNetUser user = new AspNetUser
            {
                FullName = form["FullName"],
                UserName = form["UserName"],
                UserRole = form["UserRole"],
                Status = form["Status"]

            };

            db.AspNetUsers.Add(user);

            return View();

        }

        public ActionResult NewProductView()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            List<ProductTable> productTable = db.ProductTables.ToList();

            return View(productTable);
        }


        public ActionResult AddNewProductView()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewProductView(FormCollection form)
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            ProductTable product = new ProductTable
            {
                ProductName = form["ProductName"],
                ProductStatus = "Enabled"
            };

            db.ProductTables.Add(product);
            db.SaveChanges();

            return RedirectToAction("AddNewProductView");

        }

        public ActionResult CreateCaseManagerView()
        {

            return View();
        }


        public ActionResult DisplayCaseManager()
        {

            CPV_DB1Entities db = new CPV_DB1Entities();
            List<CaseTable> cases = db.CaseTables.ToList();
            return View(cases);
        }

       

        [HttpPost]
        public ActionResult DeleteCase(int id)
        {
            using (CPV_DB1Entities db = new CPV_DB1Entities())
            {

                //CaseImage caseImage = db.CaseImages.Find(id);
                //CaseTable caseTable = db.CaseTables.Find(id);

                //if(caseImage != null)
                //{
                //    db.CaseImages.Remove(caseImage);
                //    db.SaveChanges();

                //}
                //if (caseTable != null)
                //{
                //    db.CaseTables.Remove(caseTable);
                //    db.SaveChanges();
                //}



                //List<CaseTable> cases = db.CaseTables.ToList();
                //return View(cases);

                List<CaseImage> relatedImages = db.CaseImages.Where(ci => ci.Case_Id == id).ToList();

                if (relatedImages != null && relatedImages.Count > 0)
                {
                    db.CaseImages.RemoveRange(relatedImages);
                }

                CaseTable caseTable = db.CaseTables.Find(id);

                if (caseTable != null)
                {
                    db.CaseTables.Remove(caseTable);
                }

                db.SaveChanges();

                List<CaseTable> cases = db.CaseTables.ToList();
                return View(cases);
            }
        }


        [HttpPost]
        public ActionResult ChangeFE(int id, string fe_name)
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            List<CaseTable> cases = db.CaseTables.ToList();
            CaseTable caseTable = db.CaseTables.Find(id);

            //caseTable.Final_Status = " ";

            caseTable.FE_Name = fe_name;

            db.Entry(caseTable).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return View(cases);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            using (CPV_DB1Entities db = new CPV_DB1Entities())
            {
                ProductTable productTable = db.ProductTables.Find(id);
                if (productTable != null)
                {
                    db.ProductTables.Remove(productTable);
                    db.SaveChanges();
                }

                List<ProductTable> cases = db.ProductTables.ToList();
                return View(cases);
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(string id)
        {
            using (CPV_DB1Entities db = new CPV_DB1Entities())
            {

                AspNetUser aspNetUser = db.AspNetUsers.Find(id);



                if (aspNetUser != null)
                {
                    db.AspNetUsers.Remove(aspNetUser);
                }
                db.SaveChanges();
                List<AspNetUser> users = db.AspNetUsers.ToList();
                return View(users);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCaseManagerView(FormCollection form)
        {

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var users = userManager.Users.ToList();

            CPV_DB1Entities db = new CPV_DB1Entities();

            CaseTable caseTable = new CaseTable
            {
                Application_name = form["Application_name"],
                Application_no = form["Application_no"],
                Company_Name = form["Company_Name"],
                Trade_License_Number = form["Trade_License_Number"],
                Company_Address = form["Company_Address"],
                Landmark = form["Landmark"],
                Landline = form["Landline"],
                Contacted_Person = form["Contacted_Person"],
                Contacted_Person_Mobile_No = form["Contacted_Person_Mobile_No"],
                Operating_Hours = form["Operating_Hours"],
                Emirate = form["Emirate"],
                Product = form["Product"],
                Visit_Type = form["Visit_Type"],
                Client = form["Client"],
                Allocation_Date = DateTime.TryParse(form["Allocation_Date"], out allocationDate) ? allocationDate : default(DateTime),
                FE_Name = form["FE_Name"],
                Final_Status = "Pending"
            };

            db.CaseTables.Add(caseTable);
            db.SaveChanges();

            return RedirectToAction("DisplayCaseManager");
        }

        public static List<SelectListItem> GetEmiratesList()
        {
            var emirates = new List<string> { "Dubai", "Abu Dhabi", "Ajman", "Al Ain", "Fujairah", "RAK", "Sharjah", "UAQ" };

            var emiratesList = emirates.Select(e => new SelectListItem
            {
                Value = e,
                Text = e
            }).ToList();

            return emiratesList;
        }

        public static List<string> GetStatus()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            return db.ProductTables.Select(s => s.ProductStatus).ToList();
        }

        public static List<string> GetProductList()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            return db.ProductTables.Select(s => s.ProductName).ToList();
            //return db.CaseTables.Select(s => s.Final_Status).ToList();
        }
        public static List<string> GetVisitList()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            return db.VisitTables.Select(s => s.Visit_Type).ToList();
        }


        public static List<string> GetAllUsers()
        {
            using (CPV_DB1Entities db = new CPV_DB1Entities())
            {
                List<string> users = db.AspNetUsers.Where(w=>w.UserRole == "FE")
                    .Select(s => s.UserName).ToList();
                return users;
            }
        }


        public static List<string> GetFelist2()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();

            List<string> GetFEList1 = new List<string> { "FE", "Client", "Admin" };

            return GetFEList1;

        }


        public List<string> GetFEList()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            // Create a role manager
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            var roles = roleManager.Roles.ToList();

            var manageRole = roleManager.Roles.Select(s => new ManageRoles
            {
                ID = s.Id,
                Name = s.Name,
            }).ToList();


            //var userslist = db.AspNetUsers.Join(db.AspNetRoles)
            //var _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            //var rolesWithUsers = _roleManager.Roles
            //.Select(role => new UserRoleDto
            //{
            //    RoleName = role.Name,
            //    Users = _userManager.Users
            //        .Where(user => _userManager.IsInRoleAsync(user.Id, role.Name).Result)
            //        .Select(user => new UserDto
            //        {
            //            UserId = user.Id,
            //            UserName = user.UserName,
            //                // Include other user properties as needed
            //            })
            //        .ToList()
            //}).ToList();
            //    //.ToListAsync();


            List<string> filteredUsernames = new List<string>();
            //  var filteredUsernames = rolesWithUsers
            //.SelectMany(rwu => rwu.Users) // Flatten the list of users from rolesWithUsers
            //.Distinct() // Remove duplicate usernames
            //.ToList();

            return filteredUsernames;

            //return db.AspNetUsers 
            //       .Where(s => s.UserRole == "FE")
            //       .Select(s => s.UserName)
            //       .ToList();
        }

        public async Task<List<UserRolesViewModel>> DisplayUserRoles()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var users = userManager.Users.ToList();
            var targetRoles = new List<string> { "FE" };

            List<UserRolesViewModel> usersWithTargetRoles = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var userRoles = await userManager.GetRolesAsync(user.Id);

                if (userRoles.Any(role => targetRoles.Contains(role)))
                {
                    var userWithRoles = new UserRolesViewModel
                    {
                        UserName = user.UserName,
                        // Add other properties as needed
                    };
                    usersWithTargetRoles.Add(userWithRoles);
                }
            }

            // Removed the line below as it is unnecessary and might cause issues
            // ViewData.Model = usersWithTargetRoles;

            return usersWithTargetRoles;
        }


        private IList<string> GetUserRolesForFE()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            // Find users with the "FE" role
            var usersWithFERole = userManager.Users.Where(u => userManager.IsInRole(u.Id, "FE")).ToList();

            // Create a list to store the roles of users with the "FE" role
            var userRoles = new List<string>();

            // Iterate through each user and get their roles
            foreach (var user in usersWithFERole)
            {
                var roles = userManager.GetRoles(user.Id);
                userRoles.AddRange(roles);
            }

            return userRoles;
        }


        public async Task<ActionResult> _FEView()
        {


            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            // Get all users from the database
            var allUsers = userManager.Users.ToList();

            // Find users with the "FE" role
            var usersWithFERole = allUsers.Where(user => userManager.IsInRole(user.Id, "FE")).ToList();

            // Create a list to store the usernames of users with the "FE" role
            var usernamesWithFERole = new List<string>();

            // Iterate through each user and get their username
            foreach (var user in usersWithFERole)
            {
                // Add the username to the list
                usernamesWithFERole.Add(user.UserName);
            }

            return View(usernamesWithFERole);
        }



        public static List<string> GetClientList()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            return db.ClientTables.Select(s => s.Client).ToList();
        }
        public static List<string> GetCaseList()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            return db.CaseTables.Select(s => s.Client).ToList();
        }


        public ActionResult DisplayVerifyManager()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            List<CaseTable> cases = db.CaseTables.ToList();
            return View(cases);

        }



        [HttpPost]
        public ActionResult EditVerifyManager(FormCollection form)
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            CaseTable caseTable = db.CaseTables.Find(int.Parse(form["Id"].ToString()));


            caseTable.Validated_Office_Landline = form["Validated_Office_Landline"].ToString();
            caseTable.Validated_Office_Landline_No = form["Validated_Office_Landline_No"].ToString();
            caseTable.Validated_Office_Landline_NotReason = form["Validated_Office_Landline_NotReason"].ToString();
            caseTable.Source_of_Validated_Number = form["Source_of_Validated_Number"].ToString();
            caseTable.Office_Location_Type = form["Office_Location_Type"].ToString();
            caseTable.Subject_company_signboard = form["Subject_company_signboard"].ToString();
            caseTable.Available_OfficeDoor = form["Available_OfficeDoor"].ToString();
            caseTable.Different_CompanyNameBoard_Seen = form["Different_CompanyNameBoard_Seen"].ToString();
            caseTable.Type_Signboard_OnDoor = form["Type_Signboard_OnDoor"].ToString();
            caseTable.Name_SocietyBoard = form["Name_SocietyBoard"].ToString();
            caseTable.SecurityGuard_Building = form["SecurityGuard_Building"].ToString();
            caseTable.BusinessActivity_Seen = form["BusinessActivity_Seen"].ToString();
            caseTable.TradeLicense_Displayed = form["TradeLicense_Displayed"].ToString();
            caseTable.Setup_of_Office = form["Setup_of_Office"].ToString();
            caseTable.Assets_Seen = form["Assets_Seen"].ToString();
            caseTable.WebsiteAddress_Active = form["WebsiteAddress_Active"].ToString();
            caseTable.Nature_of_Business = form["Nature_of_Business"].ToString();
            caseTable.Office_observation = form["Office_observation"].ToString();
            caseTable.No_Employees_Seen = int.Parse(form["No_Employees_Seen"].ToString());
            caseTable.No_Employees_ContactedPerson = int.Parse(form["No_Employees_ContactedPerson"].ToString());
            caseTable.If_Sister_ConcernCompany = form["If_Sister_ConcernCompany"].ToString();
            caseTable.Co_Owenrship_details = form["Co_Owenrship_details"].ToString();
            caseTable.External_Audit = form["External_Audit"].ToString();
            caseTable.Annual_Revenue = form["Annual_Revenue"].ToString();
            caseTable.Applicants_Designation = form["Applicants_Designation"].ToString();
            caseTable.DoJ_Applicant = form["DoJ_Applicant"].ToString();
            caseTable.Gross_Salary = form["Gross_Salary"].ToString();
            caseTable.Profit = form["Profit"].ToString();
            caseTable.Accommodation_Provided = form["Accommodation_Provided"].ToString();
            caseTable.Applicant_Reports_to = form["Applicant_Reports_to"].ToString();
            caseTable.AUTHORIZED_SIGNATORY = form["AUTHORIZED_SIGNATORY"].ToString();
            caseTable.Details_Verified_With = form["Details_Verified_With"].ToString();
            caseTable.Designation = form["Designation"].ToString();
            caseTable.Orginal_Seen = form["Orginal_Seen"].ToString();
            caseTable.Trade_License_captured = form["Trade_License_captured"].ToString();
            caseTable.Business_card_captured = form["Business_card_captured"].ToString();
            caseTable.Documents_Captured = form["Documents_Captured"].ToString();
            caseTable.VAT_RegNumber = form["VAT_RegNumber"].ToString();
            caseTable.Makani_No = form["Makani_No"].ToString();
            caseTable.Name_Neighbor1_feedback = form["Name_Neighbor1_feedback"].ToString();
            caseTable.Name_Neighbor2_feedback = form["Name_Neighbor2_feedback"].ToString();
            caseTable.Name_Security_feedback = form["Name_Security_feedback"].ToString();
            caseTable.FE_Decision = form["FE_Decision"].ToString();
            caseTable.Latitude = form["Latitude"].ToString();
            caseTable.Longitude = form["Longitude"].ToString();
            caseTable.Feedback = form["Feedback"].ToString();
            //caseTable.Images = form["Images"].ToString();
            //caseTable.Verifier_Signature = form["Verifier_Signature"].ToString();
            //caseTable.Customer_Signature = form["Customer_Signature"].ToString();
            caseTable.Final_Date = DateTime.TryParse(form["Final_Date"], out allocationDate) ? allocationDate : default(DateTime);
            //caseTable.Final_Status = form["Final_Status"].ToString();
            caseTable.ReceptionDesk = form["ReceptionDesk"].ToString();
            caseTable.Different_CompanyNameBoard_Seen_Reason = form["Different_CompanyNameBoard_Seen_Reason"].ToString();
            //caseTable.Application_name = form["Application_name"].ToString();
            //caseTable.Application_name = form["Application_name"].ToString();

            db.Entry(caseTable).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("DisplayVerifyManager");
        }


        public ActionResult EditVerifyManager(int id)
        {
            CPV_DB1Entities db = new CPV_DB1Entities();

            //CaseTable caseTable = db.CaseTables.Where(w => w.Id == id).First();
            CaseTable caseTable = db.CaseTables.Find(id);
            // ViewData.Model = caseTable;

            //Added code below
            List<byte[]> imageList = GetImageFromDataBase(id).ToList();

            List<string> base64Images = new List<string>();

            if (imageList.Any())
            {
                foreach (var imageData in imageList)
                {
                    string base64Image = Convert.ToBase64String(imageData);
                    base64Images.Add(base64Image);
                }

                ViewBag.Images = base64Images;
                return View(caseTable);
                //string imageBase64 = Convert.ToBase64String(images);
                //ViewBag.ImageBase64 = imageBase64;

                //ViewBag.Images = images;
            }
            else
            {
                ViewBag.Images = new List<string>();
                ViewBag.ErrorMessage = "No images found for the specified ID.";
            }
            return View(caseTable);
        }

        public IEnumerable<byte[]> GetImageFromDataBase(int Id)
        {
            var q = from data in db.CaseImages where data.Case_Id == Id select data.Image;

            return q.ToList();

        }


        public ActionResult Register()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            List<string> listRoles = db.AspNetRoles.Select(s => s.Name).ToList();
            ViewBag.Roles = listRoles;
            return View();


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            List<string> roles = roleManager.Roles.Select(r => r.Name.Trim()).ToList();
            ViewBag.Roles = roles;

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    FullName = model.FullName,
                    UserRole = model.InitialRole,
                    Status = model.Status,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,

                };

                model.UserRole = model.InitialRole;
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, model.UserRole);

                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Register", "Home");
                }
                //AddErrors(result);
            }
            return View(model);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [HttpPost]
        public ActionResult UploadImageInDataBase(HttpPostedFileBase file, string snr)
        {

            if (file == null)
            {
                ModelState.AddModelError("", "Please attached a file.");
            }

            if (ModelState.IsValid)
            {

                CPV_DB1Entities db = new CPV_DB1Entities();

                AspNetUser user = new AspNetUser();

                user.Id = snr.ToString();
                user.Images = ConvertToBytes(file);

                CaseImage image = new CaseImage();

                db.AspNetUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("FEDash");
            }
            else
            {
                return View();
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        [HttpPost]
        public ActionResult _ProfilePic(HttpPostedFileBase file, string snr)
        {
             if (file == null)
            {
                ModelState.AddModelError("", "Please attached a file.");
            }

            if (ModelState.IsValid)
            {

                CPV_DB1Entities db = new CPV_DB1Entities();

                List<AspNetUser> user = db.AspNetUsers.ToList();

                AspNetUser aspNetUser = db.AspNetUsers.Find(snr.ToString());


                if (aspNetUser != null)
                {
                    // Convert uploaded file to byte array
                    aspNetUser.Images = ConvertToBytes(file);

                    // Update existing user
                    db.Entry(aspNetUser).State = EntityState.Modified;
                    db.SaveChanges();
                }

                //user.Images = ConvertToBytes(file);


                //db.AspNetUsers.Add(user);
                //db.SaveChanges();
                return PartialView();
            }
            else
            {
                return PartialView();
            }
           
        }

        public ActionResult _ProfilePic()
        {

            return PartialView();
        }

        [HttpGet]
        public ActionResult DisplayProfilePic(string snr)
        {
            AspNetUser user = db.AspNetUsers.Find(snr);

            if (user != null && user.Images != null)
            {
                return File(user.Images, "image/jpeg"); // Adjust content type based on your image type
            }
            else
            {
                // Return a default image or handle the case when the image is not found
                // For now, return an empty image
                byte[] emptyImage = new byte[0];
                return File(emptyImage, "image/jpeg");
            }
        }

       

    }

    public class UserDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        // Include other user properties as needed
    }

    public class UserRoleDto
    {
        public string RoleName { get; set; }
        public List<UserDto> Users { get; set; }
    }

   
}