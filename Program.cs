using System;
using System.Linq;

namespace AddintionalTask_4
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.Clear();
				bool isCardValid = true;
				// Read user input data
				string cardNumber = Input("Type your card number here --> ");
				// Split string to char[] 
				char[] splitCardItems = SplitArray(cardNumber);

				//Check and convert every char to int
				int[] splitCardNumbers = CheckAndConvert(splitCardItems);
				if (splitCardNumbers.Length < 1)
					isCardValid = false;
				else
					// Check for valid user card by Lunh`s algorithm
					isCardValid = Luhn_CardValidChecker(splitCardNumbers);

				// Finding card type
				if (isCardValid)
					PrintCardType(splitCardItems, splitCardNumbers);
				else
					Console.WriteLine("Your card`s number is 'Invalid', try again or use other card number\n");
				string userInput = Input("Type 'exit' or 'q' to quit...\n");
				if (userInput.ToUpper() == "EXIT" || userInput.ToUpper() == "Q")
					break;
			}
		}
		static string Input(string text)
		{
			Console.Write(text);
			return Console.ReadLine();
		}
		static char[] SplitArray(string value)
		{
			return value.ToCharArray();
		}
		static bool Luhn_CardValidChecker(int[] numbersArray)
		{
			// lenght of all numbers 
			int arrayLenght = numbersArray.Length;
			// select every number from penultimate (2,3,4,5,6,7) -> (6, 4, 2) and the next one (2,3,4,5,6,7) -> (7, 5, 3) 
			int _number_sum = 0;
			int _nextNumber_sum = 0;

			// (6, 4, 2)
			for (int i = arrayLenght - 2; i >= 0; i -= 2)
			{
				// (6, 4, 2) -> (12, 8, 4)
				int _number = numbersArray[i] * 2;
				// (12)
				if (_number > 9)
				{
					_number_sum += 1; // (1)  
					_number_sum += _number % 10; // (2)
				}
				else
					_number_sum += _number; // (8, 4)
			}

			// (7, 5, 3)
			for (int i = arrayLenght - 1; i >= 0; i -= 2)
			{
				_nextNumber_sum += numbersArray[i];
			}

			if ((_number_sum + _nextNumber_sum) % 10 == 0)
				return true;
			else
				return false;
		}
		static int[] CheckAndConvert(char[] itemsArray)
		{
			foreach (char item in itemsArray)
			{
				if (!Char.IsNumber(item))
					return new int[] { };
			}
			return Array.ConvertAll(itemsArray, item => (int)Char.GetNumericValue(item));
		}
		static void PrintCardType(char[] items, int[] numbers)
		{
			string firstNums = String.Join("", items[0..2]);

			if (numbers.Length == 15 && (firstNums == "37" || firstNums == "34"))
				Console.WriteLine("American Express\n");
			else if (numbers.Length == 16 && (firstNums == "22" || firstNums == "51" || firstNums == "52" || firstNums == "53" || firstNums == "54" || firstNums == "55"))
				Console.WriteLine("MasterCard\n");
			else if ((numbers.Length == 13 || numbers.Length == 16) && firstNums[0] == '4')
				Console.WriteLine("Visa\n");
			else
				Console.WriteLine("\nNo one from list");
		}
	}
}
