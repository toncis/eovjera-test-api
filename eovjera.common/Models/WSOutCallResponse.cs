using System;
using System.Collections.Generic;
using System.Text;

namespace eOvjera.Common.Models
{
    public class WSOutCallResponse : ICloneable
    {
        public object Value { get; set; }
        public bool IsArray { get; set; }
        public int ItemTemplateId { get; set; }
        public int ItemTemplateTypeId { get; set; }
        public int? ItemRow { get; set; }
        public string JSONResponsePath { get; set; }
        public long FormId { get; set; }

        public object Clone()
        {
            var newItem = new WSOutCallResponse();
            newItem.FormId = this.FormId;
            newItem.IsArray = this.IsArray;
            newItem.ItemTemplateTypeId = this.ItemTemplateTypeId;
            newItem.ItemTemplateId = this.ItemTemplateId;
            newItem.JSONResponsePath = this.JSONResponsePath;
            newItem.Value = null;
            newItem.ItemRow = null;

            return newItem;
        }
    }
}
