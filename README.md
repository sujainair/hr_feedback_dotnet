# HR Feedback

This is my first attempt at creating a web application with .Net, created for an interview assignment.
The front end is based on [Google's Polymer Framework](https://www.polymer-project.org/) and I have used static/singleton data instead of interface with a DB.

The basic idea is to submit a HR Feedback form and see the overview of all respondents after a submit.
I have only tested this on Google Chrome.

## Build the application

For the client, go to HrFeedback\client and download the bower dependencies.

```
$ <path to Project>\HrFeedback\client > bower install
```
The solution is created in Visual Studio 2017, so just open the solution and hit build to get the application up and running.

## Running the application

After it's built, to run the application from command line,
```
$ <path to IIS>\iisexpress.exe /path:<path to project> /port:<port number>
```