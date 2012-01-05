using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace SriGreenShoppingCart.Models
{
    #region Models

    public class CategoriesModel
    {
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Category name")]
        public string CategoryName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string CategoryDescription { get; set; }
    }

    public class LatestnewsModel
    {
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Latest news")]
        public string Latestnews { get; set; }
    }

    public class LatestnewsEventsModel
    {
        [Required]
        [DataType(DataType.Html)]
        [DisplayName("Latest news")]
        public string Latestnews { get; set; }
    }

    public class ProductModel
    {
        [DisplayName("CategoryName")]
        public IEnumerable<SelectListItem> CategoryName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Name")]
        public string ProductName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        public string ProductDescription { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Price")]
        public string ProductPrice { get; set; }

        [Required]
        [DisplayName("Image")]
        public HttpPostedFileBase Image { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("ID")]
        public string Id { get; set; }
    }

    #endregion Models

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesShouldMatchAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "'{0}' and '{1}' do not match.";
        private readonly object TypeID = new object();

        public PropertiesShouldMatchAttribute(string originalProperty, string confirmProperty)
            : base(DefaultErrorMessage)
        {
            OriginalProperty = originalProperty;
            ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty { get; private set; }

        public string OriginalProperty { get; private set; }

        public override object TypeId
        {
            get
            {
                return TypeID;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                OriginalProperty, ConfirmProperty);
        }

        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
            object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
            return Equals(originalValue, confirmValue);
        }
    }
}