# Asynchronous delegates and callback

Demonstrates `BeginInvoke`/`EndInvoke` on a delegate with a callback, and how to marshal an exception thrown on the worker thread back to the caller via a collected `List<Exception>` and a `ManualResetEvent`.

Originally published at [Asynchronous delegates and callback](https://blogs.msdn.microsoft.com/thottams/2009/04/11/asynchronous-delegates-and-callback/) on the MSDN `thottams` blog.

## Building

```text
csc del.cs
del.exe
```

## Note

This is archived sample code from a blog post written years ago. It targets the .NET Framework / Visual Studio versions of that era and is kept here for reference.

