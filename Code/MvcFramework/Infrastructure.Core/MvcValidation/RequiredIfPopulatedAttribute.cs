using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Infrastructure.Core.MvcValidation
{
    public enum Comparison
    {
        IsNotPopulated,
        IsPopulated
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class RequiredIfPopulatedAttribute : ValidationAttribute, IClientValidatable
    {
        public RequiredIfPopulatedAttribute(string otherProperty, Comparison comparison)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            this.OtherProperty = otherProperty;
            this.Comparison = comparison;
            this.ErrorMessage = comparison == Comparison.IsPopulated
                                    ? "The {0} field is required if {1} is populated."
                                    : "The {0} field is required if {1} is not populated.";
        }

        public Comparison Comparison { get; private set; }
        public string OtherProperty { get; private set; }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessageString, name, this.OtherProperty);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[]
                {
                    new ModelClientValidationRequiredIfPopulatedRule(
                        string.Format(this.ErrorMessageString, metadata.GetDisplayName(), this.OtherProperty), this.OtherProperty, this.Comparison)
                };
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                var property = validationContext.ObjectInstance.GetType().GetProperty(this.OtherProperty);

                var propertyValue = property.GetValue(validationContext.ObjectInstance, null);

                if (this.IsRequired(propertyValue))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }

        private bool IsRequired(object actualPropertyValue)
        {
            switch (this.Comparison)
            {
                case Comparison.IsPopulated:
                    return actualPropertyValue != null;
                default:
                    return actualPropertyValue == null;
            }
        }
    }

    public class ModelClientValidationRequiredIfPopulatedRule : ModelClientValidationRule
    {
        public ModelClientValidationRequiredIfPopulatedRule(string errorMessage, string otherProperty, Comparison comparison)
        {
            this.ErrorMessage = errorMessage;
            this.ValidationType = comparison == Comparison.IsPopulated ? "requireifpopulated" : "requireifnotpopulated";
            this.ValidationParameters.Add("otherproperty", otherProperty);
        }
    }
}