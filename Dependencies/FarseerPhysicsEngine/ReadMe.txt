This is Farseer Physics 3.2 http://farseerphysics.codeplex.com/releases/view/55470
Aka change set 82681 http://farseerphysics.codeplex.com/SourceControl/changeset/changes/82681

It has only a few changes
If has been changed to target Silverlight 3
	This allows the same dll to run on both Windows Phone 7 and Silverlight. It is backwards compatible and will run in Silverlight 4.
Some functions were renamed.
	Some functions in FarseerPhysics.Dynamics.Body were overloaded by only changing the parameters to be passed by reference. This is a poor programming practice and the developers of Farseer Physics are unwilling to fix it. Other languages are not able to call functions overloaded in this way. The functions overloaded by changing parameters in this way were renamed to FunctionNameByRef.
The default value for FarseerPhysics.Settings.MaxPolygonVertices was changed from 8 to 20.
If you download if already compiled from the XamlPhysics project, it will have a strong name.