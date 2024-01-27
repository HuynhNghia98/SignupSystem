namespace SignupSystem.Utilities
{
	public static class CreateUserCode
	{
		public static string CreateCode(int count)
		{
			DateTime now = DateTime.Now;
			int year = now.Year;
			int month = now.Month;

			string sequence = (count + 1).ToString().PadLeft(5, '0');

			string studentCode = $"{year}{month.ToString("D2")}-{sequence}";

			return studentCode;
		}
	}
}
