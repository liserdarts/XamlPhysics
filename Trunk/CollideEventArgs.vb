'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public Class CollideEventArgs
    Inherits EventArgs
    
    Public Sub New(FixtureA As FarseerPhysics.Dynamics.Fixture, FixtureB As FarseerPhysics.Dynamics.Fixture)
        Me.FixtureA = FixtureA
        Me.FixtureB = FixtureB
    End Sub

    Public Sub New(FixtureA As FarseerPhysics.Dynamics.Fixture, FixtureB As FarseerPhysics.Dynamics.Fixture, Manifold As FarseerPhysics.Dynamics.Contacts.Contact)
        Me.New(FixtureA, FixtureB)
        Me.Manifold = Manifold
    End Sub
    
    Public Property GeometryA() As PhysicalGeometry
    Public Property GeometryB() As PhysicalGeometry

    <Obsolete("FixtureA is obsolete, use GeometryA instead.")> _
    Public Property FixtureA() As FarseerPhysics.Dynamics.Fixture
    <Obsolete("FixtureB is obsolete, use GeometryB instead.")> _
    Public Property FixtureB() As FarseerPhysics.Dynamics.Fixture

    Public Property Manifold() As FarseerPhysics.Dynamics.Contacts.Contact

    Public Property Cancel As Boolean
End Class