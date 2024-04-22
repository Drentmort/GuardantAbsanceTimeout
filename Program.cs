using Skyros.Videonet.GuardantChecker;
using System;

namespace GuradantAbsanseTimeout
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"{DateTime.Now} Начинаем проверку ключа GUARDANT");
			Console.WriteLine($"{DateTime.Now} ключ GUARDANT найден = {GuardantKeyChecker.HasGuardantKey()}");
			Console.WriteLine($"{DateTime.Now} Завершаем проверку ключа GUARDANT");

			Console.WriteLine($"{DateTime.Now} Нажите Enter для выхода..");
			Console.ReadLine();
		}
	}
}
