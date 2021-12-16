using System;
using System.Runtime.InteropServices;
/***********************************************************************
 * 
 * 
 * EXAMPLE CLASS
 * 
 * 
 * 
 * ********************************************************************/
namespace DotNetLib
{
    public static class Lib
    {
        private static int s_CallCount = 1;

        [StructLayout(LayoutKind.Sequential)]
        public struct LibArgs
        {
            public IntPtr Message;
            public int Number;
        }

        public static int Hello(IntPtr arg, int argLength)
        {
            if (argLength < Marshal.SizeOf(typeof(LibArgs)))
            {
                return 1;
            }

            LibArgs libArgs = Marshal.PtrToStructure<LibArgs>(arg);
            Console.WriteLine($"Hello, world! from {nameof(Lib)} [count: {s_CallCount++}]");
            PrintLibArgs(libArgs);
            return 5;
        }

        public delegate string CustomEntryPointDelegate(LibArgs libArgs);
        public static string CustomEntryPoint(LibArgs libArgs)
        {
            Console.WriteLine($"Hello, world! from {nameof(CustomEntryPoint)} in {nameof(Lib)}");
            PrintLibArgs(libArgs);
            return "String instance";
        }
         [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
         public delegate void ErrorHandler();

         public delegate void InvokeCallBackDelegate([MarshalAs(UnmanagedType.FunctionPtr)] ErrorHandler function);
         public static void InvokeCallBack([MarshalAs(UnmanagedType.FunctionPtr)] ErrorHandler function)
         {
            function();
         }

#if NET5_0
      [UnmanagedCallersOnly]
        public static void CustomEntryPointUnmanaged(LibArgs libArgs)
        {
            CustomEntryPoint(libArgs);
        }
#endif

        private static void PrintLibArgs(LibArgs libArgs)
        {
            string message = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? Marshal.PtrToStringUni(libArgs.Message)
                : Marshal.PtrToStringUTF8(libArgs.Message);

            Console.WriteLine($"-- message: {message}");
            Console.WriteLine($"-- number: {libArgs.Number}");
        }
    }
}
