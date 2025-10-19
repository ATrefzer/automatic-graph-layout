using System.Windows;
using System.Windows.Controls;
using FontStyle = Microsoft.Msagl.Drawing.FontStyle;

namespace WpfGraphControl;

internal class LabelProperties {
    public static void ApplyFontStyle(TextBlock textBlock, FontStyle fontStyle) {
        // Apply Bold
        if ((fontStyle & FontStyle.Bold) != 0) {
            textBlock.FontWeight = FontWeights.Bold;
        }
        else {
            textBlock.FontWeight = FontWeights.Regular;
        }

        // Apply Italic
        if ((fontStyle & FontStyle.Italic) != 0) {
            textBlock.FontStyle = FontStyles.Italic;
        }
        else {
            textBlock.FontStyle = FontStyles.Normal;
        }

        // Apply text decorations (Underline and/or Strikeout)
        if ((fontStyle & FontStyle.Underline) != 0 || (fontStyle & FontStyle.Strikeout) != 0) {
            var decorations = new TextDecorationCollection();
            if ((fontStyle & FontStyle.Underline) != 0) {
                decorations.Add(TextDecorations.Underline);
            }

            if ((fontStyle & FontStyle.Strikeout) != 0) {
                decorations.Add(TextDecorations.Strikethrough);
            }

            textBlock.TextDecorations = decorations.Count > 0 ? decorations : null;
        }
    }
}