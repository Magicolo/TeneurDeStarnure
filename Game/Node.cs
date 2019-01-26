using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
	public static class NodeExtensions
	{
		public static Node Replace(this Node node, Func<Node, Node> replace)
		{
			var children = node.Children.Select(child => child.Replace(replace)).ToArray();
			return replace(new Node(node.Data, node.Build, children));
		}

		public static Node Typewrite(this Node node, float speed = 10) => speed <= 0f ? node : node.Replace(child =>
			child.Data is Data.Text data ?
			data.Value
				.Select(character => new Node(new Data.Text { Value = character.ToString() }, child.Build, child.Children))
				.Separate(Node.Delay(1f / speed))
				.Sequence() :
			child);

		public static Node Sequence(this IEnumerable<Node> nodes) => Node.Sequence(nodes.ToArray());
	}

	public sealed class Node
	{
		public static Node Text(char value) => Text(value.ToString());
		public static Node Text(string value) => new Node(
			new Data.Text { Value = value },
			node => node.Data is Data.Text data ?
				JObject.FromObject(new { Type = data.GetType().Name, data.Value }) :
				default);

		public static Node Line(string value) => Sequence(Text(value), Break());

		public static Node Break() => new Node(
			new Data.Break(),
			node => node.Data is Data.Break data ?
				JObject.FromObject(new { Type = data.GetType().Name }) :
				default);

		public static Node Delay(float time) => new Node(
			new Data.Delay { Time = time },
			node => node.Data is Data.Delay data ?
				JObject.FromObject(new { Type = data.GetType().Name, data.Time }) :
				default);

		public static Node Sequence(params Node[] nodes)
		{
			IEnumerable<Node> Children(Node node)
			{
				if (node.Data is Data.Sequence)
					foreach (var child in node.Children.SelectMany(Children)) yield return child;
				else yield return node;
			}

			if (nodes.Length == 1) return nodes[0];
			return new Node(
				new Data.Sequence(),
				node => node.Data is Data.Sequence data ?
					JObject.FromObject(new
					{
						Type = data.GetType().Name,
						Children = Children(node).Select(child => child.Build(child)).ToArray()
					}) :
					default,
				nodes);
		}

		public readonly IData Data;
		public readonly Func<Node, JObject> Build;
		public readonly Node[] Children;
		public Node(IData data, Func<Node, JObject> build, params Node[] children)
		{
			Data = data;
			Build = build;
			Children = children;
		}
	}

	public interface IData { }

	namespace Data
	{
		public struct Delay : IData { public float Time; }
		public struct Text : IData { public string Value; }
		public struct Break : IData { }
		public struct Sequence : IData { }
	}
}
