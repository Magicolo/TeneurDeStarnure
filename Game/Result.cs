using Newtonsoft.Json.Linq;

namespace Game
{
	public readonly struct Result
	{
		public readonly JToken Content;
		public readonly bool Success;

		public Result(JToken content, bool success)
		{
			Content = content;
			Success = success;
		}
	}

	public static class ResultExtensions
	{
		public static Result ToResult<T>(this T value, bool success) => new Result(value.ToJson(), success);
		public static Result ToSuccess<T>(this T value) => value.ToResult(true);
		public static Result ToFailure<T>(this T value) => value.ToResult(false);
	}
}
