﻿using System;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace Vision.Behaviors
{
    /// <summary>
    /// Implementación que le indica al ListBox que debe mostrar el SelectedItem al inicio, en vez de posicionar el scroll directamente sobre el primer elemento.
    /// </summary>
    public class ScrollIntoViewForListBox : Behavior<ListBox>
    {
        /// <summary>
        ///  When Beahvior is attached
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        /// <summary>
        /// On Selection Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AssociatedObject_SelectionChanged(object sender,
                                               SelectionChangedEventArgs e)
        {
            if (sender is ListBox)
            {
                ListBox listBox = (sender as ListBox);
                if (listBox.SelectedItem != null)
                {
                    listBox.Dispatcher.BeginInvoke(
                        (Action)(() =>
                        {
                            listBox.UpdateLayout();
                            if (listBox.SelectedItem !=
                                null)
                                listBox.ScrollIntoView(
                                    listBox.SelectedItem);
                        }));
                }
            }
        }
        /// <summary>
        /// When behavior is detached
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.SelectionChanged -=
                AssociatedObject_SelectionChanged;

        }
    }
}
