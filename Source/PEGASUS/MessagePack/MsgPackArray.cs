using System.Collections.Generic;

namespace Engine.Metafora_Dedomenon
{
	public class MsgPackArray
	{
		private readonly List<MsgPack> children;

		private readonly MsgPack owner;

		public MsgPack this[int index] => children[index];

		public int Length => children.Count;

		public MsgPackArray(MsgPack msgpackObj, List<MsgPack> listObj)
		{
			owner = msgpackObj;
			children = listObj;
		}

		public MsgPack Add()
		{
			return owner.AddArrayChild();
		}

		public MsgPack Add(string value)
		{
			MsgPack msgPack = owner.AddArrayChild();
			msgPack.AsString = value;
			return msgPack;
		}

		public MsgPack Add(long value)
		{
			MsgPack msgPack = owner.AddArrayChild();
			msgPack.SetAsInteger(value);
			return msgPack;
		}

		public MsgPack Add(double value)
		{
			MsgPack msgPack = owner.AddArrayChild();
			msgPack.SetAsFloat(value);
			return msgPack;
		}
	}
}
