using Smartass.Core.Model.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartass.Core.Model.DTO.Common
{
    public class ResponseDTO
    {
        public bool IsSuccessful { get; set; }
        public object Object { get; set; }
        public string ResponseText { get; set; }

        public ResponseDTO()
        {
            IsSuccessful = false;
            ResponseText = MessageDictionary.UnexpectedError;
        }
    }
}
