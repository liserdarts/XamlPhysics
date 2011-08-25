'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' Base class for bodies that auto create geometries
''' </summary>
Public MustInherit Class SingleGeometryBody
    Inherits PhysicalBody

    ''' <summary>
    ''' Get or set the coefficient of friction.
    ''' </summary>
    Public Property FrictionCoefficient() As Double = 0.2
    
    ''' <summary>
    ''' Get or set the coefficient of restitution.
    ''' </summary>
    Public Property RestitutionCoefficient() As Double = 0.5
    
    ''' <summary>
    ''' The collision category this body is in
    ''' </summary>
    Public Property CollisionCategory() As FarseerPhysics.Dynamics.Category = FarseerPhysics.Dynamics.Category.All
    
    ''' <summary>
    ''' The categories that this body will collide with
    ''' </summary>
    Public Property CollidesWith() As CollisionCategoryCollection

    ''' <summary>
    ''' Gets or sets the density used to calculate the mass based on size. Default is 1.
    ''' </summary>
    ''' <value>The density.</value>
    Public Property Density() As Double = 1

    Protected Overrides Sub InitializeGeometries()
        For Each Geom In Geometries
            Geom.FrictionCoefficient = FrictionCoefficient
            Geom.RestitutionCoefficient = RestitutionCoefficient
            Geom.CollisionCategory = CollisionCategory
            Geom.CollidesWith = CollidesWith
            Geom.Density = Density
        Next

        MyBase.InitializeGeometries
    End Sub
    
End Class
