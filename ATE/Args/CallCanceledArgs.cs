using System;

namespace ATE.Args
{
    public class CallCanceledArgs : EventArgs
    {
        public string Message { get; set; }
        public string SourcePhoneNumber { get; set; }
    }
}
