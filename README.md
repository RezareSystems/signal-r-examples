# SignalR Examples

These examples show various aspects of setting up SignalR using the .NET core framework, including the server, a Xamarin mobile app client and a Windows Service logger. 

The example shows a simple chat application where messages are sent between connected clients using SignalR as the communication mechanism.

The examples loosely follow those provided by Microsoft in their SignalR documentation. See the **Built With** section for a link to the SignalR documentation.

## Getting Started

The server is configured to run on IIS Express, which, by default, doesn't allow remote devices to connect to it. To get this to work with the mobile app, you will need to explicilty configure IIS Express to listen for remote connections. To do this, follow these steps:

1. Open command prompt as administrator
2. Run `netsh http add urlacl url=http://[your IP address]:50091/ user=everyone`
3. Open up port `50091` to TCP traffic through your firewall
4. If IIS Express is currently running, restart it

When you have finished using the app, it is good practice to stop IIS Express from listening by running `netsh http delete urlacl url=http://[your IP address]:50091/` in the command prompt as administrator.

### Running the Windows Service

The service cannot be debugged by pressing F5 in Visual Studio, it must be installed and run manually and then the debugger must be attached manually. 

To install and run the service, follow these steps:

1. Build the project by selecting **Build Solution** from the **Build** menu
2. From the Windows **Start** menu or screen, choose **Visual Studio**, **Visual Studio Tools**, **Developer Command Prompt** and open it as Administrator
3. Access the directory where the project was compiled, ie. *bin/[Configuration]* in the project folder
4. Run `installutil SignalR.Service.exe`
5. You will be prompted for a username and passsword to attach to the service, use the credentials of an adminstrator user

If you are wanting to make changes to the service and the re-install it, you must uninstall it first and then re-install. 

To uninstall the service, run `installutil /u SignalR.Service.exe` within the directory as above.

See the [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/framework/windows-services/how-to-debug-windows-service-applications) for information about how to debug the service.

### Prerequisites

* Visual Studio v15.7.3 or later
* .NET Core SDK 2.1 or later
* Xamarin Forms

## Built With

* [SignalR](https://docs.microsoft.com/en-us/aspnet/core/signalr/?view=aspnetcore-2.1) - Real-time messaging platform
* [Xamarin Forms](https://docs.microsoft.com/en-us/xamarin/#pivot=platforms&panel=XamarinForms) - Mobile app framework

## Authors

* **Sean Coon** - *Initial work* - [sean-coon](https://github.com/sean-coon)

See also the list of [contributors](https://github.com/RezareSystems/signal-r-examples/graphs/contributors) who participated in this project.

