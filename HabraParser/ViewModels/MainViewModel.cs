using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using ParserBase.Types;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace HabraParser.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private ParserWorker<Tuple<string, string>[]> parserWorker;

        public ObservableCollection<Tuple<string, string>> Publications { get; private set; }
        public int SelectedItem { get; set; }

        #region Commands

        [Command]
        public void OpenButtonClick()
        {
            if(SelectedItem>=0)
            {
                System.Diagnostics.Process.Start(Publications[SelectedItem].Item2);
            }
            else
            {
                MessageBox.Show("Вы ничего не выбрали", "Ошибка!", MessageBoxButton.OK);
            }
        }

        [Command]
        public void RefreshButtonClick()
        {
            Publications = new ObservableCollection<Tuple<string, string>>();
            parserWorker.Start();
        }

        #endregion

        public MainViewModel()
        {
            var habraParser = new HabraParser.Types.HabraParser();
            var habraParserSettings = new HabraParser.Types.HabraParserSettings(1, 10);
            parserWorker = new ParserWorker<Tuple<string, string>[]>(habraParser, habraParserSettings);
            parserWorker.OnNewData += OnNewDataFromParser;
            parserWorker.Start();

            Publications = new ObservableCollection<Tuple<string, string>>();
        }
        private void OnNewDataFromParser(object sender, Tuple<string, string>[] results)
        {
            foreach (var result in results)
            {
                Publications.Add(result);
            }
        }
    }
}
