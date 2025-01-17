using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace PEGASUS_LIME.Design.Algorithmos.Overkill.Protections
{
	internal class L2F
	{
		private static Dictionary<Local, FieldDef> convertedLocals = new Dictionary<Local, FieldDef>();

		private static readonly Random random = new Random();

		public static void Execute()
		{
			foreach (TypeDef item in Ovelkill.Module.Types.Where((TypeDef x) => x != Ovelkill.Module.GlobalType))
			{
				foreach (MethodDef item2 in item.Methods.Where((MethodDef x) => x.HasBody && x.Body.HasInstructions && !x.IsConstructor))
				{
					convertedLocals = new Dictionary<Local, FieldDef>();
					Process(Ovelkill.Module, item2);
				}
			}
		}

		public static string RandomString(int length)
		{
			return new string((from s in Enumerable.Repeat("ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψωABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzابتثجحخدذرزسشصضطظعغفقكلمنهويابتثجحخدذرزسشصضطظعغفقكلمنهوي0123456789艾诶比西迪伊弗吉尺杰开勒马娜哦屁吉吾儿丝提伊吾维豆贝尔维克斯吾贼德אבגדהוזחטיכךלמםנןסעפףצץקרשתאבגדהוזחטיכךלמםנןסעפףצץקרשת", length)
				select s[random.Next(s.Length)]).ToArray());
		}

		public static void Process(ModuleDef Module, MethodDef method)
		{
			method.Body.SimplifyMacros(method.Parameters);
			IList<Instruction> instructions = method.Body.Instructions;
			foreach (Instruction item in instructions)
			{
				if (item.Operand is Local local)
				{
					FieldDef fieldDef = null;
					if (!convertedLocals.ContainsKey(local))
					{
						fieldDef = new FieldDefUser(RandomString(30), new FieldSig(local.Type), FieldAttributes.Public | FieldAttributes.Static);
						Module.GlobalType.Fields.Add(fieldDef);
						convertedLocals.Add(local, fieldDef);
					}
					else
					{
						fieldDef = convertedLocals[local];
					}
					OpCode opCode = null;
					switch (item.OpCode.Code)
					{
					case Code.Ldloc:
						opCode = OpCodes.Ldsfld;
						break;
					case Code.Ldloca:
						opCode = OpCodes.Ldsflda;
						break;
					case Code.Stloc:
						opCode = OpCodes.Stsfld;
						break;
					}
					item.OpCode = opCode;
					item.Operand = fieldDef;
				}
			}
			convertedLocals.ToList().ForEach(delegate(KeyValuePair<Local, FieldDef> x)
			{
				method.Body.Variables.Remove(x.Key);
			});
			convertedLocals = new Dictionary<Local, FieldDef>();
		}
	}
}
