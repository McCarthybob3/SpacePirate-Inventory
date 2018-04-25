using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Models
{
    public class ItemAddViewModel
    {
        public Item Item { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Item.ItemName))
            {
                errors.Add(new ValidationResult("Item name is required."));
            }
            if(Item.RealValue <= 0)
            {
                errors.Add(new ValidationResult("Real value is required."));
            }
            if (Item.DisplayValue <= 0)
            {
                errors.Add(new ValidationResult("Display value is required."));
            }
            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                var extensions = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

                var extension = Path.GetExtension(ImageUpload.FileName).ToLower();

                if (!extensions.Contains(extension))
                {
                    errors.Add(new ValidationResult("Image file must be .jpg .png .gif or .jpeg"));
                }
            }
            else
            {
                errors.Add(new ValidationResult("Image is required."));
            }
            return errors;
        }
    }
}