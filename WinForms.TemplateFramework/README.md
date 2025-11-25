> [عرض باللغة العربية](README_AR.md) |
# 🏗️ WinForms Visual Inheritance: The "Generic Editor" System

This repository demonstrates how to implement **Visual Inheritance** in Windows Forms (.NET). It creates a standardized "Editor System" consisting of a **Base Form** (template) and a **Child Form** (implementation) to minimize code duplication and enforce UI consistency.

## 📖 Scenario

We are building a data entry system where every window needs a standard look and feel:

  * **Base Form (`BaseEditorForm`):** Defines the layout (Header, Save/Cancel buttons) and handles standard logic (Closing the window).
  * **Child Form (`UserEntryForm`):** Inherits the layout, adds specific input fields (Name Box), and overrides the "Save" logic to perform validation and database operations.

-----

## 🚀 Getting Started

### Prerequisites

  * Visual Studio 2019 or later.
  * .NET Framework or .NET (Core) Windows Forms support.

### Installation & Usage

1.  Clone the repository.
2.  Open the solution in Visual Studio.
3.  **CRITICAL:** Build the solution (`Ctrl + Shift + B`) before opening the designer. Visual Studio requires the Base Form to be compiled into a DLL to render the Child Form.
4.  Run the application (`F5`). The `Program.cs` is set to launch `UserEntryForm`.

-----

## 🛠️ Architecture & Design

### 1\. The Base Form (`BaseEditorForm`)

The template that defines the standard structure.

  * **UI Controls:**
      * Top Panel with Title Label.
      * "Save" and "Cancel" buttons docked to the bottom right.
  * **Key Configuration (Modifiers):**
      * `btnCancel`: **Private** (Locked). Child forms cannot move or delete this.
      * `btnSave` & `lblTitle`: **Protected** (Unlocked). Child forms can change the text or location.

#### Base Logic

The base form handles the Close event but leaves the Save logic `virtual` for the child to define.

```csharp
public partial class BaseEditorForm : Form
{
    public BaseEditorForm()
    {
        InitializeComponent();
    }

    // Standard logic: Always closes the window. Not virtual.
    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    // Trigger logic: Calls the virtual method.
    private void btnSave_Click(object sender, EventArgs e)
    {
        PerformSave();
    }

    // VIRTUAL: The child MUST override this to add functionality.
    protected virtual void PerformSave()
    {
        MessageBox.Show("Base save functionality invoked.");
    }
}
```

-----

### 2\. The Child Form (`UserEntryForm`)

The specific implementation for creating a user.

  * **Inheritance:** defined as `public partial class UserEntryForm : BaseEditorForm`
  * **Customization:**
      * Title changed to "Create New User".
      * Added `Label` and `TextBox` for user input.
      * **Logic:** Overrides `PerformSave()` to validate inputs.

#### Child Logic

```csharp
protected override void PerformSave()
{
    // 1. Specific Validation
    if (string.IsNullOrEmpty(txtName.Text))
    {
        MessageBox.Show("Please enter a name first!", "Validation Error");
        return;
    }

    // 2. Specific Save Logic
    MessageBox.Show($"User '{txtName.Text}' saved successfully.", "Success");
    
    // 3. (Optional) Call base.PerformSave() if needed
}
```

-----

## 📚 Concept Reference Sheet

Use this table to understand the relationship between the Base and Child forms.

| Keyword / Setting | Context | Explanation |
| :--- | :--- | :--- |
| `: BaseEditorForm` | Class Definition | Tells the compiler to use the custom form as the blueprint instead of the standard `Form`. |
| **Private** | Properties | **Locked.** The Child form can see the control but cannot touch, move, or edit it. |
| **Protected** | Properties | **Unlocked.** The Child form effectively "owns" this control and can modify it. |
| `virtual` | Base Code | "Here is a method, but I allow my children to replace how it works." |
| `override` | Child Code | "I am replacing the virtual method from my parent with this new code." |
| `base.Method()` | Child Code | "Run the parent's version of this code, then continue with mine." |

-----

## 💡 Pro-Tip: The "DesignMode" Crash

When using inheritance, code in the Base Form's `Load` event runs every time you open the Child Form in the Visual Studio **Designer**. If you have database connections there, the Designer will crash.

**The Fix:** Always wrap risky code in the Base Form:

```csharp
protected override void OnLoad(EventArgs e)
{
    if (this.DesignMode) return; // Stop here if inside Visual Studio Designer!

    // Safe to run database code now...
    base.OnLoad(e);
}
```

