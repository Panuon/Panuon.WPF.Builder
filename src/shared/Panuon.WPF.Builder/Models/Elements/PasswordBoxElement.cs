﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class PasswordBoxElement
        : ContentControlElement, IPasswordBoxElement
    {
        #region Ctor
        internal PasswordBoxElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public object Password
        {
            get => (Visual as PasswordBox).Password;
            set => SetValue(InternalPasswordProperty, value);
        }

        public override Type VisualType => typeof(PasswordBox);

        #region InternalPassword
        internal static string GetInternalPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(InternalPasswordProperty);
        }

        internal static void SetInternalPassword(DependencyObject obj, string value)
        {
            obj.SetValue(InternalPasswordProperty, value);
        }

        internal static readonly DependencyProperty InternalPasswordProperty =
            DependencyProperty.RegisterAttached("InternalPassword", typeof(string), typeof(PasswordBoxElement), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnInternalPasswordChanged));
        #endregion

        #endregion

        #region Methods
        protected override FrameworkElement OnCreatingVisual()
        {
            var passwordBox = base.OnCreatingVisual() as PasswordBox;
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            return passwordBox;
        }

        public override void SetValue(string propertyNameOrKey, object value)
        {
            switch (propertyNameOrKey)
            {
                case "password":
                    SetValue(InternalPasswordProperty, value);
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }
        #endregion

        #region Function
        private static void OnInternalPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = (PasswordBox)d;
            var newPassword = (string)e.NewValue;
            if (newPassword != passwordBox.Password)
            {
                passwordBox.Password = newPassword;
            }
        } 

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            passwordBox.SetValue(InternalPasswordProperty, passwordBox.Password);
        }
        #endregion
    }
}
