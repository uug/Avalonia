﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Perspex.Controls.Presenters;
using Perspex.Controls.Primitives;
using Perspex.Controls.Templates;
using Perspex.LogicalTree;
using Xunit;

namespace Perspex.Controls.UnitTests
{
    public class HeaderedItemsControlTests
    {
        [Fact]
        public void Control_Header_Should_Be_Logical_Child_Before_ApplyTemplate()
        {
            var target = new HeaderedItemsControl
            {
                Template = GetTemplate(),
            };

            var child = new Control();
            target.Header = child;

            Assert.Equal(child.Parent, target);
            Assert.Equal(child.GetLogicalParent(), target);
            Assert.Equal(new[] { child }, target.GetLogicalChildren());
        }

        [Fact]
        public void DataTemplate_Created_Control_Should_Be_Logical_Child_After_ApplyTemplate()
        {
            var target = new HeaderedItemsControl
            {
                Template = GetTemplate(),
            };

            target.Header = "Foo";
            target.ApplyTemplate();
            target.HeaderPresenter.UpdateChild();

            var child = target.HeaderPresenter.Child;

            Assert.NotNull(child);
            Assert.Equal(target, child.Parent);
            Assert.Equal(target, child.GetLogicalParent());
            Assert.Equal(new[] { child }, target.GetLogicalChildren());
        }

        [Fact]
        public void Clearing_Content_Should_Clear_Logical_Child()
        {
            var target = new HeaderedItemsControl();
            var child = new Control();

            target.Header = child;
            target.Header = null;

            Assert.Null(child.Parent);
            Assert.Null(child.GetLogicalParent());
            Assert.Empty(target.GetLogicalChildren());
        }

        private FuncControlTemplate GetTemplate()
        {
            return new FuncControlTemplate<HeaderedItemsControl>(parent =>
            {
                return new Border
                {
                    Child = new ContentPresenter
                    {
                        Name = "PART_HeaderPresenter",
                        [~ContentPresenter.ContentProperty] = parent[~HeaderedItemsControl.HeaderProperty],
                    }
                };
            });
        }
    }
}
