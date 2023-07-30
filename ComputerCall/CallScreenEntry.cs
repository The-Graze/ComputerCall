using ComputerInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerCall
{
    public class CallScreenEntry : IComputerModEntry
    {
        public string EntryName => "Computer Call";
        public Type EntryViewType => typeof(CallView);
    }
}
