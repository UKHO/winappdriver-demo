# WinAppDriverExample

[WinAppDriver](https://github.com/microsoft/WinAppDriver) is an application built and maintained by Microsoft which enables Appium to drive native Windows applications using a Selenium style interface.

WinAppDriver is the Microsoft recommended approach to UI testing UWP, WPF, WinForms and MFC applications since the deprecation of CodedUI.

This demo project shows how to set up a UI test using WinAppDriver and to run it in a pipeline.

## Using WinAppDriver

Follow instructions on <https://github.com/microsoft/WinAppDriver> to get WinAppDriver installed and running.

[This blog post](https://techcommunity.microsoft.com/t5/testingspot-blog/winappdriver-and-desktop-ui-test-automation/ba-p/1124543) also has some good pointers on how to get started and complementary tools.

## Other tools useful when writing desktop applications

### Inspect.exe

[Inspect.exe](https://docs.microsoft.com/en-us/windows/win32/winauto/inspect-objects) gives access to the UIAccess (UIA) tree used by WinAppDriver to drive the application. It can be accessed by installing the Windows SDK (by modifying your Visual Studio features in the Visual Studio Installer) and navigating to `C:\Program Files (x86)\Windows Kits\10\bin\10.0.18362.0\x64\inspect.exe`.

Officially this is considered a legacy application and Microsoft recommend the use of [Accessibility Insights for Windows](https://accessibilityinsights.io/docs/en/windows/overview) instead.

### Accessibility Insights for Windows

Stuff goes here

## Tips, Common problems and Gotchas

The WinAppDriver team has undergone some turmoil and as a result at the time of writing (May 2020) there are many open issues and outdated documentation.

Here are some pointers that we've found missing or hard to find in the documentation:

* WinAppDriver only supports Windows 10
* Run anything that is accessing the UI as administrator. This includes WinAppDriver and Inspect.exe
* `DesiredCapabilities` (frequently referenced in the documentation) is deprecated by Appium. Use `AppiumOptions` instead
