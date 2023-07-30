using ComputerInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Zenject;
namespace ComputerCall
{
    internal class MainInstal : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<IComputerModEntry>().To<CallScreenEntry>().AsSingle();
        }
    }
}
