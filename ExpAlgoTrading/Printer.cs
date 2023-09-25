using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpAlgoTrading {

	static class Printer {
		private static void Warn(this string message) {
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("[WARNING] {0}", message);
			Console.ResetColor();
		}

		private static void Error(this string message) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(message);
			Console.ResetColor();
			Environment.Exit(Environment.ExitCode);
		}

		private static void Success(this string message) {
			Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
			Console.ResetColor();
		}

		#region OnBuy, OnSell
		public static void ShowBuySignal(string symbol, double price) {
			DisplayTime();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("[СИГНАЛ К ПОКУПКЕ]: {0} за {1} USD", symbol, price);
            Console.WriteLine();
			Console.ResetColor();
		}

		public static void DisplaySellSignal(string symbol, double price) {
			DisplayTime();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("[СИГНАЛ К ПРОДАЖЕ]: {0} за {1} USD", symbol, price);
			Console.WriteLine();
			Console.ResetColor();
		}

		public static void DisplaySellForced(string symbol, double price) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("{0} был принудительно продан за {1} USD", symbol, price);
			Console.WriteLine();
			Console.ResetColor();
		}

		#endregion // OnBuy, OnSell

		#region OnWarnings

		public static void WarnFileOpen() {
			string message = "Результаты транзакций не могут быть записаны в файл, он используется другим процессом";
			message.Warn();
		}

		public static void WarnCantBuy(string symbol, double price) {
			string message = string.Format(
                "СИГНАЛ К ПОКУПКЕ: {0} (за цену {1:0.#####} USD) нельзя купить - поднимите депозит", 
				symbol, price
			);
			message.Warn();
            Console.WriteLine();
		}

		public static void WarnCantSell(string symbol, double price) {
			DisplayTime();
			string message = string.Format(
                "СИГНАЛ К ПРОДАЖЕ: {0} (по цене {1:0.#####} USD) не может быть продан - нет в наличии", 
				symbol, price
			);
			message.Warn();
			Console.WriteLine();
		}

		public static void WarnEmptyWatchlist() {
			string message = "Обязательно используйте <symbol> команду, иначе ваш список наблюдения останется пустым";
			message.Warn();
		}

		public static void WarnAlreadyInWatchlist(string inSymbol) {
			string message = string.Format("{0} уже в списке наблюдения", inSymbol);
			message.Warn();
		}

		public static void WarnNotFound(string inSymbol) {
			string message = string.Format("{0} не найден", inSymbol);
			message.Warn();
		}

		public static void WarnIsUnavailable(string inSymbol) {
			string message = string.Format("{0} не доступен", inSymbol);
			message.Warn();
		}

		public static void WarnUnknownAction(string userInput) {
			string message = string.Format("Неизвестное действие: \"{0}\"", userInput);
			message.Warn();
		}

		public static void WarnMinDepositRequired(double minDeposit) {
			string message = string.Format("Минимальный депозит {0} USD", minDeposit);
			message.Warn();
		}

		public static void WarnInvalidAmount() {
			string message = "Недопустимая сумма";
			message.Warn();
		}

		public static void WarnConnectionLost(string endpoint) {
			string message = string.Format("Подключение к {0} утеряно", endpoint);
			message.Warn();
		}

		#endregion // OnWarnings

		#region OnError

		public static void ErrorConnectionLost() {
			string message = "Нет связи: проверьте подключение к сети интернет";
			message.Error();
		}

        #endregion // OnError

        #region OnSuccess

        public static void DisplayDepositSuccess(double depositValue, double fee, double currentBalance) {
			string message = string.Format("{0} USD добавлено (комиссия: {1:P}), текущий баланс: {2:0.###} USD", depositValue, fee, currentBalance);
			message.Success();
        }

		public static void DisplayAddedSuccess(string inCryptocurrency) {
			string message = string.Format("{0} успешно добавлено", inCryptocurrency);
			message.Success();
		}

		public static void DisplayRemovedSuccess(string inCryptocurrency) {
			string message = string.Format("{0} успешно удалено", inCryptocurrency);
			message.Success();
		}
        #endregion // OnSuccess

        public static void DisplayTotalBalance(double finalBalance, string currency, double fee) {
			string message = string.Format("Вы закончили с {0:0.##} {1} (комиссия за снятие средств: {2:P})", finalBalance, currency, fee);
			Console.WriteLine(message);
		}

		public static void DisplayHelpHeader() {
			string message = "Поддерживаемые команды (без учета регистра, без <>): ";
			Console.WriteLine(message);
        }

		public static void DisplaySeparator() {
			int lineLength = 40;
			Console.WriteLine(new string('-', lineLength));
		}

		public static void PrintCommandsCommon(string inCommand, bool wasFound, InputProcessor proc) {
			if(!wasFound) {
				WarnUnknownAction(inCommand);
				proc.ShowHelp();
			}
		}

		public static void DisplayEstimatedWithdrawal(double estBalance, string currency, double fee) {
			string message = string.Format("Предполагаемый вывод: {0:0.#####} {1} (после {2:P} комиссии)", estBalance, currency, fee);
			Console.WriteLine(message);
		}

		public static void DisplayNoTransactionsYet() {
			string message = "Никаких транзакций еще не совершено.";
			Console.WriteLine(message);
		}

		public static void DisplayHeader() {
			Console.WriteLine("ExpAlgoTrading");
			Console.WriteLine("	Символы криптовалюты смотрите на https://coinmarketcap.com/exchanges/binance");
			Console.WriteLine("	- примеры использования: BTCUSDT ETHUSDT SOLUSDT ADAUSDT (регистронезависим, можно использовать косую черту)");
			Console.Write("Добавьте символы криптовалюты в свой список наблюдения: ");
		}

        public static void DisplayWatchlistEmpty() {
			string message = "Ваш список наблюдения пуст";
			Console.WriteLine(message);
		}

		public static void DisplayTime() {
			Console.WriteLine(DateTime.Now);
		}
    }
}
