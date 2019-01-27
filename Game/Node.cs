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

		public static Node Typewrite(this Node node, float speed = 50f) => speed <= 0f ? node : node.Replace(child =>
			child.Data is Data.Text data ?
			data.Value
				.Select(character => new Node(new Data.Text { Value = character.ToString() }, child.Build, child.Children))
				.Separate(Node.Delay(1f / speed))
				.Sequence() :
			child);

		public static Node Sequence(this IEnumerable<Node> nodes) => Node.Sequence(nodes.ToArray());

		public static Node Normal(this Node node, bool style = true, bool font = true) =>
			style ? Node.Style("normal", node).Normal(false, font) :
			font ? Node.FontWeight("normal", node).Normal(style, false) :
			node;

		public static Node Italic(this Node node) => Node.Style("italic", node);
		public static Node Oblique(this Node node) => Node.Style("oblique", node);
		public static Node Bold(this Node node) => Node.FontWeight("bold", node);
		public static Node Size(this Node node, float percent) => Node.FontSize($"{percent}%", node);
		public static Node Thickness(this Node node, float value) => Node.FontWeight(value.ToString(), node);
		public static Node Overline(this Node node) => Node.Decoration("overline", node);
		public static Node Underline(this Node node) => Node.Decoration("underline", node);
		public static Node LineThrough(this Node node) => Node.Decoration("line-through", node);
		public static Node Color(this Node node, byte red, byte green, byte blue) => Node.Color($"rgb({red},{green},{blue})", node);
	}

	public sealed class Node
	{
		public static Node State(Func<State, string> get) => new Node(
			new Data.State { Get = get },
			(node, state) =>
			{
				if (node.Data is Data.State data)
				{
					node = Text(data.Get(state));
					return node.Build(node, state);
				}
				return default;
			});

		public static Node Text(char value) => Text(value.ToString());
		public static Node Text(string value) => new Node(
			new Data.Text { Value = value },
			(node, _) => node.Data is Data.Text data ?
				JObject.FromObject(new { Type = data.GetType().Name, data.Value }) :
				default);

		public static Node Line(string value) => Sequence(Text(value), Break());

		public static Node Break() => new Node(
			new Data.Break(),
			(node, _) => node.Data is Data.Break data ?
				JObject.FromObject(new { Type = data.GetType().Name }) :
				default);

		public static Node Delay(float time) => new Node(
			new Data.Delay { Time = time },
			(node, _) => node.Data is Data.Delay data ?
				JObject.FromObject(new { Type = data.GetType().Name, data.Time }) :
				default);

		public static Node If(Func<State, bool> condition, Node @true, Node @false) => new Node(
			new Data.If { Condition = condition },
			(node, state) => node.Data is Data.If data ?
				(data.Condition(state) ? node.Children[0].Build(node.Children[0], state) : node.Children[1].Build(node.Children[1], state)) :
				default,
			@true, @false);

		public static Node Style(string value, params Node[] children) => new Node(
			new Data.Style { Value = value },
			(node, state) => node.Data is Data.Style data ?
				JObject.FromObject(new
				{
					Type = data.GetType().Name,
					data.Value,
					Children = node.Children.Select(child => child.Build(child, state)).ToArray()
				}) :
				default,
			children);

		public static Node FontWeight(string value, params Node[] children) => new Node(
			new Data.FontWeight { Value = value },
			(node, state) => node.Data is Data.FontWeight data ?
				JObject.FromObject(new
				{
					Type = data.GetType().Name,
					data.Value,
					Children = node.Children.Select(child => child.Build(child, state)).ToArray()
				}) :
				default,
			children);

		public static Node FontSize(string value, params Node[] children) => new Node(
			new Data.FontSize { Value = value },
			(node, state) => node.Data is Data.FontSize data ?
				JObject.FromObject(new
				{
					Type = data.GetType().Name,
					data.Value,
					Children = node.Children.Select(child => child.Build(child, state)).ToArray()
				}) :
				default,
			children);

		public static Node Decoration(string value, params Node[] children) => new Node(
			new Data.Decoration { Value = value },
			(node, state) => node.Data is Data.Decoration data ?
				JObject.FromObject(new
				{
					Type = data.GetType().Name,
					data.Value,
					Children = node.Children.Select(child => child.Build(child, state)).ToArray()
				}) :
				default,
			children);

		public static Node Color(string value, params Node[] children) => new Node(
			new Data.Color { Value = value },
			(node, state) => node.Data is Data.Color data ?
				JObject.FromObject(new
				{
					Type = data.GetType().Name,
					data.Value,
					Children = node.Children.Select(child => child.Build(child, state)).ToArray()
				}) :
				default,
			children);

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
				(node, state) => node.Data is Data.Sequence data ?
					JObject.FromObject(new
					{
						Type = data.GetType().Name,
						Children = Children(node).Select(child => child.Build(child, state)).ToArray()
					}) :
					default,
				nodes);
		}

		public readonly IData Data;
		public readonly Func<Node, State, JObject> Build;
		public readonly Node[] Children;
		public Node(IData data, Func<Node, State, JObject> build, params Node[] children)
		{
			Data = data;
			Build = build;
			Children = children;
		}
	}

	public interface IData { }

	namespace Data
	{
		public struct If : IData { public Func<Game.State, bool> Condition; }
		public struct State : IData { public Func<Game.State, string> Get; }
		public struct Delay : IData { public float Time; }
		public struct Text : IData { public string Value; }
		public struct Break : IData { }
		public struct Sequence : IData { }
		public struct Color : IData { public string Value; }
		public struct Style : IData { public string Value; }
		public struct Decoration : IData { public string Value; }
		public struct FontWeight : IData { public string Value; }
		public struct FontSize : IData { public string Value; }
	}
}
