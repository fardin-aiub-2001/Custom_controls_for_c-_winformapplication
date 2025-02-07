Custom controls in Windows Forms applications can greatly enhance the user interface by providing unique and visually appealing elements. Below is an introduction to the custom controls listed in the file, along with their usage in C# WinForms projects.

1. CirclePictureBox
The CirclePictureBox control is a custom PictureBox that displays images in a circular shape with customizable borders. It supports gradient borders, dashed lines, and smooth anti-aliased rendering.

Usage:
Add to Form: Drag and drop the CirclePictureBox control from the toolbox onto your form.

Set Image: Use the Image property to assign an image.

Customize Border: Adjust properties like BorderSize, BorderColor, BorderColor2, BorderLineStyle, and GradientAngle to style the border.

2. RoundButtons
The RoundButtons control provides rounded corner buttons, which can be customized with colors, gradients, and text.

Usage:
Add to Form: Drag and drop the RoundButtons control from the toolbox onto your form.

Customize Appearance: Set properties like BackColor, ForeColor, and Text to style the button.

Handle Events: Use the Click event to handle button interactions.

3. RoundTextBoxWithWaterMark
The RoundTextBoxWithWaterMark control is a rounded-corner textbox with a watermark feature, which displays hint text when the textbox is empty.

Usage:
Add to Form: Drag and drop the RoundTextBoxWithWaterMark control from the toolbox onto your form.

Set Watermark Text: Use the WaterMarkText property to set the hint text.

Customize Appearance: Adjust properties like BackColor, ForeColor, and BorderColor.

4. ToggleSwitch
The ToggleSwitch control is a modern toggle switch that allows users to switch between two states (e.g., ON/OFF). It supports custom colors and smooth animations.

Usage:
Add to Form: Drag and drop the ToggleSwitch control from the toolbox onto your form.

Customize Appearance: Set properties like OnBackColor, OffBackColor, OnToggleColor, and OffToggleColor.

Handle State Changes: Use the CheckedChanged event to handle state changes.
