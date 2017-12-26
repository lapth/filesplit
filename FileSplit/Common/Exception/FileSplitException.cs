using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exception
{
    public class FileSplitException: System.Exception
    {
        private String errorCode;

        public FileSplitException(String message, String errorCode) :base(errorCode + ":" + message)
        {
            this.ErrorCode = errorCode;
        }

        public string ErrorCode { get => errorCode; set => errorCode = value; }
    }
}
