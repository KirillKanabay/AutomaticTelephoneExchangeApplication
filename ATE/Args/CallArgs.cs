using System;
using ATE.Entities.ATE;

namespace ATE.Args
{
    public class CallArgs : EventArgs
    {
        public Call Call { get; }

        public CallArgs(Call call)
        {
            Call = call;
        }
    }
}