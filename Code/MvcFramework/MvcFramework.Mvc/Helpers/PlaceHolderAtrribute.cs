using System;
using System.Linq;
using System.Web.Mvc;

namespace MvcFramework.Mvc.Helpers
{
    public class PlaceHolderAttribute : Attribute, IMetadataAware
    {
        private readonly string _placeholder;
        public PlaceHolderAttribute(string placeholder)
        {
            this._placeholder = placeholder;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["placeholder"] = this._placeholder;
        }
    }
}