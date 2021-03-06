﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Nucleus;

namespace NucleusMobileSample.Droid
{
	[Activity (Label = "NucleusMobileSample", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            Core.Instance.PlatformManager.SetAndroid(this);

			global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new NucleusMobileSample.App ());
		}
	}
}

