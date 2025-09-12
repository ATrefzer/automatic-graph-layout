using System.Windows;
using System.Windows.Controls;

namespace WpfGraphControl;

/// <summary>
/// Disables expensive z-order computations during bulk updates of children.
/// Note: This is a fragile approach that relies on reflection to call private methods of the base class.
/// However, the performance gain is significant when adding/removing many children at once.
/// </summary>
public class PerformanceOptimizedCanvas : Canvas {
    private bool isZStateComputationDisabled;

    protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved) {

        if (isZStateComputationDisabled)
            return;
        base.OnVisualChildrenChanged(visualAdded, visualRemoved);
    }


    private void InvokePrivateMethodViaReflection(string name) {
        var t = typeof(Panel);
        var m = t.GetMethod(name,
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        if (m != null) {
            m.Invoke(this, null);
        }
    }

    public void BeginBatchOperation() {
        isZStateComputationDisabled = true;
    }

    public void EndBatchOperation() {
        isZStateComputationDisabled = false;
        InvalidateAll();
    }

    public void InvalidateAll() {

        // Invalidating the Z state is enough.
        //InvokePrivateMethodViaReflection("RecomputeZState");

        InvokePrivateMethodViaReflection("InvalidateZState");
        InvalidateMeasure();
        InvalidateArrange();
        //InvalidateVisual();

        // Let WPF decide when to update the layout
        //UpdateLayout();
    }

    public void ClearChildrenFast() {
        BeginBatchOperation();
        Children.Clear();
        EndBatchOperation();
    }
};