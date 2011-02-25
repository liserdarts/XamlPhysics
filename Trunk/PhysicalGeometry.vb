'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

''' <summary>
''' The base class for all geometry classes
''' </summary>s
Public MustInherit Class PhysicalGeometry
    
    Event Collide(Sender As Object, E As CollideEventArgs)
    Protected Overridable Function OnCollide(FixtureA As FarseerPhysics.Dynamics.Fixture, FixtureB As FarseerPhysics.Dynamics.Fixture, Manifold As FarseerPhysics.Dynamics.Contacts.Contact) As Boolean
        Dim E As New CollideEventArgs(FixtureA, FixtureB, Manifold)
        RaiseEvent Collide(Me, E)
        Return Not E.Cancel
    End Function

    Event Separate(Sender As Object, E As CollideEventArgs)
    Protected Overridable Sub OnSeparate(FixtureA As FarseerPhysics.Dynamics.Fixture, FixtureB As FarseerPhysics.Dynamics.Fixture)
        Dim E As New CollideEventArgs(FixtureA, FixtureB)
        RaiseEvent separate(Me, E)
    End Sub

    Public Property FrictionCoefficient() As Double = 0.2
    
    Public Property RestitutionCoefficient() As Double = 0.5
    
    Public Property CollisionCategory() As FarseerPhysics.Dynamics.Category = FarseerPhysics.Dynamics.Category.All

    Public Property CollidesWith() As CollisionCategoryCollection
    
    ''' <summary>
    ''' The <c>PhysicsBox</c> this geometry is in
    ''' </summary>
    Public Property Box As PhysicalBox

    ''' <summary>
    ''' The <c>Shape</c> object from the Farseer Physics Engine
    ''' </summary>
    Public Property Geom As FarseerPhysics.Collision.Shapes.Shape

    ''' <summary>
    ''' The <c>Fixture</c> object from the Farseer Physics Engine
    ''' </summary>
    Public Property Fixture As FarseerPhysics.Dynamics.Fixture
    
    ''' <summary>
    ''' Creates the <c>Geom</c> and <c>Fixture</c> objects
    ''' </summary>
    Public Overridable Sub Initialize(Body As PhysicalBody)
        CreateGeom(Body)
        SetGeomProperties(Body)

        Fixture = Body.Body.CreateFixture(Geom)
        Fixture.Friction = FrictionCoefficient
        Fixture.Restitution = RestitutionCoefficient
        Fixture.CollisionFilter.CollisionCategories = CollisionCategory
        If CollidesWith IsNot Nothing Then
            Fixture.CollisionFilter.CollidesWith = CollidesWith.GetCombinedValue
        End If

        Fixture.OnCollision = New FarseerPhysics.Dynamics.OnCollisionEventHandler(AddressOf OnCollide)
        Fixture.OnSeparation = New FarseerPhysics.Dynamics.OnSeparationEventHandler(AddressOf OnSeparate)
    End Sub
    
    Protected MustOverride Sub CreateGeom(Body As PhysicalBody)
    
    Protected Overridable Sub SetGeomProperties(Body As PhysicalBody)
    End Sub
    
End Class