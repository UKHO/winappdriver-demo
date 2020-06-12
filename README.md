# WinAppDriverExample
[![Build Status](https://ukhogov.visualstudio.com/Pipelines/_apis/build/status/UKHO.winappdriver-demo?branchName=master)](https://ukhogov.visualstudio.com/Pipelines/_build/latest?definitionId=246&branchName=master)

[WinAppDriver](https://github.com/microsoft/WinAppDriver) is an application built and maintained by Microsoft which enables Appium to drive native Windows applications using a Selenium style interface.

WinAppDriver is the Microsoft recommended approach to UI testing UWP, WPF, WinForms and MFC applications since the deprecation of CodedUI.

This demo project shows how to set up a UI test using WinAppDriver and to run it in a pipeline.

## Using WinAppDriver

Follow instructions on <https://github.com/microsoft/WinAppDriver> to get WinAppDriver installed and running.

[This blog post](https://techcommunity.microsoft.com/t5/testingspot-blog/winappdriver-and-desktop-ui-test-automation/ba-p/1124543) also has some good pointers on how to get started and complementary tools.

## Other tools useful when writing desktop applications

### Inspect.exe

[Inspect.exe](https://docs.microsoft.com/en-us/windows/win32/winauto/inspect-objects) gives access to the UI Automation (UIA) tree used by WinAppDriver to drive the application. It can be accessed by installing the Windows SDK (by modifying your Visual Studio features in the Visual Studio Installer) and navigating to `C:\Program Files (x86)\Windows Kits\10\bin\10.0.18362.0\x64\inspect.exe`.

Officially this is considered a legacy application and Microsoft recommend the use of [Accessibility Insights for Windows](https://accessibilityinsights.io/docs/en/windows/overview) instead.

### Accessibility Insights for Windows

[Accessibility Insights for Windows](https://accessibilityinsights.io/docs/en/windows/overview) not only allows access to the UIA tree in a more modern interface than Inspect.exe, it also provides a tool with which to scan a visible UI for common accessibilty violations.

This tool is primarily designed for assessing a Windows application for user accessibility compliance and so there are a few differences to Inspect.exe

* Navigation
  * Like Inspect.exe, the application watches the cursor and updates the UIA tree to reflect what is currently being hovered upon.
  * Press the pause button to stop watching the cursor.
  * Children and siblings of the current element may not be shown in the navigation tree view. These can be navigated to by using keyboard shortcuts defined in the settings.

* Details
  * The properties shown can be configured and may not by default include properties important to UI automation such as the AutomationId.

## Tips, Common problems and Gotchas

The WinAppDriver team has undergone some turmoil and as a result at the time of writing (May 2020) there are many open issues and outdated documentation.

Here are some pointers that we've found missing or hard to find in the documentation:

* WinAppDriver only supports Windows 10
* Run anything that is accessing the UI as administrator. This includes WinAppDriver and Inspect.exe
* `DesiredCapabilities` (frequently referenced in the documentation) is deprecated by Appium. Use `AppiumOptions` instead
* Use the [WinAppDriver](https://marketplace.visualstudio.com/items?itemName=WinAppDriver.winappdriver-pipelines-task) task to start/stop WinAppDriver when running in a CI pipeline. Attempting to replicate using powershell can lead to unexpected issues
* Not all commands supported by Appium are supported by WinAppDriver. Here is a list of the supported APIs - <https://github.com/microsoft/WinAppDriver/blob/master/Docs/SupportedAPIs.md>
