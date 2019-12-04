using PrompterWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace PrompterWPF.Behaviors
{
    public class CurrentSlideBehavior : Behavior<TextBlock>
    {
        public static readonly DependencyProperty SelectedSlideProperty =
            DependencyProperty.Register(nameof(SelectedSlide), typeof(SlideViewModel), typeof(CurrentSlideBehavior), new PropertyMetadata(null, OnSelectedSlideChanged));

        private static void OnSelectedSlideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CurrentSlideBehavior)d).Settext();
        }

        public SlideViewModel SelectedSlide
        {
            get { return (SlideViewModel)GetValue(SelectedSlideProperty); }
            set { SetValue(SelectedSlideProperty, value); }
        }

        public static readonly DependencyProperty SlidesProperty =
           DependencyProperty.Register(nameof(Slides), typeof(ObservableCollection<SlideViewModel>), typeof(CurrentSlideBehavior), new PropertyMetadata(null));

        public ObservableCollection<SlideViewModel> Slides
        {
            get { return (ObservableCollection<SlideViewModel>)GetValue(SlidesProperty); }
            set { SetValue(SlidesProperty, value); }
        }

        private void Settext()
        {
            AssociatedObject.Text = (Slides.IndexOf(SelectedSlide) + 1).ToString();
        }
    }
}
