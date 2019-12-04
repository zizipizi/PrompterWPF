using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PrompterWPF.Behaviors
{
    public class KeyToCommand : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(KeyToCommand), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParametrProperty =
        DependencyProperty.Register(nameof(CommandParametr), typeof(object), typeof(KeyToCommand), new PropertyMetadata(null));

        public object CommandParametr
        {
            get { return (object)GetValue(CommandParametrProperty); }
            set { SetValue(CommandParametrProperty, value); }
        }

        public static readonly DependencyProperty KeyProperty =
        DependencyProperty.Register(nameof(Key), typeof(Key), typeof(KeyToCommand), new PropertyMetadata(null));

        public Key Key
        {
            get { return (Key)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }



        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key)
                Command?.Execute(CommandParametr);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.KeyDown += OnKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.KeyDown -= OnKeyDown;
        }
    }
}
