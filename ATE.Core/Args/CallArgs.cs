using System;
using ATE.Core.Entities.ATE;

namespace ATE.Core.Args
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