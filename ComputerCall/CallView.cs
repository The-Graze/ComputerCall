using ComputerInterface.ViewLib;
using ComputerInterface;
using System;
using System.Collections.Generic;
using System.Text;
using ComputerInterface.Views.GameSettings;
using ComputerInterface.Views;

namespace ComputerCall
{
    internal class CallView : ComputerView
    {
        private readonly UISelectionHandler _selectionHandler;

        public CallView()
        {
            _selectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down);
            _selectionHandler.ConfigureSelectionIndicator($"<color=#{PrimaryColor}> ></color> ", "", "   ", "");
        }

        public override void OnShow(object[] args)
        {
            base.OnShow(args);

            _selectionHandler.MaxIdx = CallMan.instance.screens.Count - 1;
            _selectionHandler.CurrentSelectionIndex = 0;
            Redraw();
        }

        public void Call()
        {
           CallMan.instance.StartCoroutine(CallMan.instance.StartCall(CallMan.instance.screens[_selectionHandler.CurrentSelectionIndex], CallMan.instance.video[_selectionHandler.CurrentSelectionIndex], _selectionHandler.CurrentSelectionIndex, 3));
           ShowView<InCallView>();
        }

        public void Redraw()
        {
            var str = new StringBuilder();

            DrawHeader(str);
            DrawOptions(str);

            SetText(str);
        }

        public void DrawHeader(StringBuilder str)
        {
            str.BeginCenter().BeginColor("ffffff50").Repeat("=", SCREEN_WIDTH).AppendLine();
            str.Append("Press enter to Call").AppendLine();
            str.Repeat("=", SCREEN_WIDTH).EndColor().EndAlign().AppendLines(2);
        }

        public void DrawOptions(StringBuilder str)
        {
            str.AppendLine("Available to Call: ");
            var maps = CallMan.instance.screens;
            for (int i = 0; i < maps.Count; i++)
            {
                var formattedName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(maps[i].name);
                str.Append(_selectionHandler.GetIndicatedText(i, formattedName)).AppendLine();
            }
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            switch (key)
            {
                case EKeyboardKey.Enter:
                    Call();
                    break;
                case EKeyboardKey.Back:
                    ReturnToMainMenu();
                    break;
                default:
                    if (_selectionHandler.HandleKeypress(key))
                    {
                        Redraw();
                        return;
                    }
                    break;
            }
        }
    }
}
