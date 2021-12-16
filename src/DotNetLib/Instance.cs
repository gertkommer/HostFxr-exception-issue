using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace DotNetLib
{
	public class Instance
	{
		private string mS;
		Instance(string s)  { setS(s); }
		public struct LibArgsInstance
		{
			public IntPtr Message;
		}
	
		public delegate IntPtr getInstanceDelegate(LibArgsInstance libArgs);
		public static IntPtr getInstance(LibArgsInstance arg)
		{
			string message = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
					 ? Marshal.PtrToStringUni(arg.Message)
					 : Marshal.PtrToStringUTF8(arg.Message);
			var inst = new Instance(message);
			GCHandle handle = GCHandle.Alloc(inst);
			IntPtr ptr = GCHandle.ToIntPtr(handle);
			Console.WriteLine($"getInstance(\"{message}\") return {ptr}");
			return ptr;
		}
		private void setS(string pS)
		{
			this.mS = pS;
		}

		public delegate string getSDelegate(IntPtr instance);
		public static string getS(IntPtr instance)
		{
			Console.WriteLine($"getS() of {instance}");
			if (instance == IntPtr.Zero) return "";
			GCHandle gch = GCHandle.FromIntPtr(instance);

			Instance i = (gch.Target as Instance);
			return i.getS();
		}
		private string getS()
		{
			return mS;
		}
	}
}
