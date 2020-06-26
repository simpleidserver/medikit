// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Android.Content;
using Medikit.Mobile.Controls;
using Medikit.Mobile.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(SearchBarButtonRenderer), new[] { typeof(CustomVisual) })]
namespace Medikit.Mobile.Droid.Renderer
{
    public class SearchBarButtonRenderer : ButtonRenderer
    {
        public SearchBarButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.Gravity = Android.Views.GravityFlags.CenterVertical;
            }
        }
    }
}