This is Farseer Physics 3.2 http://farseerphysics.codeplex.com/releases/view/55470
Aka change set 82681 http://farseerphysics.codeplex.com/SourceControl/changeset/changes/82681

It has only a few changes
If has been changed to target Silverlight 3
	This allows the same dll to run on both Windows Phone 7 and Silverlight. It is backwards compatible and will run in Silverlight 4.
Some functions were renamed.
	Some functions in FarseerPhysics.Dynamics.Body were overloaded by only changing the parameters to be passed by reference. This is a poor programming practice and the developers of Farseer Physics are unwilling to fix it. Other languages are not able to call functions overloaded in this way. The functions overloaded by changing parameters in this way were renamed to FunctionNameByRef.
The default value for FarseerPhysics.Settings.MaxPolygonVertices was changed from 8 to 20.
If you download if already compiled from the XamlPhysics project, it will have a strong name.


The Farseer Physics Engine is licensed under the Microsoft Permissive License (Ms-PL) v1.1

Microsoft Permissive License (Ms-PL)

This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.

1. Definitions

The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.

A "contribution" is the original software, or any additions or changes to the software.

A "contributor" is any person that distributes its contribution under this license.

"Licensed patents" are a contributor's patent claims that read directly on its contribution.

2. Grant of Rights

(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.

(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

3. Conditions and Limitations

(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.

(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.

(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.

(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.

(E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.