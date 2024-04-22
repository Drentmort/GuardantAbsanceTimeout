using grdlic;
using System.Runtime.CompilerServices;
using System;

namespace Skyros.Videonet.GuardantChecker
{
	public static class GuardantKeyChecker
	{
		private static class KeyAccess
		{
			internal const string Name = "SKAQRTV";
			internal const uint Public = 0xadfdaad5;
			internal const uint Read = 0x24cec6e;
			internal const uint Write = 0xec8cd6f0;
			internal const uint Master = 0xe856428a;
		}

		private static class KeyTestAccess
		{
			internal const uint Public = 0x519175b7;
			internal const uint Read = 0x51917645;
		}

		private const uint BaseVideonetFeature = 451;

		static GuardantKeyChecker()
		{
			GrdlicApi.SetPathToNativeLib(string.Empty, "grdlic_x86.dll", "grdlic.dll");
		}

		public static bool HasGuardantKey()
		{
			var hasGuardantKey = false;
			try
			{
				var feature = new GrdlicApi.Feature(BaseVideonetFeature);
				feature.Open();
				feature.Close();
				hasGuardantKey = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{DateTime.Now} Проверка ключа Guardant не прошла. {ex.Message};");
				hasGuardantKey = false;
			}
			return hasGuardantKey;
		}

		private static void Open(this GrdlicApi.Feature feature)
		{
			var vendorAccessCodes = new VendorCodes(KeyAccess.Public, KeyAccess.Read);

			// Выполняет логин к заданному компоненту (Feature) для создания сессии в соответствии с предварительно определенными параметрами поиска.
			var status = feature.Login(vendorAccessCodes, visibility: string.Empty);
			ThrowIfStatusError(status);
		}

		internal static void Close(this GrdlicApi.Feature feature)
		{
			var status = feature.Logout();
			ThrowIfStatusError(status);
		}

		private static void ThrowIfStatusError(Status status, [CallerMemberName] string caller = null)
		{
			if (status != Status.OK)
			{
				var description = GetStatusDescription(status);
				throw new InvalidOperationException($"{caller}: {description}");
			}
		}

		private static string GetStatusDescription(Status status)
		{
			var errorStats = GrdlicApi.GrdGetErrorMessage(status, GrdLanguageId.GRD_LANG_EN, out string error);
			if (errorStats == Status.OK)
			{
				return $"status={status}, error={error}";
			}
			else
			{
				return $"status={status}";
			}
		}
	}
}