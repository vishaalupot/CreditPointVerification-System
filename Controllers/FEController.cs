using CPV_Mark3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPV_Mark3.Controllers
{
    public class FEController : Controller
    {
        private CPV_DB1Entities db = new CPV_DB1Entities();
        DateTime allocationDate;
        // GET: FE
        public ActionResult FEDash()
        {
            CPV_DB1Entities db = new CPV_DB1Entities();
            List<CaseTable> cases = db.CaseTables.ToList();
            cases = cases.Where(w => w.FE_Name == User.Identity.Name && w.Final_Status == "Pending").ToList();
            return View(cases);
        }

        public ActionResult SubmitView(int id)
        {

            CPV_DB1Entities db = new CPV_DB1Entities();

            //CaseTable caseTable = db.CaseTables.Where(w => w.Id == id).First();
            CaseTable caseTable = db.CaseTables.Find(id);
            // ViewData.Model = caseTable;
            return View(caseTable);
        }

        [HttpPost]
        public ActionResult SubmitView(FormCollection form)
        {

            CPV_DB1Entities db = new CPV_DB1Entities();

            int snr1 = int.Parse(form["Id"]);

            CaseTable Loc = db.CaseTables.Where(a => a.Id == snr1).FirstOrDefault();


            Loc.Latitude = form["Latitude"];
            Loc.Longitude = form["Longitude"];
            Loc.Final_Status = "PDA Captured";


            db.Entry(Loc).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("FEDash");
        }

        [HttpPost]
        public ActionResult VisitView(FormCollection form)
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
            //caseTable.Latitude = form["Latitude"].ToString();
            //caseTable.Longitude = form["Longitude"].ToString();
            caseTable.Feedback = form["Feedback"].ToString();
            //caseTable.Images = form["Images"].ToString();
            //caseTable.Verifier_Signature = form["Verifier_Signature"].ToString();
            //caseTable.Customer_Signature = form["Customer_Signature"].ToString();
            caseTable.Final_Date = DateTime.Now;
            caseTable.ReceptionDesk = form["ReceptionDesk"].ToString();
            caseTable.Different_CompanyNameBoard_Seen_Reason = form["Different_CompanyNameBoard_Seen_Reason"].ToString();
            //caseTable.Application_name = form["Application_name"].ToString();
            //caseTable.Application_name = form["Application_name"].ToString();

            db.Entry(caseTable).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("FEDash");
        }


        public ActionResult VisitView(int id)
        {
            CPV_DB1Entities db = new CPV_DB1Entities();

            //CaseTable caseTable = db.CaseTables.Where(w => w.Id == id).First();
            CaseTable caseTable = db.CaseTables.Find(id);
            // ViewData.Model = caseTable;
            return View(caseTable);
        }

        public ActionResult SignatureView(int id)
        {

            CPV_DB1Entities db = new CPV_DB1Entities();
            CaseTable caseTable = db.CaseTables.Find(id);
            ViewBag.snr = id;
            Session["id"] = id;
            return View(caseTable);
        }

        [HttpPost]
        public ActionResult SignatureView(string signatureData, string signatureData2, string signatureData3, int snr)
        {
            CPV_DB1Entities db = new CPV_DB1Entities();

            CaseTable table = new CaseTable();

            var Signs = db.CaseTables.Where(a => a.Id == snr).FirstOrDefault();

            //Signs.CustSign = signatureData;

            var base64Signature = signatureData.Split(',')[1];
            var binarySignature = Convert.FromBase64String(base64Signature);

            var base64Signature2 = signatureData2.Split(',')[1];
            var binarySignature2 = Convert.FromBase64String(base64Signature2);

            var base64Signature3 = signatureData3.Split(',')[1];
            var binarySignature3 = Convert.FromBase64String(base64Signature3);

            Signs.CustSign = Convert.ToBase64String(binarySignature);
            Signs.VerifySign1 = Convert.ToBase64String(binarySignature2);
            Signs.VerifySign2 = Convert.ToBase64String(binarySignature3);


            db.Entry(Signs).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("FEDash");
        }

        [HttpGet]
        public ActionResult PhotoView(int id)
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
                ViewBag.snr = id;
                Session["id"] = id;
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


            ViewBag.snr = id;
            Session["id"] = id;
            return View(caseTable);
        }

        //public ActionResult PhotoView(FormCollection file)
        [HttpPost]
        public ActionResult PhotoView(HttpPostedFileBase file, int snr)
        {


            if (file == null)
            {
                ModelState.AddModelError("", "Please attached a file.");
            }

            if (ModelState.IsValid)
            {
                CPV_DB1Entities db = new CPV_DB1Entities();


                //CaseImage image = new CaseImage();

                //image.Case_Id = int.Parse(Session["id"].ToString());

                //image.Image = ConvertToBytes(file);
                //image.TimeStamp = DateTime.Now;

                //db.CaseImages.Add(image);

                //db.SaveChanges();

                //var Img = db.CaseImages.Where(a => a.Case_Id == snr).FirstOrDefault();

                //if (Img is null)
                //{
                    CaseImage imageNew = new CaseImage();
                    imageNew.Case_Id = int.Parse(Session["id"].ToString());
                    imageNew.TimeStamp = DateTime.Now;
                    imageNew.Image = ConvertToBytes(file);

                    db.CaseImages.Add(imageNew);
                //}
                //else
                //{
                //    Img.Image = ConvertToBytes(file);
                //    db.Entry(Img).State = EntityState.Modified;
                //}

                db.SaveChanges();
                return RedirectToAction("FEDash");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult UploadImageInDataBase(HttpPostedFileBase file, int snr)
        {

            if (file == null)
            {
                ModelState.AddModelError("", "Please attached a file.");
            }

            if (ModelState.IsValid)
            {

                CPV_DB1Entities db = new CPV_DB1Entities();


                CaseImage image = new CaseImage();

                image.Case_Id = snr;
                image.Image = ConvertToBytes(file);
                image.TimeStamp = DateTime.Now;

                db.CaseImages.Add(image);

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


        public ActionResult RetrieveImage(int id)
        {
            var images = GetImageFromDataBase(id).ToList();

            if (images.Any())
            {
                using (MemoryStream zipStream = new MemoryStream())
                {
                    using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                    {
                        for (int i = 0; i < images.Count; i++)
                        {
                            // Create an entry in the zip file for each image
                            ZipArchiveEntry entry = archive.CreateEntry($"image_{i + 1}.png");

                            // Write the image bytes to the entry in the zip file
                            using (Stream entryStream = entry.Open())
                            {
                                entryStream.Write(images[i], 0, images[i].Length);
                            }
                        }
                    }

                    // Set the content type and return the zip file
                    return File(zipStream.ToArray(), "application/zip", $"images_{id}.zip");
                }
            }
            else
            {
                // No images found
                return Content("No images found for the specified ID.");
            }
        }

        public IEnumerable<byte[]> GetImageFromDataBase(int Id)
        {
            var q = from data in db.CaseImages where data.Case_Id == Id select data.Image;

            return q.ToList();

        }

        public ActionResult DisplaySign(int id)
        {
            //byte[] imageData = GetSignFromDataBase(id);

            //if (imageData != null)
            //{
            //    string base64Image = Convert.ToBase64String(imageData);
            //    string imageSrc = string.Format("data:image/png;base64,{0}", base64Image);

            //    ViewBag.ImageSrc = imageSrc;
            //    ViewBag.Id = id;

            //    return View();
            //}
            //else
            //{
            //    return HttpNotFound(); // or any other appropriate response
            //}

            //Test code

            byte[][] imagesData = GetSignFromDataBase(id);

            if (imagesData != null && imagesData.Length == 3)
            {
                string[] base64Images = imagesData.Select(imageData => Convert.ToBase64String(imageData)).ToArray();

                string[] imageSrcs = base64Images.Select(base64Image => string.Format("data:image/png;base64,{0}", base64Image)).ToArray();

                ViewBag.ImageSrcs = imageSrcs;
                ViewBag.Id = id;

                return View();
            }
            else
            {
                //return HttpNotFound(); // or any other appropriate response
                return View();
            }

        }

        public byte[][] GetSignFromDataBase(int Id)
        {
            var q = from data in db.CaseTables where data.Id == Id select data.CustSign;
            var q2 = from data in db.CaseTables where data.Id == Id select data.VerifySign1;
            var q3 = from data in db.CaseTables where data.Id == Id select data.VerifySign2;

            string base64String = q.FirstOrDefault();
            string base64String2 = q2.FirstOrDefault();
            string base64String3 = q3.FirstOrDefault();


            if ((base64String != null) || (base64String2 != null) || (base64String3 != null))
            {
                byte[] custSign = Convert.FromBase64String(base64String);
                byte[] verifySign1 = Convert.FromBase64String(base64String2);
                byte[] verifySign2 = Convert.FromBase64String(base64String3);

                return new byte[][] { custSign, verifySign1, verifySign2 };
            }
            else
            {
                // Handle the case when base64String is null, e.g., return an empty byte array or throw an exception.
                // Example: return new byte[0];
                // Or, throw new InvalidOperationException("Base64String is null");
                return null;
            }


           
        }

    }

}