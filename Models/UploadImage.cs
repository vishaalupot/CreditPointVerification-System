using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPV_Mark3.Models
{
    public class UploadImage
    {
        public int Id { get; set; }
        public string Title { get; set; }

        
        public string Image { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}