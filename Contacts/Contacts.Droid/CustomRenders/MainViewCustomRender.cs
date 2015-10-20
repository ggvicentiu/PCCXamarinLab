using System;
using Contacts.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Hardware;
using Android.Widget;
using Android.Views;


[assembly:ExportRenderer (typeof(Contacts.Views.MainView), typeof(Contacts.Droid.MainViewCustomRender))]
namespace Contacts.Droid
{
	public class MainViewCustomRender : PageRenderer, ISensorEventListener
	{
		MainView mainView;
		SensorManager sensorManager;
		Sensor sensor;
		bool hasUpdated = false;
		DateTime lastUpdate;
		float last_x = 0.0f;
		float last_y = 0.0f;
		float last_z = 0.0f;
		const int ShakeDetectionTimeLapse = 350;
		const double ShakeThreshold = 400;

		public MainViewCustomRender ()
		{
		}

		public void OnAccuracyChanged (Android.Hardware.Sensor sensor, Android.Hardware.SensorStatus accuracy)
		{
		}

		public void OnSensorChanged (Android.Hardware.SensorEvent e)
		{
			if (e.Sensor.Type == Android.Hardware.SensorType.Accelerometer) {
				float x = e.Values [0];
				float y = e.Values [1];
				float z = e.Values [2];

				DateTime curTime = System.DateTime.Now;
				if (hasUpdated == false) {
					hasUpdated = true;
					lastUpdate = curTime;
					last_x = x;
					last_y = y;
					last_z = z;

				} else {
					if ((curTime - lastUpdate).TotalMilliseconds > ShakeDetectionTimeLapse) {
						float diffTime = (float)(curTime - lastUpdate).TotalMilliseconds;
						lastUpdate = curTime;
						float total = x + y + z - last_x - last_y - last_z;
						float speed = Math.Abs (total) / diffTime * 10000;

						if (speed > ShakeThreshold) {
							speed = 0;
							if (mainView != null) {
								mainView.ShakeReceived ();

							}

						}
						last_x = x;
						last_y = y;
						last_z = z;
					}
				}
			}
		}

		protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged (e);
			mainView = e.NewElement as MainView;
			sensorManager = (SensorManager)Context.GetSystemService (Context.SensorService);
			sensor = sensorManager.GetDefaultSensor (Android.Hardware.SensorType.Accelerometer);
			sensorManager.RegisterListener (this, sensor, Android.Hardware.SensorDelay.Game);
		}

		protected override void OnDetachedFromWindow ()
		{
			mainView = null;
			sensorManager.UnregisterListener (this);

		}



	}
}

