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
    
    Public FixtureA As FarseerPhysics.Dynamics.Fixture
    Public FixtureB As FarseerPhysics.Dynamics.Fixture
    Public Manifold As FarseerPhysics.Dynamics.Contacts.Contact

    Public Cancel As Boolean
End Class