using ClientApp.Models;

namespace ClientApp
{
	internal static class Experiments
	{
		public static TestParameters no_autoscaler()
		{
			return new TestParameters()
			{
				ScalingType = ScalingType.Horizontal,
				PodPrefix = "php-apache",
				TestUseCaseIdentifier = "no_autoscaler",
				LowModeSleepInSeconds = 1,
				TestDurationInSeconds = 300,
				HighModeStartingPointInSeconds = 0,
				HighModeDurationInSeconds = 300,
				HighModeThreadCount = 50
			};
		}

		public static TestParameters vpa_hpa_same_time()
		{
			return new TestParameters()
			{
				ScalingType = ScalingType.Both,
				PodPrefix = "php-apache",
				TestUseCaseIdentifier = "vpa_hpa_same_time",
				LowModeSleepInSeconds = 1,
				TestDurationInSeconds = 600,
				HighModeStartingPointInSeconds = 60,
				HighModeDurationInSeconds = 180,
				HighModeThreadCount = 50
			};
		}

		public static TestParameters hpa_native_default()
		{
			return new TestParameters()
			{
				ScalingType = ScalingType.Horizontal,
				PodPrefix = "php-apache",
				TestUseCaseIdentifier = "hpa_native_default",
				LowModeSleepInSeconds = 1,
				TestDurationInSeconds = 600,
				HighModeStartingPointInSeconds = 60,
				HighModeDurationInSeconds = 180,
				HighModeThreadCount = 50
			};
		}

		public static TestParameters hpa_native_smaller_high_mode()
		{
			return new TestParameters()
			{
				ScalingType = ScalingType.Horizontal,
				PodPrefix = "php-apache",
				TestUseCaseIdentifier = "hpa_native_smaller_high_mode",
				LowModeSleepInSeconds = 1,
				TestDurationInSeconds = 600,
				HighModeStartingPointInSeconds = 60,
				HighModeDurationInSeconds = 90,
				HighModeThreadCount = 50
			};
		}



		public static TestParameters hpa_native_high_mode_only()
		{
			return new TestParameters()
			{
				ScalingType = ScalingType.Horizontal,
				PodPrefix = "php-apache",
				TestUseCaseIdentifier = "hpa_native_high_mode_only",
				LowModeSleepInSeconds = 1,
				TestDurationInSeconds = 600,
				HighModeStartingPointInSeconds = 30,
				HighModeDurationInSeconds = 480,
				HighModeThreadCount = 50
			};
		}
		public static TestParameters hpa_native_low_mode_only()
		{
			return new TestParameters()
			{
				ScalingType = ScalingType.Both,
				PodPrefix = "php-apache",
				TestUseCaseIdentifier = "hpa_native_low_mode_only",
				LowModeSleepInSeconds = 10,
				TestDurationInSeconds = 600,
				HighModeStartingPointInSeconds = 0,
				HighModeDurationInSeconds = 0,
				HighModeThreadCount = 0
			};
		}


		public static TestParameters vpa_native_default()
		{
			return new TestParameters()
			{
				ScalingType = ScalingType.Vertical,
				PodPrefix = "php-apache",
				TestUseCaseIdentifier = "vpa_native_default",
				LowModeSleepInSeconds = 10,
				TestDurationInSeconds = 600,
				HighModeStartingPointInSeconds = 30,
				HighModeDurationInSeconds = 180,
				HighModeThreadCount = 10
			};
		}

		public static TestParameters vpa_native_high_mode_only()
		{
			return new TestParameters()
			{
				ScalingType = ScalingType.Vertical,
				PodPrefix = "php-apache",
				TestUseCaseIdentifier = "vpa_native_high_mode_only",
				LowModeSleepInSeconds = 10,
				TestDurationInSeconds = 600,
				HighModeStartingPointInSeconds = 30,
				HighModeDurationInSeconds = 480,
				HighModeThreadCount = 10
			};
		}

		public static TestParameters vpa_native_low_mode_only()
		{
			return new TestParameters()
			{
				ScalingType = ScalingType.Vertical,
				PodPrefix = "php-apache",
				TestUseCaseIdentifier = "vpa_native_low_mode_only",
				LowModeSleepInSeconds = 10,
				TestDurationInSeconds = 600,
				HighModeStartingPointInSeconds = 0,
				HighModeDurationInSeconds = 0,
				HighModeThreadCount = 10
			};
		}

		public static TestParameters vpa_native_default_scale_down()
		{
			return new TestParameters()
			{
				ScalingType = ScalingType.Vertical,
				PodPrefix = "php-apache",
				TestUseCaseIdentifier = "vpa_native_default_scale_down",
				LowModeSleepInSeconds = 10,
				TestDurationInSeconds = 600,
				HighModeStartingPointInSeconds = 30,
				HighModeDurationInSeconds = 180,
				HighModeThreadCount = 10
			};
		}

		public static TestParameters vpa_native_high_mode_only_scale_down()
		{
			return new TestParameters()
			{
				ScalingType = ScalingType.Vertical,
				PodPrefix = "php-apache",
				TestUseCaseIdentifier = "vpa_native_high_mode_only_scale_down",
				LowModeSleepInSeconds = 10,
				TestDurationInSeconds = 600,
				HighModeStartingPointInSeconds = 30,
				HighModeDurationInSeconds = 480,
				HighModeThreadCount = 10
			};
		}

		public static TestParameters vpa_native_low_mode_only_scale_down()
		{
			return new TestParameters()
			{
				ScalingType = ScalingType.Vertical,
				PodPrefix = "php-apache",
				TestUseCaseIdentifier = "vpa_native_low_mode_only_scale_down",
				LowModeSleepInSeconds = 10,
				TestDurationInSeconds = 600,
				HighModeStartingPointInSeconds = 0,
				HighModeDurationInSeconds = 0,
				HighModeThreadCount = 10
			};
		}
	}
}