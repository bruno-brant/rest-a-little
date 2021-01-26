![logo](https://raw.githubusercontent.com/bruno-brant/rest-a-little/master/logo.png) 
Rest-a-little 
=============

Small Windows utility to remind the user it's time to take a break from the computer.

Why?
----
I have a problematic case of RSI and need to take regular breaks from the computer. However, when I'm [concetrated](https://en.wikipedia.org/wiki/Flow_(psychology)) I can go for hours, ignoring all signs from my body that it's time to stop. This utility runs in the background and warns me if I go too long without a break.

How?
----
You configure at least two intervals: how long can you work without a break and how long should a break be. If you don't take the break until the interval ends, the app will start to bug you to take a break. Breaks needs to last at least the configured time - if you go back to using the computer before you stay the minimum configured time, the app will bug you again.


Installation
------------

### Manually
Just copy the .exe somewhere in your machine. If you want it start together with Windows, go WIN+R, type `shell:startup` and add a shortcut to the exe.

### Installator

TBD

Building from source
--------------------

Just use the dotnet CLI:

```bash
dotnet restore
dotnet build
```

Testing the source
------------------

The code has a good number of tests and they can be ran from the CLI as well:

```
dotnet test
```

> I suggest you use Visual Studio 2019 (Community, Pro, Enterprise, your pick). VS Code can also be used, but the experience with VS is much superior.

---

Sleepy icon made by [Freepik](https://www.flaticon.com/authors/freepik).
Active and resting icon were based on the sleepy icon.
