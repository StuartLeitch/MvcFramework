using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Infrastructure.Core.MvcValidation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class NotBothPopulatedAttribute : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "{0} cannot be the populated at the same time as {1}.";

        public string OtherProperty { get; private set; }

        public NotBothPopulatedAttribute(string otherProperty)
            : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            this.OtherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, this.OtherProperty);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(this.OtherProperty);
                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

                if (otherPropertyValue != null)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
               
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[] { new ModelClientValidationNotBothPopulatedRule(FormatErrorMessage(metadata.GetDisplayName()), this.OtherProperty) };            
        }
    }

    public class ModelClientValidationNotBothPopulatedRule : ModelClientValidationRule
    {
        public ModelClientValidationNotBothPopulatedRule(string errorMessage, string otherProperty)
        {
            ErrorMessage = errorMessage;
            ValidationType = "notbothpopulated";
            ValidationParameters.Add("otherproperty", otherProperty);
        }
    }
}
