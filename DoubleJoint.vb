'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at http://xamlphysics.codeplex.com/license

Public MustInherit Class DoubleJoint
    Inherits PhysicalJoint
    
    Shared Sub New()
        Body2Property = DependencyProperty.Register("Body2", GetType(UIElement), GetType(DoubleJoint), New PropertyMetadata(Nothing))
    End Sub
    
    Public Shared Body2Property As DependencyProperty
    Public Property Body2() As UIElement
        Get
            Return GetValue(Body2Property)
        End Get
        Set
            SetValue(Body2Property, Value)
        End Set
    End Property
    
End Class