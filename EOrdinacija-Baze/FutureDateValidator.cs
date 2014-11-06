using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace EOrdinacija_Baze
{
    class FutureDateValidatorAttribute : Attribute
    {
        public bool IsValid(object value)
        {
            return value != null && (DateTime)value > DateTime.Now;
        }
        public IEnumerable<ModelClientValidationRule>
          GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage,
                ValidationType = "futuredate"
            };
        }

        public string ErrorMessage { get; set; }
    }
}
