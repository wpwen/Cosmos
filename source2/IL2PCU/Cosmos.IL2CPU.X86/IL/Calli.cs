using System;

namespace Cosmos.IL2CPU.X86.IL
{
	[Cosmos.IL2CPU.OpCode(ILOpCode.Code.Calli)]
	public class Calli: ILOp
	{
		public Calli(ILOpCode aOpCode):base(aOpCode)
		{
		}

    public override void Execute(uint aMethodUID) {
      throw new Exception("TODO:");
    }

    #region Old code
		// using System;
		// using System.IO;
		// 
		// 
		// using CPU = Indy.IL2CPU.Assembler.X86;
		// 
		// namespace Indy.IL2CPU.IL.X86 {
		// 	[OpCode(OpCodeEnum.Calli)]
		// 	public class Calli: Op {
		// 		public Calli(ILReader aReader, MethodInformation aMethodInfo)
		// 			: base(aReader, aMethodInfo) {
		// 		}
		// 		public override void DoAssemble() {
		// 			throw new NotImplementedException("This file has been autogenerated and not been changed afterwards!");
		// 		}
		// 	}
		// }
		#endregion Old code
	}
}
