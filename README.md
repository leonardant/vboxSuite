vboxSuite
=========

vboxSuite is...
1)  4 portable dll's for use in .net 4 applications allowing the code to communicate with the vbox device as per...
http://www.vboxcomm.com/

2) a set of unit tests for the above [see notes below]

3) the libmpeg2 library allowing mpg2 video rendering [see notes below]

4) a store [windows RT] application demoing the above 1 & 3 combined 

The current device tested and developed against is an XTi 3340 running SW version XTI_VB.2.39 [OS on the box!].

Earlier versions of the SW may work when integrated with non windows RT players but due to a restriction in windows RT when handling web requests not terminating correctly (CRLF).The Above Software updated fixes this, thanks to those at vbox support!

The Solution contains a unit test project which although not extensive in its code coverage - covers about 40% of the code I will tabulate below this later!

As a demo application I am in the process of deploying a working WIN RT 8.1 demo app, the app will utilize the dlls above, and libmpeg2 (available at http://libmpeg2.sourceforge.net/) - detailed below.
The Demo App is a lightly modified version of "https://github.com/lucas-j/libmpeg2-winrt", so thanks to all concerned!! 

I am reviewing building a demo windows phone 8 app.

NOTES
libmpeg2 open source mpeg player / control
	compiling libmpeg2 seems only to be available in ARM, Win32 and x64 mode

	win32 / debug - no notes
	x64 / debug - lots of errors, typically Error: '__asm' keyword not supported on this architecture, so probably not worth the aggravation!
	ARM / debug - no notes

	win32 / release - no notes
	x64 / release - see above - not worth the aggravation!
	ARM / release - no notes

	With the above in mind any deployed application utilising the player would probably have to be compiled separately for each platform