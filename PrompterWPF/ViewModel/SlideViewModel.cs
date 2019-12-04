using PrompterWPF.Model;
using PrompterWPF.ViewModel.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrompterWPF.ViewModel
{
    public class SlideViewModel : ViewModelBase
    {
        public ObservableCollection<string> Texts { get; } = new ObservableCollection<string>();

        private string _label;
        private int _number;
        private int _someInt;

        private string _imagePath;

        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value) ;
        }

        public int SomeInt
        {
            get => _someInt;
            set => SetProperty(ref _someInt, value);
        }

        public int Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        public void Initialize(Slide slide)
        {
            Label = slide.Label;
            Number = slide.Number;
            ImagePath = slide.ImagePath;

            Application.Current.Dispatcher.Invoke(() =>
            {
                Texts.Clear();
                Texts.AddRange(slide.Texts.ToList());
            });
        }
    }
}
