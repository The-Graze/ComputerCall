using ComputerInterface;
using ComputerInterface.ViewLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerCall
{
    public class InCallView : ComputerView
    {
        public override void OnShow(object[] args)
        {
            base.OnShow(args);
            Text = "\n\n     Calling..";
        }
        public override void OnKeyPressed(EKeyboardKey key)
        {
            switch (key)
            {
                case EKeyboardKey.Back:
                    CallMan.instance.EndCall();
                    ReturnToMainMenu();
                    break;
                case EKeyboardKey.Option1:
                    CallMan.instance.EndCall();
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}
