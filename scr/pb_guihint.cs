using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SnLib;

namespace pbpb
{
	public static class Hints
	{
        public const string AllowRestartPC = "Добавить программу в Автозапуск и разрешить перезапуск PC при необходимости";
	}

    partial class Form1
    {

        void InitAppToolTips() {

            new ToolTip().SetToolTip( chb_AllowRestartPC, Hints.AllowRestartPC );
        }
    }
}
