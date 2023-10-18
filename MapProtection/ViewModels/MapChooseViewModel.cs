using MapUnlock.Core;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MapUnlock.ViewModels
{
    internal class MapChooseViewModel
    {
        public ICommand OpenMapSettingViewCommand { get; }
        public ICommand OpenBoostyCommand { get; }
        public ICommand OpenDiscordCommand { get; }

        public MapChooseViewModel()
        {
            OpenMapSettingViewCommand = new RelayCommand(OpenMapSettingViewCommandExecute);
            OpenBoostyCommand = new RelayCommand(OpenBoostyCommandExecute);
            OpenDiscordCommand = new RelayCommand(OpenDiscordCommandExecute);
        }

        private void OpenDiscordCommandExecute(object obj)
        {
            Process.Start("https://discord.gg/CBqDuqDWvS");
        }

        private void OpenBoostyCommandExecute(object obj)
        {
            Process.Start("https://boosty.to/skulidropek");
        }

        private void OpenMapSettingViewCommandExecute(object obj)
        {
            PageManager.Instance.OpenMapSettingView();
        }
    }
}
