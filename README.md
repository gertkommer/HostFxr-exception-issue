# HostFxr-exception-issue

This project has a native process to host .NET Core using the `hostfxr` libraries. Documentation on the `hostfxr` APIs can be found [here](https://github.com/dotnet/runtime/blob/main/docs/design/features/native-hosting.md).

## Prerequisites

* [.NET 5 SDK](https://dotnet.microsoft.com/download)

* C++ compiler
  * Windows: `cl.exe`
  * Linux/OSX: `g++`

## Build and Run

1. In order to build and run, all prerequisites must be installed. The following are also required:

    * On Linux/macOS, the C++ compiler (`g++`) must be on the path.
    * The C++ compiler (`cl.exe` or `g++`) and `dotnet` must be the same bitness (32-bit versus 64-bit).
      * On Windows, the sample is set up to use the bitness of `dotnet` to find the corresponding `cl.exe`

1. Navigate to the root directory.

1. Run the samples. Do one of the following:

    * Use `dotnet run` (which will build and run at the same time).
	

#Expected output	
The expected way it works is that nativehost(C++) will call the `InvokeCallBack` in Lib(C#). That calls the ErrorHandler function(C++) which throws an error. This error should be caught by the nativehost.
```console
This is a test to see if i get in the catch
```

#Actual output
Windows
```console
This is a test to see if i get in the catch
```
Ubuntu 20.04
```console
terminate called after throwing an instance of 'std::invalid_argument'
  what():  TEST
```