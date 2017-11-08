# XamlPhysics
## Project Description
Xaml Physics makes it possible to make a physics simulation with only xaml code. It is a wrapper around the Farseer Physics Engine.

## Example
```xml
<Physics:PhysicalBox x:Name="PhysicalBox">
    <Rectangle Canvas.Top="100" Canvas.Left="0" Width="400" Height="30" Fill="Black">
        <Physics:PhysicalBox.Body>
            <Physics:RectangleBody IsStatic="True" />
        </Physics:PhysicalBox.Body>
    </Rectangle>
    <Rectangle Canvas.Top="0" Canvas.Left="20" Width="10" Height="10" Fill="Black">
        <Physics:PhysicalBox.Body>
            <Physics:RectangleBody />
        </Physics:PhysicalBox.Body>
    </Rectangle>
</Physics:PhysicalBox>
```

## Demo
http://liserdartsgames.com/LocalResources/XamlPhysics/Index.html
