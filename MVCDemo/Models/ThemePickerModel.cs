namespace MVCDemo.Models;

/// <summary>
/// View model for the <c>_ThemePicker</c> partial. Lets a caller inject extra
/// CSS classes onto the wrapping &lt;div&gt; (for spacing/alignment in chrome)
/// and toggle the visible label next to the icon.
/// </summary>
public sealed record ThemePickerModel
{
    public string Class { get; init; } = string.Empty;

    public bool ShowLabel { get; init; } = true;
}
