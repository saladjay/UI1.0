using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UIControlLib
{
    public class AttachedPropertyButton : DependencyObject
    {
        public static readonly DependencyProperty DirectionProperty = DependencyProperty.RegisterAttached("Direction", typeof(ControlDirection), typeof(AttachedPropertyButton), new PropertyMetadata(ControlDirection.emtpy, OnDirectionChanged));

        private static void OnDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Button obj = d as Button;
            if (obj == null)
                return;
            //if ((ControlDirection)e.NewValue == ControlDirection.Left)
            //{
            //    obj.Style = (Style)obj.FindResource("DirectionLeft");
            //}
            //else if ((ControlDirection)e.NewValue == ControlDirection.Top)
            //{
            //    obj.Style = (Style)obj.FindResource("DirectionTop");
            //}
        }

        public static void SetDirection(DependencyObject obj, ControlDirection dir)
        {
            obj.SetValue(DirectionProperty, dir);
        }

        public static ControlDirection GetDirection(DependencyObject obj)
        {
            return (ControlDirection)obj.GetValue(DirectionProperty);
        }



        public static bool GetActivited(DependencyObject obj)
        {
            return (bool)obj.GetValue(ActivitedProperty);
        }

        public static void SetActivited(DependencyObject obj, bool value)
        {
            obj.SetValue(ActivitedProperty, value);
        }

        // Using a DependencyProperty as the backing store for Activited.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActivitedProperty =
            DependencyProperty.RegisterAttached("Activited", typeof(bool), typeof(AttachedPropertyButton), new PropertyMetadata(false));


    }
}
