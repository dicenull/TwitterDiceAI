using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterDiceAI
{
	class MarkovBlock
	{
		public string Key { get; }
		public string Value { get; }
		public string Next { get; }

		public MarkovBlock(string key, string value, string next)
		{
			Key = key;
			Value = value;
			Next = next;
		}

		public MarkovBlock(string[] nodes)
			: this(nodes[0], nodes[1], nodes[2])
		{ }
	}
}
