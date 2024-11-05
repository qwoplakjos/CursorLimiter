
# Cursor Limiter

CursorLimitApp is a Windows Forms application that allows users to restrict the mouse cursor to a selected area on the screen. The app provides an overlay for selecting the area, with options to enable or disable the cursor restriction and a hotkey to toggle it on and off.

## Features

- **Select Screen Area**: Choose a rectangular area on the screen to restrict the mouse cursor.
- **Toggle Cursor Restriction**: Easily enable or disable cursor restriction to the selected area.
- **Global Hotkey**: Use `Ctrl + Shift + C` to toggle cursor restriction on and off.

## Getting Started

### Prerequisites

- [.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework) (version 4.6.2 or later)
- Visual Studio or another C# development environment

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/qwoplakjos/CursorLimitApp.git
   cd CursorLimitApp
   ```

2. **Open the project in Visual Studio** (or your preferred .NET IDE).

3. **Build the solution** to restore NuGet packages and compile the app.

4. **Run the application** directly from the IDE or by building an executable.

### Download Compiled Executable

You can download a compiled executable from the [Releases](https://github.com/qwoplakjos/CursorLimiter/releases) section on GitHub.

## Usage

### Selecting an Area to Restrict

1. Click the **Select Area** button. A semi-transparent overlay will appear.
2. Drag your mouse to create a rectangular area. Release the mouse to confirm.
3. The mouse cursor is now restricted to the selected area.

### Toggling Cursor Restriction

- **Using the Toggle Hotkey**: Press `Ctrl + Shift + C` to toggle cursor restriction on or off.
- **Using the Reset Button**: Click the **Reset Cursor** button to remove the restriction.

## Code Overview

### MainForm

The main form handles user interactions, including:

- Opening the overlay form to select a restricted area.
- Managing cursor restriction through the `Cursor.Clip` property.
- Listening for a global hotkey (`Ctrl + Shift + C`) to toggle cursor restriction.

### SelectionForm

The `SelectionForm` is a semi-transparent, borderless form that allows the user to select a rectangle on the screen. The form records the rectangle’s position and size, then passes it back to `MainForm`.

### Hotkey Registration

The application uses Windows API calls to register and unregister a global hotkey. The hotkey toggles the cursor restriction state on or off.

## Contributing

Feel free to submit issues or pull requests to help improve this project.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## App UI

![image](https://github.com/user-attachments/assets/732fdf6c-51b1-4a31-b76a-967d85ee4884)


