using Prism.Commands;
using PrompterWPF.Interfaces;
using PrompterWPF.Model;
using PrompterWPF.ViewModel.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PrompterWPF.ViewModel
{
    public class PrompterViewModel : ViewModelBase
    {
        private ISlideRepository _repository;
        private bool _isManual;
        private readonly SlideRepositoryFactory _repoFactory;
        private readonly Timer _timer;
        private SlideViewModel _selectedSlide;

        public ObservableCollection<SlideViewModel> Slides { get; } = new ObservableCollection<SlideViewModel>();

        public SlideViewModel SelectedSlide
        {
            get => _selectedSlide;
            set => SetProperty(ref _selectedSlide, value);
        }
        
        private bool _isTimerOver;
        public bool IsTimerOver
        {
            get => _isTimerOver;
            set => SetProperty(ref _isTimerOver, value);
        }

        public ICommand NextSlideCommand { get; }

        public ICommand PrevSlideCommand { get; }

        public ICommand ResetCommand { get; }

        public ICommand SetRepoCommand { get; }

        public PrompterViewModel()
        {
            _repoFactory = new SlideRepositoryFactory();
            _timer = new Timer(OnTick);

            NextSlideCommand = new DelegateCommand(OnNextSlide);
            PrevSlideCommand = new DelegateCommand(OnPrevSlide);
            ResetCommand = new DelegateCommand(OnReset);
            SetRepoCommand = new DelegateCommand(SetRepo);

            SetRepo();
        }

        private void OnReset()
        {
            InitializeAsync();
        }

        private void OnTick(object state)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            IsTimerOver = true;
        }

        public void SetRepo()
        {
            _isManual = !_isManual;
            _repository = _repoFactory.GetRepository(_isTimerOver);
        }

        public async Task InitializeAsync()
        {
            

            IsTimerOver = false;

            var (slides, timer) = _repository.GetSlides();

            Application.Current.Dispatcher.Invoke(() =>
            {
                Slides.Clear();
                Slides.AddRange(slides.Select(i => 
                {
                    var s = new SlideViewModel();
                    s.Initialize(i);
                    return s;
                }).ToList());

                SelectedSlide = Slides.FirstOrDefault();
            });

            _timer.Change(timer * 1000, timer * 1000);
        }

        private void OnPrevSlide()
        {
            var index = Slides.IndexOf(SelectedSlide);
            if (index >= 1)
                SelectedSlide = Slides[index - 1];
        }

        private void OnNextSlide()
        {
            var index = Slides.IndexOf(SelectedSlide);
            if (index + 1 < Slides.Count())
                SelectedSlide = Slides[index + 1];
        }
    }
} 
