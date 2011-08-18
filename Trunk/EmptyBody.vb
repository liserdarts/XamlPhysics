'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' Exposes the <c>Geometries</c> collection to insert anything
''' </summary>
''' <remarks></remarks>
Public Class EmptyBody
    Inherits PhysicalBody
    
    Public Shadows ReadOnly Property Geometries() As GeometryCollection
        Get
            Return MyBase.Geometries
        End Get
    End Property

End Class