namespace Game
{
	public readonly struct Result
	{
		public readonly string Content;
		public readonly bool Success;

		public Result(string content, bool success)
		{
			Content = content;
			Success = success;
		}
	}

	public static class ResultExtensions
	{
		public static Result ToResult<T>(this T value, bool success) => new Result(value.Serialize(), success);
		public static Result ToSuccess<T>(this T value) => value.ToResult(true);
		public static Result ToFailure<T>(this T value) => value.ToResult(false);
	}
}
