﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal abstract class DecoratorElement
        : Element, IDecoratorElement
    {
        #region Ctor
        internal DecoratorElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        public IModule Child
        {
            get => _child;
            set
            {
                _child = value;
                _visualBuilder.AddChild(value);
            }
        }
        private IModule _child;

        public IGridElement GridSplit(IModule[] elements,
            string[] spans = null,
            object width = null,
            object height = null,
            object vertical = null,
            object horizontal = null,
            object background = null,
            object borderBrush = null,
            object borderThickness = null)
        {
            throw new NotImplementedException();
        }

        public void Stack(IModule[] elements,
            object orientation = null,
            object width = null,
            object height = null,
            object vertical = null,
            object horizontal = null,
            object background = null,
            object borderBrush = null,
            object borderThickness = null)
        {
            throw new NotImplementedException();
        }

    }
}
